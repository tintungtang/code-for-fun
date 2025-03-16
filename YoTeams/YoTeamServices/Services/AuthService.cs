using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YoTeamServices.Data;
using YoTeamServices.Dtos;
using YoTeamServices.Helpers;

namespace YoTeamServices.Services;

using MongoDB.Driver;

public class AuthService
{
    private readonly IMongoCollection<User> userCollection;
    private readonly JwtSettings jwtSettings;

    public AuthService(ApplicationDbContext dbContext, IOptions<JwtSettings> jwtConfig)
    {
        userCollection = dbContext.GetCollection<User>("Users");
        jwtSettings = jwtConfig.Value;
    }

    public async Task<bool> RegisterUser(RegisterUserDto dto)
    {
        var existingUser = await userCollection.Find(u => u.UserName == dto.Username).FirstOrDefaultAsync();
        if (existingUser != null) return false;

        var salt = PasswordHelper.GenerateSalt();
        var hashedPassword = PasswordHelper.HashPassword(dto.Password, salt);
        var newUser = new User
        {
            Salt = salt,
            UserName = dto.Username,
            Password = hashedPassword
        };

        await userCollection.InsertOneAsync(newUser);
        return true;
    }

    public async Task<User> AuthenticateUser(LoginUserDto dto)
    {
        var user = await userCollection.Find(u => u.UserName == dto.UserName).FirstOrDefaultAsync();
        if (user == null) return null;

        return PasswordHelper.VerifyPassword(dto.Password, user.Salt,user.Password) ? user : null;
    }
    
    public string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            // new Claim(ClaimTypes.Role, user.Role), // Assuming user has a Role property
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiresInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
