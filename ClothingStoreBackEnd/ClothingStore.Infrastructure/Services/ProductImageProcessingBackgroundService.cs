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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundTaskQueue _queue;
        private readonly ILogger<ProductImageProcessingBackgroundService> _logger;
        private readonly IImageProcessingService _processor;

        public ProductImageProcessingBackgroundService(IServiceScopeFactory scopeFactory, IBackgroundTaskQueue queue, ILogger<ProductImageProcessingBackgroundService> logger, IUnitOfWork unitOfWork, IImageProcessingService processor)
        {
            _scopeFactory = scopeFactory;
            _queue = queue;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _processor = processor;
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


                    // Move to temp
                    var relativePath = await imageStorageService.MoveToPermanentAsync(job.TempFilePath, job.FileName, stoppingToken);

                    _logger.LogInformation("Moving file {File}", job.FileName);
                    // Resize 
                    var resized = await _processor.ResizeAsync(relativePath, 800, 800);

                    // Convert to WebP
                    var webp = await _processor.ConvertToWebPAsync(resized);


                    image.SetImageUrl(relativePath);
                    image.MarkAsProcessed();

                    await _unitOfWork.SaveChangesAsync(stoppingToken);

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
