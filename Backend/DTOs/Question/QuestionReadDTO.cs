using Backend.DTOs.Choices;

namespace Backend.DTOs.Question
{
    public class QuestionReadDTO
    {
        public int Id { get; set; }
        public string questionName { get; set; }
        public int subCategoryId { get; set; }
        public int? paragraphId { get; set; }
        public int yearPeriodId { get; set; }
        public List<ChoiceReadDTO> choices { get; set; }

    }
}
