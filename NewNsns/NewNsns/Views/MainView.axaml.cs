using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Android.Security.Identity;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using NewNsns.ViewModels;

namespace NewNsns.Views;

public partial class MainView : UserControl
{

    public MainView()
    {
        InitializeComponent();

        //При запуске по умолчанию включен режим нутриентов, кнопка горит зелёным
        this.NutrientsBtn.Background = Brushes.ForestGreen;

        DataContext = new MainViewModel();
    }
    
    //Переключения режимов
    private void NutrientsMode_OnClick(object? sender, RoutedEventArgs e)
    {
        this.NutrientsBtn.Background = Brushes.ForestGreen;
        this.RecipeBtn.Background = Brushes.LightGray;
        TBox.Watermark = "Type something like: 200 g chicken";
    }
    private void RecipeBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        this.RecipeBtn.Background = Brushes.ForestGreen;
        this.NutrientsBtn.Background = Brushes.LightGray;
        TBox.Watermark = "Type something like: spicy ramen";
    }
    
    //Боковая панель
    private void SettingsBtn_OnTapped(object? sender, TappedEventArgs e)
    {
        FlyoutBase.ShowAttachedFlyout(sender as Control);
    }
    
    //Кнопка поиска
    private void SearchBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
    
    private void ContactBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        string url = "https://vk.com/forbess357";
    }

    /*private void CloseRecipes_OnClick(object? sender, RoutedEventArgs e)
    {
        ListBox.IsVisible = false;
    }*/
}