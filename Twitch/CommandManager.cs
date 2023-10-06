using System.Collections.Generic;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace BassyTTSTwitch.Twitch
{
    public class CommandManager
    {

        public readonly List<Command> Commands = new List<Command>();
        public bool DoCommands = false;

        public CommandManager()
        {
            RegisterCommands();
        }

        public void RegisterCommands()
        {
            Commands.Add(new Command("random", RandomUser));
            Commands.Add(new Command("stop", StopTTS));
        }

        public void RandomUser(TTSManager tts, TwitchManager twitch)
        {
            tts.SelectRandomUser();
        }

        public void StopTTS(TTSManager tts, TwitchManager twitch)
        {
            tts.StopTTS();
        }

        public bool IsMessageCommand(string message)
        {
            Command command = GetCommand(message);
            return command != null;
        }

        public void OnNewChatMessage(object sender, OnMessageReceivedArgs args)
        {
            if (DoCommands)
            {
                ChatMessage message = args.ChatMessage;
                if (message.IsBroadcaster || message.IsModerator)
                {
                    Command command = GetCommand(message.Message);
                    if (command != null)
                    {
                        command.DoAction(TTSManager.GetInstance(), TTSManager.GetInstance().TwitchManager);
                    }
                }
            }
        }

        public Command GetCommand(string message){

            if (!message.StartsWith("!")){
                return null;
            }

            string[] split = message.Split(' ');
            string commandName = split[0].ToLowerInvariant();

            foreach (Command command in Commands)
            {
                if($"{Command.IDENTIFIER}{command.Name}".ToLowerInvariant() == commandName)
                {
                    return command;
                }
            }

            return null;

        }
    }
}
