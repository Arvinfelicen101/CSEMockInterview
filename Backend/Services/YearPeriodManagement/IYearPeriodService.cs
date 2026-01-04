using Backend.Models;

namespace Backend.Services.YearPeriodManagement;

public interface IYearPeriodService
{
    Task<IEnumerable<YearPeriods>> GetAllService();
}