using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Size.Dtos;
using ClothingStore.Domain.Common;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface ISizeService
    {
        Task<Result<List<SizeDto>>> GetAllAsync();

        Task<Result<SizeDto>> GetByIdAsync(Guid id);

        Task<Result<Guid>> CreateAsync(CreateSizeDto dto);

        Task<Result> UpdateAsync(Guid id, CreateSizeDto dto);

        Task<Result> DeleteAsync(Guid id);
    }
}
