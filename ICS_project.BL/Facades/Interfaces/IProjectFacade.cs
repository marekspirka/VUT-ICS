using ICS_project.BL.Models;

namespace ICS_project.BL.Facades;

public interface IProjectFacade
{
    Task DeleteAsync(Guid id);
    Task<ProjectDetailModel?> GetAsync(Guid id);
    Task<IList<ProjectListModel>> GetAsync();
    Task<IList<ProjectListModel>> GetUsersAsync(Guid userId);
    Task<bool> IsUserInProject(Guid userId, Guid projectId);
    Task<bool> ToggleProjectJoin(Guid userId, Guid projectId);
    Task<ProjectDetailModel> SaveAsync(ProjectDetailModel model);
}