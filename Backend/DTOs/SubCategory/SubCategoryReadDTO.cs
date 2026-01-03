using Backend.DTOs.Question;

namespace Backend.DTOs.SubCategory
{
    public class SubCategoryReadDTO
    {
        public required string SubCategoryName { get; set; }
        public required string CategoryId { get; set; }
        public required List<QuestionReadDTO> Questions { get; set; }
    }
}
