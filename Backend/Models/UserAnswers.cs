namespace Backend.Models;

public class UserAnswers
{
    public int Id { get; set; }
    //Foreign Key
    public required string UserId { get; set; }
    public Users? UserNavigation { get; set; }
    
    public int QuestionId { get; set; }
    public Questions? QuestionsNavigation { get; set; }
    
    public required int Answer { get; set; }
    public Choices? ChoicesNavigation { get; set; }
}