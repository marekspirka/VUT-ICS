using ICS_project.App.ViewModels;

namespace ICS_project.App.Views.Users;

public partial class UserListView
{
    public UserListView(UserListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
