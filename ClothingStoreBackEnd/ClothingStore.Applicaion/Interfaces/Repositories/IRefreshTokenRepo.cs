
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepo
    {

        Task<RefreshToken?> GetRefreshTokenByTokenAsync(string Token, CancellationToken cancellationToken = default);
        void Add(RefreshToken refreshToken);

    }
}
