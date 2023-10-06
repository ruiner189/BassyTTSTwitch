using System;
using System.IO;

namespace BassyTTSTwitch
{
    public static class Util
    {
        /// <summary>
        /// Gets time in milliseconds
        /// </summary>
        /// <returns>Time in milliseconds</returns>
        public static long Now()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public static string GetCredentialPath(string name)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), name);
        }
    }
}
