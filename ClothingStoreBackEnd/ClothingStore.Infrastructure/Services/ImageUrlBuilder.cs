using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Domain.Enums;

namespace ClothingStore.Infrastructure.Services
{
    public class ImageUrlBuilder: IImageUrlBuilder
    {
        public string BuildProductImageUrl(
        Guid publicId,
        ImageSizeType size)
        {
            var folder = publicId.ToString();

            var fileName = size switch
            {
                ImageSizeType.Thumbnail => "thumb.webp",
                ImageSizeType.Medium => "medium.webp",
                _ => "original.webp"
            };



            return $"/images/products/{folder}/{fileName}";
        }


    }
}
