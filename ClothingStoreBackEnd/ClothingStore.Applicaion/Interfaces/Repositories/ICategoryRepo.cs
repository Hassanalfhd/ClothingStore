using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface ICategoryRepo
    {
        Task<Category?> GetByIdAsync(Guid publicId);

        Task<List<Category>> GetAllAsync();

        Task<bool> ExistsAsync(Guid PublicId);

        Task AddAsync(Category category);

        void Update(Category category);

        void Delete(Category category);


    }
}
