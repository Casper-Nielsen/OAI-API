using OAI_API.Models;
using OAI_API.Services;

namespace OAI_API.Manager
{
    public class AnswerManager : IAnwserManager
    {
        private readonly IAnswerService _answerService;

        public AnswerManager(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public async Task<BaseAnswer> GetAnswer(string question)
        {
            var baseAnswer = await _answerService.GetAnswer(question.ToLower().Split(' '));
            ExtendedAnswer? extendedAnswer;

            switch (baseAnswer.Type)
            {
                case AnswerType.Static:
                    return baseAnswer;
                case AnswerType.external:
                    extendedAnswer = (ExtendedAnswer)baseAnswer;
                    extendedAnswer.AnswerText = $"Vi understøtter ikke external data. ({extendedAnswer.ExtededParmeter})";
                    return extendedAnswer;
                case AnswerType.Location:
                    extendedAnswer = (ExtendedAnswer)baseAnswer;
                    extendedAnswer.AnswerText = $"Vi understøtter ikke external data. ({extendedAnswer.ExtededParmeter})";
                    return extendedAnswer;
                default:
                    throw new NotImplementedException($"answer type not supported ({baseAnswer.Type})");
            }
        }
    }
}
