using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using YoTeamServices.Data;
using YoTeamServices.Entities;

public class ApplicationDbContext
{
    private readonly IMongoDatabase database;

    public ApplicationDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return database.GetCollection<T>(name);
    }

    public IMongoCollection<User> Users => database.GetCollection<User>("Users");
    public IMongoCollection<Team> Teams => database.GetCollection<Team>("Teams");
    public IMongoCollection<Role> Roles => database.GetCollection<Role>("Roles");
    public IMongoCollection<Permission> Permissions => database.GetCollection<Permission>("Permissions");
    public IMongoCollection<Skill> Skills => database.GetCollection<Skill>("Skills");
    public IMongoCollection<UserAddress> Addresses => database.GetCollection<UserAddress>("Addresses");
    public IMongoCollection<UserPhone> Phones => database.GetCollection<UserPhone>("Phones");
    public IMongoCollection<UserEmail> Emails => database.GetCollection<UserEmail>("Emails");
    
    public void InitCollections()
    {
        var collections = database.ListCollectionNames().ToList();
        List<string> collectionNames = new List<string>
        {
            "Users",
            "UserRoles",
            "UserPermissions",
            "Roles",
            "RolePermissions",
            "Permissions",
            "Teams",
            "Members",
            "MemberProfiles",
            "TeamMembers",
            "KPIs",
            "Skills",
            "UserProfileAddresses",
            "UserProfilePhones",
            "UserProfileEmails"
        };
        

        foreach (var col in collectionNames )
        {
            if (!collections.Contains(col))
                database.CreateCollection(col);
        }
    }
}