using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using YoTeamServices.Entities;
using YoTeamServices.Repositories;

namespace YoTeamServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize]
[Route("api/teams")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ITeamRepository repository;

    public TeamController(ITeamRepository repo)
    {
        repository = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        return Ok(await repository.GetTeamsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamById(string id)
    {
        var team = await repository.GetTeamByIdAsync(id);
        return team != null ? Ok(team) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] Team team)
    {
        await repository.CreateTeamAsync(team);
        return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeam(string id, [FromBody] Team updatedTeam)
    {
        var success = await repository.UpdateTeamAsync(id, updatedTeam);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(string id)
    {
        var success = await repository.DeleteTeamAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpPost("{teamId}/members")]
    public async Task<IActionResult> AddMember(string teamId, [FromBody] Member member)
    {
        var success = await repository.AddMemberAsync(teamId, member);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{teamId}/members/{memberId}")]
    public async Task<IActionResult> RemoveMember(string teamId, string memberId)
    {
        var success = await repository.RemoveMemberAsync(teamId, memberId);
        return success ? NoContent() : NotFound();
    }
}
