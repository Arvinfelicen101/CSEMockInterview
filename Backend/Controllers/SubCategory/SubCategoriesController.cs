using Backend.DTOs.SubCategory;
using Backend.Repository.SubCategory;
using Backend.Services.SubCategory;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> AddSubCategory(SubCategoryCreateDTO subcategoryDto)
        {
            await _service.CreateSubCategoryAsync(subcategoryDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {
           var subcategories = await _service.GetAllAsync();
            return Ok(subcategories);                                                                                                                                                                                                                                           
        }

    }
}
