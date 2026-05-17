using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.ProductImages.Commands.DeleteImage;

public class DeleteProductImageCommand
    : IRequest<Result<Guid>>
{
    public Guid ProductImageId { get; set; }
}