using System;
using System.Collections.Generic;
using TwitchLib.Client.Models;

namespace BassyTTSTwitch.Twitch.Cache
{
    public class User
    {
        public string Name;
        public readonly string UserId;
        public bool Mod;
        public bool Subscriber;
        public bool Broadcaster;

        public ChatMessage lastMessage;

        public readonly List<WeakReference<ChatMessage>> Messages = new List<WeakReference<ChatMessage>>();

        public User(string userId)
        {
            UserId = userId;
        }

        public User(ChatMessage message)
        {
            UserId = message.UserId;
            Update(message);
        }

        public void Update(ChatMessage message)
        {
            Name = message.DisplayName;
            Mod = message.IsModerator;
            Subscriber = message.IsSubscriber;
            Broadcaster = message.IsBroadcaster;

            lastMessage = message;
        }

        public void AddMessageToCache(ChatMessage message)
        {
            Messages.Add(new WeakReference<ChatMessage>(message));
            lastMessage = message;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
