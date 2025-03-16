using YoTeamServices.Entities;

namespace YoTeamServices.Repositories;

using MongoDB.Driver;

public class ProfileRepository : BaseRepository<Profile>
{
    public ProfileRepository(IMongoDatabase database) : base(database, "Profiles") { }
}
