using Backend.DTOs.Importer;
using Backend.Services.Importer;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Importer;

[Route("api/[controller]")]
[ApiController]
public class ImporterController : ControllerBase
{
    private readonly IImporterService _service;

    public ImporterController(IImporterService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Importer(ImporterDTO xlsx)
    {
        await _service.ProcessFileAsync(xlsx);
        return Ok(new { message = "User created successfully" });
    }
}