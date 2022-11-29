using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAI_API.Models;
using OAI_API.Services;
using OAI_API.Shared;

namespace OAI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;

        public AnswerController(
            IAnswerService answerService, 
            IQuestionService questionService)
        {
            _answerService = answerService;
            _questionService = questionService;
        }

        [HttpPost()]
        public async Task<SearchResponse> Search([FromBody] SearchRequest request)
        {
            var answer = await _answerService.GetAnswerAsync(request.Question);

            var question = await _questionService.RegisterQuestionAsync(new Question() { Text = request.Question, Keywords = answer.Keywords, Answer = answer });

            return new SearchResponse { Answer = answer.AnswerText, QuestionId = question.Id.ToHashId() };
        }
    }
}
