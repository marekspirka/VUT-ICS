using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityEditViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;
    public override IUserService UserService { get; }
    
    public Guid Id { get; set; }
    public IList<TagModel> Tags { get; set; }
    public IList<ProjectListModel> Projects { get; set; }
    public UserDetailModel User { get; set; }
    public ActivityDetailModel Activity { get; set; }
    public TimeSpan TemporaryStart { get; set; } = DateTime.Now.TimeOfDay;
    public TimeSpan TemporaryEnd { get; set; } = DateTime.Now.TimeOfDay;
    public DateTime TemporaryDate { get; set; } = DateTime.Now;
    public ProjectListModel TemporaryProject { get; set; }
    public TagModel TemporaryTag { get; set; }

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        IProjectFacade projectFacade,
        ITagFacade tagFacade,
        INavigationService navigationService,
        IMessengerService messengerService, 
        IUserService userService)
            : base(messengerService)
    {
        _activityFacade = activityFacade;
        _projectFacade = projectFacade;
        _tagFacade = tagFacade;
        _navigationService = navigationService;
        UserService = userService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await _activityFacade.GetAsync(Id);
        if (Activity == null)
        {
            Activity = ActivityDetailModel.Empty;
        }
        IEnumerable<ProjectListModel> projectsEnumerable = await _projectFacade.GetUsersAsync(CurrentUser.Id);
        Projects = projectsEnumerable.ToList();
        IEnumerable<TagModel> tagsEnumerable = await _tagFacade.GetUsersAsync(CurrentUser.Id);
        Tags = tagsEnumerable.ToList();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        //Activity = SaveDateTime(TemporaryStart, TemporaryEnd, TemporaryDate, Activity);
        Activity.Start = TemporaryDate + TemporaryStart;
        Activity.End = TemporaryDate + TemporaryEnd;
        Activity.Project = TemporaryProject;
        if (TemporaryTag != null)
        {
            Activity.Tags.Add(TemporaryTag);
        }
        try
        {
            await _activityFacade.SaveAsync(Activity, UserService.CurrentUser.Id);
            messengerService.Send(new ActivityEditMessage() { ActivitiesId = Activity.Id });
            _navigationService.BackButtonPressed();
        }
        catch
        (Exception e)
        {
            await Application.Current.MainPage.DisplayAlert("Delete Activity", $"{e.Message}", "Yes");
        }
        
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

    // private ActivityDetailModel SaveDateTime(TimeSpan tempTimeStart, TimeSpan tempTimeEnd, DateTime tempDateTime, ActivityDetailModel activityDetailModel)
    // {
    //     var timeOnlyStart = TimeOnly.FromTimeSpan(tempTimeStart);
    //     var timeOnlyEnd = TimeOnly.FromTimeSpan(tempTimeEnd);
    //     var dateOnly = DateOnly.FromDateTime(tempDateTime);
    //     var startDateTime = dateOnly.ToDateTime(timeOnlyStart);
    //     var endDateTime = dateOnly.ToDateTime(timeOnlyEnd);
    //
    //     activityDetailModel.Start = startDateTime;
    //     activityDetailModel.End = endDateTime;
    //
    //     return activityDetailModel;
    // }
}