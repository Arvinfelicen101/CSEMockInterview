using Backend.DTOs.Choices;
using Backend.Exceptions;
using Backend.Repository.ChoicesManagement;

namespace Backend.Services.ChoicesManagement
{
    public class ChoiceService : IChoiceService
    {
        public readonly IChoicesRepository _repo;

        public ChoiceService(IChoicesRepository repo)
        {
            _repo = repo;
        }
        

        public async Task UpdateChoiceAsync(int id, ChoiceUpdateDTO choice)
        {
            var choiceById = await _repo.FindChoiceById(id);
            if (choiceById == null)
                throw new NotFoundException("Choice does not exist");

            var questionId = choiceById.QuestionId;

            if (!choice.IsCorrect && choiceById.IsCorrect)
            {
                var hasAnotherCorrect = await _repo.HasAnotherCorrectChoiceAsync(questionId, id);

                if (!hasAnotherCorrect)
                    throw new BadRequestException(
                        "A question must have at least one correct choice");
            }

            choiceById.ChoiceText = choice.ChoiceText;
            choiceById.IsCorrect = choice.IsCorrect;
           
            await _repo.UpdateChoiceAsync(choiceById);
            await _repo.SaveChangesAsync();

        }

        public async Task DeleteChoiceAsync(int id)
        {
            
            var choice = await _repo.FindChoiceById(id);

            if (choice == null) throw new NotFoundException(("Choice does not exist"));

            await _repo.DeleteChoiceAsync(choice);
            await _repo.SaveChangesAsync();
        }
        
    }
}
