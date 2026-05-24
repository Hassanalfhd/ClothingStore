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
        Active = 1,
        Archived = 2,
        OutOfStock = 3
    }

    public enum Processed
    {
        Pending = 0,
        Processing = 1,
        Completed = 2,
        Failed = 3
    }

    public enum ProductSortBy
    {
        Newest = 0,
        Oldest = 1,

        PriceAsc = 2,
        PriceDesc = 3,

        NameAsc = 4,
        NameDesc = 5
    }


}
