using Moq;
using RDR2.Application.Characters.Commands.AddCharacterToGang;
using RDR2.Domain.Repositories;
using RDR2.Domain.Entities;
using RDR2.Domain.Exceptions.Character;
using RDR2.Domain.Exceptions.Gang;

public class AddCharacterToGangCommandHandlerTests
{
    private readonly Mock<ICharacterRepository> _mockCharacterRepository;
    private readonly Mock<IGangRepository> _mockGangRepository;
    private readonly AddCharacterToGangCommandHandler _handler;

    public AddCharacterToGangCommandHandlerTests()
    {
        _mockCharacterRepository = new Mock<ICharacterRepository>();
        _mockGangRepository = new Mock<IGangRepository>();
        _handler = new AddCharacterToGangCommandHandler(_mockCharacterRepository.Object, _mockGangRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddCharacterToGang()
    {
        var characterId = Guid.NewGuid();
        var gangId = Guid.NewGuid();
        var character = CreateCharacter(characterId);
        var gang = CreateGang(gangId);

        SetupCharacterRepository(characterId, character);
        SetupGangRepository(gangId, gang);

        var command = new AddCharacterToGangCommand(characterId, gangId);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(gangId, result);
        Assert.Equal(1, gang.TotalMembers);
        _mockCharacterRepository.Verify(repo => repo.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowCharacterNotFoundException_WhenCharacterNotFound()
    {
        var characterId = Guid.NewGuid();
        var gangId = Guid.NewGuid();

        _mockCharacterRepository.Setup(repo => repo.GetCharacterByIdAsync(characterId))
            .ThrowsAsync(new CharacterNotFoundException(characterId));

        var command = new AddCharacterToGangCommand(characterId, gangId);

        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowGangNotFoundException_WhenGangNotFound()
    {
        var characterId = Guid.NewGuid();
        var gangId = Guid.NewGuid();
        var character = CreateCharacter(characterId);

        _mockCharacterRepository.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);
        _mockGangRepository.Setup(repo => repo.GetGangByIdAsync(gangId))
            .ThrowsAsync(new GangNotFoundException(gangId));

        var command = new AddCharacterToGangCommand(characterId, gangId);

        await Assert.ThrowsAsync<GangNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    private Character CreateCharacter(Guid characterId)
    {
        return new Character(characterId, "John", "Marston", 30, true);
    }

    private Gang CreateGang(Guid gangId)
    {
        return new Gang(gangId, "The Boys", Guid.NewGuid());
    }

    private void SetupCharacterRepository(Guid characterId, Character character)
    {
        _mockCharacterRepository.Setup(repo => repo.GetCharacterByIdAsync(characterId)).ReturnsAsync(character);
    }

    private void SetupGangRepository(Guid gangId, Gang gang)
    {
        _mockGangRepository.Setup(repo => repo.GetGangByIdAsync(gangId)).ReturnsAsync(gang);
    }
}
