using Xunit;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.BL.Mappers;
using ICS_project.Common.Test;

namespace ICS_project.BL.Tests;

public class ProjectListModelMapperTests
{
    [Fact]
    public void MapToProjectListModel_MapEntitiesToModel_NewModel()
    {
        // Arrange
        var entities = new List<ProjectEntity>
        {
            new ProjectEntity
            {
                Id = Guid.NewGuid(),
                Name = "Project 1"
            },
            new ProjectEntity
            {
                Id = Guid.NewGuid(),
                Name = "Project 2"
            },
            new ProjectEntity
            {
                Id = Guid.NewGuid(),
                Name = "Project 3"
            },
        };
        var mapper = new ProjectListModelMapper();

        // Act
        var models = mapper.MapToProjectListModel(entities);

        // Assert
        DeepAssert.Equal(entities.Count, models.Count());

        List<ProjectListModel> modelList = models.ToList();
        for (int i = 0; i < entities.Count(); i++)
        {
            DeepAssert.Equal(entities[i].Id, modelList[i].Id);
            DeepAssert.Equal(entities[i].Name, modelList[i].Name);
        }
    }

    [Fact]
    public void MapToProjectListModel_MapEntityToModel_NewModel()
    {
        // Arrange
        var entity = new ProjectEntity
        {
            Id = Guid.NewGuid(),
            Name = "Project 1",
        };
        var mapper = new ProjectListModelMapper();

        // Act
        var model = mapper.MapToProjectListModelFromList(entity);

        // Assert
        DeepAssert.Equal(entity.Id, model.Id);
        DeepAssert.Equal(entity.Name, model.Name);
    }

    [Fact]
    public void MapToProjectEntity_MapModelToEntity_NewEntity()
    {
        // Arrange
        var model = new ProjectListModel
        {
            Id = Guid.NewGuid(),
            Name = "Project 1",
        };
        var mapper = new ProjectListModelMapper();

        // Act
        var entity = mapper.MapToProjectEntity(model);

        // Assert
        DeepAssert.Equal(model.Id, entity.Id);
        DeepAssert.Equal(model.Name, entity.Name);
    }
}
