using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;

public partial class TagListViewModel : ViewModelBase, IRecipient<TagEditMessage>, IRecipient<TagDeleteMessage>
{
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<TagModel> Tags { get; set; } = null!;

    public override IUserService UserService { get; }

    public TagListViewModel(
    ITagFacade tagFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IUserService userService)
    : base(messengerService)
    {
        this._tagFacade = tagFacade;
        this._navigationService = navigationService;

        UserService = userService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Tags = await _tagFacade.GetUsersAsync(UserService.CurrentUser.Id);
    }

    [RelayCommand]
    private async Task GoToCreateTagAsync()
    {
        await _navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task GoToEditTagAsync(Guid Id)
    {
        await _navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(TagEditViewModel.Id)] = Id });
    }

    [RelayCommand]
    private async Task GoToEditUserAsync(Guid id)
    {
        await _navigationService.GoToAsync<UserEditViewModel>(
            new Dictionary<string, object?> { [nameof(UserEditViewModel.Id)] = id });
    }

    [RelayCommand]
    private async Task GoToChangeUserAsync()
    {
        await _navigationService.GoToAsync<UserListViewModel>();
    }

    [RelayCommand]
    private async Task DeleteTagAsync(Guid Id)
    {
        bool confirmed = await Application.Current.MainPage.DisplayAlert("Delete Tag", "Are you sure you want to delete this tag?", "Yes", "No");

        if (confirmed)
        {
            await _tagFacade.DeleteAsync(Id);
            messengerService.Send(new TagDeleteMessage());
        }
    }

    public async void Receive(TagEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(TagDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
