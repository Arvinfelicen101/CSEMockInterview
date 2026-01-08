using Backend.Context;
using Backend.DTOs.SubCategory;
using Backend.Models;

namespace Backend.Repository.SubCategory
{
    public interface ISubCategoryRepository
    {
        Task<bool> CategoryExistAsync(int id);

        Task<bool> SubCategoryExistsAsync(int categoryId, string name);
        Task CreateSubCategoryAsync(SubCategories subCategory);

        Task<List<SubCategories>> GetAllAsync();
        
        Task<SubCategories?> FindByIdAsync(int id);
        Task SaveChangesAsync();
        Task UpdataSubCategory(SubCategories subCategory);
        Task DeleteSubCategory(SubCategories subCategory);
    }
}
