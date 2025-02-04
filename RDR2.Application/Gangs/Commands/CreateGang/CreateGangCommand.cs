using RDR2.Application.Abstractions;

namespace RDR2.Application.Gangs.Commands.CreateGang;

public record CreateGangCommand(string Name, Guid LeaderId) : ICommand<Guid>;