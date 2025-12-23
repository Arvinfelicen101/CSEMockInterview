namespace Backend.Models;

public class Results
{
    public int Id { get; set; }
    public int TotalScore { get; set; }
    public bool IsPassed { get; set; }
    //Foreign Key
    public string UserId { get; set; }
    public Users UsersNavigation { get; set; }
}