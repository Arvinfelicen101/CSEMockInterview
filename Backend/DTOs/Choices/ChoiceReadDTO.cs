namespace Backend.DTOs.Choices
{
    public class ChoiceReadDTO
    {
        public int Id { get; set; }
        public required string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
    
    }
}
