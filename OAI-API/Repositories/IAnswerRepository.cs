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
        Task<DataAnswer> GetAnswer(int answerId);
        Task<DataAnswer> GetAnswer(string[] answerKeyWords);
    }
}
