namespace Backend.DTOs.Importer;

public class ChoiceDTO : IEntity
{
    public int Id { get; set; }
    public string ChoiceText { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
}