using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity ActivityProgramming = new()
    {
        Id = Guid.Parse("d60a30d9-7b9c-4635-a7b3-3d89833d672c"),
        Name = "Programming",
        Start = new DateTime(2023, 05, 30, 03, 34, 50),
        End = new DateTime(2023, 05, 30, 05, 30, 50),
        ProjectId = ProjectSeeds.ProjectIFJ.Id,
        UserId = UserSeeds.UserTomSmith.Id,
    };
    
    public static readonly ActivityEntity ActivitySkiing = new()
    {
        Id = Guid.Parse("5cfd2fbd-59b1-481c-94fe-8cdf0fed3854"),
        Name = "Skiing",
        Start = new DateTime(2023, 05, 23, 04, 34, 50),
        End = new DateTime(2023, 05, 23, 07, 30, 50),
        ProjectId = ProjectSeeds.ProjectIFJ.Id,
        UserId = UserSeeds.UserTomSmith.Id,
    };
    
    public static readonly ActivityEntity ActivitySkating = new()
    {
        Id = Guid.Parse("110f6520-9cb3-401d-8abc-6950f8d4cbff"),
        Name = "Skating",
        Start = new DateTime(2021, 01, 25, 04, 50, 50),
        End = new DateTime(2021, 01, 25, 06, 30, 50),
        ProjectId = ProjectSeeds.ProjectICS.Id,
        UserId = UserSeeds.UserDarinaKratochvilova.Id,
    };

    static ActivitySeeds()
    {
        ActivitySkiing.Tags.Add(TagActivitySeeds.WorkSkiing);
        ActivitySkating.Tags.Add(TagActivitySeeds.MischiefSkating);
        ActivityProgramming.Tags.Add(TagActivitySeeds.SchoolProgramming);
    }

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ActivityEntity>().HasData(
            ActivityProgramming with {Tags = new List<TagActivityEntity>()}, 
            ActivitySkating with {Tags = new List<TagActivityEntity>()}, 
            ActivitySkiing with {Tags = new List<TagActivityEntity>()}
        );
}