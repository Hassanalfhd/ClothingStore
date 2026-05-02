using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Color.Dtos
{
    public class CreateColorDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Hex { get; set; }
    }
}
