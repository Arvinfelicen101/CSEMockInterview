using CSEMockInterview.Models.enums;
namespace CSEMockInterview.Models;

public class Category
{
    public int Id { get; set; }
    public Categories CategoryName { get; set; }

    public ICollection<SubCategories> SubCategories { get; } = new List<SubCategories>();
}