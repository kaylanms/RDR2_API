using Moq;
using RDR2.Application.Characters.Commands.MarkCharacterAsDead;
using RDR2.Domain.Entities;
using RDR2.Domain.Exceptions.Character;
using RDR2.Domain.Repositories;

public class MarkCharacterAsDeadCommandHandlerTests
{
    private readonly Mock<ICharacterRepository> _mockCharacterRepository;
    private readonly Mock<IGangRepository> _mockGangRepository;
    private readonly MarkCharacterAsDeadCommandHandler _handler;

    public MarkCharacterAsDeadCommandHandlerTests()
    {
        _mockCharacterRepository = new Mock<ICharacterRepository>();
        _mockGangRepository = new Mock<IGangRepository>();
        _handler = new MarkCharacterAsDeadCommandHandler(_mockCharacterRepository.Object, _mockGangRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldMarkCharacterAsDead_WhenCharacterExists()
    {
        var characterId = Guid.NewGuid();
        var character = new Character(characterId, "John", "Marston", 30, true);
        var gang = new Gang(Guid.NewGuid(), "The Boys", Guid.NewGuid());
        
        character.JoinGang(gang);

        _mockCharacterRepository.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);
        _mockGangRepository.Setup(repo => repo.GetGangByIdAsync(gang.Id)).ReturnsAsync(gang);

        var command = new MarkCharacterAsDeadCommand(characterId);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(characterId, result);
        Assert.False(character.IsAlive);
        Assert.Empty(gang.Characters);
        _mockCharacterRepository.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNotThrowException_WhenCharacterDoesNotExist()
    {
        var characterId = Guid.NewGuid();
        
        _mockCharacterRepository.Setup(repo => repo.GetCharacterByIdAsync(characterId))
            .ThrowsAsync(new CharacterNotFoundException(characterId));

        var command = new MarkCharacterAsDeadCommand(characterId);

        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldNotThrowException_WhenCharacterHasNoGang()
    {
        var characterId = Guid.NewGuid();
        var character = new Character(characterId, "John", "Marston", 30, true);

        _mockCharacterRepository.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);

        var command = new MarkCharacterAsDeadCommand(characterId);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(characterId, result);
        Assert.False(character.IsAlive);
        _mockCharacterRepository.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRemoveCharacterFromGang_WhenCharacterHasGang()
    {
        var characterId = Guid.NewGuid();
        var character = new Character(characterId, "John", "Marston", 30, true);
        var gang = new Gang(Guid.NewGuid(), "The Boys", Guid.NewGuid());
        
        character.JoinGang(gang);

        _mockCharacterRepository.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);
        _mockGangRepository.Setup(repo => repo.GetGangByIdAsync(gang.Id)).ReturnsAsync(gang);

        var command = new MarkCharacterAsDeadCommand(characterId);

        await _handler.Handle(command, CancellationToken.None);

        _mockGangRepository.Verify(repo => repo.SaveAsync(), Times.Never);
    }
}
