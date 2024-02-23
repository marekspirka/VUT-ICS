using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public class UserDetailModelMapper : IUserDetailModelMapper
{
    public UserDetailModel MapToUserDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                ImageUrl = entity.ImageURL
            };

    public IEnumerable<UserDetailModel> MapToUserDetailListModel(IEnumerable<UserEntity> entities)
        => entities.Select(MapToUserDetailModelFromList);

    public UserDetailModel MapToUserDetailModelFromList(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                ImageUrl = entity.ImageURL
            };

    public UserEntity MapToUserEntity(UserDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Surname = model.Surname,
            ImageURL = model.ImageUrl
        };
}