using Microsoft.AspNetCore.Mvc;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Domain.Entities;

namespace TruckWorld.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentCategoryController : ControllerBase
    {
        private readonly IComponentCategoryService _componentCategoryService;

        public ComponentCategoryController(IComponentCategoryService componentCategoryService)
        {
            _componentCategoryService = componentCategoryService;
        }


        [HttpGet("cotegories")]
        public IActionResult GetAllComponentCategories([FromQuery] int pageToken, [FromQuery] int pageSize, IComponentCategoryService componentCategoryService)
        {
            var result = componentCategoryService.Get(componentcotegory => true).Skip((pageToken - 1) * pageSize).Take(pageSize).ToList();
            return result.Any() ? Ok(result) : NotFound();
        }


        [HttpGet("{categoryId:guid}/componentCategory")]
        public async ValueTask<IActionResult> GetCategoryById([FromRoute] Guid componentCategoryId)
        {
            var result = await _componentCategoryService.GetByIdAsync(componentCategoryId);
            return result is not null ? Ok(result) : NotFound();
        }


        [HttpPost]
        public async ValueTask<IActionResult> CreateComponentCategory([FromBody] ComponentCategory componentCategory)
        {
            var result = await _componentCategoryService.CreateAsync(componentCategory);
            return CreatedAtAction(nameof(GetCategoryById), new { categroyId = result.Id }, result);
        }
        

        [HttpPut]
        public async ValueTask<IActionResult> UpdateCategory([FromBody] ComponentCategory componentCategory)
        {
            var result = await _componentCategoryService.UpdateAsync(componentCategory);
            return NoContent();
        }
        

        [HttpDelete("{categoryId:guid}")]
        public async ValueTask<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            var result = await _componentCategoryService.DeleteAsync(categoryId);
            return NoContent();
        }
    }
}
