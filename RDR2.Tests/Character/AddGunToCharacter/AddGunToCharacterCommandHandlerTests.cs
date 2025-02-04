using Moq;
using RDR2.Application.Characters.Commands.AddGunToCharacter;
using RDR2.Domain.Entities;
using RDR2.Domain.Repositories;
using RDR2.Domain.Exceptions.Character;

public class AddGunToCharacterCommandHandlerTests
{
    private readonly Mock<ICharacterRepository> _characterRepositoryMock;
    private readonly AddGunToCharacterCommandHandler _handler;

    public AddGunToCharacterCommandHandlerTests()
    {
        _characterRepositoryMock = new Mock<ICharacterRepository>();
        _handler = new AddGunToCharacterCommandHandler(_characterRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_AddsGunToCharacterAndReturnsGunId()
    {
        var characterId = Guid.NewGuid();
        var gunId = Guid.NewGuid();

        var character = new Character(characterId, "John", "Marston", 30, true);
        _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);

        var command = new AddGunToCharacterCommand(characterId, gunId);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(gunId, result);
        Assert.Contains(character.CharacterGuns, cg => cg.GunId == gunId);
        _characterRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsCharacterNotFoundException_WhenCharacterMarstonsNotExist()
    {
        var nonExistentCharacterId = Guid.NewGuid();
        var gunId = Guid.NewGuid();

        _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(nonExistentCharacterId))
            .ThrowsAsync(new CharacterNotFoundException(nonExistentCharacterId));

        var command = new AddGunToCharacterCommand(nonExistentCharacterId, gunId);

        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_AddsGunToCharacterMultipleTimes_WhenGunAlreadyAssigned()
    {
        var characterId = Guid.NewGuid();
        var gunId = Guid.NewGuid();

        var character = new Character(characterId, "John", "Marston", 30, true);
        _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);

        var command = new AddGunToCharacterCommand(characterId, gunId);

        await _handler.Handle(command, CancellationToken.None);
        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(gunId, result);
        Assert.Equal(1, character.CharacterGuns.Count(cg => cg.GunId == gunId));
        _characterRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenCharacterIsDead()
    {
        var characterId = Guid.NewGuid();
        var gunId = Guid.NewGuid();

        var character = new Character(characterId, "John", "Marston", 30, false);
        _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);

        var command = new AddGunToCharacterCommand(characterId, gunId);

        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_CallsSaveAsyncAfterAddingGun()
    {
        var characterId = Guid.NewGuid();
        var gunId = Guid.NewGuid();

        var character = new Character(characterId, "John", "Marston", 30, true);
        _characterRepositoryMock.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);

        var command = new AddGunToCharacterCommand(characterId, gunId);

        await _handler.Handle(command, CancellationToken.None);

        _characterRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
    }
}
