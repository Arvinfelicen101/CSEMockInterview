using Backend.Context;
using Backend.DTOs.Choices;
using Backend.Repository.ChoicesManagement;
using Backend.Services.ChoicesManagement;
using Moq;

namespace Backend.Tests.Services.ChoicesTest
{
    public class ChoiceServiceTest
    {
        public readonly Mock<IChoicesRepository> _repoMock;
        public readonly ChoiceService _service;

        public ChoiceServiceTest()
        {
            _repoMock = new Mock<IChoicesRepository>();

            _service = new ChoiceService(
                _repoMock.Object
                ); 
        }

        [Fact]
        public async Task UpdateChoiceAsync_WhenValid_ShouldUpdateChoice()
        {
            // Arrange
  
            var existingChoice = new Models.Choices
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

            _repoMock.Setup(r => r.SaveChangesAsync());

            var dto = new ChoiceUpdateDTO
            {
                ChoiceText = "Updated Choice",
                IsCorrect = true
            };

            // Act

            await _service.UpdateChoiceAsync(1, dto);

            // Assert

            _repoMock.Verify(
                r => r.UpdateChoiceAsync(It.Is<Models.Choices>(c =>
                    c.ChoiceText == "Updated Choice" &&
                    c.IsCorrect == true
                )),
                Times.Once
            );

            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteChoiceAsync_WhenChoiceExists_ShouldCallDeleteAndSave()
        {
            // Arrange
            var choice = new Models.Choices { Id = 1, ChoiceText = "A", IsCorrect = false };

            _repoMock.Setup(r => r.FindChoiceById(1))
                     .ReturnsAsync(choice); 

            _repoMock.Setup(r => r.DeleteChoiceAsync(choice))
                     .Returns(Task.CompletedTask);

            _repoMock.Setup(r => r.SaveChangesAsync());

            // Act
            await _service.DeleteChoiceAsync(1);

            // Assert
            _repoMock.Verify(r => r.DeleteChoiceAsync(choice), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
