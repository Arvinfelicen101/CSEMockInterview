namespace Backend.Models;

public class Choices
{
    public int Id { get; set; }
    public required string ChoiceText { get; set; }

    public required bool IsCorrect { get; set; } = false;
    //Foreign Keys
    public int QuestionId { get; set; }
    public Questions? QuestionsNavigation { get; set; }

    public ICollection<UserAnswers> UserAnswersCollection { get; } = new List<UserAnswers>();
}