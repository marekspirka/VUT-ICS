namespace ICS_project.App.Templates;

public partial class ListViewHeader : ContentView
{
    public static readonly BindableProperty UserNameProperty = BindableProperty.Create(nameof(UserName), typeof(string), typeof(ListViewHeader), string.Empty);
    public static readonly BindableProperty UserPhotoLinkProperty = BindableProperty.Create(nameof(UserPhotoLink), typeof(string), typeof(ListViewHeader));

    public string UserName
    {
        get => (string)GetValue(ListViewHeader.UserNameProperty);
        set => SetValue(ListViewHeader.UserNameProperty, value);
    }

    public string UserPhotoLink
    {
        get => (string)GetValue(ListViewHeader.UserPhotoLinkProperty);
        set => SetValue(ListViewHeader.UserPhotoLinkProperty, value);
    }

    public ListViewHeader()
    {
        InitializeComponent();
    }
}