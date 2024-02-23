using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public interface IProjectListModelMapper
{
    IEnumerable<ProjectListModel> MapToProjectListModel(IEnumerable<ProjectEntity> entities);
    ProjectListModel MapToProjectListModelFromList(ProjectEntity? entity);
    ProjectEntity MapToProjectEntity(ProjectListModel model);
}