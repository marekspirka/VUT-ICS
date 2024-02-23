using ICS_project.BL.Models;

namespace ICS_project.BL.Facades;

public interface IActivityFacade
{
    Task DeleteAsync(Guid id);
    Task<ActivityDetailModel?> GetAsync(Guid id);
    Task<IEnumerable<ActivityDetailModel>> GetUsersAsync(Guid userId);
    Task<ActivityDetailModel> SaveAsync(ActivityDetailModel model, Guid userId);
    Task<bool> IsTimeSlotFullAsync(Guid userId, Guid id, DateTime start, DateTime end);
}