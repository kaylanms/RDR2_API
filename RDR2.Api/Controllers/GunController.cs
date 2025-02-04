using Microsoft.AspNetCore.Mvc;
using RDR2.Application.Guns.Commands.CreateGun;
using RDR2.Application.Guns.Queries.GetAllGuns;
using RDR2.Application.Guns.Queries.GetById;

namespace RDR2.Api.Controllers;

public sealed class GunController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await Sender.Send(new GetAllGunsQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) => Ok(await Sender.Send(new GetGunByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGunRequest request)
    {
        var command = new CreateGunCommand(
            request.Name,
            request.Damage,
            request.Ammunition,
            request.Cost);

        var gunId = await Sender.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = gunId }, gunId);
    }
}