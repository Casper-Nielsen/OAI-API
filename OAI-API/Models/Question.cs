using OAI_API.Shared;

namespace OAI_API.Models
{
    public class Question
    {
        public string Text { get; set; } = string.Empty;
        public QuestionStatus Status { get; set; }
        public int Id { get; set; }
        public string HashId { get => Id.ToHashId(); set => Id = value.FromHashId(); }
        public string[] Keywords { get; set; } = Array.Empty<string>();
        public BaseAnswer? Answer { get; set; }

        public Question() { }

        public Question(QuestionDTO questionDTO, BaseAnswer baseAnswer)
        {
            Text = questionDTO.Question;
            Status = questionDTO.Status;
            Id = questionDTO.Id;
            Keywords = questionDTO.Keywords;
            Answer = baseAnswer;
        }
    }
}
