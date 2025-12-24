using Backend.Models;

namespace Backend.Repository.CategoryManagement;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
}