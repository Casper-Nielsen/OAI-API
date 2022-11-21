namespace OAI_API.Models
{
    public class DataAnswer
    {
        public int AnswerId { get; set; }
        public string AnswerValue { get; set; } = string.Empty;
        public AnswerType AnswerType { get; set; }
        public Status Status { get; set; }
    }

    public enum AnswerType
    {
        Static,
        Location,
        external
    }

    public enum Status
    {
        Active,
        Inactive
    }
}
