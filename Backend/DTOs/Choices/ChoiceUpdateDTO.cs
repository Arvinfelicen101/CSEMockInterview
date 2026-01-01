using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Choices
{
    public class ChoiceUpdateDTO
    {
        [Required(ErrorMessage = "Choice is required.")]
        public string ChoiceText { get; set; }

        [Required(ErrorMessage = "IsCorrect is required")]
        public bool IsCorrect { get; set; }

   
    }
}
