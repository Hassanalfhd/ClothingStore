using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(Guid publicId)
            => await _context.Categories.FirstOrDefaultAsync(x=>x.PublicId == publicId);

        public async Task<List<Category>> GetAllAsync()
            => await _context.Categories.AsNoTracking().ToListAsync();


        public async Task<bool> ExistsAsync(Guid PublicId)
            => await _context.Categories.AnyAsync(x => x.PublicId== PublicId);

        public async Task AddAsync(Category category)
            => await _context.Categories.AddAsync(category);

        public void Update(Category category)
            => _context.Categories.Update(category);


        public void Delete(Category category)
            => _context.Categories.Remove(category);
    }
}
