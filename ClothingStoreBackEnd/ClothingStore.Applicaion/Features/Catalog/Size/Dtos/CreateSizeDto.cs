using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.Features.Catalog.Size.Dtos
{
    public class CreateSizeDto
    {
        public string Name { get; set; } = string.Empty;
        public int displayOrder { get; set; } 
    }
}
