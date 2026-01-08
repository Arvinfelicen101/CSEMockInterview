using Backend.DTOs.Importer;
using Backend.Models;
using Backend.Models.enums;
using Backend.Services.Importer;
using Microsoft.Extensions.Logging.Abstractions;

namespace Backend.Tests.Services.Importer;

public class ServiceHelperTest
{
    [Fact]
    public void ImportFkMapper_Creates_Questions_And_Choices_Correctly()
    {
        //arrange
        var logger = NullLogger.Instance;
        var rawData = new List<RawDataDTO>()
        {
            new RawDataDTO()
            {
                RawQuestions = "Who is the current president of the Philippines?",
                RawParagraph = "President Bongbong and Former President Duterte were allies.",
                RawCategories = Categories.General.ToString(),
                RawSubCategories =  "R.A",
                RawChoices = new List<ChoiceDTO>()
                {
                    new ChoiceDTO()
                    {
                        ChoiceText = "Bongbong Marcos",
                        IsCorrect = true
                    },
                    new ChoiceDTO()
                    {
                        ChoiceText = "Rodrigo Duterte",
                        IsCorrect = false
                    }
                },
                RawYear = 2025,
                RawPeriods = "First"
            }
            
        };

        var fkData = new FKDataDTOs()
        {
            YearPeriodFK = new List<YearPeriods>()
            {
                new YearPeriods()
                {
                    Year = 2025,
                    Periods = Periods.First
                },
                new YearPeriods()
                {
                    Year = 2025,
                    Periods = Periods.Second
                }
            },
            ParagraphFK = new List<Paragraphs>()
            {
                new Paragraphs()
                {
                    Id = 1,
                    ParagraphText = "President Bongbong and Former President Duterte were allies.",
                },
                new Paragraphs()
                {
                    Id = 2,
                    ParagraphText = "To be or not to be.",
                }
            },
            subCategoriesFK = new List<SubCategories>()
            {
                new SubCategories()
                {
                    Id = 1,
                    SubCategoryName = "R.A"
                }
            },
            questionsCache = new List<Questions>()
            {
                new Questions()
                {
                    QuestionName = "What is 1 + 1?",
                    SubCategoryId = 3,
                    YearPeriodId = 1,
                }
            }
        };
        
        
        
        //Act
        var result = ServiceHelper.ImportFkMapper(rawData, fkData, logger);
        var correctChoice = result.Item2.First(c => c.IsCorrect);
        var wrongChoice = result.Item2.First(c => !c.IsCorrect);

        //Assert
        Assert.Equal("Bongbong Marcos", correctChoice.ChoiceText);
        Assert.True(correctChoice.IsCorrect);
        Assert.Equal("Rodrigo Duterte", wrongChoice.ChoiceText);
        Assert.False(wrongChoice.IsCorrect);
        Assert.Equal(1, rawData.Count);
        Assert.NotEqual(2, result.Item1.Count);
        Assert.NotEqual(1, result.Item2.Count);
       
    }
    
}
