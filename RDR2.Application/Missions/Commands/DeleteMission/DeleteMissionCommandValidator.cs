using FluentValidation;

namespace RDR2.Application.Missions.Commands.DeleteMission;
public sealed class DeleteMissionCommandValidator : AbstractValidator<DeleteMissionCommand>
{
    public DeleteMissionCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}
