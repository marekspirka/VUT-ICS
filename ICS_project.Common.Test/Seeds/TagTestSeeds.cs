using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.Common.Test.Seeds;

public static class TagTestSeeds
{
    public static readonly TagEntity EmptyTag = new()
    {
        Id = default,
        Name = default!,
        UserId = default
    };

    public static readonly TagEntity TagEntityFreetime = new()
    {
        Id = Guid.Parse("55b93b23-16f8-4bfe-b1a9-4df0a8fc4c28"),
        Name = "FreeTime",
        UserId = UserTestSeeds.UserEntityJanko.Id,
    };

    public static readonly TagEntity TagEntityWork = new()
    {
        Id = Guid.Parse("17d15624-36f8-4a1a-b5b5-b9ecf18be838"),
        Name = "Work",
        UserId = UserTestSeeds.UserEntityJanko.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagEntity>().HasData(
            TagEntityFreetime,
            TagEntityWork
        );
    }
}