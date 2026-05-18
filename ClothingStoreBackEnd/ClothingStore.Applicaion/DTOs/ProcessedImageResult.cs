using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Application.DTOs
{
    public class ProcessedImageResult
    {
        public string OriginalPath { get; set; } = default!;
        public string ThumbnailPath { get; set; } = default!;
        public string MediumPath { get; set; } = default!;
        public string WebpPath { get; set; } = default!;
    }
}
