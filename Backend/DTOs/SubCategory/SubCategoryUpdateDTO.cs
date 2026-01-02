using Backend.DTOs.Question;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.SubCategory
{
    public class SubCategoryUpdateDTO
    {
        [Required(ErrorMessage = "SubCategoryName is required.")]
        public string SubCategoryName { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }

        [MinLength(1)]
        public List<QuestionUpdateDTO> Questions { get; set; }
    }
}
