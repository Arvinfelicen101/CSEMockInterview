namespace CSEMockInterview.Models;

public class Choices
{
    public int Id { get; set; }
    public string ChoiceText { get; set; }

    public bool IsCorrect { get; set; } = false;
    //Foreign Keys
    public int QuestionId { get; set; }
    public Questions QuestionsNavigation { get; set; }

    public ICollection<UserAnswers> UserAnswersCollection { get; } = new List<UserAnswers>();
}