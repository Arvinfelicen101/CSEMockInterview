using Backend.Repository.Importer;
using Backend.DTOs.Importer;
using Backend.Models;
using Backend.Services.ParagraphManagement;
using Backend.Services.Question;
using Backend.Services.SubCategory;
using Backend.Services.YearPeriodManagement;

namespace Backend.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;
    private readonly IYearPeriodService _yearPeriodService;
    private readonly IParagraphManagementService _paragraphService;
    private readonly ISubCategoryService _subCategoryService;
    private readonly IQuestionService _questionService;
    private readonly ILogger<ImporterService> _logger;

    public ImporterService(IQuestionService questionService, IImporterRepository repository, IYearPeriodService yearPeriodService, IParagraphManagementService paragraphManagementService, ISubCategoryService subCategoryService, ILogger<ImporterService> logger)
    {
        _paragraphService = paragraphManagementService;
        _repository = repository;
        _yearPeriodService = yearPeriodService;
        _subCategoryService = subCategoryService;
        _logger = logger;
        _questionService = questionService;
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

    public async Task<IEnumerable<Questions>> getQuestions()
    {
        return await _questionService.GetAllAsync();
    }

    public async Task<FKDataDTOs> ExistingCache()
    {
        var YearPeriodData = await getCategoryFK();
        var ParagraphData = await getParagraphFK();
        var subCategoriesData = await getSubCategoriesFK();
        var questionsData = await getQuestions();
        var fkData = new FKDataDTOs()
        {
            YearPeriodFK = YearPeriodData,
            ParagraphFK = ParagraphData,
            subCategoriesFK = subCategoriesData,
            questionsCache = questionsData
        };
        return fkData;
    }

    public async Task ProcessFileAsync(IFormFile File)
    {
        var fkData = await ExistingCache();
        var result = await ServiceHelper.ParseFileAsync(File, _logger);
        var mappeddata = ServiceHelper.ImportFkMapper(result, fkData, _logger);
        await _repository.AddAsync(mappeddata.Item1, mappeddata.Item2);
    }
}