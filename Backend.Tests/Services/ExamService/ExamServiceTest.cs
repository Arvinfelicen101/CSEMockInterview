using Backend.Models;
using Backend.Repository.ExamRepository;
using Backend.Services.ExamService;
using Backend.DTOs.Exams;
using JetBrains.Annotations;
using Moq;

namespace Backend.Tests.Services.ExamService;

[TestSubject(typeof(Backend.Services.ExamService.ExamService))]
public class ExamServiceTest
{

    [Fact]
    public async Task METHOD()
    {
        //arrange
        var mockRepo = new Mock<IExamRepository>();
        mockRepo.Setup(r => r.SubmitExamAsync(It.IsAny<List<UserAnswers>>()));
        var dto = new List<UserExamAnswerDTO>()
        {
            new UserExamAnswerDTO()
            {
                UserId = "123",
                AnswerId = 1,
                QuestionId = 1
            },
            new UserExamAnswerDTO()
            {
                UserId = "123",
                AnswerId = 2,
                QuestionId = 2
            },
            new UserExamAnswerDTO()
            {
                UserId = "123",
                AnswerId = 3,
                QuestionId = 3
            },
        };

        var service = new Backend.Services.ExamService.ExamService(mockRepo.Object);
        //act
        await service.SubmitExamService(dto);
        //assert
        Assert.Equal(dto.First().UserId, "123");
       // 123 Assert.NotEqual(dto.Select(u => u.QuestionId));
    }
}