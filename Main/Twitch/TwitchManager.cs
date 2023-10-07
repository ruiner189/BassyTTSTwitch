using BassyTTSTwitch.Twitch.Cache;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace BassyTTSTwitch.Twitch
{
    public class TwitchManager
    {
        public UserCache Users;
        public TwitchClient Client;
        public CommandManager CommandManager;
        
        public string CurrentChannel;
        public TwitchManager()
        {
            if(CredentialManager.TryGetData(CredentialManager.GetTwitchPath(), out string data))
            {
                string[] split = data.Split(':');

                string username = split[0];
                string key = split[1];

                CommandManager = new CommandManager();
                ConnectionCredentials credentials = new ConnectionCredentials(username, key);
                var clientOptions = new ClientOptions
                {
                    MessagesAllowedInPeriod = 750,
                    ThrottlingPeriod = TimeSpan.FromSeconds(30)
                };
                WebSocketClient customClient = new WebSocketClient(clientOptions);
                Client = new TwitchClient(customClient);
                Client.Initialize(credentials, username);
                Client.OnMessageReceived += OnMessage;
                Client.OnMessageReceived += CommandManager.OnNewChatMessage;
                Client.Connect();
            }
        }

        public void JoinChannel(string channelName)
        {
            Client.JoinChannel(channelName);
            CurrentChannel = channelName;
        }

        public void PartChannel()
        {
            if(CurrentChannel != null)
            {
                Client.LeaveChannel(CurrentChannel);
                CurrentChannel = null;
            }
        }

        public void OnMessage(object sender, OnMessageReceivedArgs args)
        {
            UserCache.Instance.CreateOrUpdateUser(args.ChatMessage);
        }


    }
}
