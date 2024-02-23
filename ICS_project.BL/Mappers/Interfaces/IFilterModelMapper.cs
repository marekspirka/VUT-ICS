using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

public interface IFilterModelMapper
{
    IEnumerable<FilterModel> MapToFilterModel(IEnumerable<ActivityEntity> entities);
    FilterModel MapToFilterModel(ActivityEntity? entity);
    ActivityEntity MapToActivityEntity(FilterModel model);
}