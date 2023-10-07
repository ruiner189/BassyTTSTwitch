using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassyTTSTwitch
{
    public class Updater
    {
        public const string URL = "https://raw.githubusercontent.com/ruiner189/BassyTTSTwitch/master/version.json";

        public readonly static string UpdateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Latest");

        public static string LatestURL;

        public readonly static Version CurrentVersion = new Version(Program.VERSION);
        public static Version LatestVersion;

        private static WebClient DownloadClient;

        public static bool CheckForUpdate()
        {
            using (var client = new WebClient())
            {
                try
                {
                    string json = client.DownloadString(URL);
                    JObject obj = JObject.Parse(json);
                    if (obj != null)
                    {
                        LatestURL = (string) obj.GetValue("URL");
                        LatestVersion = new Version((string) obj.GetValue("Version"));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Update check failed!");
                    Console.WriteLine(e.Message);
                }

            }
            return !IsLatestVersion();
        }

        private static bool IsLatestVersion()
        {
            if (LatestVersion == null) return true;

            return LatestVersion <= CurrentVersion;
        }

        public static void UpdateToLatest()
        {
            if (LatestURL == null) return;

            using (var client = new WebClient())
            {
                try
                {
                    string json = client.DownloadString(URL);
                    JObject obj = JObject.Parse(json);
                    if (obj != null)
                    {
                        LatestURL = (string)obj.GetValue("URL");
                        LatestVersion = new Version((string)obj.GetValue("Version"));
                    }
                }
                catch (Exception)
                {

                }

            }
        }

        public static void DownloadLatest()
        {
            ClearFiles();
            DownloadClient = new WebClient();
            DownloadClient.DownloadFileCompleted += Completed;
            DownloadClient.DownloadFileAsync(new Uri(LatestURL), "latest.zip");

        }

        private static void Completed(object sender, AsyncCompletedEventArgs completeEvent)
        {
            DownloadClient.Dispose();
            DownloadClient = null;

            try
            {
                if (File.Exists("latest.zip"))
                {
                    if (Directory.Exists(UpdateDirectory))
                        Directory.Delete(UpdateDirectory, true);

                    ZipFile.ExtractToDirectory("latest.zip", UpdateDirectory);
                    ReplaceFiles();
                    ClearFiles();
                    Application.Exit();
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to install new version");
                Console.WriteLine(e.Message);
            }

            ClearFiles();
        }

        private static void ReplaceFiles()
        {
            if (!Directory.Exists(UpdateDirectory)) return;

            int rootLength = UpdateDirectory.Length;

            foreach (string file in Directory.GetFiles(UpdateDirectory, "*", SearchOption.AllDirectories))
            {
                string toFile = Path.Combine(Directory.GetCurrentDirectory(), file.Substring(rootLength));
                Console.WriteLine($"Copying {file} to {toFile}");
                File.Copy(file, toFile, true);
            }
        }

        private static void ClearFiles()
        {
            try
            {
                if (File.Exists("latest.zip"))
                {
                    Console.WriteLine("Deleting latest.zip");
                    File.Delete("latest.zip");
                }
                if (Directory.Exists(UpdateDirectory))
                {
                    Console.WriteLine("Deleting update directory");
                    Directory.Delete(UpdateDirectory, true);
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
