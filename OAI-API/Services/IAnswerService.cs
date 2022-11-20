using OAI_API.Models;

namespace OAI_API.Services
{
    public interface IAnswerService
    {
        BaseAnswer GetAnswer(string answer);
        BaseAnswer GetAnswer(int answerId);
        BaseAnswer GetAnswer(string[] answerKeyWords);
    }
}
