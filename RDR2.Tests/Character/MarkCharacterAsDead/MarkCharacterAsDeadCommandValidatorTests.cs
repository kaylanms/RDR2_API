using FluentValidation.TestHelper;
using RDR2.Application.Characters.Commands.MarkCharacterAsDead;

public class MarkCharacterAsDeadCommandValidatorTests
{
    private readonly MarkCharacterAsDeadCommandValidator _validator;

    public MarkCharacterAsDeadCommandValidatorTests()
    {
        _validator = new MarkCharacterAsDeadCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new MarkCharacterAsDeadCommand(Guid.Empty);
        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage("Id cannot be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Null()
    {
        var command = new MarkCharacterAsDeadCommand(Guid.Empty);
        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorMessage("Id cannot be empty.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Id_Is_Valid()
    {
        var command = new MarkCharacterAsDeadCommand(Guid.NewGuid());
        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}
