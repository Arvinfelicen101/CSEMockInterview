using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Importer;

public class ImporterDTO
{
    [Required]
    public string fileName { get; set; }
    [Required]
    public IFormFile file { get; set; }
}