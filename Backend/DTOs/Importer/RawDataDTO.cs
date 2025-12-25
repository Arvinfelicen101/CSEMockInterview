namespace Backend.DTOs.Importer;

public class RawDataDTO
{
    public string RawQuestions { get; set; }
    public string? RawParagraph { get; set; }
    public string RawCategories { get; set; }
    public string RawSubCategories { get; set; }
    public List<ChoiceDTO> RawChoices { get; set; }
    public int RawYear { get; set; }
    public string RawPeriods { get; set; }
    //year period
}