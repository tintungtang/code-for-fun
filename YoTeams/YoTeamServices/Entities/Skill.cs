using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Skill
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

    [BsonElement("level")]
    [JsonProperty("level")]
    [JsonPropertyName("level")]
    [BsonRepresentation(BsonType.String)]
    public string Level { get; set; }
}
