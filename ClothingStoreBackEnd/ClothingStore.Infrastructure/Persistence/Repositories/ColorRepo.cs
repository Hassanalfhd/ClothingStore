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
    public class ColorRepo: IColorRepo
    {
        private readonly ApplicationDbContext _context;

        public ColorRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Color?> GetByIdAsync(Guid publicId)
            => await _context.Colors.FirstOrDefaultAsync(x=>x.PublicId == publicId);

        public async Task<List<Color>> GetAllAsync()
            => await _context.Colors.AsNoTracking().ToListAsync();

        public async Task<long?> GetIdAsync(Guid publicId, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
            return result.Id;
        }
        public async Task AddAsync(Color color)
            => await _context.Colors.AddAsync(color);

        public void Update(Color color)
            => _context.Colors.Update(color);

        public void Delete(Color color)
            => _context.Colors.Remove(color);

        public async Task<bool> ExistsAsync(Guid publicId)
            => await _context.Colors.AnyAsync(x => x.PublicId == publicId);
    }
}
