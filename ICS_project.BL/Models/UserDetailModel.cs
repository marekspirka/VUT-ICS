using System;

namespace ICS_project.BL.Models;

public record UserDetailModel : Model
{
    public required string Name { get; set; }

    public required string Surname { get; set; }

    public string? ImageUrl { get; set; }

    public static UserDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Surname = string.Empty
    };
}