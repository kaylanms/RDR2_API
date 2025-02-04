using FluentValidation.TestHelper;
using RDR2.Application.Characters.Commands.CreateCharacter;
using Xunit;

public class CreateCharacterCommandValidatorTests
{
    private readonly CreateCharacterCommandValidator _validator;

    public CreateCharacterCommandValidatorTests()
    {
        _validator = new CreateCharacterCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_FirstName_Is_Empty()
    {
        var command = new CreateCharacterCommand("", "Marston", 30, true);

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.FirstName)
              .WithErrorMessage("First name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_FirstName_Is_Too_Short()
    {
        var command = new CreateCharacterCommand("J", "Marston", 30, true);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
              .WithErrorMessage("First name must be at least 2 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_FirstName_Is_Too_Long()
    {
        var command = new CreateCharacterCommand(new string('A', 51), "Marston", 30, true);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
              .WithErrorMessage("First name cannot exceed 50 characters.");
    }

    [Fact]
    public void Should_Have_Error_When_LastName_Is_Empty()
    {
        var command = new CreateCharacterCommand("John", "", 30, true);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LastName)
              .WithErrorMessage("Last name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_LastName_Is_Too_Short()
    {
        var command = new CreateCharacterCommand("John", "S", 30, true);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LastName)
              .WithErrorMessage("Last name must be at least 2 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_LastName_Is_Too_Long()
    {
        var command = new CreateCharacterCommand("John", new string('B', 51), 30, true);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LastName)
              .WithErrorMessage("Last name cannot exceed 50 characters.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(121)]
    public void Should_Have_Error_When_Age_Is_Invalid(int? age)
    {
        var command = new CreateCharacterCommand("John", "Marston", age ?? 0, true);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Age);
    }

    [Fact]
    public void Should_Have_Error_When_IsAlive_Is_Null()
    {
        var command = new CreateCharacterCommand("John", "Marston", 30, false);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.IsAlive);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new CreateCharacterCommand("John", "Marston", 30, true);
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
