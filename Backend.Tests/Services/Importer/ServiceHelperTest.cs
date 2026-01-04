using Backend.DTOs.Importer;
using Backend.Models;
using Backend.Services.Importer;
namespace Backend.Tests.Services.Importer;

public class ServiceHelperTest
{

    [Fact]
    public void ImportFkMapper_Creates_Questions_And_Choices_Correctly()
    {
        // Arrange
        var rawData = new List<RawDataDTO>
        {
            new RawDataDTO
            {
                RawQuestions = "What is 2 + 2?",
                RawParagraph = "Simple math paragraph",
                RawYear = 2026,
                RawPeriods = "Q1",
                RawCategories = "Numerical",
                RawSubCategories = "Addition",
                RawChoices = new List<ChoiceDTO>
                {
                    new ChoiceDTO { ChoiceText = "3", IsCorrect = false },
                    new ChoiceDTO { ChoiceText = "4", IsCorrect = true }
                }
            }
        };

        var fkDtos = new FKDataDTOs
        {
            ParagraphFK = new List<Paragraphs>(),
            YearPeriodFK = new List<YearPeriods>(),
            subCategoriesFK = new List<SubCategories>()
        };

        // Act
        var (questions, choices) = ServiceHelper.ImportFkMapper(rawData, fkDtos);

        // Assert
        Assert.Single(questions);
        Assert.Equal(2, choices.Count);

        var question = questions[0];

        Assert.Equal("What is 2 + 2?", question.QuestionName);
        Assert.Equal("Simple math paragraph", question.ParagraphNavigation.ParagraphText);
        Assert.Equal("Addition", question.SubCategoryNavigation.SubCategoryName);
        Assert.Equal(3, question.SubCategoryNavigation.CategoryId); // Numerical
        Assert.Equal(2026, question.YearPeriodNavigation.Year);

        Assert.Single(choices.Where(c => c.IsCorrect));
        Assert.All(choices, c => Assert.Same(question, c.QuestionsNavigation));
    }
}
