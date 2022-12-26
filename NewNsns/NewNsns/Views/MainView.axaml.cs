using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NewNsns.ViewModels;
namespace NewNsns.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        DataContext = new MainViewModel();
    }
}