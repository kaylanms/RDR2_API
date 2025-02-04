using Microsoft.EntityFrameworkCore;
using RDR2.Infrastructure.Repositories;
using RDR2.Domain.Entities;
using RDR2.Infrastructure;
using RDR2.Domain.Exceptions.Character;

public class CharacterRepositoryTests
{
    private readonly CharacterRepository _repository;
    private readonly ApplicationDbContext _dbContext;

    public CharacterRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _repository = new CharacterRepository(_dbContext);
    }

    [Fact]
    public async Task GetCharacterByIdAsync_ShouldReturnCharacter_WhenCharacterExists()
    {
        var characterId = Guid.NewGuid();
        var character = new Character(characterId, "John", "Marston", 30, true);
        await _dbContext.Characters.AddAsync(character);
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetCharacterByIdAsync(characterId);

        Assert.Equal(characterId, result.Id);
        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task GetCharacterByIdAsync_ShouldThrowCharacterNotFoundException_WhenCharacterDoesNotExist()
    {
        var characterId = Guid.NewGuid();

        await Assert.ThrowsAsync<CharacterNotFoundException>(() => _repository.GetCharacterByIdAsync(characterId));
    }
}
