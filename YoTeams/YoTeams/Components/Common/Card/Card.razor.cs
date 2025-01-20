using Microsoft.AspNetCore.Components;
namespace YoTeams.Components.Common.Card;


public partial class Card<TData> : ComponentBase
{
   public string title { get; set; } = string.Empty;
}