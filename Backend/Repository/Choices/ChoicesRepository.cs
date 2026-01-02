using Backend.Context;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Backend.Repository.Choices
{
    public class ChoicesRepository : IChoicesRepository
    {
        public readonly MyDbContext _context;

        public ChoicesRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountChoicesByQuestionIdAsync(int questionId)
        {
           return await _context.Choice.CountAsync(c => c.QuestionId == questionId);
        }

        public async Task<bool> HasAnotherCorrectChoiceAsync(int questionId, int excludingChoiceId)
        {
           return await _context.Choice.AnyAsync(c =>
           c.QuestionId == questionId &&
           c.Id != excludingChoiceId &&
           c.IsCorrect);
        }

        public async Task CreateChoicesAsync(ItemChoices choice)
        {
            await _context.AddAsync(choice);
            
        }

        public async Task <ItemChoices?> FindChoiceById(int id)
        {
            return await _context.Choice
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        

        public async Task<bool> QuestionExistAsync(int id)
        {
            //var questionExist = await _context.Question.FirstAsync(c => c.Id == id);
            //return true;
            return await _context.Question.AnyAsync(c => c.Id == id);
        }

        public async Task UpdateChoiceAsync(ItemChoices choice)
        {
           _context.Choice.Update(choice);
                }

        public async Task DeleteChoiceAsync(ItemChoices choice)
        {
           _context.Choice.Remove(choice);
        
        }
    }
}
