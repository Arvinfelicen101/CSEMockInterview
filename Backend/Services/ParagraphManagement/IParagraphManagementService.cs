using Backend.DTOs.Importer;
using Backend.Models;

namespace Backend.Services.ParagraphManagement;

public interface IParagraphManagementService
{
    Task<IEnumerable<Paragraphs>> GetAllService();
}