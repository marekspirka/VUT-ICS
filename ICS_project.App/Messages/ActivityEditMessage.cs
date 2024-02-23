namespace ICS_project.App.Messages;

public record ActivityEditMessage
{
    public required Guid ActivitiesId { get; init; }
}