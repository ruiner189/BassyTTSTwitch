using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BassyTTSTwitch
{
    public class Filter
    {

        public const string WILD_PATTERN = "\\W*";
        public string[] DefaultBlacklist = new string[] {
            "nigga*",
            "midget*",
            "gook*",
            "chink*",
            "nazi",
            "fag*",
            "gyps*",
            "retard*",
            "jap",
            "nigger*",
            "nigg*",
            "nick gehr",
            "gang bang",
            "skinhead",
            "kkk",
            "klux",
            "bukkake",
            "molest*",
            "gay lord",
            "tranny",
            "Gher*",
            "Gurz*",
            "Ryan Haywood*",
            "Ryan Haywood",
            "Mad King",
            "Adam Kovic*",
            "Adam Kovic",
            "Hitler",
            "fag",
            "dyke",
            "chink",
            "gangrape",
            "gangbang",
            "kike",
            "gook",
            "卐",
            "molest*",
            "rape",
            "rapist",
            "tabarnak*",
            "Auschwitz",
            "ryanhaywooddidnothingwrong",
            "ryandidnothingwrong"
        };
        public List<string> Blacklist = new List<string>();


        public Filter()
        {
            foreach(string word in DefaultBlacklist)
            {
                AddWordToBlacklist(word.ToLower());
            }
        }

        public void AddWordToBlacklist(string word)
        {
            Blacklist.Add(TextToPattern(word));
        }


        public string TextToPattern(string text)
        {
            return text.Replace("*", WILD_PATTERN);
        }

        public string FilterActions(string text)
        {
            return text.Replace("ACTION", "").Replace("", "");

        }

        public bool ContainsBlacklist(string text)
        {
            string lower = text.ToLower();
            foreach(string filter in Blacklist)
            {
                if(Regex.IsMatch(lower, filter))
                {
                    return true;
                }
            }
            return false;
        }

        public bool FilterText(string text, out string result)
        {
            if (ContainsBlacklist(text))
            {
                result = text;
                return false;
            } else
            {
                result = FilterActions(text);
                return !string.IsNullOrWhiteSpace(result);
            }
        }
    }
}
