using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Importer;

public class ImporterDTO
{
    [Required]
    public required string fileName { get; set; }
    [Required]
    public required IFormFile file { get; set; }
}