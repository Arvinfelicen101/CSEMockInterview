namespace Backend.Models;

public class Questions
{
    public int Id { get; set; }
    public string QuestionName { get; set; }
    
    //Foreign Keys
    public int? ParagraphId { get; set; }
    public Paragraphs ParagraphNavigation { get; set; }
    
    public int SubCategoryId { get; set; }
    public SubCategories SubCategoryNavigation { get; set; }
    
    public int YearPeriodId { get; set; }
    public YearPeriods YearPeriodNavigation { get; set; }

    public ICollection<ItemChoices> ChoicesCollection { get; } = new List<ItemChoices>();
    public ICollection<UserAnswers> AnswersCollection { get; } = new List<UserAnswers>();
}