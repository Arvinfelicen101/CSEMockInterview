using Backend.DTOs.Question;
using Backend.Repository.Question;
using Backend.Services.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Question
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _service;

        public QuestionsController(IQuestionService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddQuestion(QuestionCreateDTO question)
        {
            await _service.CreateQuestionAsync(question);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _service.GetQuestionByIdAsync(id);
            return Ok(question);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _service.GetAllAsync();
            return Ok(questions);
        }

        [HttpPatch("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateQuestion(int Id, QuestionUpdateDTO question)
        {
            await _service.UpdateQuestionAsync(Id, question);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            await _service.DeleteQuestionAsync(id);
            return NoContent();
        }


        }
    }
