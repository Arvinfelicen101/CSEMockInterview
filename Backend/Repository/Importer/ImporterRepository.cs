using Backend.Context;
using Backend.Models;

namespace Backend.Repository.Importer;

public class ImporterRepository : IImporterRepository
{
    private readonly MyDbContext _context;

    public ImporterRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(List<Questions> questionsList,List<Choices> choicesList)
    {
        await _context.Question.AddRangeAsync(questionsList);
        await _context.Choice.AddRangeAsync(choicesList);
        await _context.SaveChangesAsync();
    }
    
}