using Backend.Repository.Importer;
using Backend.DTOs.Importer;

namespace Backend.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;

    public ImporterService(IImporterRepository repository)
    {
        _repository = repository;
    }

    public async Task ParseFileAsync(ImporterDTO file)
    {
        // ServiceHelper.
    }
}