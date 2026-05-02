using ClothingStore.Application.Features.Catalog.Color.Dtos;
using ClothingStore.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/colors")]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _service;

        public ColorController(IColorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateColorDto dto)
        {
            var result = await _service.CreateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateColorDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok();
        }
    }
}
