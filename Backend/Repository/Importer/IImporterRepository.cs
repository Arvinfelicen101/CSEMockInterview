using Backend.Models;

namespace Backend.Repository.Importer;

public interface IImporterRepository
{
    Task AddAsync(List<Questions> questionsList, List<Choices> choicesList);
}