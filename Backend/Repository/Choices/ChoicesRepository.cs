using Backend.Context;
using Backend.Models;

namespace Backend.Repository.Choices
{
    public class ChoicesRepository : IChoicesRepository
    {
        public readonly MyDbContext _context;

        public ChoicesRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task CreateChoicesAsync(ItemChoices choices)
        {
            await _context.AddAsync(choices);
            await _context.SaveChangesAsync();
        }
    }
}
