using OAI_API.Models;

namespace OAI_API.Services
{
    public interface IAnswerService
    {
        /// <summary>
        /// Gets the answer from the given Id
        /// </summary>
        /// <param name="answerId">The id of the answer</param>
        /// <returns>The simple answer with the type</returns>
        Task<BaseAnswer> GetAnswerAsync(int answerId);
        /// <summary>
        /// Gets the answer that best matches the keywords.
        /// </summary>
        /// <param name="answerKeyWords">The keywords from the question</param>
        /// <returns>The simple answer with the type</returns>
        Task<BaseAnswer> GetAnswerAsync(string question);
    }
}
