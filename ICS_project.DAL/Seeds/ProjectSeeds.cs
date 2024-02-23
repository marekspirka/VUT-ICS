using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity ProjectIFJ = new()
    {
        Id = Guid.Parse("03ac647a-3807-4079-9b27-78942ea34fb9"),
        Name = "IFJ",
    };

    public static readonly ProjectEntity ProjectICS = new()
    {
        Id = Guid.Parse("7baee999-46ab-447e-9107-0efab35044bf"),
        Name = "ICS",
    };

    public static readonly ProjectEntity ProjectIZU = new()
    {
        Id = Guid.Parse("e1aa63e9-d011-46c6-a502-374806e86fd5"),
        Name = "IZU",
    };

    public static readonly ProjectEntity ProjectIPP = new()
    {
        Id = Guid.Parse("fe63da7b-d028-4cea-b7ac-e597a75e4d40"),
        Name = "IPP",
    };

    static ProjectSeeds()
    {
        ProjectIFJ.Users.Add(ProjectUserSeeds.IFJTomSmith);
        ProjectIFJ.Users.Add(ProjectUserSeeds.IFJKlaraKonecna);
        ProjectICS.Users.Add(ProjectUserSeeds.ICSDarinaKratochvilova);
        ProjectIFJ.Activities.Add(ActivitySeeds.ActivityProgramming);
        ProjectIFJ.Activities.Add(ActivitySeeds.ActivitySkiing);
        ProjectICS.Activities.Add(ActivitySeeds.ActivitySkating);
    }

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectIFJ with {Users = Array.Empty<ProjectUserEntity>(), Activities = Array.Empty<ActivityEntity>()},
            ProjectICS with {Users = Array.Empty<ProjectUserEntity>(), Activities = Array.Empty<ActivityEntity>()},
            ProjectIPP with {Users = Array.Empty<ProjectUserEntity>(), Activities = Array.Empty<ActivityEntity>()},
            ProjectIZU with {Users = Array.Empty<ProjectUserEntity>(), Activities = Array.Empty<ActivityEntity>()}
        );
}
