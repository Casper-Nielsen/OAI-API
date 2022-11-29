using OAI_API.Models;

namespace OAI_API.Services
{
    public interface IQuestionService
    {
        /// <summary>
        /// Registers the question and gives it a unique Id
        /// </summary>
        /// <returns>The information it has saved</returns>
        Task<Question> RegisterQuestionAsync(Question question); 
        /// <summary>
        /// Updates the question with the new information
        /// </summary>
        /// <param name="question">The new information that should be saved</param>
        Task UpdateQuestionAsync(Question question); 
        /// <summary>
        /// Gets the question information from the Id
        /// </summary>
        Task<Question> GetQuestionAsync(int questionId); 
    }
}
