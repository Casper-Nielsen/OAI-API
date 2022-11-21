namespace OAI_API.Models
{
    public class ExtendedAnswer : BaseAnswer
    {
        public string ExtededParmeter { get; set; } = string.Empty;
        
        public ExtendedAnswer() { }

        public ExtendedAnswer(DataAnswer dataAnswer)
        {
            ExtededParmeter = dataAnswer.AnswerValue;
            AnswerId = dataAnswer.AnswerId;
            Type = dataAnswer.AnswerType;
        }
    }
}
