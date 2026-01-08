using Backend.Models;
using Backend.Repository.ParagraphManagement;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services.ParagraphManagement;

public class ParagraphManagementService : IParagraphManagementService
{
    private readonly IParagraphManagementRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly ILogger _logger;
    
    private const string ParagraphCacheKey = "Paragraph:all";

    public ParagraphManagementService(IParagraphManagementRepository repository, IMemoryCache cache, ILogger<ParagraphManagementService> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<IEnumerable<Paragraphs>> GetAllService()
    {
        _logger.LogInformation("start get all from cache");
        if (_cache.TryGetValue(ParagraphCacheKey, out IEnumerable<Paragraphs>? cached))
        {
            _logger.LogInformation("cache hit");
            return cached!;
        }
        _logger.LogInformation("cache miss");
        var result = await _repository.GetAllAsync();

        _cache.Set(ParagraphCacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }

}