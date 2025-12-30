using Backend.Context;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Repository.YearPeriodManagement;

public class YearPeriodManagement : IYearPeriodRepository
{
    private readonly IMemoryCache _cache;
    private readonly MyDbContext _context;
    private readonly ILogger<YearPeriodManagement> _logger;

    public YearPeriodManagement(IMemoryCache cache, MyDbContext context, ILogger<YearPeriodManagement> logger)
    {
        _cache = cache;
        _context = context;
        _logger = logger;
    }

    public async Task<List<YearPeriods>> GetAllAsync()
    {
        return await _context.YearPeriod.ToListAsync();
    }
}