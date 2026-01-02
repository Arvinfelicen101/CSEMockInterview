using Backend.DTOs.Question;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.SubCategory
{
    public class SubCategoryCreateDTO
    {
        [Required(ErrorMessage = "SubCategoryName is required.")]
        public string SubCategoryName { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }

        [MinLength(1)]
        public List<QuestionCreateDTO> Questions { get; set; }
    }
}
