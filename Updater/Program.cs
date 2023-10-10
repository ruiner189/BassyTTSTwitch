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
        public const string CleanerEXEFile = "Updater.exe";
        public static string URL = "";
        public readonly static string UpdateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Latest");

        public static WebClient? DownloadClient;
        public static bool Running = true;

        public static List<Tuple<string, string>> BackupFiles = new List<Tuple<string, string>>();

        public static string[] ProgramFiles = new string[]
        {
            "Updater.dll",
            "Updater.exe",
            "Updater.pdb",
            "Updater.deps.json",
            "Updater.runetimeconfig.json",
            "latest.zip"
        };


        static void Main(string[] args)
        {
           
            if(args.Length == 0)
            {
                Console.WriteLine("Missing Argument!");
                return;
            } else if (args[0] == "clean")
            {
                ClearOldFiles();
                return;
            } else
            {
                URL = args[0];
            }

            DownloadLatest();

            while (Running)
            {

            }
        }
        
        public static void DownloadLatest()
        {
            ClearFiles();
            #pragma warning disable SYSLIB0014 
            DownloadClient = new WebClient();
            #pragma warning restore SYSLIB0014 
            DownloadClient.DownloadFileCompleted += Completed;
            Console.WriteLine($"Downloading Zip from {URL}");
            DownloadClient.DownloadFileAsync(new Uri(URL), "latest.zip");
            DownloadClient.DownloadProgressChanged += DownloadProgressChanged;
        }

        public static void ClearOldFiles()
        {
            Thread.Sleep(1000);
            DeleteOldFiles();
            RunOtherProgram();
            Running = false;
        }

        private static void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write($"\rDownloading... {e.ProgressPercentage}% ({e.BytesReceived} bytes / {e.TotalBytesToReceive} bytes)");
        }

        private static void Completed(object? sender, AsyncCompletedEventArgs? completeEvent)
        {
            DownloadClient?.Dispose();
            DownloadClient = null;

            Console.WriteLine("\nDownload Complete");

            try
            {
                if (File.Exists("latest.zip"))
                {
                    if (Directory.Exists(UpdateDirectory))
                        Directory.Delete(UpdateDirectory, true);

                    ZipFile.ExtractToDirectory("latest.zip", UpdateDirectory);
                    bool success = ReplaceFiles();

                    if (!success)
                    {
                        RestoreBackup();
                        Running = false;
                        return;
                    }

                    DeleteBackup();
                    ClearFiles();
                    RunCleanProgram();
                    Running = false;
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to install new version");
                Console.WriteLine(e.Message);
                RestoreBackup();
                return;
            }


            ClearFiles();
            Running = false;
        }

        private static void BackupFile(string filename)
        {
            string destination = filename + ".bak";
            BackupFiles.Add(new Tuple<string, string>(filename, destination));
            File.Move(filename, destination, true);
        }

        private static void RestoreBackup()
        {
            Console.WriteLine("Restoring Backup");
            foreach(Tuple<string, string> fileName in BackupFiles) {

                try
                {
                    if (File.Exists(fileName.Item2))
                    {
                        File.Move(fileName.Item2, fileName.Item1);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("Backup Restored");
            Thread.Sleep(1000);
        }

        private static void DeleteBackup()
        {
            Console.WriteLine("Deleting Backup");
            foreach (Tuple<string, string> fileName in BackupFiles)
            {
                try
                {
                    if (File.Exists(fileName.Item2))
                    {
                        string name = Path.GetFileName(fileName.Item2);
                        bool skip = false;
                        foreach (string programFile in ProgramFiles)
                        {
                            if (name.ToLowerInvariant() == programFile.ToLowerInvariant())
                            {
                                skip = true;
                                break;
                            }
                        }

                        if (skip) continue;
                        File.Delete(fileName.Item2);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void DeleteOldFiles()
        {
            Console.WriteLine("Deleting Old Files");
            foreach(string fileName in ProgramFiles)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), fileName + ".bak");

                if (File.Exists(path))
                {
                    Console.WriteLine($"Deleting {path}");
                    File.Delete(path);
                }
            }
        }

        private static bool ReplaceFiles()
        {
            if (!Directory.Exists(UpdateDirectory)) return false;

            try
            {
                foreach (string file in Directory.GetFiles(UpdateDirectory, "*", SearchOption.AllDirectories))
                {

                    string fileName = Path.GetFileName(file);

                    string toFile = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                    if (File.Exists(toFile))
                    {
                        BackupFile(toFile);
                    }

                    Console.WriteLine($"Extracting {fileName} to {toFile}");
                    File.Copy(file, toFile, true);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
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
            string path = Path.Combine(Directory.GetCurrentDirectory(), EXEFile);

            if (File.Exists(path))
            {
                Console.WriteLine($"Running {path}");
                Thread.Sleep(1000);
                Process.Start(path);
            }

        }

        private static void RunCleanProgram()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), CleanerEXEFile);
            if (File.Exists(path))
            {
                Console.WriteLine($"Cleaning up files");
                Process.Start(path, "clean");
            }
        }
    }
}