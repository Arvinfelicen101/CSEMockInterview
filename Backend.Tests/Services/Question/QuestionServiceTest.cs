using Backend.Context;
using Backend.DTOs.Choices;
using Backend.DTOs.Question;
using Backend.Models;
using Backend.Repository.Question;
using Backend.Services.Question;
using Backend.Services.Question.QuestionValidator;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Tests.Services.Question
{
    public class QuestionServiceTest
    {
        public readonly Mock<IQuestionRepository> _repoMock;
        public readonly Mock<IQuestionValidator> _validatorMock;
        public readonly Mock<MyDbContext> _contextMock;
        public readonly IMemoryCache _memoryCache;

        public readonly QuestionService _service;

    public QuestionServiceTest()
        {
            _repoMock = new Mock<IQuestionRepository>();
            _validatorMock = new Mock<IQuestionValidator>();
            _contextMock = new Mock<MyDbContext>();

            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _service = new QuestionService(
                _repoMock.Object,
                _memoryCache,
                _validatorMock.Object,
                _contextMock.Object
                );
        }

        [Fact]
        public async Task CreateQuestionAsync_WhenValid_ShouldCreateQuestion()
        {
            // Arrange
            _validatorMock
                .Setup(v => v.ValidateReferencesAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int?>()))
                .Returns(Task.CompletedTask);

            var dto = new QuestionCreateDTO
            {
                QuestionName = "Sample Question",
                SubCategoryId = 1,
                YearPeriodId = 1,
                ParagraphId = null,
                Choices = new List<ChoiceCreateDTO>
        {
            new() { ChoiceText = "A", IsCorrect = true },
            new() { ChoiceText = "B", IsCorrect = false }
        }
            };

            // Act
            await _service.CreateQuestionAsync(dto);

            // Assert
            _repoMock.Verify(
                r => r.AddQuestionAsync(It.IsAny<Questions>()),
                Times.Once);

            _contextMock.Verify(
                c => c.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetQuestionByIdAsync_WhenFound_ShouldReturnQuestion()
        {
            // Arrange
            var dto = new QuestionReadDTO
            {
                Id = 1,
                QuestionName = "Sample",
                SubCategoryId = 1,
                ParagraphId = 1,
                YearPeriodId = 1,
                choices = new List<ChoiceReadDTO>()
                {
                    new ChoiceReadDTO()
                    {
                        ChoiceText = "yes",
                        IsCorrect = true
                    },
                    new ChoiceReadDTO()
                    {
                        ChoiceText = "no",
                        IsCorrect = false
                    }
                }
            };

            _repoMock
                .Setup(r => r.GetQuestionByIdAsync(1))
                .ReturnsAsync(dto);

            // Act

            var result = await _service.GetQuestionByIdAsync(1);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Sample", result.QuestionName);
        }


        [Fact]
        public async Task UpdateQuestionAsync_WhenValid_ShouldUpdateQuestion()
        {
            // Arrange
            var existingQuestion = new Questions
            {
                Id = 1,
                QuestionName = "Old Name",
                SubCategoryId = 1,
                YearPeriodId = 1
            };

            _repoMock
                .Setup(r => r.FindQuestionByIdAsync(1))
                .ReturnsAsync(existingQuestion);

            _validatorMock
                .Setup(v => v.ValidateReferencesAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int?>()))
                .Returns(Task.CompletedTask);

            var dto = new QuestionUpdateDTO
            {
                QuestionName = "Updated Name",
                SubCategoryId = 2,
                YearPeriodId = 2,
                ParagraphId = null
            };

            // Act
            await _service.UpdateQuestionAsync(1, dto);

            // Assert
            Assert.Equal("Updated Name", existingQuestion.QuestionName);
            Assert.Equal(2, existingQuestion.SubCategoryId);

            _repoMock.Verify(
                r => r.UpdateQuestion(existingQuestion),
                Times.Once);

            _contextMock.Verify(
                c => c.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteQuestionAsync_WhenValid_ShouldDeleteQuestion()
        {
            // Arrange
            var question = new Questions
            {
                Id = 1,
                QuestionName = "what is kkk",
                YearPeriodId = 1,
                SubCategoryId = 1,
            };

            _repoMock
                .Setup(r => r.FindQuestionByIdAsync(1))
                .ReturnsAsync(question);

            // Act
            await _service.DeleteQuestionAsync(1);

            // Assert
            _repoMock.Verify(
                r => r.DeleteQuestion(question),
                Times.Once);

            _contextMock.Verify(
                c => c.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

    }
}
