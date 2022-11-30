using OAI_API.Models;
using OAI_API.Repositories;

namespace OAI_API.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(
            IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Question> GetQuestionAsync(int questionId)
        {
            var questionDTO = await _questionRepository.GetQuestionAsync(questionId);

            var answer = new BaseAnswer() { AnswerId = questionDTO.AnswerId };

            return new Question(questionDTO, answer);
        }

        public async Task<Question> RegisterQuestionAsync(Question question)
        {
            var questionDTO = await _questionRepository.RegisterQuestionAsync(new QuestionDTO(question));

            var answer = new BaseAnswer() { AnswerId = questionDTO.AnswerId };

            return new Question(questionDTO, answer);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            await _questionRepository.UpdateQuestionAsync(new QuestionDTO(question));
        }
    }
}
