namespace YoTeamServices.Dtos;

public class TeamDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<MemberDto> Members { get; set; }
}

