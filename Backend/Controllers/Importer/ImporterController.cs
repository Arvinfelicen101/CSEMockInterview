using Backend.DTOs.Importer;
using Backend.Services.Importer;
using Microsoft.AspNetCore.Mvc;
using Backend.Services.YearPeriodManagement;

namespace Backend.Controllers.Importer;

[Route("api/[controller]")]
[ApiController]
public class ImporterController : ControllerBase
{
    private readonly IImporterService _service;
    private readonly IYearPeriodService _yearPeriodService;

    public ImporterController(IImporterService service, IYearPeriodService yearPeriodService)
    {
        _service = service;
        _yearPeriodService = yearPeriodService;
    }

    [HttpPost("Import")]
    public async Task<IActionResult> Import([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");
        Console.WriteLine("passing file to service");
        await _service.ProcessFileAsync(file);
        Console.WriteLine("File passed to service");
        return Ok(new { message = "Importer successful" });
    }

    [HttpGet]
    public async Task<IActionResult> TestCategory()
    {
        var result = await _yearPeriodService.GetAllService();
        return Ok(result);
    }
}