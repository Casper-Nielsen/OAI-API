using OAI_API.Models;
using OAI_API.Repositories;

namespace OAI_API.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IAIRepository _aiRepository;

        public AnswerService(
            IAnswerRepository answerRepository,
            IAIRepository aiRepository)
        {
            _answerRepository = answerRepository;
            _aiRepository = aiRepository;
        }

        public async Task<BaseAnswer> GetAnswerAsync(int answerId)
        {
            var dataAnswer = await _answerRepository.GetAnswerAsync(answerId);

            if (dataAnswer == null)
            {
                throw new ArgumentException("There are no answer with the given Id");
            }

            BaseAnswer answer = ConvertToBaseAnswer(dataAnswer);

            return answer;
        }

        public async Task<BaseAnswer> GetAnswerAsync(string question)
        {
            var dataAnswer = await _aiRepository.GetAnswerAsync(question);

            if (dataAnswer == null)
            {
                throw new ArgumentException("There are no answer with the given keywords");
            }

            BaseAnswer answer = ConvertToBaseAnswer(dataAnswer);

            return answer;
        }

        private BaseAnswer ConvertToBaseAnswer(AnswerDTO answer)
        {
            return answer.AnswerType switch
            {
                AnswerType.Static => new BaseAnswer(answer),
                AnswerType.Location or AnswerType.External => new ExtendedAnswer(answer),
                _ => throw new NotSupportedException("answer type is not supported"),
            };
        }
    }
}
