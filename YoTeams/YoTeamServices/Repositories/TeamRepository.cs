using YoTeamServices.Dtos;
using YoTeamServices.Entities;

namespace YoTeamServices.Repositories;

using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TeamRepository : ITeamRepository
{
    private readonly IMongoCollection<Team> teamCollection;
    private readonly IMongoCollection<TeamMembers> teamMembersCollection;

    public TeamRepository(ApplicationDbContext database)
    {
        teamCollection = database.GetCollection<Team>("Teams");
        teamMembersCollection = database.GetCollection<TeamMembers>("TeamMembers");
    }

    public async Task<List<TeamDto>> GetTeamsAsync()
    {
        List<Team> teams = await teamCollection.Find(_ => true).ToListAsync();
        List<TeamMembers> teamMembers = await teamMembersCollection.Find(_ => true).ToListAsync();

       return teams.Select(team => new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            Description = team.Description,
            CreatedAt = team.CreatedAt,
            UpdatedAt = team.UpdatedAt,
            Members = teamMembers
                .Where(m => m.TeamId == team.Id)
                .Select(m => new MemberDto
                {
                    MemberId = m.MemberId,
                    CreatedAt = m.CreatedAt,
                    UpdatedAt = m.UpdatedAt,
                    IsActive = m.IsActive
                })
                .ToList() ?? new List<MemberDto>()
        }).ToList() ?? new List<TeamDto>();
    }

    public async Task<Team> GetTeamByIdAsync(string id)
    {
        return await teamCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateTeamAsync(Team team)
    {
        await teamCollection.InsertOneAsync(team);
    }

    public async Task<bool> UpdateTeamAsync(string id, Team updatedTeam)
    {
        var result = await teamCollection.ReplaceOneAsync(t => t.Id == id, updatedTeam);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteTeamAsync(string id)
    {
        var result = await teamCollection.DeleteOneAsync(t => t.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<bool> AddMemberAsync(string teamId, Member member)
    {
        var filter = Builders<Team>.Filter.Eq(t => t.Id, teamId);
        var update = Builders<Team>.Update.Push(t => t.Members, member);
        var result = await teamCollection.UpdateOneAsync(filter, update);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> RemoveMemberAsync(string teamId, string memberId)
    {
        var filter = Builders<Team>.Filter.Eq(t => t.Id, teamId);
        var update = Builders<Team>.Update.PullFilter<Member>(
            t => t.Members, 
            m => m.Id == memberId
        );
    
        var result = await teamCollection.UpdateOneAsync(filter, update);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

}

