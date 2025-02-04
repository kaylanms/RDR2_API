using FluentValidation;
using RDR2.Application.Characters.Commands.AddCharacterToGang;

public class AddCharacterToGangCommandValidator : AbstractValidator<AddCharacterToGangCommand>
{
    public AddCharacterToGangCommandValidator()
    {
        RuleFor(x => x.CharacterId)
            .NotNull()
            .Must(BeAValidGuid).WithMessage("CharacterId cannot be empty.");
            
        RuleFor(x => x.GangId)
            .NotNull()
            .Must(BeAValidGuid).WithMessage("GangId cannot be empty.");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}
