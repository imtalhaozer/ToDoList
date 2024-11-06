using FluentValidation;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Kullan�c� ad� gerekli")
            .Length(3, 20).WithMessage("Kullan�c� ad� 3 ile 20 karakter aras�nda olmal�");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email adresi gerekli")
            .EmailAddress().WithMessage("Ge�ersiz email adresi");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("�ifre gerekli")
            .MinimumLength(6).WithMessage("�ifre en az 6 karakter olmal�");
    }
}