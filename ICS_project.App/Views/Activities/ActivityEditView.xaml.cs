using ICS_project.App.ViewModels;

namespace ICS_project.App.Views.Activities;

public partial class ActivityEditView
{
    public ActivityEditView(ActivityEditViewModel viewModel) :
        base(viewModel)
    {
        InitializeComponent();
    }
}