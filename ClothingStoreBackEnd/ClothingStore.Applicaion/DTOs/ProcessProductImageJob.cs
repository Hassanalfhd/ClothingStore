using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.DTOs
{
    public class ProcessProductImageJob
    {
        public long ProductImageId { get; set; }

        public string TempFilePath { get; set; } = default!;

        public string FileName { get; set; } = default!;
    }
}
