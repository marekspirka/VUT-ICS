using ICS_project.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_project.BL.Test.MapperTests.Seeds;
    public class ProjectListMapperSeeds
    {
        public ProjectListMapperSeeds()
        {
            var project1 = new ProjectListModel { Id = Guid.NewGuid(), Name = "ProjectA" };
            var project2 = new ProjectListModel { Id = Guid.NewGuid(), Name = "ProjectB" };
            var project3 = new ProjectListModel { Id = Guid.NewGuid(), Name = "ProjectC" };
            var project4 = new ProjectListModel { Id = Guid.NewGuid(), Name = "ProjectD" };
            var project5 = new ProjectListModel { Id = Guid.NewGuid(), Name = "ProjectE" };
            var project6 = new ProjectListModel { Id = Guid.NewGuid(), Name = "ProjectF" };

        }
    }

