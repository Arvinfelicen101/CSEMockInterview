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
    

    //create a dto for the list of fks, should be in service if it need to insert data, possible to be not. just separate method
    public static async Task<MappedDataFK> ImportFKMapper(List<RawDataDTO> list, FKDataDTOs dtos)
    {
        //PREPARE CACHE VARIABLES FIRST / dictionaries
        
        //need linq 
        var questions = new List<Questions>();
        var choices = new List<Choices>();
        
        
        foreach (var rowData in list)
        {
            //just check every fk if it exist or not in the cache
            questions.Add(new Questions()
            {
                QuestionName = rowData.RawQuestions,
                ParagraphNavigation = new Paragraphs()
                {
                    ParagraphText = rowData.RawParagraph // should be variable of entity 
                },
                SubCategoryNavigation = new SubCategories()
                {
                    SubCategoryName = rowData.RawSubCategories
                },
                YearPeriodNavigation = new YearPeriods()
                {
                    Year = rowData.RawYear,
                    Periods = rowData.RawPeri()
                }
            });
            
            // choices.Add(new Choices()
            // {
            //     ChoiceText = rowData.RawChoices.Where(c => c.ChoiceText == ra)
            // });
        }

        return new MappedDataFK()
        {
            SubCategoriesList = subCategories,
            YearPeriodsList = yearPeriod,
            ParagraphsList = paragraphs
        };
    }
    
}