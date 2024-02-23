using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.Common.Test.Seeds;

public static class ProjectUserTestSeeds
{
    public static readonly ProjectUserEntity EmptyUserProject = new()
    {
        Id = default,
        ProjectId = default,
        UserId = default,
    };

    public static readonly ProjectUserEntity ProjectUserEntity1 = new()
    {
        Id = Guid.Parse("2af21c3e-4f2d-4a84-b4e4-4d8d78a4fbf5"),
        ProjectId = ProjectTestSeeds.ProjectEntitySport.Id,
        UserId = UserTestSeeds.UserEntityMarek.Id,
    };

    public static readonly ProjectUserEntity ProjectUserEntity2 = new()
    {
        Id = Guid.Parse("1a8f8a1e-b5b9-4db5-ba8a-24e64111a56d"),
        ProjectId = ProjectTestSeeds.ProjectEntityICS.Id,
        UserId = UserTestSeeds.UserEntityJanko.Id,
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectUserEntity>().HasData(
            ProjectUserEntity1,
            ProjectUserEntity2
        );
    }
}