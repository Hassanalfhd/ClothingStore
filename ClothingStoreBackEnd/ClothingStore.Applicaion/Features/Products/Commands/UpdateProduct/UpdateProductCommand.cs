using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid PublicId, string Name, string Description, decimal Price, string Currency,
        Guid CategoryId, Guid CreatedBy): IRequest<Result>;
    
}

