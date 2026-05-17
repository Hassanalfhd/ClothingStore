namespace ClothingStore.Application.Features.ProductImages.Commands.ReorderImages;

public class ReorderProductImageItemDto
{
    public Guid ProductImageId { get; set; }
    public int DisplayOrder { get; set; }
}