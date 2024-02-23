using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL.Seeds;

public static class UserSeeds
{

    public static readonly UserEntity UserTomSmith = new()
    {
        Id = Guid.Parse("854c99f8-30a1-48e9-9975-5c5ba0d7e703"),
        Name = "Tom",
        Surname = "Smith",
        ImageURL = @"https://static.gigwise.com/artists/Editors_7_quesada_750.jpg",
    };

    public static readonly UserEntity UserPetrNovak = new()
    {
        Id = Guid.Parse("79267a2c-bc56-46f5-8e7c-1020512f9f38"),
        Name = "Petr",
        Surname = "Novak",
        ImageURL = @"https://as2.ftcdn.net/v2/jpg/00/60/59/01/1000_F_60590191_YKoAK1ZcOkyR4PDQcmVfXszbqOk1uSbJ.jpg"
    };

    public static readonly UserEntity UserKlaraKonecna = new()
    {
        Id = Guid.Parse("f3a9d51d-4c23-4f89-89c1-b59f3a0b8e9e"),
        Name = "Klara",
        Surname = "Konecna",
        ImageURL = @"https://babewings.cz/wp-content/uploads/2020/03/Super-Woman-obe-oci.jpg"
    };

    public static readonly UserEntity UserDarinaKratochvilova = new()
    {
        Id = Guid.Parse("e82ed712-0d16-49b2-8a27-ec99742cf9a4"),
        Name = "Darina",
        Surname = "Novotna",
        ImageURL = @"https://images.squarespace-cdn.com/content/v1/5ceffd427a27670001628fb2/1561716917340-U5L6LYWZQL3D5JC7T3RR/download.jpeg"
    };

    static UserSeeds()
    {
        UserDarinaKratochvilova.Activities.Add(ActivitySeeds.ActivitySkating);
        UserDarinaKratochvilova.Projects.Add(ProjectUserSeeds.ICSDarinaKratochvilova);
        UserTomSmith.Projects.Add(ProjectUserSeeds.IFJTomSmith);
        UserTomSmith.Activities.Add(ActivitySeeds.ActivityProgramming);
        UserKlaraKonecna.Activities.Add(ActivitySeeds.ActivitySkiing);
        UserKlaraKonecna.Projects.Add(ProjectUserSeeds.IFJKlaraKonecna);
    }


    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<UserEntity>().HasData(
            UserTomSmith with {Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<ProjectUserEntity>()},
            UserKlaraKonecna with {Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<ProjectUserEntity>()},
            UserPetrNovak with {Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<ProjectUserEntity>()},
            UserDarinaKratochvilova with {Activities = Array.Empty<ActivityEntity>(), Projects = Array.Empty<ProjectUserEntity>()}
        );
}