using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TachoHelper.Services;

namespace TachoServiceClient.Droid
{
    [Activity(Label = "TachoServiceClient", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Android.Util.Log.Info("tachotag", $"TachoClient is creating");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Android.Util.Log.Info("tachotag", $"TachoClient create done");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Info("tachotag", $"TachoClient is creating");
            var Tachocardservice = TachoService.Instance; //DependencyService.Get<ITachoService>();
            Tachocardservice.StopTachoProcess();
            Tachocardservice.UnbindService();
            base.OnDestroy();
            Android.Util.Log.Info("tachotag", $"TachoClient is creating");
        }
    }
}