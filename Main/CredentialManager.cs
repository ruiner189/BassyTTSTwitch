using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassyTTSTwitch
{
    public static class CredentialManager
    {
        private const string GOOGLE = "google-credential";
        private const string GPT = "chatgpt-credential";
        private const string TWITCH = "twitch-credential";

        private static string[] FileNames = new string[]
        {
            GOOGLE,
            GPT,
            TWITCH
        };

        public static bool HasAllCredentials()
        {
            foreach(var file in FileNames)
            {
                if (!File.Exists(Util.GetCredentialPath(file))){
                    return false;
                }
            }

            return true;
        }

        public static string GetMissingCredentials()
        {
            string s = "Missing the following credentials: ";
            foreach (var file in FileNames)
            {
                if (!File.Exists(Util.GetCredentialPath(file))){
                    s += $"\r\n{file}";
                }
            }
            return s;
        }

        public static string GetGooglePath()
        {
            return Util.GetCredentialPath(GOOGLE);
        }

        public static string GetChatGPTPath()
        {
            return Util.GetCredentialPath(GPT);
        }

        public static string GetTwitchPath()
        {
            return Util.GetCredentialPath(TWITCH);
        }

        public static bool TryGetData(string path, out string data)
        {
            if (File.Exists(path))
            {
                data = File.ReadAllText(path);
                return true;
            }
            else
            {
                Console.WriteLine($"Could not find credential at {path}");
                data = null;
                return false;
            }
        }
    }
}
