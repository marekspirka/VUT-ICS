using ICS_project.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_project.BL.Test.MapperTests.Seeds;
internal class UserDetailMapperSeeds
{
    public UserDetailMapperSeeds()
    {
        var userDetail1 = new UserDetailModel { Id = Guid.NewGuid(), Name = "John", Surname = "Doe", ImageUrl = "https://example.com/johndoe.jpg" };
        var userDetail2 = new UserDetailModel { Id = Guid.NewGuid(), Name = "Jane", Surname = "Doe", ImageUrl = "https://example.com/janedoe.jpg" };
        var userDetail3 = new UserDetailModel { Id = Guid.NewGuid(), Name = "Alice", Surname = "Smith", ImageUrl = "https://example.com/alicesmith.jpg" };
        var userDetail4 = new UserDetailModel { Id = Guid.NewGuid(), Name = "Bob", Surname = "Smith", ImageUrl = "https://example.com/bobsmith.jpg" };
        var userDetail5 = new UserDetailModel { Id = Guid.NewGuid(), Name = "Emily", Surname = "Jones", ImageUrl = "https://example.com/emilyjones.jpg" };
        var userDetail6 = new UserDetailModel { Id = Guid.NewGuid(), Name = "David", Surname = "Jones", ImageUrl = "https://example.com/davidjones.jpg" };
    }
}

