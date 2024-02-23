using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public interface IActivityDetailModelMapper
{
    ActivityDetailModel MapToActivityDetailModel(ActivityEntity? entity);
    //ActivityEntity MapToActivityEntity(ActivityDetailModel model);
    IEnumerable<ActivityDetailModel> MapToActivityListModel(IEnumerable<ActivityEntity> entities);
    ActivityEntity MapToActivityEntityWithout(ActivityDetailModel model);
    ActivityEntity MapToActivityEntityWithProject(ActivityDetailModel model);
    ActivityEntity MapToActivityEntityWithTags(ActivityDetailModel model);
    ActivityEntity MapToActivityEntityWithTagsProject(ActivityDetailModel model);
}