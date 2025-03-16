using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

public class MemberProfiles
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("memberId")]
    [JsonProperty("memberId")]
    [JsonPropertyName("memberId")]
    public string MemberId { get; set; } // Reference to the Member collection

    [BsonElement("profileId")]
    [JsonProperty("profileId")]
    [JsonPropertyName("profileId")]
    public string ProfileId { get; set; } // Reference to the Profile collection

    [BsonElement("linkedAt")]
    [JsonProperty("linkedAt")]
    [JsonPropertyName("linkedAt")]
    public DateTime LinkedAt { get; set; } = DateTime.UtcNow; // Timestamp for tracking
}
