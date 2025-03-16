using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class Team
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("name")]
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [BsonElement("description")]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [BsonElement("isActive")]
    [JsonProperty("isActive")]
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;

    [BsonElement("createdAt")]
    [JsonProperty("createdAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    [JsonProperty("updatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
    
    [BsonIgnore]
    [JsonProperty("members")]
    [JsonPropertyName("members")]
    public List<Member> Members { get; set; } = new List<Member>();
}



