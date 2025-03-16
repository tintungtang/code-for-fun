namespace YoTeams.Services;

using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> Login(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", new { username, password });

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        
        return false;
    }
}

