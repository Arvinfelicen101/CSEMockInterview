namespace Backend.DTOs.Question
{
    public class QuestionListDTO
    {
        public required string QuestionName {  get; set; }
        public required int SubCategoryId { get; set; }
        public int? ParagraphId { get; set; }
        public int YearPeriodId { get; set; }
  
    }
}
