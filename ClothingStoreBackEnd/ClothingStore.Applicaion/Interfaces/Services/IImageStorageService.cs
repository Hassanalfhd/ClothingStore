using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;
using ClothingStore.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface IImageStorageService
    {
        Task<TempImageResult> SaveTempAsync(IFormFile file, CancellationToken cancellationToken);

        Task<string> MoveToPermanentAsync(string tempFilePath, string fileName, CancellationToken cancellationToken);

        Task DeleteAsync(string relativePath, CancellationToken cancellationToken);
    }
}
