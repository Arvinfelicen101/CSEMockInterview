using System.Text;
using Backend.DTOs.Importer;
using Backend.Models.enums;
using Backend.Models;
using ClosedXML.Excel;
using Paragraphs = Backend.Models.Paragraphs;

namespace Backend.Services.Importer;

public static class ServiceHelper
{
    public static async Task<List<RawDataDTO>> ParseFileAsync(ImporterDTO xlsx)
    {
        string sheetName;
        var extractedData = new List<RawDataDTO>();
        using (var stream = new MemoryStream())
        {
            var filename = xlsx.file.FileName;
            string year = filename.Substring(0, 3);
            int i = 5;
            var period = new StringBuilder();

            while (i < filename.Length)
            {
                char charPeriod = filename[i];

                if (charPeriod == '_')
                    break;

                period.Append(charPeriod);
                i++;
            }

            string result = period.ToString();
            await xlsx.file.CopyToAsync(stream);
            try
            {
                using (var workbook = new XLWorkbook(stream))
                {
                    foreach (var worksheet in workbook.Worksheets)
                    {
                        sheetName = worksheet.Name;
                        
                        var rows = worksheet.RowsUsed().Skip(1);
                        foreach (var row in rows)
                        {
                            extractedData.Add(new RawDataDTO()
                            {
                                RawCategories = sheetName,
                                RawQuestions = row.Cell(1).GetString(),
                                RawSubCategories = row.Cell(2).GetString(),
                                RawChoices = new List<ChoiceDTO>()
                                {
                                    
                                    new ChoiceDTO()
                                    {
                                        ChoiceText = row.Cell(3).GetString(),
                                        IsCorrect = false
                                    },
                                    new ChoiceDTO()
                                    {
                                        ChoiceText = row.Cell(4).GetString(),
                                        IsCorrect = false
                                    },
                                    new ChoiceDTO()
                                    {
                                        ChoiceText = row.Cell(5).GetString(),
                                        IsCorrect = false
                                    },
                                    new ChoiceDTO()
                                    {
                                        ChoiceText = row.Cell(6).GetString(),
                                        IsCorrect = true
                                    }
                                    
                                },
                            
                                RawParagraph = row.Cell(7).GetString(),
                                RawYear = Convert.ToInt32(year),
                                RawPeriods = result
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Failed to retrieve DTO list.", e);
            }
            
        }
        return extractedData;
    }
    
    //create a dto for the list of fks, should be in service if it need to insert data, possible to be not. just separate method
    public static (List<Questions>, List<Choices>) ImportFkMapper(List<RawDataDTO> list, FKDataDTOs dtos)
    {
        //PREPARE CACHE VARIABLES FIRST / dictionaries
        var paragraphCache = dtos.ParagraphFK.ToDictionary(p => p.ParagraphText, p => p);
        var yearPeriodCache = dtos.YearPeriodFK.ToDictionary(y => (y.Year, y.Periods), y => y);
        var subCategoryCache = dtos.subCategoriesFK.ToDictionary(s => s.SubCategoryName, s => s);
        var categoryMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["Verbal"] = 1,
            ["Analytical"] = 2,
            ["Numerical"] = 3,
            ["Clerical"] = 4,
            ["General"] = 5
        };
        var questions = new List<Questions>();
        var choices = new List<Choices>();
        
        
        foreach (var rowData in list)
        {
            var rawParagraph = rowData.RawParagraph;

            if (string.IsNullOrWhiteSpace(rawParagraph))
                continue;
            
            if (!paragraphCache.TryGetValue(rawParagraph, out var paragraph))
            {
                paragraph = new Paragraphs()
                {
                    ParagraphText = rawParagraph
                };
                paragraphCache[rawParagraph] = paragraph;
            }
           
            if (!yearPeriodCache.TryGetValue((rowData.RawYear, Enum.TryParse<Periods>(rowData.RawPeriods, true, out var period) ? period : default), out var yearPeriods))
            {
                yearPeriods = new YearPeriods
                {
                    Year = rowData.RawYear,
                    Periods = period
                };
                yearPeriodCache[(rowData.RawYear, period)] = yearPeriods;
            }
            
            if (!subCategoryCache.TryGetValue(rowData.RawSubCategories, out var subCategories))
            {
                if (!categoryMap.TryGetValue(rowData.RawCategories, out var categoryId))
                    throw new Exception($"Unknown category: {rowData.RawCategories}");

                subCategories = new SubCategories
                {
                    SubCategoryName = rowData.RawSubCategories,
                    CategoryId = categoryId
                };
                
                subCategoryCache[rowData.RawSubCategories] = subCategories;
            }
            //might need mapping and lookup for category
            //check for duplicates in cache
            var questionData = new Questions()
            {
                QuestionName = rowData.RawQuestions,
                ParagraphNavigation = paragraph,
                SubCategoryNavigation = subCategories,
                YearPeriodNavigation = yearPeriods,
            };
            questions.Add(questionData);

            foreach (var choiceList in rowData.RawChoices)
            {
                choices.Add(new Choices()
                {
                    ChoiceText = choiceList.ChoiceText,
                    IsCorrect = choiceList.IsCorrect,
                    QuestionsNavigation = questionData
                });
            }
        }

        return (questions, choices);
    }
    
}