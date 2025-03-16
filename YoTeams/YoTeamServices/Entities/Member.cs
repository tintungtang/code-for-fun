using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Member
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("role")]
    [JsonProperty("role")]
    [JsonPropertyName("role")]
    public string Role { get; set; }
    
    [BsonElement("name")]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [BsonElement("rank")]
    [JsonProperty("rank")]
    [JsonPropertyName("rank")]
    public string Rank { get; set; }
    
    [BsonElement("power")]
    [JsonProperty("power")]
    [JsonPropertyName("power")]
    public string Power { get; set; }

    [BsonElement("joinedAt")]
    [JsonProperty("joinedAt")]
    [JsonPropertyName("joinedAt")]
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}


