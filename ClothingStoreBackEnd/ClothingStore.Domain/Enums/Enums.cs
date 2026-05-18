using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Domain.Enums
{
    public enum ImageSizeType
    {
        Thumbnail,
        Medium,
        Original
    }

    public enum ProductStatus
    {
        Draft = 0,
        Published = 1,
        Archived = 2,
        OutOfStock = 3
    }
}
