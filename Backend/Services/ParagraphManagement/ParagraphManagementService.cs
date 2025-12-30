using Backend.DTOs.Importer;
using Backend.Repository.ParagraphManagement;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services.ParagraphManagement;

public class ParagraphManagementService : IParagraphManagementService
{
    private readonly IParagraphManagementRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly ILogger _logger;
    
    private const string ParagraphCacheKey = "Paragraph:all";

    public ParagraphManagementService(IParagraphManagementRepository repository, IMemoryCache cache, ILogger logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<IEnumerable<ParagraphDTO>> GetAllService()
    {
        if (_cache.TryGetValue(ParagraphCacheKey, out IEnumerable<ParagraphDTO> cached))
        {
            return cached;
        }

        var result = await _repository.GetAllAsync();
        var mapParagraph = result.Select(p => new ParagraphDTO()
        {
            Id = p.Id,
            ParagraphText = p.ParagraphText
        }).ToList();

        _cache.Set(ParagraphCacheKey, mapParagraph, TimeSpan.FromMinutes(10));
        return mapParagraph;
    }
}