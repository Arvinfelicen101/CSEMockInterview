using Backend.Context;
using Backend.DTOs.SubCategory;
using Backend.Models;
using Backend.Repository.SubCategory;
using Backend.Services.SubCategory;
using Backend.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Tests.Services.SubCategory
{
    public class SubCategoryServiceTest
    {
        private readonly Mock<ISubCategoryRepository> _repoMock;
        private readonly Mock<MyDbContext> _contextMock;
        private readonly Mock<IMemoryCache> _cacheMock;
        private readonly SubCategoryService _service;

        public SubCategoryServiceTest()
        {
            _repoMock = new Mock<ISubCategoryRepository>();
            _contextMock = new Mock<MyDbContext>();
            _cacheMock = new Mock<IMemoryCache>();

            _service = new SubCategoryService(
                _contextMock.Object,
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

            var dto = new SubCategoryUpdateDTO
            {
                SubCategoryName = "Updated",
                CategoryId = 1
            };

            // Act
            await _service.UpdateSubCategoryAsync(1, dto);

            // Assert
            Assert.Equal("Updated", subCategory.SubCategoryName);
            _repoMock.Verify(r => r.UpdataSubCategoryAsync(subCategory), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteSubCategoryAsync_WhenValid_ShouldCallDelete()
        {
            // Arrange
            var subCategory = new SubCategories { Id = 1 };
            _repoMock.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(subCategory);

            // Act
            await _service.DeleteSubCategoryAsync(1);

            // Assert
            _repoMock.Verify(r => r.DeleteSubCategoryAsync(subCategory), Times.Once);
        }

    }
}
