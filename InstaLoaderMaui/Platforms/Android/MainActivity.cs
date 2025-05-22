using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Ads.Interstitial;
using Android.Gms.Ads;
using Android.OS;
using Android.Util;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Xamarin.Google.UserMesssagingPlatform;
using Plugin.MauiMTAdmob;
using UraniumUI.Material.Controls;
using Android.BillingClient.Api;
using System.Collections.Immutable;
using AndroidHUD;
using Plugin.MauiMTAdmob.Controls;
using static InstaLoaderMaui.MainPage;
using Firebase.Analytics;

namespace InstaLoaderMaui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private static string Tag = nameof(MainActivity);

    public static MainActivity ActivityCurrent { get; set; }
    public MainActivity()
    {
        ActivityCurrent = this;
    }

    protected override async void OnCreate(Bundle? savedInstanceState)
    {
        Console.WriteLine($"{Tag}: OnCreate");
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState);

        // Fixes "strict-mode" error when fetching webpage... idek..
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().PermitAll().Build();
        StrictMode.SetThreadPolicy(policy);

        /*// log ANRs
        StrictMode.SetVmPolicy(new StrictMode.VmPolicy.Builder()
                       .DetectAll()
                       .PenaltyLog()
                       //.PenaltyDeath()
                       .Build());
        */

        AskPermissions();

        //LoadBillingClient();
    }

    protected override void OnResume()
    {
        base.OnResume();

        //HandlePendingTransactions();
    }

    protected override void OnNewIntent(Intent? intent)
    {
        base.OnNewIntent(intent);

        Console.WriteLine($"{Tag}: OnNewIntent");

        if (intent != null)
        {
            Console.WriteLine($"{Tag}: received new intent");
            var data = intent.GetStringExtra(Intent.ExtraText);
            if (data != null)
            {
                Console.WriteLine($"{Tag}: received data from new intent: {data}");

                Task.Run(async () =>
                {
                    //ResetVars();
                    MainPage mp = (MainPage)Shell.Current.CurrentPage;
                    await mp.ClearTextfield();
                    // give ontextchanged handler time to call showEmptyUI
                    await Task.Delay(250);
                    string SharedText = data.ToString();
                    TextField mTextField = (TextField)mp.FindByName("main_textfield");
                    if (mTextField != null)
                    {
                        mTextField.Text = SharedText;
                    }
                });
            }
        }


        string shareText = Intent.GetStringExtra(Intent.ExtraText);


    }

    private void AskPermissions()
    {
        if ((int)Build.VERSION.SdkInt >= 33
            && ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReadMediaAudio) != Permission.Granted)
        {
            ActivityCompat.RequestPermissions(
                MainActivity.ActivityCurrent, new string[] { Android.Manifest.Permission.ReadMediaAudio }, 101);

        }
        else if ((int)Build.VERSION.SdkInt < 33
            && ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WriteExternalStorage) != Permission.Granted)
        {
            ActivityCompat.RequestPermissions(
            MainActivity.ActivityCurrent, new string[] { Android.Manifest.Permission.ReadExternalStorage, Android.Manifest.Permission.WriteExternalStorage }, 101);
        }
    }
}
