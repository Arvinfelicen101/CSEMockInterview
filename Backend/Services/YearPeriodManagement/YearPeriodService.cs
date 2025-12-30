using Backend.DTOs.Importer;
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

    public async Task<IEnumerable<YearPeriodDTO>> GetAllService()
    {
        _logger.LogInformation("Fetching all YearPeriod...");
        if (_cache.TryGetValue(YearPeriodCacheKey, out List<YearPeriodDTO> cached))
        {
            _logger.LogInformation("YearPeriod CACHE HIT");
            return cached;
        }
        
        _logger.LogInformation("YearPeriod hit missed");
        var result = await _repository.GetAllAsync();
        var mapYearPeriod = result.Select(y => new YearPeriodDTO()
        {
            Id = y.Id,
            year = y.Year,
            period = y.Periods.ToString()
        }).ToList();
        
        //set cache
        _cache.Set(YearPeriodCacheKey, mapYearPeriod,TimeSpan.FromMinutes(10));
        return mapYearPeriod;
    }
}