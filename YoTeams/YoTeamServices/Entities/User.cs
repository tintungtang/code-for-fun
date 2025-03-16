using System.ComponentModel.DataAnnotations;
using YoTeamServices.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }
   
    [BsonElement("userName")]
    [JsonProperty("userName")]
    [JsonPropertyName("userName")]
    public string UserName { get; set; }

    [BsonElement("password")]
    [Newtonsoft.Json.JsonIgnore]
    public string Password { get; set; }

    [BsonElement("salt")]
    [Newtonsoft.Json.JsonIgnore]
    public string Salt { get; set; }
    
    [BsonElement("createdAt")]
    [JsonProperty("createdAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    [JsonProperty("updatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
    
    
    [BsonIgnore]
    public List<Role> Roles { get; set; } = new List<Role>();
}

