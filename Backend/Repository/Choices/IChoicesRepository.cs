using Backend.Models;

namespace Backend.Repository.Choices
{
    public interface IChoicesRepository
    {
        Task<int> CountChoicesByQuestionIdAsync(int questionId);
        Task<bool> HasAnotherCorrectChoiceAsync(int questionId, int excludingChoiceId);
        Task<bool> QuestionExistAsync(int id);
        Task CreateChoicesAsync(ItemChoices choices);
        Task<ItemChoices?> FindChoiceById(int id);
        Task UpdateChoiceAsync(ItemChoices choice);
        Task DeleteChoiceAsync(ItemChoices choice);
    }
}
