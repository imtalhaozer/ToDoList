using System.Security.Claims;
using Application.Models.Dtos.Tokens;
using Domain.Entities;

namespace Infrastructure.Services.Abstracts;

public interface IJwtService
{
    Task<TokenResponseDto> CreateJwtTokenAsync(User user);
}