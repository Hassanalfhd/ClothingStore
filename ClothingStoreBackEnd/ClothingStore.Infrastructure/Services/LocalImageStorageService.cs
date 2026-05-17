using ClothingStore.Application.DTOs;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ClothingStore.Infrastructure.Services
{
    public class LocalImageStorageService : IImageStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly FoldersSettings _folderSettings;
        public LocalImageStorageService(IWebHostEnvironment environment, IOptions<FoldersSettings> options)
        {
            _environment = environment;
            _folderSettings = options.Value;
        }

        public async Task<TempImageResult> SaveTempAsync(
      IFormFile file,
      CancellationToken cancellationToken = default)
        {

            var extension = Path.GetExtension(file.FileName);

            var fileName = $"{Guid.NewGuid()}{extension}";

            try
            {
                var tempFolder = Path.Combine(
                 _environment.WebRootPath,
                 _folderSettings.RootFolder,
                 _folderSettings.TempFolder);
            
            Directory.CreateDirectory(tempFolder);

            var fullPath = Path.Combine(tempFolder, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(stream, cancellationToken);

            return new TempImageResult
            {
                FileName = fileName,
                TempFilePath = fullPath,
            };
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public async Task<string> MoveToPermanentAsync(
        string tempFilePath,
        string fileName,
        CancellationToken cancellationToken = default)
        {
            var permanentFolder = Path.Combine(
               _environment.WebRootPath,
               _folderSettings.RootFolder,
               _folderSettings.ProductsFolder);

            Directory.CreateDirectory(permanentFolder);

            var destinationPath = Path.Combine(permanentFolder, fileName);


            File.Move(tempFilePath, destinationPath);


            var relativePath = Path.Combine(_folderSettings.RootFolder, _folderSettings.ProductsFolder, fileName).Replace("\\", "/");


            await Task.CompletedTask;
            return relativePath;

        }

        public async Task DeleteAsync(
        string relativePath,
        CancellationToken cancellationToken = default)
        {
            var fullPath = Path.Combine(
                _environment.WebRootPath,
                relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            await Task.CompletedTask;
        }


    }
}
