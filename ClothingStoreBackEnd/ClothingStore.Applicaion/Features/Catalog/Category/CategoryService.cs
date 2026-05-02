using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Category.Dtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Features.Catalog.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(ICategoryRepo categoryRepo, IUnitOfWork unitOfWork)
        {

            _categoryRepo = categoryRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();

            return Result<List<CategoryDto>>.Success(categories.Select(c => new CategoryDto
            {
                PublicID = c.PublicId,
                Name = c.Name,
                Description = c.Description,

            }).ToList());
        }

        public async Task<Result<CategoryDto>?> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
               return Result<CategoryDto>.Failure("Category is not found!");

            return Result<CategoryDto>.Success(new CategoryDto
            {
                PublicID = category.PublicId,
                Name = category.Name,
                Description = category.Description
            });
        }

        public async Task<Result<Guid>> CreateAsync(CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Category name is required");

            var category = new Domain.Entities.Category(dto.Name, dto.Description);

            await _categoryRepo.AddAsync(category);

            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(category.PublicId);
        }

        public async Task<Result> UpdateAsync(Guid id, CreateCategoryDto dto)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
                return Result.Failure("Error: somethings went wrong");
            _categoryRepo.Update(category);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }


        public async Task<Result> DeleteAsync(Guid id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
                return Result.Failure("Error: somethings went wrong");

            _categoryRepo.Delete(category);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
