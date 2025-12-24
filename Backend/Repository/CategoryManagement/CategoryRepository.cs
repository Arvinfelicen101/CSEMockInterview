using Backend.Context;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Repository.CategoryManagement;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMemoryCache _cache;
    private readonly MyDbContext _context;
    private readonly ILogger<CategoryRepository> _logger;

    public CategoryRepository(IMemoryCache cache, MyDbContext context, ILogger<CategoryRepository> logger)
    {
        _cache = cache;
        _context = context;
        _logger = logger;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Category.ToListAsync();
    }
}