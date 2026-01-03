using Backend.Context;
using Backend.DTOs.SubCategory;
using Backend.Repository.SubCategory;
using Backend.Exceptions;
using Backend.Models;
using Microsoft.Extensions.Caching.Memory;


namespace Backend.Services.SubCategory
{
    public class SubCategoryService : ISubCategoryService
    {
        public readonly MyDbContext _context;
        public readonly ISubCategoryRepository _repo;
        public readonly IMemoryCache _cache;

        public SubCategoryService(MyDbContext context, ISubCategoryRepository repo, IMemoryCache cache)
        {
            _context = context;
            _repo = repo;
            _cache = cache;
        }

        // Create
        public async Task CreateSubCategoryAsync(SubCategoryCreateDTO dto)
        {
            if (!await _repo.CategoryExistAsync(dto.CategoryId))
                throw new NotFoundException("Category does not exist");

            if (await _repo.SubCategoryExistsAsync(dto.CategoryId, dto.SubCategoryName))
                throw new BadRequestException("SubCategory already exists in this category");

            var subCategory = new SubCategories
            {
                SubCategoryName = dto.SubCategoryName,
                CategoryId = dto.CategoryId,

            };

            if (dto.Questions != null)
            {

                foreach (var question in dto.Questions)
                {
                    var questionInfo = new Questions
                    {
                        QuestionName = question.QuestionName,
                        SubCategoryId = question.SubCategoryId,
                        YearPeriodId = question.YearPeriodId,
                        ParagraphId = question.ParagraphId
                    };
                    subCategory.QuestionsCollection.Add(questionInfo);
                }
            }

            await _repo.CreateSubCategoryAsync(subCategory);
            _cache.Remove(CacheKeys.SubCategoryAll);
        }

        // Read
        public async Task<List<SubCategories>> GetAllAsync()
        {
            if (_cache.TryGetValue(CacheKeys.SubCategoryAll, out List<SubCategories>? cached))
                return cached!;

            var result = await _repo.GetAllAsync();

            _cache.Set(CacheKeys.SubCategoryAll, result);

            return result;
        }

        public async Task UpdateSubCategoryAsync(int id, SubCategoryUpdateDTO dto)
        {
            var question = await _repo.FindByIdAsync(id);
            if (question == null) throw new Exception("SubCategory does not exist");

            if (!await _repo.CategoryExistAsync(dto.CategoryId))
                throw new NotFoundException("Category does not exist");

            if (await _repo.SubCategoryExistsAsync(dto.CategoryId, dto.SubCategoryName))
                throw new BadRequestException("SubCategory already exists in this category");

            question.SubCategoryName = dto.SubCategoryName;
            question.CategoryId = dto.CategoryId;

            await _repo.UpdataSubCategoryAsync(question);
            await _context.SaveChangesAsync();
            _cache.Remove(CacheKeys.SubCategoryAll);
        }

        public async Task DeleteSubCategoryAsync(int id)
        {
           var question = await _repo.FindByIdAsync(id);
            if (question == null) throw new Exception("SubCategory does not exist");

            await _repo.DeleteSubCategoryAsync(question);
            
        }

      
    }
}