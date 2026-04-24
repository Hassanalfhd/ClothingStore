using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepo: IRefreshTokenRepo
    {

        private readonly ApplicationDbContext _context;

        public RefreshTokenRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string Token, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .Where(x => x.Token == Token)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public void Add(RefreshToken refreshToken) => _context.Add(refreshToken);


    }
}
