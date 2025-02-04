using Microsoft.AspNetCore.Mvc;
using RDR2.Application.Characters.Commands.AddCharacterToGang;
using RDR2.Application.Characters.Commands.AddGunToCharacter;
using RDR2.Application.Characters.Commands.CreateCharacter;
using RDR2.Application.Characters.Commands.MarkCharacterAsDead;
using RDR2.Application.Characters.Queries.GetAllCharacters;
using RDR2.Application.Characters.Queries.GetCharacterById;

namespace RDR2.Api.Controllers;

public sealed class CharacterController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await Sender.Send(new GetAllCharactersQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) => Ok(await Sender.Send(new GetCharacterByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCharacterRequest request)
    {
        var command = new CreateCharacterCommand(
            request.FirstName,
            request.LastName,
            request.Age,
            request.IsAlive);

        var characterId = await Sender.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = characterId }, characterId);
    }
    [HttpPost("AddGun")]
    public async Task<IActionResult> AddGun([FromBody] AddGunToCharacterRequest request) 
        => Ok(await Sender.Send(new AddGunToCharacterCommand(request.CharacterId, request.GunId)));

    [HttpPost("AddToGang")]
    public async Task<IActionResult> AddToGang([FromBody] AddCharacterToGangRequest request) 
        => Ok(await Sender.Send(new AddCharacterToGangCommand(request.CharacterId, request.GangId)));

    [HttpPost("MarkAsDead")]
    public async Task<IActionResult> MarkAsDead([FromBody] MarkCharacterAsDeadRequest request) 
        => Ok(await Sender.Send(new MarkCharacterAsDeadCommand(request.Id)));
}
