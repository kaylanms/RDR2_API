using Microsoft.AspNetCore.Mvc;
using RDR2.Application.Gangs.Commands.CreateGang;
using RDR2.Application.Gangs.Queries.GetAllGangs;

namespace RDR2.Api.Controllers;

public sealed class GangController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await Sender.Send(new GetAllGangsQuery()));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGangCommand request)
        => Ok(await Sender.Send(new CreateGangCommand(request.Name, request.LeaderId)));
}