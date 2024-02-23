using Xunit;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.BL.Mappers;
using ICS_project.Common.Test;

namespace ICS_project.BL.Tests;

public class TagModelMapperTests
{
    [Fact]
    public void MapToTagModel_MapEntityToModel_NewModel()
    {
        // Arrange
        var entity = new TagEntity
        {
            Id = Guid.NewGuid(),
            Name = "Music"
        };
        var mapper = new TagModelMapper();

        // Act
        var model = mapper.MapToTagModel(entity);

        // Assert
        DeepAssert.Equal(entity.Id, model.Id);
        DeepAssert.Equal(entity.Name, model.Name);
    }

    [Fact]
    public void MapToTagEntity_MapModelToEntity_NewEntity()
    {
        // Arrange
        var model = new TagModel
        {
            Id = Guid.NewGuid(),
            Name = "School"
        };
        var mapper = new TagModelMapper();

        // Act
        var entity = mapper.MapToTagEntity(model);

        // Assert
        DeepAssert.Equal(model.Id, entity.Id);
        DeepAssert.Equal(model.Name, entity.Name);
    }
}