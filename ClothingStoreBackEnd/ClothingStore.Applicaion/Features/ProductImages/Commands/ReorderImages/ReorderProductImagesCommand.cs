using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.ProductImages.Commands.ReorderImages;

public class ReorderProductImagesCommand
    : IRequest<Result<bool>>
{
    public List<ReorderProductImageItemDto> Images { get; set; }
        = [];
}