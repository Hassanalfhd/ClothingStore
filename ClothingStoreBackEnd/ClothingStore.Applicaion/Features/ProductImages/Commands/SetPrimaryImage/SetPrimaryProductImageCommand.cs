using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.ProductImages.Commands.SetPrimaryImage;

public class SetPrimaryProductImageCommand
    : IRequest<Result<Guid>>
{
    public Guid ProductImageId { get; set; }
}