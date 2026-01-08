using Backend.Context;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Repository.YearPeriodManagement;

public class YearPeriodManagement : IYearPeriodRepository
{
    private readonly MyDbContext _context;
    
    public YearPeriodManagement(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<YearPeriods>> GetAllAsync()
    {
        return await _context.YearPeriod.ToListAsync();
    }

}