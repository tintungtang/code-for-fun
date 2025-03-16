using YoTeamServices.Entities;

namespace YoTeamServices.Repositories;

using MongoDB.Driver;

public class SkillRepository : BaseRepository<Skill>
{
    public SkillRepository(IMongoDatabase database) : base(database, "Skills") { }
}
