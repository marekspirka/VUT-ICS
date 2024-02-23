using ICS_project.BL.Models;

namespace ICS_project.BL.Facades;

public interface ITagFacade
{
    Task DeleteAsync(Guid id);
    Task<TagModel> SaveAsync(TagModel model, Guid userId);
    Task<TagModel?> GetAsync(Guid id);
    Task<IEnumerable<TagModel>> GetAsync();
    Task<IEnumerable<TagModel>> GetUsersAsync(Guid userId);
}