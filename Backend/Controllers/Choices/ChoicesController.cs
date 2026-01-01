using Backend.DTOs.Choices;
using Backend.Services.Choices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Choices
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoicesController : ControllerBase
    {
        public readonly IChoiceService _service;

        public ChoicesController(IChoiceService service)
        {
            _service = service;
        }

        //[HttpPost("AddChoice")]
        //public Task<IActionResult> CreateChoice(ChoiceDTO choice)
        //{

        //}

        [HttpPut("{id:int}")]
        public async Task <IActionResult> UpdateChoice(int id, ChoiceUpdateDTO dto)
        {
            await _service.UpdateChoiceAsync(id, dto);
            return Ok(new { message = "Choice Updated Successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task <IActionResult> DeleteChoice(int id)
        {
            await _service.DeleteChoiceAsync(id);
            return NoContent();
        }
    }
}
