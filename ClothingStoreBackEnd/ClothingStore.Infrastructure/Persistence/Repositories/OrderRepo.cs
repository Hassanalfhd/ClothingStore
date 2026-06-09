using System;
using System.Collections.Generic;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public sealed class OrderRepo: IOrderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderRepo(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            Order order,
            CancellationToken ct = default)
        {
            await _context.Orders.AddAsync(order, ct);
        }

        public async Task<Order?> GetByPublicIdAsync(
            Guid publicId,
            CancellationToken ct = default)
        {
            return await _context.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.PublicId == publicId,
                    ct);
        }

        public async Task<IReadOnlyList<Order>>
            GetByUserIdAsync(
            long userId,
            CancellationToken ct = default)
        {
            return await _context.Orders
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(ct);
        }
    }
}
