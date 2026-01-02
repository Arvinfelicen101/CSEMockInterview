using Backend.DTOs.SubCategory;

namespace Backend.Services.SubCategory
{
    public interface ISubCategoryService
    {
        Task CreateSubCategoryAsync(SubCategoryCreateDTO dto);
        Task<List<SubCategoryListDTO>> GetAllAsync();
        Task UpdateSubCategoryAsync (int id, SubCategoryUpdateDTO dto);
        Task DeleteSubCategoryAsync(int id);
    }
}
