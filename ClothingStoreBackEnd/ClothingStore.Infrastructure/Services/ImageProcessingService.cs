using System;
using ClothingStore.Application.Interfaces.Services;

namespace ClothingStore.Infrastructure.Services
{
    public class ImageProcessingService : IImageProcessingService
    {

        public async Task<string> ResizeAsync(string inputPath, int width, int height)
        {
            using var image = await Image.LoadAsync(inputPath);

            image.Mutate(x => x.Resize(width, height));

            var output = inputPath.Replace(".", "_resized.");

            await image.SaveAsync(output);

            return output;
        }

        public async Task<string> ConvertToWebPAsync(string inputPath)
        {
            using var image = await Image.LoadAsync(inputPath);

            var output = Path.ChangeExtension(inputPath, ".webp");

            await image.SaveAsWebpAsync(output);

            return output;
        }

        public async Task<string> CompressAsync(string inputPath)
        {
            using var image = await Image.LoadAsync(inputPath);

            var output = inputPath.Replace(".", "_compressed.");

            await image.SaveAsync(output);

            return output;
        }
    }
}
