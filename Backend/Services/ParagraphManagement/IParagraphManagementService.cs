using Backend.DTOs.Importer;

namespace Backend.Services.ParagraphManagement;

public interface IParagraphManagementService
{
    Task<IEnumerable<ParagraphDTO>> GetAllService();
}