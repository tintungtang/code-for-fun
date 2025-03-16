namespace YoTeamServices.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class TeamMembers
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("teamId")]
    [JsonProperty("teamId")]
    [JsonPropertyName("teamId")]
    public string TeamId { get; set; }

    [BsonElement("memberId")]
    [JsonProperty("memberId")]
    [JsonPropertyName("memberId")]
    public string MemberId { get; set; }

    [BsonElement("role")]
    [JsonProperty("role")]
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [BsonElement("isActive")]
    [JsonProperty("isActive")]
    [JsonPropertyName("isActive")]
    public Boolean IsActive { get; set; }

    [BsonElement("createdAt")]
    [JsonProperty("createdAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    [JsonProperty("updatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
