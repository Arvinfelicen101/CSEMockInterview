namespace Backend.Services.Question.QuestionValidator
{
    public interface IQuestionValidator
    {
        Task ValidateReferencesAsync(int subCategoryId, int yearPeriodId, int? paragraphId);
    }
}
