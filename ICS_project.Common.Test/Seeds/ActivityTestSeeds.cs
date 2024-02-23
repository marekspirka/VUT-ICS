using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.Common.Test.Seeds;

public static class ActivityTestSeeds
{
    public static readonly ActivityEntity EmptyActivity = new()
    {
        Id = default,
        Name = default!,
        Start = default,
        End = default,
        UserId = default!,
    };


    public static readonly ActivityEntity ActivityEntityFootball = new()
    {
        Id = Guid.NewGuid(),
        Name = "Football",
        Start = new DateTime(2020, 03, 24, 03, 34, 50),
        End = new DateTime(2020, 03, 24, 08, 30, 50),
        Description = "My first sport.",
        UserId = UserTestSeeds.UserEntityMarek.Id,
        User = UserTestSeeds.UserEntityMarek,
        ProjectId = ProjectTestSeeds.ProjectEntitySport.Id,
        Project = ProjectTestSeeds.ProjectEntitySport,
        Tags = new List<TagActivityEntity>(),
    };

    public static readonly ActivityEntity ActivityEntityProgramming = new()
    {
        Id = Guid.NewGuid(),
        Name = "Programming",
        Start = new DateTime(2018, 03, 24, 07, 34, 50),
        End = new DateTime(2018, 03, 24, 10, 30, 50),
        Description = "Work on the project.",
        UserId = UserTestSeeds.UserEntityJanko.Id,
        User = UserTestSeeds.UserEntityJanko,
        ProjectId = ProjectTestSeeds.ProjectEntityICS.Id,
        Project = ProjectTestSeeds.ProjectEntityICS,
        Tags = new List<TagActivityEntity>(),
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            ActivityEntityFootball,
            ActivityEntityProgramming
        );
    }
}