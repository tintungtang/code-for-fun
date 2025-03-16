namespace YoTeams.Models;

public class AuthValidateResponse
{
    public string Username { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}
