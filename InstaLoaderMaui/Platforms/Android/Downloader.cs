using Green.Mobileapps.Instaloader;

namespace InstaLoaderMaui.Platforms.Android
{
    static class Downloader
    {
        private static readonly string Tag = nameof(Downloader);
        public static async Task DownloadPost(InstaLoader instaLoader, string id, string path)
        {
            Console.WriteLine($"{Tag} DownloadPost: id={id} path={path}");

            // TODO remove
            // id = "DJ3NrwjxC_7";

            //string filePath = instaLoader.DownloadPost(id, path);
            string filePath = instaLoader.DownloadPost(id, path.TrimStart('/').TrimEnd('/'));

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
