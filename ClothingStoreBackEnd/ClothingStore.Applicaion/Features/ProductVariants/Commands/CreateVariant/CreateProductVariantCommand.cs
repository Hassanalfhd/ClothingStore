using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.ValueObjects;
using MediatR;

namespace ClothingStore.Application.Features.ProductVariants.Commands.CreateVariant
{
    public sealed record  CreateProductVariantCommand(Guid ProductId, Guid ColorId, Guid SizeId, Guid CreatedBy, decimal Price, string Currency,  int StockQuantity, string SKU) :IRequest<Result<Guid>>;
}
