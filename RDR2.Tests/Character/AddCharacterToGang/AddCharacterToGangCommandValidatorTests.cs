using System.Linq.Expressions;
using FluentValidation.TestHelper;
using RDR2.Application.Characters.Commands.AddCharacterToGang;

public class AddCharacterToGangCommandValidatorTests
{
    private readonly AddCharacterToGangCommandValidator _validator;

    public AddCharacterToGangCommandValidatorTests()
    {
        _validator = new AddCharacterToGangCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_CharacterId_Is_Null()
    {
        var command = CreateCommand(characterId: Guid.Empty, gangId: Guid.NewGuid());
        ValidateProperty(command, x => x.CharacterId, "CharacterId cannot be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_CharacterId_Is_Empty()
    {
        var command = CreateCommand(characterId: Guid.Empty, gangId: Guid.NewGuid());
        ValidateProperty(command, x => x.CharacterId, "CharacterId cannot be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_GangId_Is_Null()
    {
        var command = CreateCommand(characterId: Guid.NewGuid(), gangId: Guid.Empty);
        ValidateProperty(command, x => x.GangId, "GangId cannot be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_GangId_Is_Empty()
    {
        var command = CreateCommand(characterId: Guid.NewGuid(), gangId: Guid.Empty);
        ValidateProperty(command, x => x.GangId, "GangId cannot be empty.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_CharacterId_And_GangId_Are_Valid()
    {
        var command = CreateCommand(characterId: Guid.NewGuid(), gangId: Guid.NewGuid());
        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.CharacterId);
        result.ShouldNotHaveValidationErrorFor(x => x.GangId);
    }

    private AddCharacterToGangCommand CreateCommand(Guid characterId, Guid gangId)
    {
        return new AddCharacterToGangCommand(characterId, gangId);
    }

    private void ValidateProperty(AddCharacterToGangCommand command, Expression<Func<AddCharacterToGangCommand, Guid>> property, string expectedErrorMessage)
    {
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(property)
            .WithErrorMessage(expectedErrorMessage);
    }
}
