using System.Text;
using Backend.Repository.Importer;
using Backend.DTOs.Importer;
using Backend.Services.CategoryManagement;
using ClosedXML.Excel;

namespace Backend.Services.Importer;

public class ImporterService : IImporterService
{
    private readonly IImporterRepository _repository;
    private readonly ICategoryService _categoryService;

    public ImporterService(IImporterRepository repository, ICategoryService categoryService)
    {
        _repository = repository;
        _categoryService = categoryService;
    }

    public async Task<IEnumerable<CategoryDTO>> getCategoryFK()
    {
        return await _categoryService.GetAllService();
    }
    
    public async Task ProcessFileAsync(ImporterDTO xlsx)
    {
        var categoryData = await getCategoryFK();
        var result = await ServiceHelper.ParseFileAsync(xlsx);
        var mappeddata = await ServiceHelper.ImportMapper(result);
        
    }
    
}