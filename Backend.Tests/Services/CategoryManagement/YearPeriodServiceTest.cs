using Backend.Models;
using Backend.Models.enums;
using Backend.Repository.YearPeriodManagement;
using Backend.Services.YearPeriodManagement;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace Backend.Tests.Services.CategoryManagement;

public class YearPeriodServiceTest
{

    [Fact]
    public async Task GetAllService_UsesCache_DoesNotHitRepositoryTwice()
    {
        // Arrange
        var repoMock = new Mock<IYearPeriodRepository>();

        repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Category>
            {
                new Category { Id = 1, CategoryName = Categories.Analytical }
            });

        var memoryCache = new MemoryCache(new MemoryCacheOptions());

        var loggerMock = new Mock<ILogger<YearPeriodService>>();

        var service = new YearPeriodService(
            memoryCache,
            repoMock.Object,
            loggerMock.Object
        );
        
        //act
        var firstCall = await service.GetAllService();  // cache MISS
        var secondCall = await service.GetAllService(); // cache HIT
        
        
        //assert
        Assert.Single(firstCall);
        Assert.Single(secondCall);
        
        repoMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}