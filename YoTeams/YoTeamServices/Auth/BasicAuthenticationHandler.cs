using NuGet.Packaging;
using YoTeamServices.Entities;
using YoTeamServices.Helpers;

namespace YoTeamServices.Auth;

using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IMongoCollection<User> _userCollection;
    private readonly IMongoCollection<UserRoles> _userRoleCollection;
    private readonly IMongoCollection<Role> _rolesCollection;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ApplicationDbContext dbContext
    )
        : base(options, logger, encoder)
    {
        _userCollection = dbContext.GetCollection<User>("Users");
        _userRoleCollection = dbContext.GetCollection<UserRoles>("UserRoles");
        _rolesCollection = dbContext.GetCollection<Role>("Roles");
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        try
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var authHeaderValue = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Replace("Basic ", "")));
            var credentials = authHeaderValue.Split(':');

            if (credentials.Length != 2)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var userName = credentials[0];
            var password = credentials[1];

            var user = await _userCollection.Find(u => u.UserName == userName).FirstOrDefaultAsync();
           

            if (user == null || !PasswordHelper.VerifyPassword(password, user.Salt, user.Password))
            {
                return AuthenticateResult.Fail("Invalid UserName or Password");
            }
            
            var userRoles = await _userRoleCollection
                .Find(ur => ur.UserId == user.Id)
                .ToListAsync();
            var filter = Builders<Role>.Filter.In(u => u.Id, userRoles.Select(r => r.RoleId));
            var roles =  await _rolesCollection.Find(filter).ToListAsync();
            
            var roleNames = roles.Select(r => r.Name);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
             
            };
            // Add role claims
            claims.AddRange(roleNames.Select(role => new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return AuthenticateResult.Fail("Error processing authentication");
        }
    }
}
