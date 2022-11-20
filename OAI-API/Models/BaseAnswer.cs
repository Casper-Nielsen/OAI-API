namespace OAI_API.Models
{
    public class BaseAnswer
    {
        public string AnswerText { get; set; } = string.Empty;
        public int AnswerId { get; set; }
        public AnswerType Type { get; set; }
    }
}
