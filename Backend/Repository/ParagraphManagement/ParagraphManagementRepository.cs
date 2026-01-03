using Backend.Context;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Repository.ParagraphManagement;

public class ParagraphManagementRepository : IParagraphManagementRepository
{
    private readonly MyDbContext _context;
    private readonly ILogger<ParagraphManagementRepository> _logger;
    private readonly IMemoryCache _cache;

    public ParagraphManagementRepository(MyDbContext context, ILogger<ParagraphManagementRepository> logger, IMemoryCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    public async Task<IEnumerable<Paragraphs>> GetAllAsync()
    {
        return await _context.Paragraph.ToListAsync();
    }
}