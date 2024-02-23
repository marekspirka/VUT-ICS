using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_project.BL.Test.MapperTests.Seeds;
public class ProjectDetailMapperSeeds
{
    public ProjectDetailMapperSeeds()
    {
        var project1 = new ProjectDetailModel
        {
            Id = Guid.Parse("9c7c4b6f-6286-4b5d-aa7f-59a3f80f862f"),
            Name = "Project 1",
            Users = new List<UserDetailModel>
            {
                new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Jan",
                    Surname = "Novak"
                }
            },
        };

        var project2 = new ProjectDetailModel
        {
            Id = Guid.Parse("cec87981-5b06-40b7-9a03-01c186d1618d"),
            Name = "Project 1",
            Users = new List<UserDetailModel>
            {
                new UserDetailModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Jan",
                    Surname = "Novak"
                }
            },
        };

        List<ProjectDetailModel> seedData = new List<ProjectDetailModel> { project1, project2 };


    }
}
