using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Brand.Dtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Features.Catalog.Brand
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepo  _repo;
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IBrandRepo repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<BrandDto>>> GetAllAsync()
        {
            var brands = await _repo.GetAllAsync();

            var result = brands.Select(brand=> new BrandDto
            {
                PublicId = brand.PublicId,
                Name = brand.Name,
                Slug = brand.Slug,
                LogoUrl = brand.LogoUrl,
                Description = brand.Description,
            }).ToList();

            return Result<List<BrandDto>>.Success(result);
        }

        public async Task<Result<BrandDto>> GetByIdAsync(Guid id)
        {
            var brand = await _repo.GetByIdAsync(id);

            if (brand == null)
                return Result<BrandDto>.Failure("Brand not found");

            return Result<BrandDto>.Success(new BrandDto
            {
                PublicId = brand.PublicId,
                Name = brand.Name,
                Slug = brand.Slug,
                LogoUrl = brand.LogoUrl,
                Description = brand.Description,
            });
        }

        public async Task<Result<Guid>> CreateAsync(CreateBrandDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return Result<Guid>.Failure("brand name is required");

            var brand = new Domain.Entities.Brand
            (dto.Name, dto.Slug, dto.Description, dto.LogoUrl);
            
            await _repo.AddAsync(brand);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(brand.PublicId);
        }

        public async Task<Result> UpdateAsync(Guid id, CreateBrandDto dto)
        {
            var brand = await _repo.GetByIdAsync(id);

            if (brand == null)
                return Result.Failure("Brand not found");

            var newBrand = new Domain.Entities.Brand
            (dto.Name, dto.Slug, dto.Description, dto.LogoUrl);

            brand.Update(newBrand);

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var brand = await _repo.GetByIdAsync(id);

            if (brand == null)
                return Result.Failure("Brand not found");

            _repo.Delete(brand);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }

}
