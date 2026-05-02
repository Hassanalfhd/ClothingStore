using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Category.Dtos;
using ClothingStore.Domain.Common;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<Result<List<CategoryDto>>> GetAllAsync();
        Task<Result<CategoryDto>?> GetByIdAsync(Guid id);
        Task<Result<Guid>> CreateAsync(CreateCategoryDto dto);
        Task<Result> UpdateAsync(Guid id, CreateCategoryDto dto);
        Task<Result> DeleteAsync(Guid id);
    }
}
