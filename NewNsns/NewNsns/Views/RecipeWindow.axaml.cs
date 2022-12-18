using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NewNsns.Views;

public partial class RecipeWindow : Window
{
    public RecipeWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}