using Backend.DTOs.Choices;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Backend.DTOs.Question
{
    public class QuestionCreateDTO
    {

        [Required(ErrorMessage = "Question Name is required.")]
        public string questionName { get; set; }


        [Required(ErrorMessage = "subCategoryId is required.")]
        public int subCategoryId { get; set; } // FK to subCategories

        public int? paragraphId { get; set; } // FK to paragraphs

        
        [Required(ErrorMessage = "yearPeriodId is required.")]
        public int yearPeriodId { get; set; } // FK to yearPeriods

        [MinLength(2)]
        public List<ChoiceDTO> choices { get; set; }


    }

}
