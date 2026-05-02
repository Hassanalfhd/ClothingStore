using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Application.Interfaces.Repositories
{
    public interface IColorRepo
    {
        Task<Color?> GetByIdAsync(Guid publicId);

        Task<List<Color>> GetAllAsync();

        Task AddAsync(Color color);

        void Update(Color color);

        void Delete(Color color);

        Task<bool> ExistsAsync(Guid id);

    }
}
