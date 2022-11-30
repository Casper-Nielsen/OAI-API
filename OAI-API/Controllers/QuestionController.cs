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
        private readonly IQuestionService _questionService;

        public QuestionController(
            IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("feedback")]
        public async Task Feedback([FromBody] FeedbackRequest request)
        {
            var question = await _questionService.GetQuestionAsync(request.QuestionId.FromHashId());

            question.Status = request.Feedback ? QuestionStatus.accepted : QuestionStatus.rejected;

            await _questionService.UpdateQuestionAsync(question);
        }
    }
}
