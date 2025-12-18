using CSEMockInterview.Repository.Importer;
namespace CSEMockInterview.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;

    public ImporterService(IImporterRepository repository)
    {
        _repository = repository;
    }
}