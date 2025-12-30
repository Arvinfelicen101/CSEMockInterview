using Backend.DTOs.Question;

namespace Backend.Services.Question
{
    public interface IQuestionService
    {
        Task CreateQuestionService(QuestionCreateDTO question);

        Task <QuestionReadDTO> GetQuestionByIdService(int id);
        Task<List<QuestionListDTO>> GetAllService();

        Task UpdateQuestionAsync(int id, QuestionUpdateDTO question);

        Task DeleteQuestionAsync(int id);
    }
}
