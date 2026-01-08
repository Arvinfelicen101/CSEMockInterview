using Backend.DTOs.SubCategory;
using Backend.Services.SubCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.SubCategory
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        public readonly ISubCategoryService _service;

        public SubCategoriesController(ISubCategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddSubCategory(SubCategoryCreateDTO subcategoryDto)
        {
            await _service.CreateSubCategoryAsync(subcategoryDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllSubCategories()
        {
           var subcategories = await _service.GetAllAsync();
            return Ok(subcategories);                                                                                                                                                                                                                                           
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateSubCategory(int id,  SubCategoryUpdateDTO subcategoryDto)
        {
            await _service.UpdateSubCategoryAsync(id, subcategoryDto);
            return Ok(new { message = "SubCategory updated successfully" });
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            await _service.DeleteSubCategoryAsync(id);
            return NoContent();
        }
    }
}
