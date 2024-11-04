using FluentValidation;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.UserName).NotEmpty().WithMessage("UserName cannot be empty");
        RuleFor(command => command.Password).NotEmpty().WithMessage("Password cannot be empty");
        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Invalid email format");
    }
}