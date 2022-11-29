using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAI_API.Models;
using OAI_API.Services;

namespace OAI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost()]
        public async Task<SearchResponse> Search([FromBody] SearchRequest request)
        {
            var answer = await _answerService.GetAnswerAsync(request.Question);

            return new SearchResponse { Answer = answer.AnswerText, QuestionId = answer.AnswerId.ToString() };
        }
    }
}
