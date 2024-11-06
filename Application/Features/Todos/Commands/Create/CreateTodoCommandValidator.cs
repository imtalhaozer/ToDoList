using FluentValidation;

namespace Application.Features.ToDos.Commands.Create;

public class CreateTodoCommandValidator:AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty().WithMessage("Baþlýk gerekli");
        RuleFor(c => c.Description).NotEmpty().WithMessage("Açýklama gerekli");
        RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId gerekli");


    }
}