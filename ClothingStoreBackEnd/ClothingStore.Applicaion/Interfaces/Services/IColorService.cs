using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Color.Dtos;
using ClothingStore.Domain.Common;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface IColorService
    {
        Task<Result<List<ColorDto>>> GetAllAsync();

        Task<Result<ColorDto>> GetByIdAsync(Guid id);

        Task<Result<Guid>> CreateAsync(CreateColorDto dto);

        Task<Result> UpdateAsync(Guid id, CreateColorDto dto);

        Task<Result> DeleteAsync(Guid id);
    }
}
