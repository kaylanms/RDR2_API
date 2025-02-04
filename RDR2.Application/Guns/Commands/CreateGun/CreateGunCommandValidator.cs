using FluentValidation;

namespace RDR2.Application.Guns.Commands.CreateGun;

public sealed class CreateGunCommandValidator : AbstractValidator<CreateGunCommand>
{
    public CreateGunCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Cost).NotNull();
        RuleFor(x => x.Ammunition).NotNull();
        RuleFor(x => x.Damage).NotNull();
    }
}