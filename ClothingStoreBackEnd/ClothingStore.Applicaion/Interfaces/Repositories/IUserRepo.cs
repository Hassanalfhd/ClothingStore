using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IUserRepo
    {
        public long Add(UserProfile userProfile);

        Task<long?> GetIdAsync(Guid PublicId, CancellationToken cancellationToken = default);

    }
}
