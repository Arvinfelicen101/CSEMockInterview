using Backend.DTOs.Importer;

namespace Backend.Services.YearPeriodManagement;

public interface IYearPeriodService
{
    Task<IEnumerable<CategoryDTO>> GetAllService();
}