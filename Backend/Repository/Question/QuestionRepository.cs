

using Backend.Context;
using Backend.DTOs.Choices;
using Backend.DTOs.Question;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.Question

{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MyDbContext _context;

    public QuestionRepository (MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SubCategoryExistsAsync(int id)
        {
            return await _context.SubCategory.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> YearPeriodExistsAsync(int id)
        {
            return await _context.YearPeriod.AnyAsync(y => y.Id == id);
        }
       

        public async Task<bool> ParagraphExistsAsync(int id)
        {
            return await _context.Paragraph.AnyAsync(p => p.Id == id);
        }

        public async Task AddQuestionAsync(Questions question)
        {
            await _context.AddAsync(question);
        }    
           

        public async Task<QuestionReadDTO?> GetQuestionByIdAsync(int id)
        {
            return await _context.Question
                .AsNoTracking()
                .Where(q => q.Id == id)
                .Select(q => new QuestionReadDTO
                {
                    Id = q.Id,
                    QuestionName = q.QuestionName,
                    SubCategoryId = q.SubCategoryId,
                    ParagraphId = q.ParagraphId,
                    YearPeriodId = q.YearPeriodId,
                    choices = q.ChoicesCollection.Select(c => new ChoiceReadDTO
                    {
                        Id = c.Id,
                        ChoiceText = c.ChoiceText,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<QuestionListDTO>> GetAllAsync()
        {
            return await _context.Question
                .AsNoTracking()
                .Select(q => new QuestionListDTO
                {
                   QuestionName = q.QuestionName,
                   SubCategoryId = q.SubCategoryId,
                   ParagraphId = q.ParagraphId,
                   YearPeriodId= q.YearPeriodId
                })
                .ToListAsync();
        }

        public async Task<Questions?> FindQuestionByIdAsync(int id)
        {
          return await _context.Question
                .Include(q => q.ChoicesCollection)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateQuestionAsync(Questions question)
        {
            _context.Question.Update(question);
           
        }

        public async Task DeleteQuestionAsync(Questions question)
        {
            _context.Question.Remove(question);

        }

       
    }
}
