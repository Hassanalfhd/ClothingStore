using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface IImageProcessingService
    {
        Task<string> ResizeAsync(string inputPath, int width, int height);

        Task<string> ConvertToWebPAsync(string inputPath);

        Task<string> CompressAsync(string inputPath);

        Task<ProcessedImageResult> ProcessAsync(
       string inputPath,
       string outputFolder,
       CancellationToken cancellationToken);
    }
}
