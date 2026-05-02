using ClothingStore.Application.Features.Catalog.Category.Dtos;
using ClothingStore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/categories")]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var result = await _service.CreateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateCategoryDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}
