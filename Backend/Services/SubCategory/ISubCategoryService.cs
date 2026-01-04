using Backend.DTOs.SubCategory;
using Backend.Models;

namespace Backend.Services.SubCategory
{
    public interface ISubCategoryService
    {
        Task CreateSubCategoryAsync(SubCategoryCreateDTO dto);
        Task<List<SubCategories>> GetAllAsync();
        Task UpdateSubCategoryAsync (int id, SubCategoryUpdateDTO dto);
        Task DeleteSubCategoryAsync(int id);
    }
}
