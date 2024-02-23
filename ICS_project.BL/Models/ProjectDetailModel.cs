using System;
using System.Collections;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Models;

public record ProjectDetailModel : Model
{
    public required string Name { get; set; }
    public ICollection<UserDetailModel>? Users { get; set; }

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty
    };
}