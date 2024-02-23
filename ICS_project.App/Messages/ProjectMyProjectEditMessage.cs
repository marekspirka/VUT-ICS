namespace ICS_project.App.Messages;

public record ProjectMyProjectEditMessage
{
    public required Guid ProjectId { get; init; }
}