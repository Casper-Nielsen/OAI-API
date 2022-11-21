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
        Task<BaseAnswer> GetAnswer(int answerId);
        Task<BaseAnswer> GetAnswer(string[] answerKeyWords);
    }
}
