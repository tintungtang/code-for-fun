using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace YoTeamServices.Entities;

public class MemberSkills
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [BsonElement("memberId")]
    [JsonProperty("memberId")]
    [JsonPropertyName("memberId")]
    public string MemberId { get; set; }

    [BsonElement("skillId")]
    [JsonProperty("skillId")]
    [JsonPropertyName("skillId")]
    public string SkillId { get; set; }

    [BsonElement("proficiency")]
    [JsonProperty("proficiency")]
    [JsonPropertyName("proficiency")]
    public string Proficiency { get; set; }
    
    [BsonElement("yearOfExperience")]
    [JsonProperty("yearOfExperience")]
    [JsonPropertyName("yearOfExperience")]
    public int YearOfExperience { get; set; }

    [BsonElement("certified")]
    [JsonProperty("certified")]
    [JsonPropertyName("certified")]
    public bool Certified { get; set; } = false; // Indicates if the skill is certified
}
