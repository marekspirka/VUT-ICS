
using System.Diagnostics;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;
using ICS_project.Common.Test;
using ICS_project.DAL.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using ICS_project.Common.Test.Seeds;
using Xunit;
using Xunit.Abstractions;


namespace ICS_project.BL.Tests.FacadeTests;

public class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;

    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserDetailModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = "Marek",
            Surname = "Preskocil",
        };

        //Act
        user = await _userFacadeSUT.SaveAsync(user);
    }

    [Fact]
    public async Task NewUser_Insert_UserAdded()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = "Marek",
            Surname = "Preskocil",
        };

        //Act
        user = await _userFacadeSUT.SaveAsync(user);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var userFromDb = await dbxAssert.Users.SingleAsync(i => i.Id == user.Id);
        DeepAssert.Equal(user, UserDetailModelMapper.MapToUserDetailModel(userFromDb));
    }

    [Fact]
    public async Task NewUser_FindUser_UserFound()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = Guid.Parse(input: "9999d98b-9d01-4021-a6c8-6a80a2ed77d6"),
            Name = @"Jozko",
            Surname = @"Zakocil",
        };

        //Act
        user = await _userFacadeSUT.SaveAsync(user);

        //Assert
        var user_from_database = await _userFacadeSUT.GetAsync(user.Id);
        DeepAssert.Equal(user_from_database.Id, user.Id);
    }


    [Fact]
    public async Task GetById_NonExistent()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = default!,
            Name = default!,
            Surname = default!,
            ImageUrl = default!
        };

        //Act
        var empty_user = await _userFacadeSUT.GetAsync(user.Id);

        //Assert
        Assert.Null(empty_user);
    }

    [Fact]
    public async Task CreateUser_DeleteUserByID_DeletedUser()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = Guid.Parse(input: "8888d98b-9d01-4021-a6c8-6a80a2ed77d6"),
            Name = @"Jozko",
            Surname = @"Vyskocil",
        };

        //Act
        user = await _userFacadeSUT.SaveAsync(user);
        await _userFacadeSUT.DeleteAsync(user.Id);

        //Assert
        var deleted_user = await _userFacadeSUT.GetAsync(user.Id);
        Assert.Null(deleted_user);
    }

    [Fact]
    public async Task CreateUser_UpdateUser_UpdatedUser()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = @"Stef",
            Surname = @"Zmr",
        };

        //Act
        user = await _userFacadeSUT.SaveAsync(user);
        var new_user = await _userFacadeSUT.GetAsync(user.Id);

        new_user.Name += "an";
        new_user.Surname += "zol";

        new_user = await _userFacadeSUT.SaveAsync(new_user);

        var updated_user = await _userFacadeSUT.GetAsync(new_user.Id);

        //Assert
        DeepAssert.Equal(updated_user.Name, "Stefan");
        DeepAssert.Equal(updated_user.Surname, "Zmrzol");
    }

    [Fact]
    public async Task CreateUser_AddUrlImage_AddedImgUrl()
    {
        //Arrange
        var user = new UserDetailModel()
        {
            Id = Guid.Empty,
            Name = @"Gustav",
            Surname = @"Radiator",
        };
        //Act
        user = await _userFacadeSUT.SaveAsync(user);
        var new_user = await _userFacadeSUT.GetAsync(user.Id);

        new_user.ImageUrl += @"https://static.gigwise.com/artists/Editors_7_quesada_750.jpg";

        new_user = await _userFacadeSUT.SaveAsync(new_user);

        var updated_user = await _userFacadeSUT.GetAsync(new_user.Id);

        //Assert
        DeepAssert.Equal(updated_user.ImageUrl, "https://static.gigwise.com/artists/Editors_7_quesada_750.jpg");
    }
}