namespace Application.Models.Dtos.Tokens;

public class TokenResponseDto
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
}