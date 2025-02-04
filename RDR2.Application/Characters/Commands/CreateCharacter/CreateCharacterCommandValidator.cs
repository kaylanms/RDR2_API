using FluentValidation;

namespace RDR2.Application.Characters.Commands.CreateCharacter;

public sealed class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
{
    public CreateCharacterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Age)
            .NotNull().WithMessage("Age is required.")
            .GreaterThan(0).WithMessage("Age must be greater than zero.")
            .LessThanOrEqualTo(120).WithMessage("Age must be 120 or less.");

        RuleFor(x => x.IsAlive)
            .NotNull().WithMessage("IsAlive status is required.");
    }
}
