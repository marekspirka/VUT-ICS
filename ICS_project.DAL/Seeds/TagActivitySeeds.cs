using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL.Seeds;

public static class TagActivitySeeds
{
    public static readonly TagActivityEntity SchoolProgramming = new()
    {
        Id = Guid.Parse("4c6b11fb-07a3-4d2c-a9e6-1c6547c94721"),
        TagId = TagSeeds.TagSchool.Id,
        ActivityId = ActivitySeeds.ActivityProgramming.Id
    };

    public static readonly TagActivityEntity WorkSkiing = new()
    {
        Id = Guid.Parse("5c8a9383-b2e4-42dc-a012-3f47bf45db6d"),
        TagId = TagSeeds.TagWork.Id,
        ActivityId = ActivitySeeds.ActivitySkiing.Id
    };
    
    public static readonly TagActivityEntity MischiefSkating = new()
    {
        Id = Guid.Parse("a7d02af9-8709-4662-a8eb-945a14a67b02"),
        TagId = TagSeeds.TagMischief.Id,
        ActivityId = ActivitySeeds.ActivitySkating.Id
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<TagActivityEntity>().HasData(
            SchoolProgramming with {Tag = null, Activity = null},
            WorkSkiing with {Tag = null, Activity = null},
            MischiefSkating with {Tag = null, Activity = null}
        );
}