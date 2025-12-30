using Backend.DTOs.Choices;

namespace Backend.Services.Choices
{
    public interface IChoiceService
    {
        Task CreateChoiceService(ChoiceDTO choice);
    }
}
