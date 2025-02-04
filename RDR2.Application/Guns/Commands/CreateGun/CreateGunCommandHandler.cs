using RDR2.Application.Abstractions;
using RDR2.Domain.Entities;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Guns.Commands.CreateGun;

internal sealed class CreateGunCommandHandler : ICommandHandler<CreateGunCommand, Guid>
{
    private readonly IGunRepository _repository;

    public CreateGunCommandHandler(IGunRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreateGunCommand request, CancellationToken cancellationToken)
    {
        var gun = new Gun(
            Guid.NewGuid(),
            request.Name,
            request.Damage,
            request.Ammunition,
            request.Cost);

        await _repository.CreateAsync(gun);

        return gun.Id;
    }
}