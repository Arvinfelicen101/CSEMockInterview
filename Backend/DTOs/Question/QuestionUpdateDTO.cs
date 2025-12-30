using Backend.DTOs.Choices;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Question
{
    public class QuestionUpdateDTO
    {
        [Required(ErrorMessage = "Question Name is required.")]
        public string questionName {  get; set; }

        [Required(ErrorMessage = "subCategoryId is required.")]
        public int subCategoryId { get; set; }
        public int? paragraphId { get; set; }

        [Required(ErrorMessage = "yearPeriodId is required.")]
        public int yearPeriodId { get; set; }
        public List<ChoiceDTO> choices { get; set; }
    }
}
