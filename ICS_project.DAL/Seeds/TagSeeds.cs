using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL.Seeds;

public static class TagSeeds
{

    public static readonly TagEntity TagSchool = new()
    {
        Id = Guid.Parse("15a17df4-fce6-4f69-a8c9-9e026b0ad2e4"),
        Name = "School",
        UserId = UserSeeds.UserTomSmith.Id,
    };

    public static readonly TagEntity TagWork = new()
    {
        Id = Guid.Parse("110f6520-9cb3-401d-8abc-6950f8d4cbff"),
        Name = "Work",
        UserId = UserSeeds.UserKlaraKonecna.Id,
    };
    
    public static readonly TagEntity TagMischief = new()
    {
        Id = Guid.Parse("d0ebd9b5-05aa-4d53-b5a5-8afd0231bc3f"),
        Name = "Mischief",
        UserId = UserSeeds.UserDarinaKratochvilova.Id,
    };

    static TagSeeds()
    {
        TagWork.Activities.Add(TagActivitySeeds.WorkSkiing);
        TagMischief.Activities.Add(TagActivitySeeds.MischiefSkating);
        TagSchool.Activities.Add(TagActivitySeeds.SchoolProgramming);
    }
    
    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<TagEntity>().HasData(
            TagSchool with {Activities = Array.Empty<TagActivityEntity>()}, 
            TagWork with {Activities = Array.Empty<TagActivityEntity>()}, 
            TagMischief with {Activities = Array.Empty<TagActivityEntity>()}
        );
}