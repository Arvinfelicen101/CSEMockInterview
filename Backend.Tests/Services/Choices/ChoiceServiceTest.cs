using Backend.Context;
using Backend.DTOs.Choices;
using Backend.Models;
using Backend.Repository.Choices;
using Backend.Services.Choices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Tests.Services.Choices
{
    public class ChoiceServiceTest
    {
        public readonly Mock<IChoicesRepository> _repoMock;
        public readonly Mock<MyDbContext> _contextMock;
        public readonly ChoiceService _service;

        public ChoiceServiceTest()
        {
            _repoMock = new Mock<IChoicesRepository>();
            _contextMock = new Mock<MyDbContext>();

            _service = new ChoiceService(
                _repoMock.Object,
                _contextMock.Object
                );
        }

        [Fact]
        public async Task UpdateChoiceAsync_WhenValid_ShouldUpdateChoice()
        {
            // Arrange
  
            var existingChoice = new ItemChoices
            {
                Id = 1,
                ChoiceText = "Old Choice",
                IsCorrect = true,
                QuestionId = 10
            };

            _repoMock
                .Setup(r => r.FindChoiceById(1))
                .ReturnsAsync(existingChoice);

            _repoMock
                .Setup(r => r.HasAnotherCorrectChoiceAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            var dto = new ChoiceUpdateDTO
            {
                ChoiceText = "Updated Choice",
                IsCorrect = true
            };

            // Act

            await _service.UpdateChoiceAsync(1, dto);

            // Assert

            _repoMock.Verify(
                r => r.UpdateChoiceAsync(It.Is<ItemChoices>(c =>
                    c.ChoiceText == "Updated Choice" &&
                    c.IsCorrect == true
                )),
                Times.Once
            );

            _contextMock.Verify(
                c => c.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact]
        public async Task DeleteChoiceAsync_WhenChoiceExists_ShouldCallDeleteAndSave()
        {
            // Arrange
            var choice = new ItemChoices { Id = 1, ChoiceText = "A", IsCorrect = false };

            _repoMock.Setup(r => r.FindChoiceById(1))
                     .ReturnsAsync(choice); 

            _repoMock.Setup(r => r.DeleteChoiceAsync(choice))
                     .Returns(Task.CompletedTask);

            // Act
            await _service.DeleteChoiceAsync(1);

            // Assert
            _repoMock.Verify(r => r.DeleteChoiceAsync(choice), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
    }
}
