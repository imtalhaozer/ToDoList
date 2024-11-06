using FluentValidation;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullanýcý adý gerekli")
            .Length(3, 20).WithMessage("Kullanýcý adý 3 ile 20 karakter arasýnda olmalý");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email adresi gerekli")
            .EmailAddress().WithMessage("Geçersiz email adresi");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Þifre gerekli")
            .MinimumLength(6).WithMessage("Þifre en az 6 karakter olmalý");
    }
}