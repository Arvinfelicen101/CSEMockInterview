using Backend.DTOs.Importer;

namespace Backend.Services.CategoryManagement;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetAllService();
}