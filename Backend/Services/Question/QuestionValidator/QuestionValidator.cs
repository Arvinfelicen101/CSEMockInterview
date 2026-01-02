
using Backend.Exceptions;
using Backend.Models;
using Backend.Repository.Question;

namespace Backend.Services.Question.QuestionValidator
{
    public class QuestionValidator : IQuestionValidator
    {
        private readonly IQuestionRepository _repo;

        public QuestionValidator(IQuestionRepository repo)
        {
            _repo = repo;
        }
        public async Task ValidateReferencesAsync(int subCategoryId, int yearPeriodId, int? paragraphId)
        {
            if (!await _repo.SubCategoryExistsAsync(subCategoryId))
                throw new NotFoundException("SubCategory not found");

            if (!await _repo.YearPeriodExistsAsync(yearPeriodId))
                throw new NotFoundException("Year Period not found");

            if (paragraphId.HasValue && !await _repo.ParagraphExistsAsync(paragraphId.Value))
                throw new NotFoundException("Paragraph not found");
        }
    }
}
