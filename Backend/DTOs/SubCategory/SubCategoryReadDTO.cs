using Backend.DTOs.Question;

namespace Backend.DTOs.SubCategory
{
    public class SubCategoryReadDTO
    {
        public string SubCategoryName { get; set; }
        public string CategoryId { get; set; }
        public List<QuestionReadDTO> Questions { get; set; }
    }
}
