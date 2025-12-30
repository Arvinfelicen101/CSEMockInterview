using Backend.DTOs.Question;
using Backend.Models;
using Backend.Repository.Question;
using Backend.Exceptions;
using Xunit.Sdk;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Caching.Memory;
using DocumentFormat.OpenXml.Wordprocessing;
using Backend.Services.Question.QuestionValidator;

namespace Backend.Services.Question
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repo;
        private readonly IMemoryCache _cache;
        private readonly IQuestionValidator _validator;

        public QuestionService(IQuestionRepository repo, IMemoryCache cache, IQuestionValidator validator)
        {
            _repo = repo;
            _cache = cache;
            _validator = validator;
        }
        public async Task CreateQuestionService(QuestionCreateDTO question)
        {
            await _validator.ValidateReferencesAsync(
                question.subCategoryId, 
                question.yearPeriodId,
                question.paragraphId);

            if (question.choices == null || question.choices.Count < 2)
                throw new BadRequestException("A question must have at least 2 choices");

            if(!question.choices.Any(c => c.isCorrect))
                throw new BadRequestException("At least one choice must be correct");

            var questionInfo = new Questions
            {
                QuestionName = question.questionName,
                SubCategoryId = question.subCategoryId,
                YearPeriodId = question.yearPeriodId,
                ParagraphId = question.paragraphId,
            };

            foreach (var choice in question.choices)
            {
                var choiceInfo = new ItemChoices
                {
                    ChoiceText = choice.choiceText,
                    IsCorrect = choice.isCorrect,
                };

                questionInfo.ChoicesCollection.Add(choiceInfo);
            }

            await _repo.AddQuestionAsync(questionInfo);
            _cache.Remove(CacheKeys.QuestionsAll);
        }

        public async Task<QuestionReadDTO> GetQuestionByIdService(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Invalid ID");

            var question = await _repo.GetQuestionByIdAsync(id);

            if (question == null)
                throw new NotFoundException($"Question with ID {id} not found");

            return question;
        }

        public async Task<List<QuestionListDTO>> GetAllService()
        {
            if (_cache.TryGetValue(CacheKeys.QuestionsAll, out List<QuestionListDTO> cached))
                return cached;

            var result = await _repo.GetAllAsync();

            _cache.Set(
                CacheKeys.QuestionsAll,
                result,
                TimeSpan.FromMinutes(10)
                );

            return result;      
        }

        public async Task UpdateQuestionAsync(int id, QuestionUpdateDTO question)
        {
            var questionById = await _repo.FindQuestionByIdAsync(id);
            if (questionById == null)
                throw new NotFoundException("Question does not exist.");

            await _validator.ValidateReferencesAsync(
                question.subCategoryId,
                question.yearPeriodId,
                question.paragraphId);

            questionById.QuestionName = question.questionName;

            questionById.SubCategoryId = question.subCategoryId;
         
            questionById.YearPeriodId = question.yearPeriodId;

            questionById.ParagraphId = question.paragraphId;


            await _repo.UpdateQuestionAsync(questionById);
            _cache.Remove(CacheKeys.QuestionsAll);

        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _repo.FindQuestionByIdAsync(id);
            if (question == null) throw new NotFoundException("Question does not exist.");

            await _repo.DeleteQuestionAsync(question);
            _cache.Remove(CacheKeys.QuestionsAll);
        }

      
    }

    
}
