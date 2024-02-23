using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public class ProjectListModelMapper : IProjectListModelMapper
{
    public IEnumerable<ProjectListModel> MapToProjectListModel(IEnumerable<ProjectEntity> entities)
        => entities.Select(MapToProjectListModelFromList);

    public ProjectListModel MapToProjectListModelFromList(ProjectEntity? entity)
        => entity is null
            ? ProjectListModel.Empty
            : new ProjectListModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

    public ProjectEntity MapToProjectEntity(ProjectListModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name
        };
}