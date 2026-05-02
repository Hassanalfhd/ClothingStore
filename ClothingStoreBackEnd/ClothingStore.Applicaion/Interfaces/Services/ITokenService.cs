using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.DTOs;

namespace ClothingStore.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> CreateAccessTokenAsync(TokenRequestDto user);
        string CreateRefreshToken();
        TokenRequestDto GetTokenRequestDtoFromToken(string token);
    }

}
