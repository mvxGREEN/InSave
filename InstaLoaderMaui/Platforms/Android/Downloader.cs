using Android.Content;
using Green.Mobileapps.Instaloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaLoaderMaui.Platforms.Android
{
    static class Downloader
    {
        private static readonly string Tag = nameof(Downloader);
        public static async Task DownloadPost(string id, string path)
        {
            Console.WriteLine($"{Tag} DownloadPost: id={id} path={path}");

            // TODO remove
            // id = "DJ3NrwjxC_7";

            var instaLoader = new InstaLoader(MainActivity.ActivityCurrent);
            string filePath = instaLoader.DownloadPost(id, path);
            //string filePath = instaLoader.DownloadPost(id, path + id + ".jpg");

            // TODO scan file
            //ScanDownload(filePath);

            /*
             * TODO update UI
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                StatusLabel.Text = $"Downloaded!";
            });
            */
        }

    }
}
