using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IUserRepo
    {
        public long Add(UserProfile userProfile);

    }
}
