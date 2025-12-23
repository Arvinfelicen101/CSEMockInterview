using Backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Backend.DTOs.Importer;

public class FullDbCachePreloader : IHostedService
{
    private readonly IMemoryCache _cache;
    private readonly IServiceProvider _serviceProvider;
    private const int BatchSize = 1000; // adjust for large datasets

    public FullDbCachePreloader(IMemoryCache cache, IServiceProvider serviceProvider)
    {
        _cache = cache;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Preload everything at startup
        await RefreshAllCache(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    // -----------------------
    // Public methods to refresh cache after CRUD
    // -----------------------
    public async Task RefreshAllCache(CancellationToken cancellationToken = default)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();

        await RefreshCategories(db, cancellationToken);
        await RefreshParagraphs(db, cancellationToken);
        await RefreshChoices(db, cancellationToken);

        // Add more models here as needed
    }

    public async Task RefreshCategories(MyDbContext db, CancellationToken cancellationToken = default)
    {
        var categories = await db.Category
            .Select(c => new CategoryDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName.ToString() 
            })
            .ToListAsync(cancellationToken);

        _cache.Set("Categories", categories.ToDictionary(c => c.Id, c => c));
    }

    public async Task RefreshParagraphs(MyDbContext db, CancellationToken cancellationToken = default)
    {
        var paragraphs = await db.Paragraph
            .Select(p => new ParagraphDTO
            {
                Id = p.Id,
                ParagraphText = p.ParagraphText
            })
            .ToListAsync(cancellationToken);

        _cache.Set("Paragraphs", paragraphs.ToDictionary(p => p.Id, p => p));
    }

    public async Task RefreshChoices(MyDbContext db, CancellationToken cancellationToken = default)
    {
        var choices = await db.Choice
            .Select(c => new ChoiceDTO
            {
                Id = c.Id,
                ChoiceText = c.ChoiceText,
                IsCorrect = c.IsCorrect,
                QuestionId = c.QuestionId
            })
            .ToListAsync(cancellationToken);

        _cache.Set("Choices", choices.ToDictionary(c => c.Id, c => c));
    }

    public async Task RefreshCategoryById(int categoryId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();

        var category = await db.Category
            .Where(c => c.Id == categoryId)
            .Select(c => new CategoryDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName.ToString()
            })
            .FirstOrDefaultAsync();

        if (category == null)
        {
            var dict = _cache.Get<Dictionary<int, CategoryDTO>>("Categories");
            if (dict != null) dict.Remove(categoryId);
            return;
        }

        var categoriesDict = _cache.Get<Dictionary<int, CategoryDTO>>("Categories") ?? new Dictionary<int, CategoryDTO>();
        categoriesDict[categoryId] = category;
        _cache.Set("Categories", categoriesDict);
    }
}
