using FluentValidation;

namespace Application.Features.ToDos.Commands.Create;

public class CreateTodoCommandValidator:AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(c => c.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is required");
        
    }
}