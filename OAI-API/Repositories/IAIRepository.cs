using OAI_API.Models;

namespace OAI_API.Repositories
{
    public interface IAIRepository
    {
        /// <summary>
        /// gets the answer using a ai
        /// </summary>
        /// <param name="question">the question that should be answered</param>
        /// <returns>the answer information</returns>
        Task<AnswerDTO> GetAnswerAsync(string question);
    }
}
