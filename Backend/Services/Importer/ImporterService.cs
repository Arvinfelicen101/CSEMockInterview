using System.Text;
using Backend.Repository.Importer;
using Backend.DTOs.Importer;
using Backend.Models;
using Backend.Services.ParagraphManagement;
using Backend.Services.SubCategory;
using Backend.Services.YearPeriodManagement;
using ClosedXML.Excel;

namespace Backend.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;
    private readonly IYearPeriodService _yearPeriodService;
    private readonly IParagraphManagementService _paragraphService;
    private readonly ISubCategoryService _subCategoryService;

    public ImporterService(IImporterRepository repository, IYearPeriodService yearPeriodService, IParagraphManagementService paragraphManagementService, ISubCategoryService subCategoryService)
    {
        _paragraphService = paragraphManagementService;
        _repository = repository;
        _yearPeriodService = yearPeriodService;
        _subCategoryService = subCategoryService;
    }

    public async Task<IEnumerable<YearPeriods>> getCategoryFK()
    {
        return await _yearPeriodService.GetAllService();
    }
    
    public async Task<IEnumerable<Paragraphs>> getParagraphFK()
    {
        return await _paragraphService.GetAllService();
    }
    
    public async Task<IEnumerable<SubCategories>> getSubCategoriesFK()
    {
        return await _subCategoryService.GetAllAsync();
    }

    public async Task<FKDataDTOs> ExistingCache()
    {
        var YearPeriodData = await getCategoryFK();
        var ParagraphData = await getParagraphFK();
        var subCategoriesData = await getSubCategoriesFK();
        var fkData = new FKDataDTOs()
        {
            YearPeriodFK = YearPeriodData,
            ParagraphFK = ParagraphData,
            subCategoriesFK = subCategoriesData
        };
        return fkData;
    }
    
    //should be map and insert fk data
    public async Task ProcessFileAsync(ImporterDTO xlsx)
    {
        var fkData = await ExistingCache();
        var result = await ServiceHelper.ParseFileAsync(xlsx);
        var mappeddata = ServiceHelper.ImportFkMapper(result, fkData);

        await _repository.AddAsync(mappeddata.Item1, mappeddata.Item2);

    }
    
    
    //map and insert question and choices data
    
    
}