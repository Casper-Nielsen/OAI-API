using OAI_API.Models;

namespace OAI_API.Repositories
{
    public interface IAnswerRepository
    {
        DataAnswer GetAnswer(string answer);
        DataAnswer GetAnswer(int answerId);
        DataAnswer GetAnswer(string[] answerKeyWords);
    }
}
