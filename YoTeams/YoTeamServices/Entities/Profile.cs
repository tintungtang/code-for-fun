using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Profile
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("firstName")]
    [JsonProperty("firstName")]
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    
    [BsonElement("lastName")]
    [JsonProperty("lastName")]
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    
    [BsonElement("dateOfBirth")]
    [JsonProperty("dateOfBirth")]
    [JsonPropertyName("dateOfBirth")]
    public string DateOfBirth { get; set; }


    [BsonElement("languages")]
    [JsonProperty("languages")]
    [JsonPropertyName("languages")]
    public List<string> Langugues { get; set; }
    

    [BsonElement("createdAt")]
    [JsonProperty("createdAt")]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [BsonElement("updatedAt")]
    [JsonProperty("updatedAt")]
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

