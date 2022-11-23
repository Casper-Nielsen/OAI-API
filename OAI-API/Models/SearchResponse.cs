namespace OAI_API.Models
{
    /// <summary>
    /// Contains the answer to the question
    /// </summary>
    public class SearchResponse
    {
        public string QuestionId { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
