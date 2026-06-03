using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.ValueObjects;

namespace ClothingStore.Application.DTOs
{
    public class ProductVariantCartDto
    {
        public long Id { get; set; }

        public Guid PublicId { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int StockQuantity{get;set;}
    }
}
