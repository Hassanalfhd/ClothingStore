using ClothingStore.Domain.Enums;

namespace ClothingStore.Application.Interfaces.Services;

public interface IImageUrlBuilder
{
    string BuildProductImageUrl(
        Guid publicId,
        ImageSizeType size);
}