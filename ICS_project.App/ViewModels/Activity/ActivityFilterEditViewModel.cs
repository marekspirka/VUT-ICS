using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;


public partial class ActivityFilterEditViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{
    private static readonly IActivityFacade ActivityFacade;
    private static readonly INavigationService NavigationService;

    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    public override IUserService UserService { get; }
    
    public IEnumerable<ActivityListViewModel> Activities { get; set; }
    public IEnumerable<ProjectDetailModel> Projects { get; set; }
    public IEnumerable<TagModel> Tags { get; set; }
    public ActivityDetailModel Activity { get; set; }
    public ActivityDetailModel FilterActivity { get; set; }
    public UserEditViewModel User { get; set; }
    public TimeSpan TemporaryStart { get; set; }
    public TimeSpan TemporaryEnd { get; set; }
    
    public ActivityFilterEditViewModel(
        IActivityFacade activityFacade, 
        INavigationService navigationService,
        IMessengerService messengerService,
        IUserService userService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        UserService = userService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        // Activity = SaveDateTime(TemporaryStart, TemporaryEnd, TemporaryDate, Activity);
        await _activityFacade.SaveAsync(Activity, UserService.CurrentUser.Id);
        messengerService.Send(new ActivityEditMessage() { ActivitiesId = Activity.Id });
        _navigationService.BackButtonPressed();
    }

    [RelayCommand]
    private async Task GoToBackAsync()
    {
        _navigationService.BackButtonPressed();
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}