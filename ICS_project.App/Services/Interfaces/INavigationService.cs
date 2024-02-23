using ICS_project.App.Models;
using ICS_project.App.ViewModels;

namespace ICS_project.App.Services;

public interface INavigationService
{
    IEnumerable<RouteModel> Routes { get; }

    Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel;

    Task GoToAsync(string route);

    bool BackButtonPressed();

    Task GoToAsync(string route, IDictionary<string, object?> parameters);

    Task GoToAsync<TVieMode>()
        where TVieMode : IViewModel;
}