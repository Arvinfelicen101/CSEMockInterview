using System.Text;
using Backend.Repository.Importer;
using Backend.DTOs.Importer;
using Backend.Services.YearPeriodManagement;
using ClosedXML.Excel;

namespace Backend.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;
    private readonly IYearPeriodService _yearPeriodService;

    public ImporterService(IImporterRepository repository, IYearPeriodService yearPeriodService)
    {
        _repository = repository;
        _yearPeriodService = yearPeriodService;
    }

    public async Task<IEnumerable<CategoryDTO>> getCategoryFK()
    {
        return await _yearPeriodService.GetAllService();
    }
    
    public async Task ProcessFileAsync(ImporterDTO xlsx)
    {
        var categoryData = await getCategoryFK();
        var fkData = new FKDataDTOs()
        {
            categoryFK = categoryData
        };
        
        var result = await ServiceHelper.ParseFileAsync(xlsx);
        var mappeddata = await ServiceHelper.ImportMapper(result, fkData);
        
    }
    
}