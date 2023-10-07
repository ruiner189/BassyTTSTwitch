using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        public const string URL = "https://raw.githubusercontent.com/ruiner189/BassyTTSTwitch/master/Main/version.json";
        public const string EXEFile = "updater.exe";

        public readonly static string UpdateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Latest");

        public static string LatestURL;

        public readonly static Version CurrentVersion = new Version(Program.VERSION);
        public static Version LatestVersion;

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
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), EXEFile))) return true;
            
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

            RunOtherProgram();
            Application.Exit();
        }

        private static void RunOtherProgram()
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), EXEFile)))
            {
                Process.Start(Path.Combine(Directory.GetCurrentDirectory(), EXEFile), LatestURL);
            }
        }


    }
}
