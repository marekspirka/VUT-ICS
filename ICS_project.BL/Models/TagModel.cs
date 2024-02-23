using System;

namespace ICS_project.BL.Models;

public record TagModel : Model
{
    public required string Name { get; set; }

    public static TagModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty
    };
}