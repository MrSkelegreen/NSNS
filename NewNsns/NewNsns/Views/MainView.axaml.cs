using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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
    static readonly HttpClient Client = new HttpClient();
    //Переключения режимов
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
    
    //Боковая панель
    private void SettingsBtn_OnTapped(object? sender, TappedEventArgs e)
    {
        FlyoutBase.ShowAttachedFlyout(sender as Control);
    }
    
    //Кнопка поиска
    private void SearchBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        TBlock.IsVisible = true;
    }
}