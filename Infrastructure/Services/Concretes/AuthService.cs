using Application.Features.Users.Commands.Create;
using Application.Features.Users.Queries.GetByEmail;
using Application.Models.Dtos.Requests;
using Application.Models.Dtos.Tokens;
using BlogSite.Service.Concretes;
using Core.Models;
using Domain.Entities;
using Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Concretes
{
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public AuthService(JwtService jwtService, IMediator mediator, UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<ReturnModel<TokenResponseDto>> LoginAsync(LoginRequestDto dto)
        {
            var query = new GetByEmailUserQuery{ Email = dto.Email };
            var user = await _mediator.Send(query);
            var tokenResponse = await _jwtService.CreateJwtTokenAsync(user);

            return new ReturnModel<TokenResponseDto>
            {
                Data = tokenResponse,
                Message = "Giriş Başarılı",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<TokenResponseDto>> RegisterAsync(RegisterRequestDto dto)
        {
            var command = new CreateUserCommand
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Email = dto.Email
            };

            var user = await _mediator.Send(command);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
            {
                return new ReturnModel<TokenResponseDto>
                {
                    Message = "Rol ataması başarısız",
                    StatusCode = 400,
                    Success = false
                };
            }
            var tokenResponse = await _jwtService.CreateJwtTokenAsync(user);

            return new ReturnModel<TokenResponseDto>
            {
                Data = tokenResponse,
                Message = "Kayıt işlemi Başarılı",
                StatusCode = 200,
                Success = true
            };
        }
    }
}