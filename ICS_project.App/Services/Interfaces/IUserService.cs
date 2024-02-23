using ICS_project.BL.Models;

namespace ICS_project.App.Services;

public interface IUserService
{
    public UserDetailModel CurrentUser { get; set; }
}