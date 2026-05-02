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

        public async Task<Category?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken )
            => await _context.Categories.FirstOrDefaultAsync(x=>x.PublicId == publicId, cancellationToken);

        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);


        public async Task<long?> GetIdAsync(Guid publicId, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.AsNoTracking().Select(x=> new {x.Id, x.PublicId}).FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
            return result.Id;
        }

        public async Task<bool> ExistsAsync(Guid publicId, CancellationToken cancellationToken )
            => await _context.Categories.AnyAsync(x => x.PublicId== publicId, cancellationToken);

        public async Task AddAsync(Category category, CancellationToken cancellationToken )
            => await _context.Categories.AddAsync(category, cancellationToken);

        public void Update(Category category)
            => _context.Categories.Update(category);


        public void Delete(Category category)
            => _context.Categories.Remove(category);
    }
}
