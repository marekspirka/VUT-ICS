using Xunit;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.BL.Mappers;
using ICS_project.Common.Test;

namespace ICS_project.BL.Tests;

public class ProjectDetailModelMapperTests
{
    [Fact]
    public void MapToProjectDetailModel_MapEntityToModel_NewModel()
    {
        // Arrange
        var entity = new ProjectEntity
        {
            Id = Guid.NewGuid(),
            Name = "Save the world",
            Users = new List<ProjectUserEntity>(),
            Activities = new List<ActivityEntity>()
        };
        var mapper = new ProjectDetailModelMapper(new UserDetailModelMapper());

        // Act
        var model = mapper.MapToProjectDetailModel(entity);

        // Assert
        DeepAssert.Equal(entity.Id, model.Id);
        DeepAssert.Equal(entity.Name, model.Name);
        //DeepAssert.Equal(entity.Users, model.Users);
        //DeepAssert.Equal(entity.Activities, model.Activities);
    }

    [Fact]
    public void MapToProjectEntity_MapModelToEntity_NewEntity()
    {
        // Arrange
        var model = new ProjectDetailModel
        {
            Id = Guid.NewGuid(),
            Name = "Get Help pls",
            Users = new List<UserDetailModel>(),
        };
        var mapper = new ProjectDetailModelMapper(new UserDetailModelMapper());

        // Act
        var entity = mapper.MapToProjectEntity(model);

        // Assert
        DeepAssert.Equal(model.Id, entity.Id);
        DeepAssert.Equal(model.Name, entity.Name);
        //DeepAssert.Equal(model.Users, entity.Users);
        //DeepAssert.Equal(model.Activities, entity.Activities);
    }
}