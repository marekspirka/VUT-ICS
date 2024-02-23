using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ICS_project.App.Messages;
using ICS_project.App.Services;
using ICS_project.BL.Models;

namespace ICS_project.App.ViewModels;

public abstract class ViewModelBase : ObservableRecipient, IViewModel, IRecipient<UserChangeMessage>
{
    private bool NeedRefresh = true;

    protected readonly IMessengerService messengerService;

    public abstract IUserService UserService { get; }

    public UserDetailModel CurrentUser { get; set; }

    protected ViewModelBase(IMessengerService messengerService)
        : base(messengerService.Messenger)
    {
        this.messengerService = messengerService;
        IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (NeedRefresh)
        {
            CurrentUser = this.UserService.CurrentUser;

            await LoadDataAsync();

        }
    }
    public async void Receive(UserChangeMessage message)
    {
        CurrentUser = this.UserService.CurrentUser;
        await LoadDataAsync();
    }

    protected virtual Task LoadDataAsync()
        => Task.CompletedTask;

}