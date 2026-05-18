using ClothingStore.Application.Common.Constants;
using ClothingStore.Application.DTOs;
using ClothingStore.Application.Interfaces.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;


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

        
        public async Task<ProcessedImageResult> ProcessAsync(
      string inputPath,
      string outputFolder,
      CancellationToken cancellationToken)
        {
            Directory.CreateDirectory(outputFolder);

            using var image = await Image.LoadAsync(inputPath, cancellationToken);

            var fileName = Path.GetFileNameWithoutExtension(inputPath);

            // 1. ORIGINAL (copy)
            var originalPath = Path.Combine(outputFolder, $"{fileName}_original.webp");
            await image.SaveAsWebpAsync(originalPath);

            // 2. THUMBNAIL
            var thumb = image.Clone(x =>
                x.Resize(new ResizeOptions
                {
                    Size = new Size(ImageSizes.Thumbnail, ImageSizes.Thumbnail),
                    Mode = ResizeMode.Crop
                }));

            var thumbnailPath = Path.Combine(outputFolder, $"{fileName}_thumb.webp");
            await thumb.SaveAsWebpAsync(thumbnailPath);

            // 3. MEDIUM
            var medium = image.Clone(x =>
                x.Resize(new ResizeOptions
                {
                    Size = new Size(ImageSizes.Medium, ImageSizes.Medium),
                    Mode = ResizeMode.Max
                }));

            var mediumPath = Path.Combine(outputFolder, $"{fileName}_medium.webp");
            await medium.SaveAsWebpAsync(mediumPath);

            // 4. WEBP HIGH QUALITY
            var webpPath = Path.Combine(outputFolder, $"{fileName}.webp");
            await image.SaveAsWebpAsync(webpPath);

            return new ProcessedImageResult
            {
                OriginalPath = originalPath,
                ThumbnailPath = thumbnailPath,
                MediumPath = mediumPath,
                WebpPath = webpPath
            };

        }
    }
}
