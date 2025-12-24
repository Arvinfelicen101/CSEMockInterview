using Backend.DTOs.Importer;
using Backend.Repository.CategoryManagement;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services.CategoryManagement;

public class CategoryService : ICategoryService
{
    private readonly IMemoryCache _cache;
    private readonly ICategoryRepository _repository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(IMemoryCache cache, ICategoryRepository repository, ILogger<CategoryService> logger)
    {
        _cache = cache;
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllService()
    {
        // _logger.LogInformation("Fetching all categories...");
        var result = await _repository.GetAllAsync();
        var mapCategory = result.Select(c => new CategoryDTO()
        {
            Id = c.Id,
            CategoryName = c.CategoryName.ToString()
        });
        return mapCategory;
    }
}