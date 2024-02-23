using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public interface IUserDetailModelMapper
{
    UserDetailModel MapToUserDetailModel(UserEntity? entity);
    IEnumerable<UserDetailModel> MapToUserDetailListModel(IEnumerable<UserEntity> entities);
    UserDetailModel MapToUserDetailModelFromList(UserEntity? entity);
    UserEntity MapToUserEntity(UserDetailModel model);
}