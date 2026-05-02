using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Category.Dtos
{
    public class CategoryDto
    {
        public Guid PublicID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
