using System;

namespace ICS_project.BL.Models;

public record ProjectListModel : Model
{
    public required string Name { get; set; }
    public bool IsJoined { get; set; }

    public static ProjectListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        IsJoined = false
    };
}

