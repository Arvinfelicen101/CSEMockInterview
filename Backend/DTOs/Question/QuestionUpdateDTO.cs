using Backend.DTOs.Choices;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Question
{
    public class QuestionUpdateDTO
    {
        [Required(ErrorMessage = "Question Name is required.")]
        public required string QuestionName {  get; set; }

        [Required(ErrorMessage = "subCategoryId is required.")]
        public required int SubCategoryId { get; set; }
        public int? ParagraphId { get; set; }

        [Required(ErrorMessage = "yearPeriodId is required.")]
        public required int YearPeriodId { get; set; }
    }
}
