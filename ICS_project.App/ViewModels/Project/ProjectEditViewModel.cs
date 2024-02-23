using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectEditViewModel : ViewModelBase,
    IRecipient<ProjectEditMessage>,
    IRecipient<ProjectAddMessage>,
    IRecipient<ProjectDeleteMessage>
{
    private readonly IProjectFacade projectFacade;
    private readonly INavigationService navigationService;

    public Guid Id { get; set; }
    public ProjectDetailModel Project { get; private set; }
    public ProjectDetailModel NewProject { get; private set; } = ProjectDetailModel.Empty;

    public override IUserService UserService { get; }

    public ProjectEditViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IUserService userService)
        : base(messengerService)
    {
        this.projectFacade = projectFacade;
        this.navigationService = navigationService;

        this.UserService = userService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await projectFacade.GetAsync(Id);
        if (Project != null)
        {
            NewProject = Project;
        }
    }

    [RelayCommand]
    private async Task GoToBackAsync()
    {
         navigationService.BackButtonPressed();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await projectFacade.SaveAsync(NewProject);

        messengerService.Send(new ProjectEditMessage() { ProjectId = NewProject.Id });
        messengerService.Send(new UserEditMessage
        {
            UserId = NewProject.Id
        });

        navigationService.BackButtonPressed();
    }

    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectAddMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}