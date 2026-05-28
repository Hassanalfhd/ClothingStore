using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using ClothingStore.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ClothingStore.Infrastructure.Services
{
    public class ProductImageProcessingBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IBackgroundTaskQueue _queue;
        private readonly ILogger<ProductImageProcessingBackgroundService> _logger;
        private readonly FoldersSettings _folderSettings;

        public ProductImageProcessingBackgroundService(IServiceScopeFactory scopeFactory, IBackgroundTaskQueue queue, ILogger<ProductImageProcessingBackgroundService> logger, IOptions<FoldersSettings> options)
        {
            _scopeFactory = scopeFactory;
            _queue = queue;
            _logger = logger;
            _folderSettings = options.Value;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    var job = await _queue.DequeueAsync(stoppingToken);
                    _logger.LogInformation("Processing image {ImageId}", job.ProductImageId);

                    using var scope = _scopeFactory.CreateScope();

                    var imageStorageService = scope.ServiceProvider.GetRequiredService<IImageStorageService>();

                    var productImageRepo = scope.ServiceProvider.GetRequiredService<IProductImageRepo>();

                    var image = await productImageRepo.GetByIdAsync(job.ProductImageId, stoppingToken);


                    if(image is null)
                        continue;

                    
                    var outputFolder = Path.Combine("wwwroot/", _folderSettings.RootFolder, _folderSettings.ProductsFolder, image.PublicId.ToString()).Replace("\\", "/");

                    _logger.LogInformation("Moving file {File}", job.FileName);

                    var processor = scope.ServiceProvider.GetRequiredService<IImageProcessingService>();
                    var result = await processor.ProcessAsync(job.TempFilePath, outputFolder, stoppingToken);

                    image.SetImageUrl(result.WebpPath);

                    image.MarkAsProcessed();

                    var unitOfWork = scope.ServiceProvider
                        .GetRequiredService<IUnitOfWork>();

                    await unitOfWork.SaveChangesAsync(stoppingToken);


                    _logger.LogInformation("Image processed successfully {ImageId}", job.ProductImageId);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                   "Error while processing product image.");
                    
                }
            }
        }

    }
}
