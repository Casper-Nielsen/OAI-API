using System.Text.Json.Serialization;

namespace OAI_API.Models
{
    /// <summary>
    /// Holds the Data about a Answer that should be saved
    /// </summary>
    public class AnswerDTO
    {
        [JsonPropertyName("id")]
        public int AnswerId { get; set; }
        [JsonPropertyName("text")]
        public string AnswerValue { get; set; } = string.Empty;
        [JsonPropertyName("type")]
        public AnswerType AnswerType { get; set; }
        [JsonPropertyName("state")]
        public Status Status { get; set; }
        [JsonPropertyName("keywords")]
        public string[] ValidKeywords { get; set; } = Array.Empty<string>();
    }

    public enum AnswerType
    {
        Static,
        External,
        Location,
    }

    public enum Status
    {
        Active,
        Inactive
    }
}
