namespace Backend.DTOs.Categories;

public class GetCategoryDTO
{
    public int Id { get; set; }
    public required string categoryName { get; set; }
}