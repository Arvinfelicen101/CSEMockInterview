using Backend.Context;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Repository.YearPeriodManagement;

public class YearPeriod : IYearPeriodRepository
{
    private readonly IMemoryCache _cache;
    private readonly MyDbContext _context;
    private readonly ILogger<YearPeriod> _logger;

    public YearPeriod(IMemoryCache cache, MyDbContext context, ILogger<YearPeriod> logger)
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