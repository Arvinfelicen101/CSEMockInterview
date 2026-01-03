using Backend.DTOs.Question;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.SubCategory
{
    public class SubCategoryUpdateDTO
    {
        [Required(ErrorMessage = "SubCategoryName is required.")]
        public required string SubCategoryName { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public required int CategoryId { get; set; }

        [MinLength(1)]
        public required List<QuestionUpdateDTO> Questions { get; set; }
    }
}
