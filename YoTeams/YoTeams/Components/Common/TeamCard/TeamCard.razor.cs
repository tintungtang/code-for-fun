using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using YoTeams.Models;

namespace YoTeams.Components.Common.TeamCard;

public partial class TeamCard : ComponentBase
{
    [Parameter] public Team? team { get; set; } = default;
    
    protected string GetName() => team?.Name ?? string.Empty;
    
    protected int GetMembers() => team?.Members.Count ?? 0;
}