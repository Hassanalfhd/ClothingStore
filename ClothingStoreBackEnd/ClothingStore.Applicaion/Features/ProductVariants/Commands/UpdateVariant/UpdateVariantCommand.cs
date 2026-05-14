using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.ProductVariants.Commands.UpdateVariant
{
    public record UpdateVariantCommand(
        Guid VaraintId,
        Guid ProductId,
        Guid ColorId,
        Guid SizeId,
        Guid CreatedBy,
        decimal Price, 
        string Currency,
        int StockQuantity,
        string SKU) :IRequest<Result>;
}
