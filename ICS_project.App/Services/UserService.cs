using ICS_project.BL.Models;

namespace ICS_project.App.Services;

public class UserService : IUserService
{
    public UserDetailModel CurrentUser { get; set; } = UserDetailModel.Empty;

}