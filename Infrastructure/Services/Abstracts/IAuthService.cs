using Application.Models.Dtos.Requests;
using Application.Models.Dtos.Tokens;
using Core.Models;

namespace Infrastructure.Services.Abstracts;

public interface IAuthService
{
    Task<ReturnModel<TokenResponseDto>> LoginAsync(LoginRequestDto dto);
    Task<ReturnModel<TokenResponseDto>> RegisterAsync(RegisterRequestDto dto);
}