using FluentValidation;

namespace Application.Features.Categories.Commands.Create;

public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kategori ad� gerekli")
            .Length(3, 20).WithMessage("Kategori ad� 3 ile 20 karakter aras�nda olmal�");
    }
}