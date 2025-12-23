namespace Backend.DTOs.Importer;

public class CategoryDTO : IEntity
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
}