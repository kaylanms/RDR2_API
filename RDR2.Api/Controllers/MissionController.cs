using Microsoft.AspNetCore.Mvc;
using RDR2.Application.Missions.Commands.CreateMission;
using RDR2.Application.Missions.Commands.DeleteMission;
using RDR2.Application.Missions.Commands.UpdateMission;
using RDR2.Application.Missions.Queries.GetAllMissions;
using RDR2.Application.Missions.Queries.GetMissionById;

namespace RDR2.Api.Controllers;

public sealed class MissionController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await Sender.Send(new GetAllMissionsQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) => Ok(await Sender.Send(new GetMissionByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMissionRequest request)
    {
        var command = new CreateMissionCommand(
            request.Name,
            request.Overview,
            request.IsPrimary);

        var missionId = await Sender.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = missionId }, missionId);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMissionRequest request)
        => Ok(await Sender.Send(new UpdateMissionCommand(request.Id, request.Name, request.Overview, request.IsPrimary)));

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteMissionRequest request)
        => Ok(await Sender.Send(new DeleteMissionCommand(request.Id)));
}
