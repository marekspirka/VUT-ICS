using ICS_project.DAL.Entities;
using ICS_project.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using ICS_project.Common.Test.Seeds;

namespace ICS_project.DAL.Test;
public class DatabaseTests
{
    private IDbContextFactory<ICS_projectDbContext> DbContextFactory;
    private readonly ICS_projectDbContext _dbContextSUT;

    public DatabaseTests()
    {
        DbContextFactory = new DbContextSqLiteFactory("DatabaseTestDb.db", false);
        _dbContextSUT = DbContextFactory.CreateDbContext();
    }

    [Fact]
    public async Task AddActivityFootball()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        //Arrange
        var user = UserTestSeeds.UserEntityMarek;
        var activity = ActivityTestSeeds.ActivityEntityFootball;
        var project = ProjectTestSeeds.ProjectEntitySport;

        //Act
        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Activities.Add(activity);
        _dbContextSUT.Projects.Add(project);
        await _dbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities
            .SingleAsync(i => i.Id == activity.Id);
        Assert.Equal(activity.Id, actualEntity.Id);
        Assert.Equal(activity.Name, actualEntity.Name);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task AddActivityProgramming()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        //Arrange
        var user = UserTestSeeds.UserEntityJanko;
        var activity = ActivityTestSeeds.ActivityEntityProgramming;
        var project = ProjectTestSeeds.ProjectEntityICS;

        //Act
        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Activities.Add(activity);
        _dbContextSUT.Projects.Add(project);
        await _dbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities
            .SingleAsync(i => i.Id == activity.Id);
        Assert.Equal(activity.Id, actualEntity.Id);
        Assert.Equal(activity.Name, actualEntity.Name);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task ChangeDescriptionFootballActivity()
    {
        //Arrange
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        var user = UserTestSeeds.UserEntityMarek;
        var activity = ActivityTestSeeds.ActivityEntityFootball;
        var project = ProjectTestSeeds.ProjectEntitySport;

        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Activities.Add(activity);
        _dbContextSUT.Projects.Add(project);
        await _dbContextSUT.SaveChangesAsync();

        //Act
        var modifiedActivity = new ActivityEntity()
        {
            Id = activity.Id,
            Name = "Football",
            Start = new DateTime(2023, 01, 26, 12, 34, 59),
            End = new DateTime(2023, 01, 26, 12, 54, 59),
            Description = "My first sport modified.",
            UserId = user.Id,
            User = user,
            Project = project,
            Tags = new List<TagActivityEntity>(),
        };

        _dbContextSUT.Entry(activity).State = EntityState.Detached;
        _dbContextSUT.Activities.Update(modifiedActivity);
        await _dbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities
            .SingleAsync(i => i.Id == modifiedActivity.Id);
        Assert.Equal(modifiedActivity.Id, actualEntity.Id);
        Assert.Equal(modifiedActivity.Name, actualEntity.Name);
        Assert.Equal(modifiedActivity.Start, actualEntity.Start);
        Assert.Equal(modifiedActivity.End, actualEntity.End);
        Assert.Equal(modifiedActivity.Description, actualEntity.Description);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }


    [Fact]
    public async Task DeleteFootballActivity()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        // Arrange
        var user = UserTestSeeds.UserEntityMarek;
        var activity = ActivityTestSeeds.ActivityEntityFootball;
        var project = ProjectTestSeeds.ProjectEntitySport;

        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.Activities.Add(activity);
        await _dbContextSUT.SaveChangesAsync();

        // Act
        _dbContextSUT.Activities.Remove(activity);
        await _dbContextSUT.SaveChangesAsync();

        // Assert
        var deletedEntity = await _dbContextSUT.Activities.FindAsync(activity.Id);
        Assert.Null(deletedEntity);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task DeleteProgrammingActivity()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        // Arrange
        var user = UserTestSeeds.UserEntityJanko;
        var activity = ActivityTestSeeds.ActivityEntityProgramming;
        var project = ProjectTestSeeds.ProjectEntityICS;

        _dbContextSUT.Users.Add(user);
        _dbContextSUT.Projects.Add(project);
        _dbContextSUT.Activities.Add(activity);
        await _dbContextSUT.SaveChangesAsync();

        // Act
        _dbContextSUT.Activities.Remove(activity);
        await _dbContextSUT.SaveChangesAsync();

        // Assert
        var deletedEntity = await _dbContextSUT.Activities.FindAsync(activity.Id);
        Assert.Null(deletedEntity);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }


    [Fact]
    public async Task AddProjectIPPProject()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        //Arrange
        var project = new ProjectEntity()
        {
            Id = Guid.Parse("9c7c4b6f-6286-4b5d-aa7f-59a3f80f862f"),
            Activities = new List<ActivityEntity>(),
            Name = "IPPProject",
            Users = new List<ProjectUserEntity>()
        };

        //Act
        _dbContextSUT.Projects.Add(project);
        await _dbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects
            .SingleAsync(i => i.Id == project.Id);
        Assert.Equal(project.Id, actualEntity.Id);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task AddProjectFromTestSeeds()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        //Arrange
        var project = ProjectTestSeeds.ProjectEntityICS;

        //Act
        _dbContextSUT.Projects.Add(project);
        await _dbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects
            .SingleAsync(i => i.Id == project.Id);
        Assert.Equal(project.Id, actualEntity.Id);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task DeleteIPPProject()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        // Arrange
        var projectId = Guid.Parse("9c7c4b6f-6286-4b5d-aa7f-59a3f80f862f");
        var project = new ProjectEntity()
        {
            Id = projectId,
            Activities = new List<ActivityEntity>(),
            Name = "IPPProject",
            Users = new List<ProjectUserEntity>()
        };
        _dbContextSUT.Projects.Add(project);
        await _dbContextSUT.SaveChangesAsync();

        // Act
        _dbContextSUT.Projects.Remove(project);
        await _dbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects.FindAsync(projectId);
        Assert.Null(actualEntity);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task AddTagFromSeedsFreetime()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        //Arrange
        var tag = TagTestSeeds.TagEntityFreetime;

        //Act
        _dbContextSUT.Tag.Add(tag);
        await _dbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Tag
            .SingleAsync(i => i.Id == tag.Id);
        Assert.Equal(tag.Id, actualEntity.Id);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task AddTagFromSeedsWork()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        //Arrange
        var tag = TagTestSeeds.TagEntityWork;

        //Act
        _dbContextSUT.Tag.Add(tag);
        await _dbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Tag
            .SingleAsync(i => i.Id == tag.Id);
        Assert.Equal(tag.Id, actualEntity.Id);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task DeleteTagFromSeeds()
    {
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();

        // Arrange
        var tag = TagTestSeeds.TagEntityWork;
        _dbContextSUT.Tag.Add(tag);
        await _dbContextSUT.SaveChangesAsync();

        // Act
        _dbContextSUT.Tag.Remove(tag);
        await _dbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Tag.FindAsync(tag.Id);
        Assert.Null(actualEntity);

        await _dbContextSUT.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task GetAllUsers()
    {
        // Arrange
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();
        var users = new List<UserEntity>{
            new UserEntity()
        {
            Id = Guid.Parse(input: "56e671cb-1b10-4229-97b2-55c767ae8f24"),
            Name = "Ondrej",
            Surname = "Novak",
            ImageURL = @"https://static.gigwise.com/artists/Editors_7_quesada_750.jpg"
        },
            new UserEntity()
        {
            Id = Guid.Parse(input: "3ef3d7f5-6d0b-4d13-b3b3-81b2385e5f6e"),
            Name = "Marek",
            Surname = "Spirka",
            ImageURL = @"https://static.gigwise.com/artists/Editors_7_quesada_750.jpg"
        }, };

        await _dbContextSUT.Users.AddRangeAsync(users);
        await _dbContextSUT.SaveChangesAsync();

        // Act
        var result = await _dbContextSUT.Users.ToListAsync();

        // Assert
        Assert.Equal(users.Count, result.Count);
        Assert.All(users, u => Assert.Contains(u, result));
    }

    [Fact]
    public async Task GetUserById()
    {
        // Arrange
        await _dbContextSUT.Database.EnsureDeletedAsync();
        await _dbContextSUT.Database.EnsureCreatedAsync();
        var user = new UserEntity()
        {
            Id = Guid.Parse(input: "2d2b618e-19f6-4d22-9921-36a4c4ec4f8b"),
            Name = "Katerina",
            Surname = "Lojdova",
            ImageURL = @"https://static.gigwise.com/artists/Editors_7_quesada_750.jpg"
        };

        await _dbContextSUT.Users.AddAsync(user);
        await _dbContextSUT.SaveChangesAsync();

        // Act
        var result = await _dbContextSUT.Users.FindAsync(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Name, result.Name);
        Assert.Equal(user.Surname, result.Surname);
    }
}
