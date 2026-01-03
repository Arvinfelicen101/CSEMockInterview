using Backend.Models;

namespace Backend.Repository.YearPeriodManagement;

public interface IYearPeriodRepository
{
    Task<List<YearPeriods>> GetAllAsync();
}