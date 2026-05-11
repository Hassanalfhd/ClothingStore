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
    public class BrandRepo : IBrandRepo
    {
        private readonly ApplicationDbContext _context;

        public BrandRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Brand?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
            => await _context.Brands.FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);

        public async Task<List<Brand>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Brands.AsNoTracking().ToListAsync(cancellationToken);


        public async Task<long?> GetIdAsync(Guid publicId, CancellationToken cancellationToken)
        {
            var result = await _context.Brands.AsNoTracking().Select(x => new { x.Id, x.PublicId }).FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
            return result.Id;
        }

        public async Task<bool> ExistsAsync(Guid publicId, CancellationToken cancellationToken)
            => await _context.Categories.AnyAsync(x => x.PublicId == publicId, cancellationToken);

        public async Task AddAsync(Brand category, CancellationToken cancellationToken)
            => await _context.Brands.AddAsync(category, cancellationToken);

        public void Update(Brand category)
            => _context.Brands.Update(category);


        public void Delete(Brand category)
            => _context.Brands.Remove(category);
    }
}
