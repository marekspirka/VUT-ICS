using CommunityToolkit.Mvvm.Input;
using ICS_project.App.Services;
using ICS_project.App.ViewModels;

namespace ICS_project.App;

public partial class AppShell
{
    private readonly INavigationService _navigationService;
    
    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;
    
        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToActivitiesAsync()
        => await _navigationService.GoToAsync<ActivityListViewModel>();

    [RelayCommand]
    private async Task GoToProjectsAsync()
        => await _navigationService.GoToAsync<ProjectListViewModel>();
    
    [RelayCommand]
    private async Task GoToTagsAsync()
        => await _navigationService.GoToAsync<TagListViewModel>();

    [RelayCommand]
    private async Task GoToUsersAsync()
        => await _navigationService.GoToAsync<UserListViewModel>();
}