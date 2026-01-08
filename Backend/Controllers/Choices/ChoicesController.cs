using Backend.DTOs.Choices;
using Backend.Services.ChoicesManagement;
using Microsoft.AspNetCore.Authorization;
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
        
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task <IActionResult> UpdateChoice(int id, ChoiceUpdateDTO dto)
        {
            await _service.UpdateChoiceAsync(id, dto);
            return Ok(new { message = "Choice Updated Successfully." });
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task <IActionResult> DeleteChoice(int id)
        {
            await _service.DeleteChoiceAsync(id);
            return NoContent();
        }
    }
}
