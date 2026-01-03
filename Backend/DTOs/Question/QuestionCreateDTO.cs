using Backend.DTOs.Choices;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Backend.DTOs.Question
{
    public class QuestionCreateDTO
    {

        [Required(ErrorMessage = "Question Name is required.")]
        public required string QuestionName { get; set; }


        [Required(ErrorMessage = "subCategoryId is required.")]
        public required int SubCategoryId { get; set; } // FK to subCategories

        public int? ParagraphId { get; set; } // FK to paragraphs

        
        [Required(ErrorMessage = "yearPeriodId is required.")]
        public required int YearPeriodId { get; set; } // FK to yearPeriods

        [MinLength(2)]
        public required List<ChoiceCreateDTO> Choices { get; set; }


    }

}
