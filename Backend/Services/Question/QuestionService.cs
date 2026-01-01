using Backend.DTOs.Question;
using Backend.Models;
using Backend.Repository.Question;
using Backend.Exceptions;
using Xunit.Sdk;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Caching.Memory;
using DocumentFormat.OpenXml.Wordprocessing;
using Backend.Services.Question.QuestionValidator;
using Backend.Context;

namespace Backend.Services.Question
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repo;
        private readonly IMemoryCache _cache;
        private readonly IQuestionValidator _validator;
        private readonly MyDbContext _context;

        public QuestionService(IQuestionRepository repo, IMemoryCache cache, IQuestionValidator validator, MyDbContext context)
        {
            _repo = repo;
            _cache = cache;
            _validator = validator;
            _context = context;
        }
        public async Task CreateQuestionAsync(QuestionCreateDTO question)
        {
            await _validator.ValidateReferencesAsync(
                question.SubCategoryId, 
                question.YearPeriodId,
                question.ParagraphId);

            if (question.Choices == null || question.Choices.Count < 2)
                throw new BadRequestException("A question must have at least 2 choices");

            if(!question.Choices.Any(c => c.IsCorrect))
                throw new BadRequestException("At least one choice must be correct");

            var questionInfo = new Questions
            {
                QuestionName = question.QuestionName,
                SubCategoryId = question.SubCategoryId,
                YearPeriodId = question.YearPeriodId,
                ParagraphId = question.ParagraphId,
            };

            foreach (var choice in question.Choices)
            {
                var choiceInfo = new ItemChoices
                {
                    ChoiceText = choice.ChoiceText,
                    IsCorrect = choice.IsCorrect,
                };

                questionInfo.ChoicesCollection.Add(choiceInfo);
            }

            await _repo.AddQuestionAsync(questionInfo);
            await _context.SaveChangesAsync();
            _cache.Remove(CacheKeys.QuestionsAll);
        }

        public async Task<QuestionReadDTO> GetQuestionByIdAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Invalid ID");

            var question = await _repo.GetQuestionByIdAsync(id);

            if (question == null)
                throw new NotFoundException($"Question with ID {id} not found");

            return question;
        }

        public async Task<List<QuestionListDTO>> GetAllAsync()
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
                question.SubCategoryId,
                question.YearPeriodId,
                question.ParagraphId);

            questionById.QuestionName = question.QuestionName;

            questionById.SubCategoryId = question.SubCategoryId;
         
            questionById.YearPeriodId = question.YearPeriodId;

            questionById.ParagraphId = question.ParagraphId;


            await _repo.UpdateQuestionAsync(questionById);
            await _context.SaveChangesAsync();
            _cache.Remove(CacheKeys.QuestionsAll);

        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _repo.FindQuestionByIdAsync(id);
            if (question == null) throw new NotFoundException("Question does not exist.");

            await _repo.DeleteQuestionAsync(question);
            await _context.SaveChangesAsync();
            _cache.Remove(CacheKeys.QuestionsAll);
        }

      
    }

    
}
