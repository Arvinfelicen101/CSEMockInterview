using Backend.Models;

namespace Backend.Repository.ParagraphManagement;

public interface IParagraphManagementRepository
{
    Task<IEnumerable<Paragraphs>> GetAllAsync();
}