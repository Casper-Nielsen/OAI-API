using OAI_API.Models;

namespace OAI_API.Manager
{
    public interface IAnwserManager
    {
        /// <summary>
        /// Gets the answer that is the best match for the question
        /// </summary>
        /// <param name="question">A question that should be answered</param>
        /// <returns>The answer with usefull information about it</returns>
        Task<BaseAnswer> GetAnswer(string question);
    }
}
