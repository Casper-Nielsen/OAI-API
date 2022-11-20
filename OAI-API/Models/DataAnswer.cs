namespace OAI_API.Models
{
    public class DataAnswer
    {
        string AnswerValue { get; set; } = string.Empty;
        int AnswerId { get; set; }
        AnswerType AnswerType { get; set; }
        Status Status { get; set; }
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
