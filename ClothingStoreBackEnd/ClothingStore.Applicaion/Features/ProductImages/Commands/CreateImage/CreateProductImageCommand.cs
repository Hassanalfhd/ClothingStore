using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ClothingStore.Application.Features.ProductImages.Commands.CreateImage
{
    public class CreateProductImageCommand : IRequest<Result<Guid>>
    {
        public Guid? ProductId { get; set; }
        public Guid? ProductVariantId { get; set; }
        public IFormFile File { get; set; } = default!;

        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
    }
}
