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

        public async Task<Size?> GetByIdAsync(Guid id)
            => await _context.Sizes.FindAsync(id);

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
