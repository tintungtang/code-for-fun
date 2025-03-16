using YoTeamServices.Dtos;
using YoTeamServices.Entities;

namespace YoTeamServices.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITeamRepository
{
    Task<List<TeamDto>> GetTeamsAsync();
    Task<Team> GetTeamByIdAsync(string id);
    Task CreateTeamAsync(Team team);
    Task<bool> UpdateTeamAsync(string id, Team updatedTeam);
    Task<bool> DeleteTeamAsync(string id);
    Task<bool> AddMemberAsync(string teamId, Member member);
    Task<bool> RemoveMemberAsync(string teamId, string memberId);
}
