namespace Backend.Models;

public class Paragraphs
{
    public int Id { get; set; }
    public required string ParagraphText { get; set; }
    
    //NaVIGATION
    public ICollection<Questions> QuestionsCollection { get; } = new List<Questions>();
}