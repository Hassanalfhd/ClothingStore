using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Products.Commands.ToggleActivateProduct
{
    public record ToggleActivateCommand(Guid PublicId): IRequest<Result>;


}
