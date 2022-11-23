namespace OAI_API.Models
{
    /// <summary>
    /// Holds the Data about a Answer that should be saved
    /// </summary>
    public class AnswerDTO
    {
        public int AnswerId { get; set; }
        public string AnswerValue { get; set; } = string.Empty;
        public AnswerType AnswerType { get; set; }
        public Status Status { get; set; }
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
