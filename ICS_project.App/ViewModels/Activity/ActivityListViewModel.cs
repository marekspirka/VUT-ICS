using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }
}

[QueryProperty(nameof(User), nameof(User))]
public partial class ActivityListViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>
{
    private readonly IFilterFacade _filterFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly IProjectFacade _projectFacade;
    private readonly ITagFacade _tagFacade;
    private readonly INavigationService _navigationService;
    public override IUserService UserService { get; }
    public IEnumerable<ActivityDetailModel> Activities { get; set; } = null!;
    public IList<ProjectListModel> Projects { get; set; } = null!;
    public IList<TagModel> Tags { get; set; } = null!;
    public UserDetailModel User { get; set; }

    public string FilterPeriodMode { get; set; } = "Everything";
    public TagModel? FilterTag { get; set; } = null;
    public ProjectListModel? FilterProject { get; set; } = null;

    public Guid? TagId { get; set; } = null;

    public bool tagBool { get; set; } = true;
    public bool projectBool { get; set; } = true;

    public string tagName { get; set; }
    public string projectName { get; set; }

    public Guid? ProjectId { get; set; } = null;

    public DateTime? SelectedDateStart { get; set; } = null;
    public DateTime? SelectedDateEnd { get; set; } = null;
    public DateTime FilterTimeStartDate { get; set; } = DateTime.MinValue;
    public DateTime FilterTimeEndDate { get; set; } = DateTime.MaxValue;
    public TimeSpan FilterTimeStart { get; set; } = new TimeSpan(00, 00, 00);
    public TimeSpan FilterTimeEnd { get; set; } = new TimeSpan(23, 59, 59);

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        IFilterFacade filterFacade,
        IProjectFacade projectFacade,
        ITagFacade tagFacade,
        INavigationService navigationService,
        IMessengerService messengerService, IUserService userService)
        : base(messengerService)
    {
        _activityFacade = activityFacade;
        _filterFacade = filterFacade;
        _projectFacade = projectFacade;
        _tagFacade = tagFacade;
        _navigationService = navigationService;
        UserService = userService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activities = await _filterFacade.GetByEverythingAsync(UserService.CurrentUser.Id, SelectedDateStart, SelectedDateEnd, FilterTimeStartDate, FilterTimeEndDate, TagId, ProjectId);

        IEnumerable<ProjectListModel> projectsEnumerable = await _projectFacade.GetUsersAsync(CurrentUser.Id);
        Projects = projectsEnumerable.ToList();
        IEnumerable<TagModel> tagsEnumerable = await _tagFacade.GetUsersAsync(CurrentUser.Id);
        Tags = tagsEnumerable.ToList();
    }

    [RelayCommand]
    private async Task GoToCreateActivityAsync()
    {
        await _navigationService.GoToAsync<ActivityEditViewModel>();
    }

    [RelayCommand]
    private async Task GoToEditAsync(Guid id)
    {
        await _navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(ActivityEditViewModel.Id)] = id });
    }


    [RelayCommand]
    private async Task GoToFilterAsync()
    {
        await _navigationService.GoToAsync<ActivityFilterEditViewModel>();
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
    private async Task DeleteActivityAsync(Guid Id)
    {
        bool confirmed = await Application.Current.MainPage.DisplayAlert("Delete Activity", "Are you sure you want to delete this activity?", "Yes", "No");

        if (confirmed)
        {
            await _activityFacade.DeleteAsync(Id);
            messengerService.Send(new ActivityDeleteMessage());
        }
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task FilterPeriodModeAsync()
    {
        DateTime now = DateTime.Now;

        if (FilterPeriodMode == "Everything")
        {
            SelectedDateStart = null;
            SelectedDateEnd = null;
        }
        else if (FilterPeriodMode == "Today")
        {
            SelectedDateStart = now.AddDays(-1);
            SelectedDateEnd = null;
        }
        else if (FilterPeriodMode == "This week")
        {
            SelectedDateStart = now.StartOfWeek(DayOfWeek.Monday);
            SelectedDateEnd = null;
        }
        else if (FilterPeriodMode == "Last week")
        {
            SelectedDateStart = now.StartOfWeek(DayOfWeek.Monday).AddDays(-7);
            SelectedDateEnd = now.StartOfWeek(DayOfWeek.Monday).AddDays(-1);
        }
        else if (FilterPeriodMode == "This month")
        {
            SelectedDateStart = new DateTime(now.Year, now.Month, 1);
            SelectedDateEnd = null;
        }
        else if (FilterPeriodMode == "Last month")
        {
            var lastMonth = new DateTime(now.Year, now.Month, 1);

            SelectedDateStart = lastMonth.AddMonths(-1);
            SelectedDateEnd = lastMonth.AddMonths(1).AddDays(-1);
        }

        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task FilterTimeAsync()
    {
        FilterTimeStartDate = new DateTime() + FilterTimeStart;
        FilterTimeEndDate = new DateTime() + FilterTimeEnd;

        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task FilterTagAsync()
    {
        if (FilterTag != null)
        {
            TagId = FilterTag.Id;
            tagName = FilterTag.Name;
        }
        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task FilterProjectAsync()
    {
        if (FilterProject != null)
        {
            ProjectId = FilterProject.Id;
            projectName = FilterProject.Name;
        }
        await LoadDataAsync();
    }
}