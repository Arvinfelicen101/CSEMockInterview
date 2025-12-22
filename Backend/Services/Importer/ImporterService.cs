using Backend.Repository.Importer;
using Backend.DTOs.Importer;
using ClosedXML.Excel;

namespace Backend.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;

    public ImporterService(IImporterRepository repository)
    {
        _repository = repository;
    }

    public async Task ProcessFileAsync(ImporterDTO xlsx)
    {
        
    }

    public async Task<List<RawDataDTO>> ParseFileAsync(ImporterDTO xlsx)
    {
        var extractedData = new List<RawDataDTO>();
        using (var stream = new MemoryStream())
        {
            await xlsx.file.CopyToAsync(stream);

            using (var workbook = new XLWorkbook(stream))
            {
                foreach (var worksheet in workbook.Worksheets)
                {
                    string sheetName = worksheet.Name;
                    var rows = worksheet.RowsUsed().Skip(1);
                    foreach (var row in rows)
                    {
                        extractedData.Add(new RawDataDTO()
                        {
                            RawCategories = sheetName,
                            RawQuestions = row.Cell(1).GetString(),
                            RawSubCategories = row.Cell(2).GetString(),
                            RawChoices = new List<string>()
                            {
                                row.Cell(3).GetString(),
                                row.Cell(4).GetString(),
                                row.Cell(5).GetString(),
                            },
                            RawAnswers = row.Cell(6).GetString(),
                        });
                    }
                }
            }
        }

        return extractedData;
    }
}