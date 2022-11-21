using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAI_API.Manager;
using OAI_API.Models;

namespace OAI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnwserManager _anwserManager;

        public AnswerController(IAnwserManager anwserManager)
        {
            _anwserManager = anwserManager;
        }

        [HttpPost()]
        public async Task<SearchResponse> Search([FromBody] SearchRequest request)
        {
            var answer = await _anwserManager.GetAnswer(request.Question);

            return new SearchResponse { Answer = answer.AnswerText, QuestionId = answer.AnswerId.ToString() };
        }
    }
}
