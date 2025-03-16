using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

public class Role
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

    [BsonElement("permissions")]
    [JsonProperty("permissions")]
    [JsonPropertyName("permissions")]
    public List<string> PermissionIds { get; set; } = new List<string>();

    [BsonIgnore]
    [JsonProperty("permissionDetails")]
    [JsonPropertyName("permissionDetails")]
    public List<Permission> PermissionDetails { get; set; } = new List<Permission>(); 
}

