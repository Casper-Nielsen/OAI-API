namespace OAI_API.Models
{
    public class QuestionDTO
    {
        public string Question { get; set; } = string.Empty;
        public QuestionStatus Status { get; set; }
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public string[] Keywords { get; set; } = Array.Empty<string>();

        public QuestionDTO() {  }

        public QuestionDTO(Question question)
        {
            Question = question.Text;
            Status = question.Status;
            Id = question.Id;
            Keywords = question.Keywords;
            AnswerId = question.Answer?.AnswerId ?? 0;
        }
    }

    public enum QuestionStatus
    {
        pending,
        proced,
        accepted,
        rejected
    }
}
