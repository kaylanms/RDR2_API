using FluentAssertions;
using Moq;
using RDR2.Application.Characters.Commands.CreateCharacter;
using RDR2.Domain.Entities;
using RDR2.Domain.Repositories;

public class CreateCharacterCommandHandlerTests
{
    private readonly Mock<ICharacterRepository> _characterRepositoryMock;
    private readonly CreateCharacterCommandHandler _handler;

    public CreateCharacterCommandHandlerTests()
    {
        _characterRepositoryMock = new Mock<ICharacterRepository>();
        _handler = new CreateCharacterCommandHandler(_characterRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Character_And_Return_Id()
    {
        var command = new CreateCharacterCommand("John", "Doe", 30, true);
        var cancellationToken = CancellationToken.None;

        var result = await _handler.Handle(command, cancellationToken);

        result.Should().NotBeEmpty();
        _characterRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Character>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Call_Repository_With_Correct_Data()
    {
        var command = new CreateCharacterCommand("Arthur", "Morgan", 35, true);
        Character? createdCharacter = null;
        
        _characterRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Character>()))
            .Callback<Character>(c => createdCharacter = c);

        var cancellationToken = CancellationToken.None;

        await _handler.Handle(command, cancellationToken);

        createdCharacter.Should().NotBeNull();
        createdCharacter.FirstName.Should().Be("Arthur");
        createdCharacter.LastName.Should().Be("Morgan");
        createdCharacter.Age.Should().Be(35);
        createdCharacter.IsAlive.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Repository_Fails()
    {
        var command = new CreateCharacterCommand("John", "Doe", 30, true);
        _characterRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Character>()))
            .ThrowsAsync(new Exception("Database error"));

        var cancellationToken = CancellationToken.None;

        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, cancellationToken));
    }
}
