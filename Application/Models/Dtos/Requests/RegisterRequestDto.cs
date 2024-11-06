namespace Application.Models.Dtos.Requests;

public sealed record RegisterRequestDto(string Email, string Password, string UserName);