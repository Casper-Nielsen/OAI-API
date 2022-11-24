namespace OAI_API.Models
{
    /// <summary>
    /// Holds the base information about a Answer
    /// </summary>
    public class BaseAnswer
    {
        public string AnswerText { get; set; } = string.Empty;
        public int AnswerId { get; set; }
        public AnswerType Type { get; set; }

        public BaseAnswer() { }

        public BaseAnswer(AnswerDTO dataAnswer)
        {
            AnswerText = dataAnswer.AnswerValue;
            AnswerId = dataAnswer.AnswerId;
            Type = dataAnswer.AnswerType;
        }
    }
}
