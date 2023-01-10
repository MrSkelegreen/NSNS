using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NewNsns.Android;

public partial class AndroidWindow : Window
{
    public AndroidWindow()
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