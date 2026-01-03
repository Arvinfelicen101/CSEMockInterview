namespace Backend.DTOs.Importer;

public class YearPeriodDTO 
{
    public int Id { get; set; }
    public required int year { get; set; }
    public required string period { get; set; } 
}