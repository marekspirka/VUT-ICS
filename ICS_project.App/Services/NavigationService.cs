using ICS_project.App.Models;
using ICS_project.App.ViewModels;
using ICS_project.App.Views.Activities;
using ICS_project.App.Views.Projects;
using ICS_project.App.Views.Tags;
using ICS_project.App.Views.Users;

namespace ICS_project.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(UserListView), typeof(UserListViewModel)),
        new("//users/edit", typeof(UserEditView), typeof(UserEditViewModel)),

        new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        new("//activities/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)), 
        new("//activities/filters/edit", typeof(ActivityFilterEditView), typeof(ActivityFilterEditViewModel)),
        
        new("//projects", typeof(ProjectListView), typeof(ProjectListViewModel)), 
        new("//projects/edit", typeof(ProjectEditView), typeof(ProjectEditViewModel)),

        new("//tags", typeof(TagListView), typeof(TagListViewModel)),
        new("//tags/edit", typeof(TagEditView), typeof(TagEditViewModel)),
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool BackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}