namespace OAI_API.Models
{
    /// <summary>
    /// A Extended Answer that have parmeters that could be used to find the answer
    /// </summary>
    public class ExtendedAnswer : Answer
    {
        public string ExtededParmeter { get; set; } = string.Empty;
        
        public ExtendedAnswer() { }

        public ExtendedAnswer(AnswerDTO dataAnswer)
        {
            ExtededParmeter = dataAnswer.AnswerValue;
            AnswerId = dataAnswer.AnswerId;
            Type = dataAnswer.AnswerType;
            Keywords = dataAnswer.ValidKeywords;
        }
    }
}
