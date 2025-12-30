using Backend.DTOs.Importer;
using Backend.Models;
using Backend.Models.enums;

namespace Backend.Services.Importer;

public static class ServiceHelper
{

    public static async Task<MappedData> ImportMapper(List<RawDataDTO> list)
    {
        var categories = new List<Category>();
        var paragraphs = new List<Paragraphs>();
        var choices = new List<ItemChoices>();
        //fk first to be mapped, thrn implement caching
        foreach (var rowData in list)
        {
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
                categories.Add(new Category()
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

            paragraphs.Add(new Paragraphs()
            {
                ParagraphText = rowData.RawParagraph
            });
            
            var allChoices = rowData.RawChoices.ToList();
            if (!string.IsNullOrWhiteSpace(rowData.RawAnswers))
                allChoices.Add(rowData.RawAnswers);

           // choices.AddRange(allChoices.Select(c => new Choices { ChoiceText = c }));

        }

        return new MappedData(categories, paragraphs, choices);
    }
}