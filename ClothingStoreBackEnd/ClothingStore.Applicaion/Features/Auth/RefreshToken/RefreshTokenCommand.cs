using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Auth.DTOs;
using ClothingStore.Domain.Common;
using MediatR;

namespace ClothingStore.Application.Features.Auth.RefreshToken;
public  record RefreshTokenCommand(
     string AccessToken, string RefreshToken)
    : IRequest<Result<AuthResponseDto>>;
