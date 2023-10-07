using System;

namespace BassyTTSTwitch.Twitch
{
    public class Command
    {
        public const string IDENTIFIER = "!";
        public readonly string Name;
        public readonly Action<TTSManager, TwitchManager> Action;


        public Command (string name, Action<TTSManager, TwitchManager> action)
        {
            Name = name;
            Action = action;
        }

        public void DoAction(TTSManager tts, TwitchManager twitch)
        {
            Action?.Invoke(tts, twitch);
        }
    }
}
