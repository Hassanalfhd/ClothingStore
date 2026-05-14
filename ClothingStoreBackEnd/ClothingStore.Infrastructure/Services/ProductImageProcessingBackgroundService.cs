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


        public ProductImageProcessingBackgroundService(IServiceScopeFactory scopeFactory, IBackgroundTaskQueue queue, ILogger<ProductImageProcessingBackgroundService> logger, IUnitOfWork unitOfWork)
        {
            _scopeFactory = scopeFactory;
            _queue = queue;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    var job = await _queue.DequeueAsync(stoppingToken);

                    using var scope = _scopeFactory.CreateScope();

                    var imageStorageService = scope.ServiceProvider.GetRequiredService<IImageStorageService>();

                    var productImageRepo = scope.ServiceProvider.GetRequiredService<IProductImageRepo>();

                    var image = await productImageRepo.GetByIdAsync(job.ProductImageId, stoppingToken);


                    if(image is null)
                        continue;


                    var relativePath = await imageStorageService.MoveToPermanentAsync(job.TempFilePath, job.FileName, stoppingToken);

                    image.SetImageUrl(relativePath);
                    image.MarkAsProcessed();

                    await _unitOfWork.SaveChangesAsync(stoppingToken);

                    
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
