using Backend.Models.enums;

namespace Backend.Models;

public class YearPeriods
{
    public int Id { get; set; }
    public int Year { get; set; }
    public Periods Periods { get; set; }
    
    //NaVIGATION
    public ICollection<Questions> QuestionsCollection { get; } = new List<Questions>();
}