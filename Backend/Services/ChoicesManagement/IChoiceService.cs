using Backend.DTOs.Choices;

namespace Backend.Services.ChoicesManagement
{
    public interface IChoiceService
    {
        Task UpdateChoiceAsync(int id, ChoiceUpdateDTO choice);
        Task DeleteChoiceAsync(int id);
    }
}
