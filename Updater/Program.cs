using System;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;

namespace Updater // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public const string EXEFile = "BassyTTSTwitch.exe";
        public static string URL = "https://github.com/ruiner189/BassyTTSTwitch/releases/download/v1.0.1/BassyTTSTwitch.zip";
        public readonly static string UpdateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Latest");

        public static WebClient? DownloadClient;
        public static bool Running = true;

        static void Main(string[] args)
        {
           if(args.Length == 0)
           {
                Console.WriteLine("Missing URL!");
                return;
           }

            URL = args[0];
           
            Console.WriteLine(URL);
            Console.WriteLine(Directory.GetCurrentDirectory());
            DownloadLatest();

            while (Running)
            {

            }
        }
        
        public static void DownloadLatest()
        {
            ClearFiles();
            DownloadClient = new WebClient();
            DownloadClient.DownloadFileCompleted += Completed;
            Console.WriteLine("Downloading Zip");
            DownloadClient.DownloadFileAsync(new Uri(URL), "latest.zip");

        }

        private static void Completed(object sender, AsyncCompletedEventArgs completeEvent)
        {
            DownloadClient.Dispose();
            DownloadClient = null;

            Console.WriteLine("Zip downloaded");

            try
            {
                if (File.Exists("latest.zip"))
                {
                    if (Directory.Exists(UpdateDirectory))
                        Directory.Delete(UpdateDirectory, true);

                    ZipFile.ExtractToDirectory("latest.zip", UpdateDirectory);
                    ReplaceFiles();
                    ClearFiles();
                    RunOtherProgram();
                    Running = false;
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to install new version");
                Console.WriteLine(e.Message);
            }

            ClearFiles();
            Running = false;
        }

        private static void ReplaceFiles()
        {
            if (!Directory.Exists(UpdateDirectory)) return;

            int rootLength = UpdateDirectory.Length;

            foreach (string file in Directory.GetFiles(UpdateDirectory, "*", SearchOption.AllDirectories))
            {
                string toFile = Path.Combine(Directory.GetCurrentDirectory(), file.Substring(rootLength + 1));
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

        private static void RunOtherProgram()
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), EXEFile)))
            {
                Process.Start(Path.Combine(Directory.GetCurrentDirectory(), EXEFile));
            }
        }
    }
}