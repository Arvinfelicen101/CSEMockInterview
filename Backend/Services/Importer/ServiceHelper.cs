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
    

    //create a dto for the list of fks, should be in service if it need to insert data
    public static async Task<MappedData> ImportMapper(List<RawDataDTO> list, FKDataDTOs dtos)
    {
        //need linq 
        var categories = new List<Category>();
        var paragraphs = new List<Paragraphs>();
        var choices = new List<Choices>();
        var questions = new List<Questions>();
        var subCategories = new List<SubCategories>();
        var yearPeriod = new List<YearPeriods>();
        
        //fk first to be mapped, thrn implement caching
        foreach (var rowData in list)
        {
            //create a if else cache check before mapping instead inside the mapping conditionals sp it can skip fast
            //year period mapping, needs cache, insert data right after mapping then cache it
            if (!dtos.YearPeriodFK.Any(y => y.year == rowData.RawYear && y.period == rowData.RawPeriods))
            {
                if (rowData.RawPeriods == Periods.First.ToString())
                {
                    yearPeriod.Add(new YearPeriods()
                    {
                    
                        Year = rowData.RawYear,
                        Periods = Periods.First
                    });
                }
                else if (rowData.RawPeriods == Periods.Second.ToString())
                {
                    yearPeriod.Add(new YearPeriods()
                    {
                        Year = rowData.RawYear,
                        Periods = Periods.Second
                    });
                }
            }
            // paragraph mapping, needs cache, insert right then cache it
            if (!dtos.ParagraphFK.Any(p => p.ParagraphText == rowData.RawParagraph))
            {
                paragraphs.Add(new Paragraphs()
                {
                    ParagraphText = rowData.RawParagraph,
                });
            }
            //category mapping
            if (rowData.RawCategories == Categories.Verbal.ToString())
            {
                categories.Add(new Category()
                {
                    Id = 0,
                    CategoryName = Categories.Verbal
                });
                subCategories.Add(new SubCategories()
                {
                    SubCategoryName = rowData.RawSubCategories,
                    CategoryId = 0,
                });
            }
            else if (rowData.RawCategories == Categories.Analytical.ToString())
            {
                categories.Add(new Category()
                {
                    Id = 1,
                    CategoryName = Categories.Analytical
                });
                subCategories.Add(new SubCategories()
                {
                    SubCategoryName = rowData.RawSubCategories,
                    CategoryId = 1,
                });
            }
            else if (rowData.RawCategories == Categories.Clerical.ToString())
            {
                categories.Add(new Category()
                {
                    Id = 3,
                    CategoryName = Categories.Clerical
                });
                subCategories.Add(new SubCategories()
                {
                    SubCategoryName = rowData.RawSubCategories,
                    CategoryId = 3,
                });
            } 
            else if (rowData.RawCategories == Categories.General.ToString())
            {
                categories.Add(new Category()
                {
                    Id = 4,
                    CategoryName = Categories.General
                });
                subCategories.Add(new SubCategories()
                {
                    SubCategoryName = rowData.RawSubCategories,
                    CategoryId = 4,
                });
            } 
            else if (rowData.RawCategories == Categories.Numerical.ToString())
            {
                categories.Add(new Category()
                {
                    Id = 2,
                    CategoryName = Categories.Numerical
                });
                if ()
                {
                    subCategories.Add(new SubCategories()
                {
                    SubCategoryName = rowData.RawSubCategories,
                    CategoryId = 2,
                });
                }
                
            }
        }
        
        foreach (var row in list)
        {
            //insert fk above first before mapping
            questions.Add(new Questions()
            {
                QuestionName = row.RawQuestions,
                ParagraphId = 1,
                SubCategoryId = 1,
                YearPeriodId = 1,
            });
            
            //choices mapping, no need cache
            var allChoices = row.RawChoices;
            choices.AddRange(allChoices.Select(
                c => new Choices()
                {
                    ChoiceText = c.ChoiceText,
                    IsCorrect = c.IsCorrect,
                    QuestionId = 1 // data should be from cache?
                })
            );
        }
        return new MappedData(categories);
    }
}