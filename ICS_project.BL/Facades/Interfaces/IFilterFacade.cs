using ICS_project.BL.Models;

namespace ICS_project.BL.Facades;

public interface IFilterFacade
{
    Task<IEnumerable<ActivityDetailModel>> GetByEverythingAsync(Guid userId, DateTime? startDate, DateTime? endDate, DateTime startTime, DateTime endTime, Guid? tagId, Guid? projectId);

}