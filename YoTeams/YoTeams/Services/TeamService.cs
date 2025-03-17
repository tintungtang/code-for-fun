using Microsoft.AspNetCore.Components;

namespace YoTeams.Services;

using YoTeams.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TeamService
{
    private readonly HttpClient _httpClient;
    private IConfiguration configuration;

    public TeamService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Team>> GetTeamsAsync()
    {
        
        _httpClient.DefaultRequestHeaders.Add("Cookie",
            "AUTH_JWT=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2N2Q2OGM5NTU4NDQ2ZDI4MTQwYzIyMDgiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwianRpIjoiZTRlNzJmNGYtZDQ5OS00MGZmLTkzNGUtMzc3OTk2MTBkNjg0IiwiZXhwIjoxNzQyMTkxNzQ4LCJpc3MiOiJ5b3VyLWFwcCIsImF1ZCI6InlvdXItY2xpZW50In0.0jQxpyORuD8r2-5LZnBf9Ox-nEpEoSdnW--YjBM96kY");
        return await _httpClient.GetFromJsonAsync<List<Team>>( "teams") ?? new List<Team>();
    }
}
