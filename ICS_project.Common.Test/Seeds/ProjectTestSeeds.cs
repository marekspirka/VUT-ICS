using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.Common.Test.Seeds;

public static class ProjectTestSeeds
{
    public static readonly ProjectEntity EmptyProject = new()
    {
        Id = default,
        Name = default!,
    };

    public static readonly ProjectEntity ProjectEntitySport = new()
    {
        Id = Guid.Parse(input: "33e6633f-2d4a-4fb4-aa4f-4f7e4f5f5d1f"),
        Name = "Sport",
    };

    public static readonly ProjectEntity ProjectEntityICS = new()
    {
        Id = Guid.Parse(input: "97c7dab9-6e0a-4da8-8af7-986e15b2d7e7"),
        Name = "ICS",
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectEntitySport,
            ProjectEntityICS
        );
    }
}