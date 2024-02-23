using ICS_project.BL.Facades;
using ICS_project.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using ICS_project.DAL.Entities;
using ICS_project.BL.Mappers;
using ICS_project.Common.Test;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.BL.Tests.FacadeTests;

public class ProjectFacadeTests : FacadeTestsBase
{
    private readonly IProjectFacade _projectFacadeSUT;

    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectDetailModelMapper, ProjectListModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        // Arrange
        var project = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Get Help pls",
        };

        // Act
        project = await _projectFacadeSUT.SaveAsync(project);
    }

    [Fact]
    public async Task NewProject_Insert_ProjectAdded()
    {
        // Arrange
        var project = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Get Help pls",
            Users = new List<UserDetailModel>(),
        };

        // Act
        project = await _projectFacadeSUT.SaveAsync(project);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var projectFromDb = await dbxAssert.Projects.SingleAsync(i => i.Id == project.Id);
        DeepAssert.Equal(project, ProjectDetailModelMapper.MapToProjectDetailModel(projectFromDb));
    }

    [Fact]
    public async Task Get_ProjectEntity()
    {
        // Arrange
        var project = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Animating",
            Users = new List<UserDetailModel>(),
        };
        project = await _projectFacadeSUT.SaveAsync(project);

        // Act
        var projectGet = await _projectFacadeSUT.GetAsync(project.Id);

        // Assert
        DeepAssert.Equal(project, projectGet);
        DeepAssert.Equal(project.Id, projectGet.Id);
        DeepAssert.Equal(project.Name, projectGet.Name);
    }

    [Fact]
    public async Task Get_ProjectList()
    {
        // Arrange
        var project1 = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Animating",
        };
        project1 = await _projectFacadeSUT.SaveAsync(project1);

        var project2 = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Drawing",
        };
        project2 = await _projectFacadeSUT.SaveAsync(project2);

        // Act
        var projectsGet = await _projectFacadeSUT.GetAsync();

        // Assert
        List<ProjectListModel> projectsList = projectsGet.ToList();

        DeepAssert.Equal(project1.Id, projectsList[0].Id);
        DeepAssert.Equal(project1.Name, projectsList[0].Name);

        DeepAssert.Equal(project2.Id, projectsList[1].Id);
        DeepAssert.Equal(project2.Name, projectsList[1].Name);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        // Arrange
        var project = new ProjectDetailModel()
        {
            Id = default,
            Name = default!,
        };

        // Act
        project = await _projectFacadeSUT.GetAsync(project.Id);

        // Assert
        Assert.Null(project);
    }

    [Fact]
    public async Task UpdateProject_UpDatedProject()
    {
        // Arrange
        var project = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "Crying",
        };

        //Act
        project = await _projectFacadeSUT.SaveAsync(project);
        var new_project = await _projectFacadeSUT.GetAsync(project.Id);

        new_project.Name += " in the corner";

        new_project = await _projectFacadeSUT.SaveAsync(new_project);

        var updated_project = await _projectFacadeSUT.GetAsync(new_project.Id);

        //Assert
        DeepAssert.Equal(updated_project.Name, "Crying in the corner");
    }

    [Fact]
    public async Task Project_DeleteById()
    {
        // Arrange
        var project = new ProjectDetailModel()
        {
            Id = Guid.NewGuid(),
            Name = "ICS",
        };
        project = await _projectFacadeSUT.SaveAsync(project);

        // Act
        await _projectFacadeSUT.DeleteAsync(project.Id);

        // Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Tag.AnyAsync(i => i.Id == project.Id));
    }
}

