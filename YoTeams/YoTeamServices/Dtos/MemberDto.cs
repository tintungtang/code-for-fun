namespace YoTeamServices.Dtos;

public class MemberDto
{
    public string MemberId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
