using ICS_project.App.ViewModels;

namespace ICS_project.App.Views.Projects;

public partial class ProjectListView
{
    private new readonly ProjectListViewModel ViewModel;
    public ProjectListView(ProjectListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();

        this.ViewModel = viewModel;
    }

    void FindOnlyMyProjects(object sender, CheckedChangedEventArgs e)
    {
        if (this.ViewModel.FindMyProject)
        {
            this.ViewModel.LoadMyProjectAsync();
        }
        else
        {
            this.ViewModel.LoadAllProjectsAsync();
        }
    }
}