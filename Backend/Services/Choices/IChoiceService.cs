using Backend.DTOs.Choices;

namespace Backend.Services.Choices
{
    public interface IChoiceService
    {
        Task CreateChoiceAsync(ChoiceCreateDTO choice);
        Task UpdateChoiceAsync(int id, ChoiceUpdateDTO choice);

        Task DeleteChoiceAsync(int id);
    }
}
