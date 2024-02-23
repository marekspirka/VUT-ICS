using ICS_project.App.ViewModels;
using System.ComponentModel;

namespace ICS_project.App.Views.Activities;

public partial class ActivityListView
{
    private new readonly ActivityListViewModel ViewModel;

    public ActivityListView(ActivityListViewModel viewModel) 
        : base(viewModel)
    {
        InitializeComponent();

        this.ViewModel = viewModel;
    }

    private async void FilterPeriodModeChanged(object sender, EventArgs e)
    {
        if (ViewModel != null)
        {
            await ViewModel.FilterPeriodModeAsync();
        }
    }

    private async void FilterTagChanged(object sender, EventArgs e)
    {

        if (ViewModel != null && ViewModel.tagBool)
        {
            await Application.Current.MainPage.DisplayAlert("Filter by tag", $"{ViewModel.tagName}", "Ok");
            await ViewModel.FilterTagAsync();
        }

        ViewModel.tagBool = !ViewModel.tagBool;
    }

    private async void FilterProjectChanged(object sender, EventArgs e)
    {

        if (ViewModel != null && ViewModel.projectBool)
        {
            await Application.Current.MainPage.DisplayAlert("Filter by project", $"{ViewModel.projectName}", "Ok");
            await ViewModel.FilterProjectAsync();
        }
        ViewModel.projectBool = !ViewModel.projectBool;
    }
}