using System.Text.Json.Serialization;

namespace YoTeamServices.Dtos;

public class LoginUserDto
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
