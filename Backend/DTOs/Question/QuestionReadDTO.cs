using Backend.DTOs.Choices;

namespace Backend.DTOs.Question
{
    public class QuestionReadDTO
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public int SubCategoryId { get; set; }
        public int? ParagraphId { get; set; }
        public int YearPeriodId { get; set; }
        public List<ChoiceReadDTO> choices { get; set; }

    }
}
