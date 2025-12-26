using Backend.DTOs.Importer;
using Backend.Repository.CategoryManagement;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services.CategoryManagement;

public class CategoryService : ICategoryService
{
    private readonly IMemoryCache _cache;
    private readonly ICategoryRepository _repository;
    private readonly ILogger<CategoryService> _logger;

    private const string CategoryCacheKey = "category:all";

    public CategoryService(IMemoryCache cache, ICategoryRepository repository, ILogger<CategoryService> logger)
    {
        _cache = cache;
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllService()
    {
        // _logger.LogInformation("Fetching all categories...");
        if (_cache.TryGetValue(CategoryCacheKey, out List<CategoryDTO> cached))
        {
            _logger.LogInformation("CATEGORY CACHE HIT");
            return cached;
        }
        
        _logger.LogInformation("Category hit missed");
        var result = await _repository.GetAllAsync();
        var mapCategory = result.Select(c => new CategoryDTO()
        {
            
            Id = c.Id,
            CategoryName = c.CategoryName.ToString()
        }).ToList();
        
        //set cache
        _cache.Set(CategoryCacheKey, mapCategory,TimeSpan.FromMinutes(10));
        return mapCategory;
    }
}