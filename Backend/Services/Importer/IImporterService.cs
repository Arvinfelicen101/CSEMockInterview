using Backend.DTOs.Importer;

namespace Backend.Services.Importer;

public interface IImporterService
{
    Task ProcessFileAsync(ImporterDTO xlsx);
    Task<List<RawDataDTO>> ParseFileAsync(ImporterDTO xlsx);
}