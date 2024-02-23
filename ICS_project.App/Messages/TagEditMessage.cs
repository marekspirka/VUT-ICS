namespace ICS_project.App.Messages;

public record TagEditMessage
{
    public required Guid TagId { get; init; }
}