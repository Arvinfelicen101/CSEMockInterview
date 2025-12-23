using Backend.Context;

namespace Backend.Repository.Importer;

public class ImporterRepository : IImporterRepository
{
    private readonly MyDbContext _context;

    public ImporterRepository(MyDbContext context)
    {
        _context = context;
    }
}