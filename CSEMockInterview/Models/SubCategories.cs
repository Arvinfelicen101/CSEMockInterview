namespace CSEMockInterview.Models;

public class SubCategories
{
    public int Id { get; set; }
    public string SubCategoryName { get; set; }
    
    //Foreign key
    public int CategoryId { get; set; }
    public Category categoryNavigation { get; set; }

    public ICollection<Questions> QuestionsCollection { get; } = new List<Questions>();
}