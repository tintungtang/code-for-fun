using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

public class UserEmail
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }
    
    [BsonElement("email")]
    [JsonProperty("email")]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [BsonElement("isPrimary")]
    [JsonProperty("isPrimary")]
    [JsonPropertyName("isPrimary")]
    public bool IsPrimary { get; set; }

    [BsonElement("type")]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }
}

