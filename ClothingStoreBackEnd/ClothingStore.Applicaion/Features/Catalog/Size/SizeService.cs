using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Size.Dtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;

namespace ClothingStore.Application.Features.Catalog.Size
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepo  _repo;
        private readonly IUnitOfWork _unitOfWork;
        public SizeService(ISizeRepo repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<SizeDto>>> GetAllAsync()
        {
            var sizes = await _repo.GetAllAsync();

            var result = sizes.Select(s => new SizeDto
            {
                PublicId = s.PublicId,
                Name = s.Name,
                displayOrder= s.DisplayOrder
            }).ToList();

            return Result<List<SizeDto>>.Success(result);
        }

        public async Task<Result<SizeDto>> GetByIdAsync(Guid id)
        {
            var size = await _repo.GetByIdAsync(id);

            if (size == null)
                return Result<SizeDto>.Failure("Size not found");

            return Result<SizeDto>.Success(new SizeDto
            {
                PublicId = size.PublicId,
                Name = size.Name,
                displayOrder = size.DisplayOrder
            });
        }

        public async Task<Result<Guid>> CreateAsync(CreateSizeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return Result<Guid>.Failure("Size name is required");

            var size = new Domain.Entities.Size(dto.Name, dto.displayOrder);
            
            

            await _repo.AddAsync(size);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(size.PublicId);
        }

        public async Task<Result> UpdateAsync(Guid id, CreateSizeDto dto)
        {
            var size = await _repo.GetByIdAsync(id);

            if (size == null)
                return Result.Failure("Size not found");

            _repo.Update(size);

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var size = await _repo.GetByIdAsync(id);

            if (size == null)
                return Result.Failure("Size not found");

            _repo.Delete(size);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }

}
