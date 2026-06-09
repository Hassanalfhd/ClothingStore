using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Orders.Commands.PlaceOrder
{
    public sealed record PlaceOrderCommand(
      Guid UserId,
      string RecipientName,
      string PhoneNumber,
      string City,
      string AddressLine
  ) : IRequest<Result<Guid>>;
}
