using FluentValidation;

namespace RDR2.Application.Missions.Commands.UpdateMission;
public sealed class UpdateMissionCommandValidator : AbstractValidator<UpdateMissionCommand>
{
    public UpdateMissionCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Overview).NotEmpty();
        RuleFor(x => x.IsPrimary).NotNull();
    }
}
