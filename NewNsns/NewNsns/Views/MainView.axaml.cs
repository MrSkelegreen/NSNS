using System.Net.Mail;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace NewNsns.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        this.NutrientsBtn.Background = Brushes.ForestGreen;
    }

    private void NutrientsMode_OnClick(object? sender, RoutedEventArgs e)
    {
        this.NutrientsBtn.Background = Brushes.ForestGreen;
        this.RecipeBtn.Background = Brushes.LightGray;
    }

    private void RecipeBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        this.RecipeBtn.Background = Brushes.ForestGreen;
        this.NutrientsBtn.Background = Brushes.LightGray;
    }


    private void SettingsBtn_OnTapped(object? sender, TappedEventArgs e)
    {
        FlyoutBase.ShowAttachedFlyout(sender as Control);
    }
}