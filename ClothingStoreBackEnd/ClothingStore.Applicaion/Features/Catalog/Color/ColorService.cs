using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Color.Dtos;
using ClothingStore.Application.Features.Catalog.Size.Dtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;

namespace ClothingStore.Application.Features.Catalog.Color
{
    public class ColorService : IColorService
    {
        private readonly IColorRepo _repo;
        private readonly IUnitOfWork _unitOfWork;
        public ColorService(IColorRepo repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ColorDto>>> GetAllAsync()
        {
            var colors = await _repo.GetAllAsync();

            var result = colors.Select(c => new ColorDto
            {
                PublicId = c.PublicId,
                Name = c.Name,
                 Hex = c.HexCode
            }).ToList();

            return Result<List<ColorDto>>.Success(result);
        }

        public async Task<Result<ColorDto>> GetByIdAsync(Guid id)
        {
            var color = await _repo.GetByIdAsync(id);

            if (color == null)
                return Result<ColorDto>.Failure("Color not found");

            return Result<ColorDto>.Success(new ColorDto
            {
                PublicId = color.PublicId,
                Name = color.Name,
                Hex = color.HexCode
            });
        }

        public async Task<Result<Guid>> CreateAsync(CreateColorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return Result<Guid>.Failure("Color name is required");

            var color = new Domain.Entities.Color
            (
                dto.Name.Trim(),
                 dto.Hex?.Trim()
            );

            await _repo.AddAsync(color);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(color.PublicId);
        }

        public async Task<Result> UpdateAsync(Guid id, CreateColorDto dto)
        {
            var color = await _repo.GetByIdAsync(id);

            if (color == null)
                return Result.Failure("Color not found");

            _repo.Update(color);
            await   _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var color = await _repo.GetByIdAsync(id);

            if (color == null)
                return Result.Failure("Color not found");

            _repo.Delete(color);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }

}
