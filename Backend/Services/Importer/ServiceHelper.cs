using Backend.DTOs.Importer;
using Backend.Models.enums;
using Backend.Models;
namespace Backend.Services.Importer;

public static class ServiceHelper
{
    public static async Task<MappedData> ImportMapper(List<RawDataDTO> list)
    {
        var categories = new List<Category>();
        var paragraphs = new List<Paragraphs>();
        var choices = new List<Choices>();
        var questions = new List<Questions>();
        var subCategories = new List<SubCategories>();
        var yearPeriod = new List<YearPeriods>();
        //fk first to be mapped, thrn implement caching
        foreach (var rowData in list)
        {
            //category mapping
            if (Enum.TryParse<Categories>(rowData.RawCategories, out var Verbal)
                && Verbal == Categories.Verbal)
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.Verbal
                });
            } else if (Enum.TryParse<Categories>(rowData.RawCategories, out var Analytical) 
                       && Analytical == Categories.Analytical)
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.Analytical
                });
            } else if (Enum.TryParse<Categories>(rowData.RawCategories, out var Numerical) 
                       && Numerical == Categories.Numerical)
            {
                categories.Add(new Models.Category()
                {
                    CategoryName = Categories.Numerical
                });
            } else if (Enum.TryParse<Categories>(rowData.RawCategories, out var Clerical) 
                       && Clerical == Categories.Clerical)
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.Clerical
                });
            } else if (Enum.TryParse<Categories>(rowData.RawCategories, out var General) 
                       && General == Categories.General)
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.General
                });
            }
            
            //year period mapping
            if (rowData.RawPeriods == Periods.First.ToString())
            {
                yearPeriod.Add(new YearPeriods()
                {
                    Year = rowData.RawYear,
                    Periods = Periods.First
                });
            }
            else
            {
                yearPeriod.Add(new YearPeriods()
                {
                    Year = rowData.RawYear,
                    Periods = Periods.Second
                });
            }
            
            // paragraph mapping
            paragraphs.Add(new Paragraphs()
            {
                ParagraphText = rowData.RawParagraph,
            });
            //sub category mapping
            subCategories.Add(new SubCategories()
            {
                SubCategoryName = rowData.RawSubCategories,
                CategoryId = 1, // data should be from cache
            });
            
            //questions mapping
            questions.Add(new Questions()
            {
                QuestionName = rowData.RawQuestions,
                ParagraphId = 1, // data should be from cache
                SubCategoryId = 1, // data should be from cache
                YearPeriodId = 1,  // data should be from cache
            });
            
            //choices mapping
            var allChoices = rowData.RawChoices;
            choices.AddRange(allChoices.Select(
                c => new Choices()
                {
                    ChoiceText = c.ChoiceText,
                    IsCorrect = c.IsCorrect,
                    QuestionId = 1 // data shoulbe from cache?
                }
                ));
            
        }

        return new MappedData(categories);
    }
}