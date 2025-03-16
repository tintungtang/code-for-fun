using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

public class UserRoles
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("userId")]
    [JsonPropertyName("userId")]
    public string UserId { get; set; } 

    [BsonElement("roleId")]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("roleId")]
    [JsonPropertyName("roleId")]
    public string RoleId { get; set; }
}
