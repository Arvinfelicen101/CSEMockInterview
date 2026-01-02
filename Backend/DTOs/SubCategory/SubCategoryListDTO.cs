using Backend.DTOs.Question;

namespace Backend.DTOs.SubCategory
{
    public class SubCategoryListDTO
    {
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public List<QuestionListDTO> Questions { get; set; }
    }
}
