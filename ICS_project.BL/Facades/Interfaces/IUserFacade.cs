using ICS_project.BL.Models;

namespace ICS_project.BL.Facades;

public interface IUserFacade
{
	Task DeleteAsync(Guid id);
	Task<UserDetailModel?> GetAsync(Guid id);
	Task<IEnumerable<UserDetailModel>> GetAsync();
	Task<UserDetailModel> SaveAsync(UserDetailModel model);
}

