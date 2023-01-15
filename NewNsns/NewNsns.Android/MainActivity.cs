using Android.App;
using Android.Content.PM;
using Android.OS;
using Avalonia.Android;



namespace NewNsns.Android;

[Activity(Label = "NewNsns.Android", Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon",
    LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
public class MainActivity : AvaloniaMainActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
    }
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
    {
        

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }
}
