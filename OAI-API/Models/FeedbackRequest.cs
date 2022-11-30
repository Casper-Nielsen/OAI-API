namespace OAI_API.Models
{
    public class FeedbackRequest
    {
        public string QuestionId { get; set; } = string.Empty;
        public bool Feedback { get; set; }
    }
}
