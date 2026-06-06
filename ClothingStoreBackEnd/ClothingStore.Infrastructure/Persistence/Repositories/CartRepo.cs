using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Catalog.Cart.Dtos;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class CartRepo: ICartRepo
    {
        private readonly ApplicationDbContext _context;

        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            return await _context.Carts
                .AsSplitQuery()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    !x.IsCheckedOut,
                    cancellationToken);
        }

        public async Task<Cart?> GetByPublicIdAsync(Guid publicId, CancellationToken cancellationToken)
        {
            return await _context.Carts
                .AsSplitQuery()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x =>
                    x.PublicId == publicId &&
                    !x.IsCheckedOut,
                    cancellationToken);
        }

        public async Task AddAsync(Cart cart, CancellationToken cancellationToken)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
        }


        public async Task<bool> UserHasActiveCartAsync(long userId, CancellationToken cancellationToken)
        {
            return await _context.Carts
                .AnyAsync(x =>
                    x.UserId == userId &&
                    !x.IsCheckedOut,
                    cancellationToken);
        }


       

        public async Task<CartDto?> GetCartAsync(long userId, CancellationToken cancellationToken)
        {
            return await _context.Carts
        .Where(c => c.UserId == userId)
        .Select(c => new CartDto
        {
            CartId = c.PublicId,


            TotalItems = c.Items.Sum(i => i.Quantity),

            SubTotal = c.Items
            .Sum(i=>i.Quantity * i.UnitPrice.Amount),

            Items = c.Items.Select(i => new CartItemDto
            {
                CartItemPublicId = i.PublicId,
                VariantPublicId = i.VariantPublicId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice.Amount,
                Currency = i.UnitPrice.Currency,
                Quantity = i.Quantity,
                LineTotal = i.Quantity * i.UnitPrice.Amount
            }).ToList()
        })
        .FirstOrDefaultAsync(cancellationToken);
        }


    }
}
