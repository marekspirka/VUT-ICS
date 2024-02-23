using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Facades;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class TagEditViewModel : ViewModelBase, IRecipient<TagEditMessage>
{
    private readonly ITagFacade tagFacade;
    private readonly INavigationService navigationService;

    public Guid Id { get; set; }
    public TagModel Tag { get; private set; }
    public TagModel NewTag { get; private set; } = TagModel.Empty;

    public override IUserService UserService { get; }

    public TagEditViewModel(
    ITagFacade tagFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IUserService userService)
    : base(messengerService)
    {
        this.tagFacade = tagFacade;
        this.navigationService = navigationService;

        this.UserService = userService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Tag = await tagFacade.GetAsync(Id);
        if (Tag != null)
        {
            NewTag = Tag;
        }
    }

    [RelayCommand]
    private async Task GoToBackAsync()
    {
        navigationService.BackButtonPressed();
    }

    [RelayCommand]
    private async Task SaveAsync(Guid userId)
    {
        await tagFacade.SaveAsync(NewTag, userId);
            
        messengerService.Send(new TagEditMessage() { TagId = NewTag.Id });
        messengerService.Send(new UserEditMessage
        {
            UserId = NewTag.Id
        });

        navigationService.BackButtonPressed();
    }

    public async void Receive(TagEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(TagAddMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(TagDeleteMessage message)
    {
        await LoadDataAsync();
    }
}