using OAI_API.Models;
using OAI_API.Repositories;

namespace OAI_API.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerService _answerService;

        public QuestionService(
            IQuestionRepository questionRepository, 
            IAnswerService answerService)
        {
            _questionRepository = questionRepository;
            _answerService = answerService;
        }

        public async Task<Question> GetQuestionAsync(int questionId)
        {
            var questionDTO = await _questionRepository.GetQuestionAsync(questionId);

            var answer = await _answerService.GetAnswerAsync(questionDTO.AnswerId);

            return new Question(questionDTO, answer);
        }

        public async Task<Question> RegisterQuestionAsync(Question question)
        {
            var questionDTO = await _questionRepository.RegisterQuestionAsync(new QuestionDTO(question));

            var answer = await _answerService.GetAnswerAsync(questionDTO.AnswerId);

            return new Question(questionDTO, answer);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            await _questionRepository.UpdateQuestionAsync(new QuestionDTO(question));
        }
    }
}
