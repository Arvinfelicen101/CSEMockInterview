using Backend.DTOs.Choices;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Question
{
    public class QuestionUpdateDTO
    {
        [Required(ErrorMessage = "Question Name is required.")]
        public string QuestionName {  get; set; }

        [Required(ErrorMessage = "subCategoryId is required.")]
        public int SubCategoryId { get; set; }
        public int? ParagraphId { get; set; }

        [Required(ErrorMessage = "yearPeriodId is required.")]
        public int YearPeriodId { get; set; }
    }
}
