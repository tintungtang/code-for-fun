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
        return await _httpClient.GetFromJsonAsync<List<Team>>( "http://localhost:5270/api/teams") ?? new List<Team>();
    }
}
