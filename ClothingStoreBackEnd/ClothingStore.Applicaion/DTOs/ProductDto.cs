using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string ProductName{ get; set; } = string.Empty;
    }
}
