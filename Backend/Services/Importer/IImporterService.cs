using Backend.DTOs.Importer;

namespace Backend.Services.Importer;

public interface IImporterService
{
    Task ProcessFileAsync(IFormFile File);
}