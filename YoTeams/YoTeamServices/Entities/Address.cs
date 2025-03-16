using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

public class UserAddress
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }
    
    [BsonElement("street")]
    [JsonProperty("street")]
    [JsonPropertyName("street")]
    public string Street { get; set; }

    [BsonElement("city")]
    [JsonProperty("city")]
    [JsonPropertyName("city")]
    public string City { get; set; }

    [BsonElement("state")]
    [JsonProperty("state")]
    [JsonPropertyName("state")]
    public string State { get; set; }

    [BsonElement("country")]
    [JsonProperty("country")]
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [BsonElement("isPrimary")]
    [JsonProperty("isPrimary")]
    [JsonPropertyName("isPrimary")]
    public bool IsPrimary { get; set; }

    [BsonElement("type")]
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }
}

