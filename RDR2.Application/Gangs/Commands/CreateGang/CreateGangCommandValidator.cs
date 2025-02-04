using FluentValidation;

namespace RDR2.Application.Gangs.Commands.CreateGang;

public sealed class CreateGangCommandValidator : AbstractValidator<CreateGangCommand>
{
    public CreateGangCommandValidator()
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Name).NotEmpty();
    }
}