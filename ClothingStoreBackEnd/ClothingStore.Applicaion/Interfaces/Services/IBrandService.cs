using ClothingStore.Application.Features.Catalog.Brand.Dtos;
using ClothingStore.Domain.Common;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task<Result<List<BrandDto>>> GetAllAsync();

        Task<Result<BrandDto>> GetByIdAsync(Guid id);

        Task<Result<Guid>> CreateAsync(CreateBrandDto dto);

        Task<Result> UpdateAsync(Guid id, CreateBrandDto dto);

        Task<Result> DeleteAsync(Guid id);
    }
}
