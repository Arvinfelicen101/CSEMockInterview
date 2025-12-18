using Microsoft.AspNetCore.Mvc;
using CSEMockInterview.Services.Importer;
namespace CSEMockInterview.Controllers.Importer;

public class ImporterController : ControllerBase
{
    private readonly IImporterService _service;

    public ImporterController(IImporterService service)
    {
        _service = service;
    }
}