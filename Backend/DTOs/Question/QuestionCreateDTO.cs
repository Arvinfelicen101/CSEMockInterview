using Backend.DTOs.Choices;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Backend.DTOs.Question
{
    public class QuestionCreateDTO
    {

        [Required(ErrorMessage = "Question Name is required.")]
        public string QuestionName { get; set; }


        [Required(ErrorMessage = "subCategoryId is required.")]
        public int SubCategoryId { get; set; } // FK to subCategories

        public int? ParagraphId { get; set; } // FK to paragraphs

        
        [Required(ErrorMessage = "yearPeriodId is required.")]
        public int YearPeriodId { get; set; } // FK to yearPeriods

        [MinLength(2)]
        public List<ChoiceCreateDTO> Choices { get; set; }


    }

}
