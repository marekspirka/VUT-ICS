using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public interface ITagModelMapper
{
    TagModel MapToTagModel(TagEntity? entity);
    TagEntity MapToTagEntity(TagModel model);
    IEnumerable<TagModel> MapToTagListModel(IEnumerable<TagEntity> entities);
    TagModel MapToTagModelFromList(TagEntity? entity);
}