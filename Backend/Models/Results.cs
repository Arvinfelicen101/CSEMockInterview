namespace Backend.Models;

public class Results
{
    public int Id { get; set; }
    public required int TotalScore { get; set; }
    public required bool IsPassed { get; set; }
    public required TimeSpan duration { get; set; }
    //Foreign Key
    public required string UserId { get; set; }
    public Users? UsersNavigation { get; set; }
}