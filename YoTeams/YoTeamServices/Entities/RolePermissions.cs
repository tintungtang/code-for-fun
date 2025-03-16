using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

public class RolePermissions
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("roleId")]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("roleId")]
    [JsonPropertyName("roleId")]
    public string RoleId { get; set; }
    
    [BsonElement("permissionId")]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("permissionId")]
    [JsonPropertyName("permissionId")]
    public string PermissionId { get; set; }
}
