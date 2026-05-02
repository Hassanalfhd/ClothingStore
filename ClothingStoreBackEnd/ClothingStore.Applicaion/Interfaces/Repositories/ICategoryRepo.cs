using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface ICategoryRepo
    {
        Task<Category?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken = default);

        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<long?> GetIdAsync(Guid PublicId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid publicId, CancellationToken cancellationToken = default);

        Task AddAsync(Category category, CancellationToken cancellationToken = default);

        void Update(Category category);

        void Delete(Category category);


    }
}
