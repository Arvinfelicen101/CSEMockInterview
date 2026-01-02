using Backend.DTOs.Question;
using Backend.Models;

namespace Backend.Repository.Question
{
    public interface IQuestionRepository
    {
        Task<bool> SubCategoryExistsAsync(int id);
        Task<bool> YearPeriodExistsAsync(int id);
        Task<bool> ParagraphExistsAsync(int id);
        Task AddQuestionAsync(Questions question);
        Task<QuestionReadDTO?> GetQuestionByIdAsync(int id);
        Task<Questions?> FindQuestionByIdAsync(int id);
        Task<List<QuestionListDTO>> GetAllAsync();
        Task UpdateQuestionAsync(Questions question);
        Task DeleteQuestionAsync(Questions question);

    }
}
