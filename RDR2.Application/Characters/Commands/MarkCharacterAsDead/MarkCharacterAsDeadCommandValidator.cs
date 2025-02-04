using FluentValidation;

namespace RDR2.Application.Characters.Commands.MarkCharacterAsDead;

public sealed class MarkCharacterAsDeadCommandValidator : AbstractValidator<MarkCharacterAsDeadCommand>
{
    public MarkCharacterAsDeadCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty.");
    }
}
