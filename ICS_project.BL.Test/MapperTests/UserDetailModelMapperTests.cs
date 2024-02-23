using Xunit;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.BL.Mappers;
using ICS_project.Common.Test;

namespace ICS_project.BL.Tests;

public class UserDetailModelMapperTests
{
    [Fact]
    public void MapToUserDetailModel_MapEntityToModel_NewModel()
    {
        // Arrange
        var entity = new UserEntity
        {
            Id = Guid.Parse("31ad2414-64d7-4e08-89ba-398b82e99b94"),
            Name = "Gandalf",
            Surname = "Grey",
            ImageURL = "https://example.com/Gandalf.jpg"
        };
        var mapper = new UserDetailModelMapper();

        // Act
        var model = mapper.MapToUserDetailModel(entity);

        // Assert
        DeepAssert.Equal(entity.Id, model.Id);
        DeepAssert.Equal(entity.Name, model.Name);
        DeepAssert.Equal(entity.Surname, model.Surname);
        DeepAssert.Equal(entity.ImageURL, model.ImageUrl);
    }

    [Fact]
    public void MapToUserDetailListModel_MapEntitiesToModel_NewModel()
    {
        // Arrange
        var entities = new List<UserEntity>
        {
            new UserEntity
            {
                Id = Guid.Parse("03e281ac-7472-412a-8038-b90936595789"),
                Name = "Saruman",
                Surname = "White",
                ImageURL = "https://example.com/Saruman.jpg"
            },
            new UserEntity
            {
                Id = Guid.Parse("f36f7d98-8c0c-4c53-8a96-450602c471a1"),
                Name = "Frodo",
                Surname = "Baggins",
                ImageURL = "https://example.com/Frodo.jpg"
            }
        };
        var mapper = new UserDetailModelMapper();

        // Act
        var models = mapper.MapToUserDetailListModel(entities);

        // Assert
        DeepAssert.Equal(entities.Count, models.Count());

        List<UserDetailModel> modelList = models.ToList();
        for (int i = 0; i < entities.Count(); i++)
        {
            DeepAssert.Equal(entities[i].Id, modelList[i].Id);
            DeepAssert.Equal(entities[i].Name, modelList[i].Name);
            DeepAssert.Equal(entities[i].Surname, modelList[i].Surname);
            DeepAssert.Equal(entities[i].ImageURL, modelList[i].ImageUrl);
        }
    }

    [Fact]
    public void MapToUserDetailModelFromList_MapEntityToModel_NewModel()
    {
        // Arrange
        var entity = new UserEntity
        {
            Id = Guid.Parse("7409aae6-b9e2-4c2a-9be5-c3901c51aea2"),
            Name = "Aragorn",
            Surname = "II",
            ImageURL = "https://example.com/Aragorn.jpg"
        };
        var mapper = new UserDetailModelMapper();

        // Act
        var model = mapper.MapToUserDetailModelFromList(entity);

        // Assert
        DeepAssert.Equal(entity.Id, model.Id);
        DeepAssert.Equal(entity.Name, model.Name);
        DeepAssert.Equal(entity.Surname, model.Surname);
        DeepAssert.Equal(entity.ImageURL, model.ImageUrl);
    }

    [Fact]
    public void MapToUserEntity_MapModelToEntity_NewEntity()
    {
        // Arrange
        var model = new UserDetailModel
        {
            Id = Guid.Parse("a5c1639a-d2cf-406b-a7ab-3380eab6602e"),
            Name = "Arwen",
            Surname = "Evenstar",
            ImageUrl = "https://example.com/Arwen.jpg"
        };
        var mapper = new UserDetailModelMapper();

        // Act
        var entity = mapper.MapToUserEntity(model);

        // Assert
        DeepAssert.Equal(model.Id, entity.Id);
        DeepAssert.Equal(model.Name, entity.Name);
        DeepAssert.Equal(model.Surname, entity.Surname);
        DeepAssert.Equal(model.ImageUrl, entity.ImageURL);
    }
}

