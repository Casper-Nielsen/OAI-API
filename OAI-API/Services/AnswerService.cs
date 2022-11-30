using OAI_API.Models;
using OAI_API.Repositories;

namespace OAI_API.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IAIRepository _aiRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ILunchRepository _lunchRepository;

        public AnswerService(
            IAnswerRepository answerRepository,
            IAIRepository aiRepository,
            ILocationRepository locationRepository,
            ILunchRepository lunchRepository)
        {
            _answerRepository = answerRepository;
            _aiRepository = aiRepository;
            _locationRepository = locationRepository;
            _lunchRepository = lunchRepository;
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

            BaseAnswer answer = await FillExtendedAnswer(ConvertToBaseAnswer(dataAnswer));

            return answer;
        }

        /// <summary>
        /// Convertes the DTO into the base answer and extentet answer
        /// </summary>
        /// <param name="answer">Answer to converet</param>
        /// <returns>Base answer or extentet answer</returns>
        private BaseAnswer ConvertToBaseAnswer(AnswerDTO answer)
        {
            return answer.AnswerType switch
            {
                AnswerType.Static => new BaseAnswer(answer),
                AnswerType.Location or AnswerType.External => new ExtendedAnswer(answer),
                _ => throw new NotSupportedException("answer type is not supported"),
            };
        }

        /// <summary>
        /// Fills out the missing answer information for Extended answer
        /// </summary>
        /// <param name="baseAnswer">Answer that should get the missing information</param>
        /// <returns>full answer information</returns>
        private async Task<BaseAnswer> FillExtendedAnswer(BaseAnswer baseAnswer)
        {

            ExtendedAnswer? extendedAnswer;

            switch (baseAnswer.Type)
            {
                case AnswerType.Static:
                    return baseAnswer;

                case AnswerType.External:
                    extendedAnswer = (ExtendedAnswer)baseAnswer;
                    extendedAnswer.AnswerText = extendedAnswer.ExtededParmeter switch
                    {
                        "frokost" => await _lunchRepository.GetLunchMenuAsync(),
                        "klokken" => $"Klokken er {DateTime.Now:HH:mm}",
                        _ => $"Vi understøtter ikke external data. ({extendedAnswer.ExtededParmeter})",
                    };
                    return extendedAnswer;

                case AnswerType.Location:
                    extendedAnswer = (ExtendedAnswer)baseAnswer;
                    extendedAnswer.AnswerText = await _locationRepository.GetDirectionsToLocationAsync(extendedAnswer.ExtededParmeter);
                    return extendedAnswer;

                default:
                    throw new NotImplementedException($"answer type not supported ({baseAnswer.Type})");
            }
        }
    }
}
