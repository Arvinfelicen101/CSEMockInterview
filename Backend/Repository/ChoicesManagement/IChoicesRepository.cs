using Backend.Models;

namespace Backend.Repository.ChoicesManagement
{
    public interface IChoicesRepository
    {
        Task<int> CountChoicesByQuestionIdAsync(int questionId);
        Task<bool> HasAnotherCorrectChoiceAsync(int questionId, int excludingChoiceId);
        Task<bool> QuestionExistAsync(int id);
        Task CreateChoicesAsync(Choices choices);
        Task<Choices?> FindChoiceById(int id);
        Task UpdateChoiceAsync(Choices choice);
        Task DeleteChoiceAsync(Choices choice);
    }
}
