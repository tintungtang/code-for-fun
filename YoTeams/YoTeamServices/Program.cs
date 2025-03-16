using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YoTeamServices.Auth;
using YoTeamServices.Data;
using YoTeamServices.Middlewares;
using YoTeamServices.Repositories;
using YoTeamServices.Services;


var builder = WebApplication.CreateBuilder(args);
// Register Password settings
builder.Services.Configure<PasswordSettings>(builder.Configuration.GetSection("PasswordSettings"));

// Register JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Register MongoDB settings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// Register ApplicationDbContext
builder.Services.AddSingleton<ApplicationDbContext>();

// Register Database Initializer
builder.Services.AddSingleton<DatabaseInitializer>();

// Register Repository
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

// Configure Basic Authentication
// Register authentication service
builder.Services.AddScoped<AuthService>();
// Add authentication services
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "JwtOrCookie";
    })
    .AddPolicyScheme("JwtOrCookie", "JWT or Cookie", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            // Check Authorization Header first
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader?.StartsWith("Bearer ") == true)
                return JwtBearerDefaults.AuthenticationScheme;

            // If no Authorization header, check for AUTH_JWT cookie
            if (context.Request.Cookies.ContainsKey("AUTH_JWT"))
                return JwtBearerDefaults.AuthenticationScheme;

            return CookieAuthenticationDefaults.AuthenticationScheme;
        };
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"])
            )
        };

        // Extract JWT token from the cookie if Authorization header is missing
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (string.IsNullOrEmpty(context.Token) &&
                    context.Request.Cookies.ContainsKey("AUTH_JWT"))
                {
                    context.Token = context.Request.Cookies["AUTH_JWT"];
                }
                return Task.CompletedTask;
            }
        };
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/auth/accessdenied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();


builder.Services.AddAuthorization();
// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true; // Show supported API versions in response headers
    options.AssumeDefaultVersionWhenUnspecified = true; // Default version if not specified
    options.DefaultApiVersion = new ApiVersion(1, 0); // Default API version (v1.0)
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Read version from URL segment
});

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5296") 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});



var app = builder.Build();

// app.UseMiddleware<JwtCookieAuthenticationMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await initializer.InitializeDatabase();
}

app.UseCors("AllowBlazorClient");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();