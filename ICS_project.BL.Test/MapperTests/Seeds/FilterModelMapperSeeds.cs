using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_project.BL.Test.MapperTests.Seeds;

public class FilterModelMapperSeeds
{
    public FilterModelMapperSeeds()
    {

        var activities = new List<ActivityEntity>
            {
                new ActivityEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Activity 1",
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(2),
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
                },
                new ActivityEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Activity 2",
                    Start = DateTime.Now.AddDays(-1),
                    End = DateTime.Now.AddDays(-1).AddHours(3),
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
                },
                new ActivityEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Activity 3",
                    Start = DateTime.Now.AddDays(-2),
                    End = DateTime.Now.AddDays(-2).AddHours(4),
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
                }
            };

        var filterModels = activities.Select(a => new FilterModel
        {
            Id = a.Id,
            Start = a.Start,
            End = a.End,
            //Project = a.Project,
            //Tags = a.Tags
        });

    }
}
