using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;

public partial class UserListViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade userFacade;
    private readonly INavigationService navigationService;

    public IEnumerable<UserDetailModel> Users { get; set; } = null!;

    public override IUserService UserService { get; }

    public UserListViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IUserService userService)
        : base(messengerService)
    {
        this.userFacade = userFacade;
        this.navigationService = navigationService;

        UserService = userService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await userFacade.GetAsync();
    }


    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync<UserEditViewModel>();
    }

    [RelayCommand]
    private async Task GoToActivityWithUserAsync(Guid Id)
    {
        this.UserService.CurrentUser = await userFacade.GetAsync(Id);

        messengerService.Send(new UserChangeMessage());
        await navigationService.GoToAsync<ActivityListViewModel>();
    }

    [RelayCommand]
    private async Task DeleteUserAsync(Guid Id)
    {
        bool confirmed = await Application.Current.MainPage.DisplayAlert("Delete User", "Are you sure you want to delete this user?", "Yes", "No");

        if (confirmed)
        {
            await userFacade.DeleteAsync(Id);
            messengerService.Send(new UserDeleteMessage());
        }
    }

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}