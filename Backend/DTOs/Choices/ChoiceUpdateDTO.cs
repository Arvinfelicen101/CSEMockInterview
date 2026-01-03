using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Choices
{
    public class ChoiceUpdateDTO
    {
        [Required(ErrorMessage = "Choice is required.")]
        public required string ChoiceText { get; set; }

        [Required(ErrorMessage = "IsCorrect is required")]
        public required bool IsCorrect { get; set; }

   
    }
}
