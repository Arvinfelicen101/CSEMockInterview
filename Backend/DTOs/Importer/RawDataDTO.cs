namespace Backend.DTOs.Importer;

public class RawDataDTO
{
    public required string RawQuestions { get; set; }
    public string? RawParagraph { get; set; }
    public required string RawCategories { get; set; }
    public required string RawSubCategories { get; set; }
    public required List<ChoiceDTO> RawChoices { get; set; }
    public int RawYear { get; set; }
    public required string RawPeriods { get; set; }
    //year period
}