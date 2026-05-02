using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.ValueObjects;
using MediatR;

namespace ClothingStore.Application.Features.Products.CreateProduct;

    public record CreateProductCommand
    (string Name, string Description, decimal Price, string Currency,
        bool IsActive,
        long CreatedBy, long CategoryId):IRequest<Result<Guid>>;


