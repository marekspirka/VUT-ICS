using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL.Seeds;

public static class ProjectUserSeeds
{
    public static readonly ProjectUserEntity IFJTomSmith = new()
    {
        Id = Guid.Parse("08ca6f49-6d8e-4a92-bbfe-35e15adfb4a4"),
        ProjectId = ProjectSeeds.ProjectIFJ.Id,
        UserId = UserSeeds.UserTomSmith.Id,
    };
    
    public static readonly ProjectUserEntity IFJKlaraKonecna = new()
    {
        Id = Guid.Parse("a4946474-12fd-4a11-a2ae-dd4aa79a1b4e"),
        ProjectId = ProjectSeeds.ProjectIFJ.Id,
        UserId = UserSeeds.UserKlaraKonecna.Id,
    };
    
    public static readonly ProjectUserEntity ICSDarinaKratochvilova = new()
    {
        Id = Guid.Parse("a7be96a2-3ab5-4617-b42c-856fea8ea86c"),
        ProjectId = ProjectSeeds.ProjectICS.Id,
        UserId = UserSeeds.UserDarinaKratochvilova.Id,
    };


    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ProjectUserEntity>().HasData(
            IFJTomSmith,
            IFJKlaraKonecna,
            ICSDarinaKratochvilova
        );
}

