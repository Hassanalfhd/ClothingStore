using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Color.Dtos
{
    public class ColorDto
    {
        public Guid PublicId { get; set; }
        public string? Hex { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
