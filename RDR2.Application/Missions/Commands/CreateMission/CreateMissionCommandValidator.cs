using FluentValidation;

namespace RDR2.Application.Missions.Commands.CreateMission;
public sealed class CreateMissionCommandValidator : AbstractValidator<CreateMissionCommand>
{
    public CreateMissionCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Overview).NotEmpty();
        RuleFor(x => x.IsPrimary).NotNull();
    }
}
