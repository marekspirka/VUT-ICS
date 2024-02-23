using CommunityToolkit.Mvvm.Messaging;

namespace ICS_project.App.Services;

public interface IMessengerService
{
    IMessenger Messenger { get; }

    void Send<TMessage>(TMessage message)
        where TMessage : class;
}