using Backend.Context;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Repository.ParagraphManagement;

public class ParagraphManagementRepository : IParagraphManagementRepository
{
    private readonly MyDbContext _context;

    public ParagraphManagementRepository(MyDbContext context, IMemoryCache cache)
    {
        _context = context;
    }

    public async Task<IEnumerable<Paragraphs>> GetAllAsync()
    {
        return await _context.Paragraph.ToListAsync();
    }
}