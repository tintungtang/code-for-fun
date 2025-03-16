using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace YoTeamServices.Middlewares;

public class JwtCookieAuthenticationMiddleware
{
    private readonly RequestDelegate next;
    private readonly IConfiguration configuration;

    public JwtCookieAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        this.next = next;
        this.configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/auth/login"))
        {
            await next(context);
        }
        else
        {
            var token = context.Request.Cookies["AUTH_JWT"];
            if (!string.IsNullOrEmpty(token))
            {
                var principal = ValidateToken(token);
                if (principal != null)
                {
                    context.User = principal;
                }
            }
        }

        await next(context);
    }

    private ClaimsPrincipal ValidateToken(string token)
    {
        var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]);

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JwtSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }
}
