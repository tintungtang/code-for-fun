using Microsoft.Extensions.Options;
using YoTeamServices.Helpers;

namespace YoTeamServices.Data;
    
using MongoDB.Driver;
using YoTeamServices.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class DatabaseInitializer
{
    private readonly ApplicationDbContext dbContext;
    private readonly PasswordSettings passwordSettings;

    public DatabaseInitializer(ApplicationDbContext dbContext, IOptions<PasswordSettings> passwordSettings)
    {
        this.dbContext = dbContext;
        this.passwordSettings = passwordSettings.Value;
    }

    public async Task InitializeDatabase()
    {
        dbContext.InitCollections();
        
        await SeedPermissions();
        await SeedRoles();
        await SeedUsers();
        await SeedEmails();
        await SeedPhones();
        await SeedAddresses();
        await AssignRolePermissions();
        await AssignUserRoles();
        await SeedTeams();
        await SeedMembers();
        await SeedSkills();
        await SeedProfiles();
        await SeedTeamMembers();
        await SeedMemberProfiles();
        await SeedMemberSkills();
    }
    
    private List<T> LoadSeedData<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine(filePath + " not found. Skipping seeding.");
            return null;
        }
        
        var jsonData = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(jsonData);
    }

    private async Task SeedCollection<T>(IMongoCollection<T> collection, string filePath)
    {
        
        if (collection.CountDocuments(_ => true) == 0)
        {
            List<T> data = LoadSeedData<T>(filePath);

            if (data != null)
            {
                collection.InsertMany(data);
                Console.WriteLine("Seeded successfully.");
            }
        }
    }
    
    private async Task SeedPermissions()
    {
       await SeedCollection<Permission>(
           dbContext.GetCollection<Permission>("Permissions"), 
           Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Permissions.json"));
    }
    
    private async Task SeedRoles()
    {
        await SeedCollection<Role>(
            dbContext.GetCollection<Role>("Roles"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Roles.json")
        );
    }

    private async Task SeedUsers()
    {
        var data = LoadSeedData<User>(Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Users.json"));
        var userCollection = dbContext.GetCollection<User>("Users");
        foreach (var user in data)
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                var salt = PasswordHelper.GenerateSalt();
                user.Salt = salt;
                user.Password = PasswordHelper.HashPassword(passwordSettings.DefaultPassword, salt);
            }
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
        }
        
        await userCollection.InsertManyAsync(data);
    }
    
    private async Task SeedMembers()
    {
        await SeedCollection<Member>(
            this.dbContext.GetCollection<Member>("Members"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Members.json"));
    }
    
    private async Task SeedTeams()
    {
        await SeedCollection<Team>(
            this.dbContext.GetCollection<Team>("Teams"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Teams.json"));
    }
    
    private async Task SeedTeamMembers()
    {
        await SeedCollection<TeamMembers>(
            this.dbContext.GetCollection<TeamMembers>("TeamMembers"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "TeamMembers.json"));
    }
    
    private async Task SeedEmails()
    {
        await SeedCollection<UserEmail>(
            this.dbContext.GetCollection<UserEmail>("UserProfileEmails"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Emails.json"));
    } 
    
    private async Task SeedPhones()
    {
        await SeedCollection<UserPhone>(
            this.dbContext.GetCollection<UserPhone>("UserProfilePhones"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Phones.json"));
    } 
    
    private async Task SeedAddresses()
    {
        await SeedCollection<UserAddress>(
            this.dbContext.GetCollection<UserAddress>("UserProfileAddresses"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Addresses.json"));
    } 
    
    private async Task SeedProfiles()
    {
        await SeedCollection<Profile>(
            this.dbContext.GetCollection<Profile>("Profiles"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Profiles.json"));
    }
    
    private async Task SeedMemberProfiles()
    {
        await SeedCollection<MemberProfiles>(
            this.dbContext.GetCollection<MemberProfiles>("MemberProfiles"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "MemberProfiles.json"));
    }
    
    private async Task SeedSkills()
    {
        await SeedCollection<Skill>(
            this.dbContext.GetCollection<Skill>("Skills"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "Skills.json"));
    } 
    
    private async Task SeedMemberSkills()
    {
        await SeedCollection<MemberSkills>(
            this.dbContext.GetCollection<MemberSkills>("MemberSkills"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "MemberSkills.json"));
    } 
    
    
    private async Task AssignUserRoles()
    {
        await SeedCollection<UserRoles>(
            dbContext.GetCollection<UserRoles>("UserRoles"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "UserRoles.json")
        );
    }
    
    private async Task AssignRolePermissions()
    {
        await SeedCollection<RolePermissions>(
            dbContext.GetCollection<RolePermissions>("RolePermissions"),
            Path.Combine(Directory.GetCurrentDirectory(), "Seeds", "RolePermissions.json")
        );
    }
}
