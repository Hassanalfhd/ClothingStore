using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface ISizeRepo
    {
        Task<Size?> GetByIdAsync(Guid id);

        Task<List<Size>> GetAllAsync();

        Task AddAsync(Size size);

        void Update(Size size);

        void Delete(Size size);

        Task<bool> ExistsAsync(Guid publicId);
    }
}
