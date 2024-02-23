using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.Common.Test.Seeds;

public static class TagActivitySeeds
{
    public static readonly TagActivityEntity EmptyTagActivity = new()
    {
        Id = default,
        TagId = default,
        ActivityId = default,
    };

    public static readonly TagActivityEntity TagActivityEntity1 = new()
    {
        Id = Guid.Parse("a07d12a9-d95f-4132-9e1a-7a676dd690d5"),
        TagId = TagTestSeeds.TagEntityFreetime.Id,
        ActivityId = ActivityTestSeeds.ActivityEntityFootball.Id,
    };

    public static readonly TagActivityEntity TagActivityEntity2 = new()
    {
        Id = Guid.Parse("e0968b05-10d3-4c71-8b43-8c3a4237bb43"),
        TagId = TagTestSeeds.TagEntityWork.Id,
        ActivityId = ActivityTestSeeds.ActivityEntityProgramming.Id,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TagActivityEntity>().HasData(
            TagActivityEntity1,
            TagActivityEntity2
        );
    }
}