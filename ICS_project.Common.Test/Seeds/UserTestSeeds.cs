using ICS_project.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.Common.Test.Seeds;

public static class UserTestSeeds
{
    public static readonly UserEntity EmptyUser = new()
    {
        Id = default,
        Name = default!,
        Surname = default!,
        ImageURL = default!
    };

    public static readonly UserEntity UserEntityMarek = new()
    {
        Id = Guid.Parse(input: "26d8d98b-9d01-4021-a6c8-6a80a2ed77d6"),
        Name = "Marek",
        Surname = "Dobehal",
        ImageURL = @"https://static.gigwise.com/artists/Editors_7_quesada_750.jpg"
    };

    public static readonly UserEntity UserEntityJanko = new()
    {
        Id = Guid.Parse(input: "53c66f28-7b9c-466e-98c3-3f10d7de22f5"),
        Name = "Janko",
        Surname = "Spadol",
        ImageURL = @"https://img.freepik.com/free-photo/young-bearded-man-with-striped-shirt_273609-5677.jpg"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntityMarek,
            UserEntityJanko
        );
    }
}