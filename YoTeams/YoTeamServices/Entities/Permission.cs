using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Permission
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
    
    [BsonElement("key")]
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [BsonElement("description")]
    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

