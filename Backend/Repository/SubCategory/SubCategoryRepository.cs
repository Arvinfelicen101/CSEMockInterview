using Backend.Context;
using Backend.DTOs.SubCategory;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Backend.Repository.SubCategory
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        public readonly MyDbContext _context;

        public SubCategoryRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExistAsync(int id)
        {
            return await _context.Category.AnyAsync(c => c.Id == id);

        }

        public async Task<bool> SubCategoryExistsAsync(int categoryId, string name)
        {
            return await _context.SubCategory.AnyAsync(sc => 
            sc.CategoryId == categoryId &&
            sc.SubCategoryName == name);
        }

        public async Task CreateSubCategoryAsync(SubCategories subCategory)
        {
            await _context.AddAsync(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SubCategories>> GetAllAsync()
        {
            return await _context.SubCategory
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<SubCategories?> FindByIdAsync(int id)
        {
           return await _context.SubCategory
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdataSubCategoryAsync(SubCategories subCategory)
        {
            _context.SubCategory.Update(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubCategoryAsync(SubCategories subCategory)
        {
            _context.SubCategory.Remove(subCategory);
            await _context.SaveChangesAsync();
        }

        
    }

    }

