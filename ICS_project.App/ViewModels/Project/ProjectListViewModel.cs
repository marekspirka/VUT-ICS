using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectListViewModel : ViewModelBase, IRecipient<ProjectEditMessage>, IRecipient<ProjectDeleteMessage>, IRecipient<ProjectMyProjectDeleteMessage>, IRecipient<ProjectMyProjectEditMessage>
{
    private readonly IProjectFacade projectFacade;
    private readonly INavigationService navigationService;

    public IList<ProjectListModel> Projects { get; set; } = null!;
    public Guid Id { get; set; }

    public override IUserService UserService { get; }

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IUserService userService)
        : base(messengerService)
    {
        this.projectFacade = projectFacade;
        this.navigationService = navigationService;

        UserService = userService;
    }

    [ObservableProperty] 
    public bool findMyProject;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Projects = await projectFacade.GetAsync();
        for (int i = 0; i < Projects.Count(); i++)
        {
            Projects[i].IsJoined = await projectFacade.IsUserInProject(CurrentUser.Id, Projects[i].Id);
        }
    }

 
    public async void LoadMyProjectAsync()
    {
        await base.LoadDataAsync();
        Projects = await projectFacade.GetUsersAsync(UserService.CurrentUser.Id);
        for (int i = 0; i < Projects.Count(); i++)
        {
            Projects[i].IsJoined = await projectFacade.IsUserInProject(CurrentUser.Id, Projects[i].Id);
        }
    }

    public async void LoadAllProjectsAsync()
    {
        await base.LoadDataAsync();
        Projects = await projectFacade.GetAsync();
        for (int i = 0; i < Projects.Count(); i++)
        {
            Projects[i].IsJoined = await projectFacade.IsUserInProject(CurrentUser.Id, Projects[i].Id);
        }
    }

    [RelayCommand]
    private async Task GoToCreateProjectAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task GoToEditProjectAsync(Guid Id)
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(ProjectEditViewModel.Id)] = Id });
    }

    [RelayCommand]
    private async Task GoToEditUserAsync(Guid userId)
    {
        await navigationService.GoToAsync<UserEditViewModel>(
            new Dictionary<string, object?> { [nameof(UserEditViewModel.Id)] = userId });
    }

    [RelayCommand]
    private async Task GoToChangeUserAsync()
    {
        await navigationService.GoToAsync<UserListViewModel>();
    }

    [RelayCommand]
    private async Task DeleteProjectAsync(Guid Id)
    {
        bool confirmed = await Application.Current.MainPage.DisplayAlert("Delete Project", "Deleting this project will also delete all related activities.\n\nSo are you sure you want to delete this project?", "Yes", "No");

        if (confirmed)
        {
            await projectFacade.DeleteAsync(Id);
            if (FindMyProject == true)
            {
                messengerService.Send(new ProjectMyProjectDeleteMessage());
            }
            else
            {
                messengerService.Send(new ProjectDeleteMessage());
            }
        }

    }

    [RelayCommand]
    private async Task JoinProjectAsync(Guid projectId)
    {
        bool isJoined = await projectFacade.ToggleProjectJoin(CurrentUser.Id, projectId);
        Projects.First(p => p.Id == projectId).IsJoined = isJoined;
        
        if (FindMyProject == true)
        {
            messengerService.Send(new ProjectMyProjectEditMessage() {ProjectId = projectId});
        }
        else
        {
            messengerService.Send(new ProjectEditMessage() { ProjectId = projectId });
        }
    }

    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectMyProjectEditMessage message)
    {
        await base.LoadDataAsync();
        Projects = await projectFacade.GetUsersAsync(UserService.CurrentUser.Id);
    }

    public async void Receive(ProjectMyProjectDeleteMessage message)
    {
        await base.LoadDataAsync();
        Projects = await projectFacade.GetUsersAsync(UserService.CurrentUser.Id);
    }
}
