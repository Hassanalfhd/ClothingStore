using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IBrandRepo
    {
        Task<Brand?> GetByIdAsync(Guid publicId, CancellationToken cancellationToken = default);

        Task<List<Brand>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<long?> GetIdAsync(Guid PublicId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid publicId, CancellationToken cancellationToken = default);

        Task AddAsync(Brand category, CancellationToken cancellationToken = default);

        void Update(Brand category);

        void Delete(Brand category);

    }
}
