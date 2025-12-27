using System.Text;
using Backend.DTOs.Importer;
using Backend.Models.enums;
using Backend.Models;
using ClosedXML.Excel;

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
                throw new Exception();
            }
            
        }
        return extractedData;
    }

    //create a dto for the list of fks
    public static async Task<MappedData> ImportMapper(List<RawDataDTO> list, FKDataDTOs dtos)
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

            if (rowData.RawCategories == Categories.Verbal.ToString() &&
                !(dtos.categoryFK.Any(c => c.CategoryName == rowData.RawCategories)))
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.Verbal
                });
            } else if (rowData.RawCategories == Categories.Analytical.ToString() &&
                       !(dtos.categoryFK.Any(c => c.CategoryName == rowData.RawCategories)))
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.Analytical
                });
            } else if (rowData.RawCategories == Categories.Clerical.ToString() &&
                       !(dtos.categoryFK.Any(c => c.CategoryName == rowData.RawCategories)))
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.Clerical
                });
            } else if (rowData.RawCategories == Categories.General.ToString() &&
                       !(dtos.categoryFK.Any(c => c.CategoryName == rowData.RawCategories)))
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.General
                });
            } else if (rowData.RawCategories == Categories.Numerical.ToString() &&
                       !(dtos.categoryFK.Any(c => c.CategoryName == rowData.RawCategories)))
            {
                categories.Add(new Category()
                {
                    CategoryName = Categories.Numerical
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
                    QuestionId = 1 // data should be from cache?
                }
                ));
        }

        return new MappedData(categories);
    }
}