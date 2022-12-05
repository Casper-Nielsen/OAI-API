using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAI_API.Models;
using OAI_API.Services;
using OAI_API.Shared;

namespace OAI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;

        public QuestionController(
            IQuestionService questionService,
            IAnswerService answerService)
        {
            _questionService = questionService;
            _answerService = answerService;
        }

        [HttpPost()]
        public async Task<SearchResponse> SearchAsync([FromBody] SearchRequest request)
        {
            var answer = await _answerService.GetAnswerAsync(request.Question);

            var question = await _questionService.RegisterQuestionAsync(new Question() { Text = request.Question, Keywords = answer.Keywords, Answer = answer });

            return new SearchResponse { Answer = answer.AnswerText, QuestionId = question.Id.ToHashId() };
        }

        [HttpPut("feedback")]
        public async Task FeedbackAsync([FromBody] FeedbackRequest request)
        {
            var question = await _questionService.GetQuestionAsync(request.QuestionId.FromHashId());

            question.Status = request.Feedback ? QuestionStatus.accepted : QuestionStatus.rejected;

            await _questionService.UpdateQuestionAsync(question);
        }
    }
}
