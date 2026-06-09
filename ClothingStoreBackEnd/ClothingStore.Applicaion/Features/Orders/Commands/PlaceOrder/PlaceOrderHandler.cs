using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using MediatR;

namespace ClothingStore.Application.Features.Orders.Commands.PlaceOrder
{
    public sealed class PlaceOrderHandler
     : IRequestHandler<
         PlaceOrderCommand,
         Result<Guid>>
    {
        private readonly ICartRepo _cartRepository;
        private readonly IOrderRepo _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepo _userRepo;

        public PlaceOrderHandler(
            ICartRepo cartRepository,
            IOrderRepo orderRepository,
            IUnitOfWork unitOfWork,
            IUserRepo userRepo)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _userRepo = userRepo;
        }

        public async Task<Result<Guid>> Handle(
            PlaceOrderCommand request,
            CancellationToken ct)
        {
            var userId = await _userRepo.GetIdAsync(request.UserId, ct);
            if (userId is null)
                return Result<Guid>.Failure(
                    "User not found");

            var cart =
                await _cartRepository.GetByUserIdAsync(
                    userId.Value,
                    ct);

            if (cart is null)
                return Result<Guid>.Failure(
                    "Cart not found");

            if (!cart.Items.Any())
                return Result<Guid>.Failure(
                    "Cart is empty");

            var order = new  Order(
                userId.Value,
                request.RecipientName,
                request.PhoneNumber,
                request.City,
                request.AddressLine);

            foreach (var item in cart.Items)
            {
                order.AddItem(
                    item.ProductId,
                    item.VariantId,
                    item.ProductName,
                    item.UnitPrice.Amount,
                    item.Quantity);
            }

            order.Validate();

            await _orderRepository.AddAsync(
                order,
                ct);

            cart.Clear();

            await _unitOfWork.SaveChangesAsync(ct);

            order.SetOrderNumber(
                $"ORD-{order.Id:D6}");

            await _unitOfWork.SaveChangesAsync(ct);

            return Result<Guid>.Success(
                order.PublicId);
        }
    }
}
