using FluentValidation;
using System;

namespace RDR2.Application.Characters.Commands.AddGunToCharacter;

public sealed class AddGunToCharacterCommandValidator : AbstractValidator<AddGunToCharacterCommand>
{
    public AddGunToCharacterCommandValidator()
    {
        RuleFor(x => x.CharacterId)
            .NotEmpty().WithMessage("Character ID must be provided.")
            .Must(BeAValidGuid).WithMessage("Character ID is invalid.");

        RuleFor(x => x.GunId)
            .NotEmpty().WithMessage("Gun ID must be provided.")
            .Must(BeAValidGuid).WithMessage("Gun ID is invalid.");
    }

    private bool BeAValidGuid(Guid guid)
    {
        return guid != Guid.Empty;
    }
}
