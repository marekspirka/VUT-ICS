using Xunit;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.BL.Mappers;
using ICS_project.Common.Test;

namespace ICS_project.BL.Tests;

public class FilterModelMapperTests
{
    [Fact]
    public void MapToFilterModel_MapEntitiesToModel_NewModel()
    {
        // Arrange
        var entities = new List<ActivityEntity>
        {
            new ActivityEntity
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Parse("08/11/2018 07:22:16"),
                End = DateTime.Parse("08/11/2018 09:22:16"),
                Name = "Skiing",
                ProjectId = Guid.Parse("13c7d15b-6e0d-4f17-84d7-f35610cfceab"),
                Project = new ProjectEntity
                {
                    Id = Guid.Parse("13c7d15b-6e0d-4f17-84d7-f35610cfceab"),
                    Name = "SuperSecret"
                },
                UserId = Guid.Parse("e85b9eb9-7f2c-4a75-a6b5-232bbd335233"),
                User = new UserEntity
                {
                    Id = Guid.Parse("e85b9eb9-7f2c-4a75-a6b5-232bbd335233"),
                    Name = "Rebecca",
                    Surname = "Novakova"
                }
            },
            new ActivityEntity
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Parse("07/12/2018 07:22:16"),
                End = DateTime.Parse("09/12/2018 09:22:16"),
                Name = "Longboarding",
                ProjectId = Guid.Parse("1ef21b7d-5c9e-47cc-aec9-29141ed5a952"),
                Project = new ProjectEntity
                {
                    Id = Guid.Parse("1ef21b7d-5c9e-47cc-aec9-29141ed5a952"),
                    Name = "Work"
                },
                UserId = Guid.Parse("259b7d24-678e-4a53-b2d9-3cb9e3a3ebbb"),
                User = new UserEntity
                {
                    Id = Guid.Parse("259b7d24-678e-4a53-b2d9-3cb9e3a3ebbb"),
                    Name = "Jan",
                    Surname = "Novak"
                }
            }
        };
        var mapper = new FilterModelMapper(new ProjectDetailModelMapper(new UserDetailModelMapper()), new TagModelMapper(), new UserDetailModelMapper());

        // Act
        var models = mapper.MapToFilterModel(entities);

        // Assert
        DeepAssert.Equal(entities.Count, models.Count());

        List<FilterModel> modelList = models.ToList();
        for (int i = 0; i < entities.Count(); i++)
        {
            DeepAssert.Equal(entities[i].Id, modelList[i].Id);
            DeepAssert.Equal(entities[i].Start, modelList[i].Start);
            DeepAssert.Equal(entities[i].End, modelList[i].End);
            DeepAssert.Equal(entities[i].Project.Id, modelList[i].Project.Id);
        }
    }

    [Fact]
    public void MapToFilterModel_MapEntityToModel_NewModel()
    {
        // Arrange
        var entity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Start = DateTime.Parse("07/08/2018 07:22:16"),
            End = DateTime.Parse("09/08/2018 09:22:16"),
            Name = "Homework",
            ProjectId = Guid.Parse("522e222a-60d7-4475-913a-c1d2c1e18787"),
            Project = new ProjectEntity
            {
                Id = Guid.Parse("522e222a-60d7-4475-913a-c1d2c1e18787"),
                Name = "School"
            },
            UserId = Guid.Parse("33af3f36-c26f-48af-a58e-85c832da5e5c"),
            User = new UserEntity
            {
                Id = Guid.Parse("33af3f36-c26f-48af-a58e-85c832da5e5c"),
                Name = "Ash",
                Surname = "Pikachu"
            }
        };
        var mapper = new FilterModelMapper(new ProjectDetailModelMapper(new UserDetailModelMapper()), new TagModelMapper(), new UserDetailModelMapper());

        // Act
        var model = mapper.MapToFilterModel(entity);

        // Assert
        DeepAssert.Equal(entity.Id, model.Id);
        DeepAssert.Equal(entity.Start, model.Start);
        DeepAssert.Equal(entity.End, model.End);
        DeepAssert.Equal(entity.Project.Id, model.Project.Id);
    }

    [Fact]
    public void MapToActivityEntity_MapModelToEntity_NewEntity()
    {
        // Arrange
        var model = new FilterModel
        {
            Id = Guid.NewGuid(),
            Start = DateTime.Parse("07/08/2020 07:22:16"),
            End = DateTime.Parse("09/08/2020 09:22:16"),
            Project = new ProjectDetailModel()
            {
                Id = Guid.Parse("db9b4755-5b5c-4215-87b8-501a4d547a63"),
                Name = "Ring destruction"
            },
            User = new UserDetailModel()
            {
                Id = Guid.Parse("42ca7e88-9b31-4c63-af13-6a0a8c90d903"),
                Name = "Bilbo",
                Surname = "Baggins"
            }
        };
        var mapper = new FilterModelMapper(new ProjectDetailModelMapper(new UserDetailModelMapper()), new TagModelMapper(), new UserDetailModelMapper());

        // Act
        var entity = mapper.MapToActivityEntity(model);

        // Assert
        DeepAssert.Equal(model.Id, entity.Id);
        DeepAssert.Equal(model.Start, entity.Start);
        DeepAssert.Equal(model.End, entity.End);
        DeepAssert.Equal(model.Project.Id, entity.Project.Id);
        DeepAssert.Equal(model.User.Id, entity.User.Id);
        DeepAssert.Equal(model.User.Name, entity.User.Name);
    }
}
