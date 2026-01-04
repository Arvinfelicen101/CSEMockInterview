using Backend.DTOs.Importer;
using Backend.Models;
using Backend.Repository.YearPeriodManagement;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services.YearPeriodManagement;

public class YearPeriodService : IYearPeriodService
{
    private readonly IMemoryCache _cache;
    private readonly IYearPeriodRepository _repository;
    private readonly ILogger<YearPeriodService> _logger;

    private const string YearPeriodCacheKey = "YearPeriod:all";

    public YearPeriodService(IMemoryCache cache, IYearPeriodRepository repository, ILogger<YearPeriodService> logger)
    {
        _cache = cache;
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<YearPeriods>> GetAllService()
    {
        _logger.LogInformation("Fetching all YearPeriod...");
        if (_cache.TryGetValue(YearPeriodCacheKey, out List<YearPeriods>? cached))
        {
            _logger.LogInformation("YearPeriod CACHE HIT");
            return cached!;
        }
        
        _logger.LogInformation("YearPeriod hit missed");
        var result = await _repository.GetAllAsync();
   
        
        //set cache
        _cache.Set(YearPeriodCacheKey, result,TimeSpan.FromMinutes(10));
        return result;
    }
}