using OAI_API.Models;
using OAI_API.Repositories;

namespace OAI_API.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<BaseAnswer> GetAnswer(int answerId)
        {
            var dataAnswer = await _answerRepository.GetAnswer(answerId);

            if (dataAnswer == null)
            {
                throw new ArgumentException("There are no answer with the given Id");
            }

            BaseAnswer answer = ConvertToBaseAnswer(dataAnswer);

            return answer;
        }

        public async Task<BaseAnswer> GetAnswer(string[] answerKeyWords)
        {
            var dataAnswer = await _answerRepository.GetAnswer(answerKeyWords);

            if (dataAnswer == null)
            {
                throw new ArgumentException("There are no answer with the given Id");
            }

            BaseAnswer answer = ConvertToBaseAnswer(dataAnswer);

            return answer;
        }

        private BaseAnswer ConvertToBaseAnswer(DataAnswer answer)
        {
            return answer.AnswerType switch
            {
                AnswerType.Static => new BaseAnswer(answer),
                AnswerType.Location or AnswerType.external => new ExtendedAnswer(answer),
                _ => throw new NotSupportedException("answer type is not supported"),
            };
        }
    }
}
