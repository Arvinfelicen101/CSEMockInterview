using Backend.DTOs.Choices;
using Backend.Repository.Choices;
using DocumentFormat.OpenXml.Presentation;

namespace Backend.Services.Choices
{
    public class ChoiceService : IChoiceService
    {
        public readonly IChoicesRepository _repo;

        public ChoiceService(IChoicesRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateChoiceService(ChoiceDTO choice)
        {
           
        }
    }
}
