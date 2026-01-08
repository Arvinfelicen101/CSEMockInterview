using Backend.Models;

namespace Backend.Repository.ChoicesManagement
{
    public interface IChoicesRepository
    {
        Task<bool> HasAnotherCorrectChoiceAsync(int questionId, int excludingChoiceId);
        Task<Choices?> FindChoiceById(int id);
        Task UpdateChoiceAsync(Choices choice);
        Task DeleteChoiceAsync(Choices choice);
        Task SaveChangesAsync();
    }
}
