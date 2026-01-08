using Backend.Context;
using Backend.DTOs.SubCategory;
using Backend.Models;
using Backend.Repository.SubCategory;
using Backend.Services.SubCategory;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Backend.DTOs.Question;

namespace Backend.Tests.Services.SubCategory
{
    public class SubCategoryServiceTest
    {
        private readonly Mock<ISubCategoryRepository> _repoMock;
        private readonly Mock<IMemoryCache> _cacheMock;
        private readonly SubCategoryService _service;

        public SubCategoryServiceTest()
        {
            _repoMock = new Mock<ISubCategoryRepository>();
            _cacheMock = new Mock<IMemoryCache>();

            _service = new SubCategoryService(
                _repoMock.Object,
                _cacheMock.Object
            );
        }

        [Fact]
        public async Task CreateSubCategoryAsync_WhenValid_ShouldCreateSubCategory()
        {
            // Arrange
            _repoMock.Setup(r => r.CategoryExistAsync(It.IsAny<int>())).ReturnsAsync(true);
            _repoMock.Setup(r => r.SubCategoryExistsAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);

            var dto = new SubCategoryCreateDTO
            {
                SubCategoryName = "New Sub",
                CategoryId = 1
            };

            // Act
            await _service.CreateSubCategoryAsync(dto);

            // Assert
            _repoMock.Verify(r => r.CreateSubCategoryAsync(It.IsAny<SubCategories>()), Times.Once);
        }

        [Fact]
        public async Task UpdateSubCategoryAsync_WhenValid_ShouldUpdate()
        {
            // Arrange
            var subCategory = new SubCategories { Id = 1, SubCategoryName = "Old" };

            _repoMock.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(subCategory);
            _repoMock.Setup(r => r.CategoryExistAsync(It.IsAny<int>())).ReturnsAsync(true);
            _repoMock.Setup(r => r.SubCategoryExistsAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);
            _repoMock.Setup(s => s.SaveChangesAsync());
            var dto = new SubCategoryUpdateDTO
            {
                SubCategoryName = "Updated",
                Questions = new List<QuestionUpdateDTO>()
                {
                    new QuestionUpdateDTO()
                    {
                        ParagraphId = 1,
                        QuestionName = "whamp whamp?",
                        SubCategoryId = 4,
                        YearPeriodId = 1
                    }
                },
                CategoryId = 1
            };

            // Act
            await _service.UpdateSubCategoryAsync(1, dto);

            // Assert
            Assert.Equal("Updated", subCategory.SubCategoryName);
            _repoMock.Verify(r => r.UpdataSubCategory(subCategory), Times.Once);
            _repoMock.Verify(q => q.SaveChangesAsync(), Times.Once);
           
        }

        [Fact]
        public async Task DeleteSubCategoryAsync_WhenValid_ShouldCallDelete()
        {
            // Arrange
            var subCategory = new SubCategories { Id = 1, SubCategoryName = "jampok", CategoryId = 1};
            _repoMock.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(subCategory);

            // Act
            await _service.DeleteSubCategoryAsync(1);

            // Assert
            _repoMock.Verify(r => r.DeleteSubCategory(subCategory), Times.Once);
        }

    }
}
