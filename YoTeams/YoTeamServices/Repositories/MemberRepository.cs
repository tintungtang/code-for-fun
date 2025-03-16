using YoTeamServices.Entities;

namespace YoTeamServices.Repositories;

using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;

public class MemberRepository : BaseRepository<Member>
{
    public MemberRepository(IMongoDatabase database) : base(database, "Members") { }
}

