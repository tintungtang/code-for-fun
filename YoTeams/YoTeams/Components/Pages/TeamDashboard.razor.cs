using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using YoTeams.Models;


namespace YoTeams.Components.Pages;

public partial class TeamDashboard : ComponentBase
{

    [Inject] protected NavigationManager NavManager { get; set; }
    [Inject] protected AppDbContext DbContext { get; set; }
    [Parameter] public int id { get; set; }

    protected List<Member> members = new();

    protected void GoBackToHome()
    {
        NavManager.NavigateTo("/", true);
    }
    
    protected override async Task OnInitializedAsync()
    {
        members = await DbContext.Members.ToListAsync();
    }
}   