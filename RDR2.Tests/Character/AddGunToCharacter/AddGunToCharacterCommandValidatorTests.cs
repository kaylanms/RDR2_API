using FluentValidation.TestHelper;
using RDR2.Application.Characters.Commands.AddGunToCharacter;
using System;
using Xunit;

public class AddGunToCharacterCommandValidatorTests
{
    private readonly AddGunToCharacterCommandValidator _validator;

    public AddGunToCharacterCommandValidatorTests()
    {
        _validator = new AddGunToCharacterCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_CharacterId_Is_Empty()
    {
        var command = new AddGunToCharacterCommand(Guid.Empty, Guid.NewGuid());

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.CharacterId);
    }

    [Fact]
    public void Should_Have_Error_When_GunId_Is_Empty()
    {
        var command = new AddGunToCharacterCommand(Guid.NewGuid(), Guid.Empty);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.GunId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_CharacterId_And_GunId_Are_Valid()
    {
        var command = new AddGunToCharacterCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.CharacterId);
        result.ShouldNotHaveValidationErrorFor(x => x.GunId);
    }
}
