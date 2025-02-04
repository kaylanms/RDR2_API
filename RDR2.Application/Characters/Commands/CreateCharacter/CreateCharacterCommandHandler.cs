using FluentValidation;
using RDR2.Application.Abstractions;
using RDR2.Domain.Entities;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Characters.Commands.CreateCharacter;

public sealed class CreateCharacterCommandHandler : ICommandHandler<CreateCharacterCommand, Guid>
{
    private readonly ICharacterRepository _characterRepository;

    public CreateCharacterCommandHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<Guid> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = new Character(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.Age,
            request.IsAlive);

        await _characterRepository.CreateAsync(character);
        
        return character.Id;
    }
}
