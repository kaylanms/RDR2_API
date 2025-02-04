namespace RDR2.Domain.Models;

public record MissionResponse(
    Guid Id,
    string Name,
    string Overview,
    bool IsPrimary);