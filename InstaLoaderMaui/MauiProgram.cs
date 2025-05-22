using Microsoft.Extensions.Logging;
using UraniumUI;

#if ANDROID
using Firebase;
using Microsoft.Maui.LifecycleEvents;
using Plugin.MauiMTAdmob;
#endif

namespace InstaLoaderMaui;

public static class MauiProgram
{
    private static readonly string Tag = nameof(MauiProgram);

    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();
                fonts.AddMaterialIconFonts();
            });

#if ANDROID
        builder.UseMauiMTAdmob()
            .ConfigureLifecycleEvents(events =>
            {
                events.AddAndroid(android =>
                {
                    android.OnCreate((activity, bundle) =>
                    {
                        Console.WriteLine($"{Tag} OnCreate");

                        // init firebase
                        try
                        {
                            FirebaseApp.InitializeApp(activity);
                        }
                        catch (System.Exception e)
                        {
                            Console.WriteLine($"{Tag} failed to init firebase app");
                        }

                    });
                    android.OnResume(activity =>
                    {
                        Console.WriteLine($"{Tag} OnResume");
                    });
                    android.OnDestroy(activity =>
                    {
                        Console.WriteLine($"{Tag} OnDestroy");
                    });
                });
            });
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
