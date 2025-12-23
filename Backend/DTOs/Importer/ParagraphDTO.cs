namespace Backend.DTOs.Importer;

public class ParagraphDTO: IEntity
{
    public int Id { get; set; }
    public string ParagraphText { get; set; }
    public int questionId { get; set; }
}