namespace Backend.DTOs.Choices
{
    public class ChoiceCreateDTO
    {
        public required string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
