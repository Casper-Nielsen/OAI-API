using OAI_API.Models;

namespace OAI_API.Repositories
{
    public interface IAnswerRepository
    {
        /// <summary>
        /// Gets the answers information from the given Id
        /// </summary>
        /// <param name="answerId">The id of the answer</param>
        /// <returns>all the answers information</returns>
        Task<AnswerDTO> GetAnswerAsync(int answerId);
        /// <summary>
        /// Gets the answers information that best matches the keywords.
        /// </summary>
        /// <param name="answerKeyWords">The keywords from the question</param>
        /// <returns>all the answers information</returns>
        Task<AnswerDTO> GetAnswerAsync(string[] answerKeyWords);
    }
}
