using OAI_API.Models;

namespace OAI_API.Repositories
{
    public interface IQuestionRepository
    {
        /// <summary>
        /// Saves the question and returns with the default information
        /// </summary>
        Task<QuestionDTO> RegisterQuestionAsync(QuestionDTO question);
        /// <summary>
        /// Updates the question
        /// </summary>
        Task UpdateQuestionAsync(QuestionDTO question);
        /// <summary>
        /// Gets the question information from the Id
        /// </summary>
        Task<QuestionDTO> GetQuestionAsync(int questionid);
    }
}
