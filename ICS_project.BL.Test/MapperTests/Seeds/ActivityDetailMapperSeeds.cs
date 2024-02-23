using ICS_project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICS_project.BL.Models;

namespace ICS_project.BL.Test.MapperTests.Seeds;

public class ActivityDetailMapperSeeds
{
    ActivityDetailMapperSeeds()
    {
        var activityEntity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Name = "Basketball",
            Start = DateTime.Now,
            End = DateTime.Now.AddHours(2),
            Description = "This is a first test activity",
            ProjectId = Guid.Parse("9c7c4b6f-6286-4b5d-aa7f-59a3f80f862f"),
            Project = new ProjectEntity()
            {
                Id = Guid.Parse("9c7c4b6f-6286-4b5d-aa7f-59a3f80f862f"),
                Activities = new List<ActivityEntity>(),
                Name = "IPPProject",
                Users = new List<ProjectUserEntity>()
            },
            UserId = Guid.Parse(input: "26d8d98a-9d01-4021-a6f8-6a80a2ed77d6"),
            User = new UserEntity
            {
                Id = Guid.Parse(input: "26d8d98a-9d01-4021-a6f8-6a80a2ed77d6"),
                Name = "Marek",
                Surname = "Dobehal",
                ImageURL = @"https://static.gigwise.com/artists/Editors_7_quesada_750.jpg"
            },
            Tags = new List<TagActivityEntity>()
        };

        var activityDetailModel = new ActivityDetailModel
        {
            Id = Guid.NewGuid(),
            Name = "Test Activity",
            Start = DateTime.Now,
            End = DateTime.Now.AddHours(2),
            Description = "This is a long description of very first test activity",
            Project = new ProjectDetailModel()
            {
                Id = Guid.Parse("9c7c4b6f-6286-4b5d-aa7f-59a3f80f862f"),
                Name = "IPPProject",
                Users = new List<UserDetailModel>()
            },
            User = new UserDetailModel()
            {
                Id = Guid.Parse(input: "26d8d98a-9d01-4021-a6f8-6a80a2ed77d6"),
                Name = "Marek",
                Surname = "Dobehal",
            },
            Tags = new List<TagModel>()
        };

    }
}

