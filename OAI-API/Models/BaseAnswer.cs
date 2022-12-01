namespace OAI_API.Models
{
    /// <summary>
    /// Holds the base information about a Answer
    /// </summary>
    public class Answer
    {
        public string AnswerText { get; set; } = string.Empty;
        public int AnswerId { get; set; }
        public string[] Keywords { get; set; } = Array.Empty<string>();
        public AnswerType Type { get; set; }

        public Answer() { }

        public Answer(AnswerDTO dataAnswer)
        {
            AnswerText = dataAnswer.AnswerValue;
            AnswerId = dataAnswer.AnswerId;
            Type = dataAnswer.AnswerType;
            Keywords = dataAnswer.ValidKeywords;
        }
    }
}
