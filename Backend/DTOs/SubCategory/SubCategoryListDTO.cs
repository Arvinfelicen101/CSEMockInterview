using Backend.DTOs.Question;

namespace Backend.DTOs.SubCategory
{
    public class SubCategoryListDTO
    {
        public required string SubCategoryName { get; set; }
        public required int CategoryId { get; set; }
        public required List<QuestionListDTO> Questions { get; set; }
    }
}
