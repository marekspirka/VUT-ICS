using Xunit;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.BL.Mappers;
using ICS_project.Common.Test;

namespace ICS_project.BL.Tests;

public class ActivityDetailModelMapperTests
{
    [Fact]
    public void MapToActivityDetailModel_MapEntityToModel_NewModel()
    {
        // Arrange
        var entity = new ActivityEntity
        {
            Id = Guid.Parse("7a9b3ed8-fc80-4d6d-9b32-0b8d7e2625b1"),
            Name = "Ghost Busting",
            Start = DateTime.Parse("07/08/2020 07:22:16"),
            End = DateTime.Parse("09/08/2020 09:22:16"),
            Description = "We busted some ghosts",
            ProjectId = Guid.Parse("19d3c256-40b6-49b7-a20c-406b811d51b1"),
            Project = new ProjectEntity
            {
                Id = Guid.Parse("19d3c256-40b6-49b7-a20c-406b811d51b1"),
                Name = "GhostBustersLLC"
            },
            UserId = Guid.Parse("f4a4df4b-4e13-4fcf-958c-985ed7a8a413"),
            User = new UserEntity
            {
                Id = Guid.Parse("f4a4df4b-4e13-4fcf-958c-985ed7a8a413"),
                Name = "Peter",
                Surname = "Venkman"
            },
        };
        var mapper = new ActivityDetailModelMapper(new ProjectDetailModelMapper(new UserDetailModelMapper()), new UserDetailModelMapper(), new TagModelMapper());

        // Act
        var model = mapper.MapToActivityDetailModel(entity);

        // Assert
        DeepAssert.Equal(entity.Id, model.Id);
        DeepAssert.Equal(entity.Name, model.Name);
        DeepAssert.Equal(entity.Start, model.Start);
        DeepAssert.Equal(entity.End, model.End);
        DeepAssert.Equal(entity.Description, model.Description);
        DeepAssert.Equal(entity.Project.Id, model.Project.Id);
        DeepAssert.Equal(entity.User.Id, model.User.Id);
    }

    [Fact]
    public void MapToActivityEntity_MapModelToEntity_NewEntity()
    {
        // Arrange
        var model = new ActivityDetailModel
        {
            Id = Guid.Parse("5b2e6240-e9dc-4e5b-8dde-397c49a507df"),
            Name = "Ghost Hunting",
            Start = DateTime.Parse("07/08/2020 07:22:16"),
            End = DateTime.Parse("09/08/2020 09:22:16"),
            Description = "We Hunted some ghosts",
            Project = new ProjectDetailModel()
            {
                Id = Guid.Parse("808fd09e-bf9d-40a0-ba8f-8f9b8ddf52df"),
                Name = "GhostHuntersLLC"
            },
            User = new UserDetailModel()
            {
                Id = Guid.Parse("50fca578-f173-4f61-bed4-3721d31791d2"),
                Name = "John",
                Surname = "Venkman"
            }
        };
        var mapper = new ActivityDetailModelMapper(new ProjectDetailModelMapper(new UserDetailModelMapper()), new UserDetailModelMapper(), new TagModelMapper());

        // Act
        var entity = mapper.MapToActivityEntity(model);

        // Assert
        DeepAssert.Equal(model.Id, entity.Id);
        DeepAssert.Equal(model.Name, entity.Name);
        DeepAssert.Equal(model.Start, entity.Start);
        DeepAssert.Equal(model.End, entity.End);
        DeepAssert.Equal(model.Description, entity.Description);
        DeepAssert.Equal(model.Project.Id, entity.Project.Id);
        DeepAssert.Equal(model.User.Id, entity.User.Id);
    }
}
