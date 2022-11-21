using OAI_API.Models;

namespace OAI_API.Manager
{
    public interface IAnwserManager
    {
        Task<BaseAnswer> GetAnswer(string question);
    }
}
