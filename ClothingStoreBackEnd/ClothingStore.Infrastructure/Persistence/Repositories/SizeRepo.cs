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
    public class SizeRepo : ISizeRepo
    {
        private readonly ApplicationDbContext _context;

        public SizeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Size?> GetByIdAsync(Guid publicId)
            => await _context.Sizes.FirstOrDefaultAsync(x=> x.PublicId == publicId);

        public async Task<long?> GetIdAsync(Guid publicId, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
            return result.Id;
        }

        public async Task<List<Size>> GetAllAsync()
            => await _context.Sizes.AsNoTracking().ToListAsync();

        public async Task AddAsync(Size size)
            => await _context.Sizes.AddAsync(size);

        public void Update(Size size)
            => _context.Sizes.Update(size);

        public void Delete(Size size)
            => _context.Sizes.Remove(size);

        public async Task<bool> ExistsAsync(Guid publicId)
            => await _context.Sizes.AnyAsync(x => x.PublicId == publicId);
    }
}
