using ICS_project.App.ViewModels;

namespace ICS_project.App.Views.Tags;

public partial class TagListView
{
    public TagListView(TagListViewModel viewModel) 
        : base(viewModel)
    {
        InitializeComponent();
    }
}