using ICS_project.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_project.BL.Test.MapperTests.Seeds;

public class TagMapperSeeds
{
    public TagMapperSeeds() 
    {
        var tag1 = new TagModel { Id = Guid.NewGuid(), Name = "TagA" };
        var tag2 = new TagModel { Id = Guid.NewGuid(), Name = "TagB" };
        var tag3 = new TagModel { Id = Guid.NewGuid(), Name = "TagC" };
        var tag4 = new TagModel { Id = Guid.NewGuid(), Name = "TagD" };
        var tag5 = new TagModel { Id = Guid.NewGuid(), Name = "TagE" };
        var tag6 = new TagModel { Id = Guid.NewGuid(), Name = "TagF" };
    }
}

