using Backend.Models;

namespace Backend.Repository.Choices
{
    public interface IChoicesRepository
    {
        Task CreateChoicesAsync(ItemChoices choices);
    }
}
