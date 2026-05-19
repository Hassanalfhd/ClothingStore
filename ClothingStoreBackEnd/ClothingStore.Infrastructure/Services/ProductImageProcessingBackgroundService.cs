using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ClothingStore.Infrastructure.Services
{
    public class ProductImageProcessingBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IBackgroundTaskQueue _queue;
        private readonly ILogger<ProductImageProcessingBackgroundService> _logger;

        public ProductImageProcessingBackgroundService(IServiceScopeFactory scopeFactory, IBackgroundTaskQueue queue, ILogger<ProductImageProcessingBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _queue = queue;
            _logger = logger;
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

                    var outputFolder = Path.Combine("wwwroot/images/products", image.PublicId.ToString());

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
