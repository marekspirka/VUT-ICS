using CommunityToolkit.Mvvm.Input;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserEditViewModel : ViewModelBase
{
    private readonly IUserFacade userFacade;
    private readonly INavigationService navigationService;

    public Guid Id { get; set; }
    public UserDetailModel User { get; private set; }
    public UserDetailModel NewUser { get; private set; } = UserDetailModel.Empty;

    public string ProfileImage { get; set; } = "https://t4.ftcdn.net/jpg/00/65/77/27/360_F_65772719_A1UV5kLi5nCEWI0BNLLiFaBPEkUbv5Fv.jpg";

    public override IUserService UserService { get; }

    public UserEditViewModel(
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

        User = await userFacade.GetAsync(Id);

        if (User != null)
        {
            NewUser = User;
            ProfileImage = User.ImageUrl;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await userFacade.SaveAsync(NewUser);

        messengerService.Send(new UserEditMessage () { UserId = NewUser.Id });

        navigationService.BackButtonPressed();
    }

    [RelayCommand]
    private async Task GoToBackAsync()
    {
        navigationService.BackButtonPressed();
    }

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }
}
