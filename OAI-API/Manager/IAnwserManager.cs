using OAI_API.Models;

namespace OAI_API.Manager
{
    public interface IAnwserManager
    {
        BaseAnswer GetAnwser(string question);
    }
}
