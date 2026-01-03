using Backend.DTOs.Importer;
using Backend.Services.Importer;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
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

    [HttpPost]
    public async Task<IActionResult> Importer(ImporterDTO xlsx)
    {
        await _service.ProcessFileAsync(xlsx);
        return Ok(new { message = "User created successfully" });
    }

    [HttpGet]
    public async Task<IActionResult> TestCategory()
    {
        var result = await _yearPeriodService.GetAllService();
        return Ok(result);
    }
}