using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

public interface IProjectDetailModelMapper
{
    public ProjectDetailModel MapToProjectDetailModel(ProjectEntity? entity);
    public ProjectEntity MapToProjectEntity(ProjectDetailModel model);
}