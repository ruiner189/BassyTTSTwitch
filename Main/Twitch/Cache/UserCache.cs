using BassyTTSTwitch.Twitch.Events;
using BassyTTSTwitch.Twitch.Handlers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Models;

namespace BassyTTSTwitch.Twitch.Cache
{
    public class UserCache
    {
        public static UserCache Instance = new UserCache();

        public ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        public List<UserEventHandler> EventHandlers = new List<UserEventHandler>();

        public User GetOrCreateUser(ChatMessage message)
        {
            string id = message.UserId;
            if (!Users.ContainsKey(id))
            {
                User user = new User(message);
                if(Users.TryAdd(id, user))
                {
                    UserEvent ue = new UserEvent(user);
                    foreach (UserEventHandler handler in EventHandlers)
                    {
                        handler.OnNewUser(ue);
                    }
                }
            }

            return Users[id];
        }

        public User GetOrCreateUser(string userId)
        {
            if (!Users.ContainsKey(userId))
            {
               User user = new User(userId);
               if(Users.TryAdd(userId, user))
               {
                    UserEvent ue = new UserEvent(user);
                    foreach(UserEventHandler handler in EventHandlers)
                    {
                        handler.OnNewUser(ue);
                    }
               }
            }

            return Users[userId];
        }

        public bool TryGetUser(string username, out User user)
        {
            user = Users.Values.FirstOrDefault(u => u.Name == username);
            return user != null;
        }


        public List<User> GetAllUsers()
        {
            return Users.Values.ToList();
        }

        public void UpdateUser(ChatMessage chatMessage)
        {
            User u = GetOrCreateUser(chatMessage);
            u.Update(chatMessage);
        }

        public void CreateOrUpdateUser(ChatMessage chatMessage)
        {
            User u = GetOrCreateUser(chatMessage);
            u.Update(chatMessage);
        }

        public void ClearCache()
        {
            Users.Clear();
        }

        public void AddHandler(UserEventHandler handler)
        {
            EventHandlers.Add(handler);
        }
    }
}
