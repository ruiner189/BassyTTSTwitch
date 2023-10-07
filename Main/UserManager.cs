using BassyTTSTwitch.Twitch.Cache;
using BassyTTSTwitch.Twitch.Events;
using BassyTTSTwitch.Twitch.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BassyTTSTwitch
{
    public class UserManager : UserEventHandler
    {
        public static UserManager Instance = new UserManager();

        public bool Broadcaster = false;
        public bool Mods = false;
        public bool VIP = false;
        public bool Subscriber = false;
        public bool Regular = false;
        public int MessageFilterDuration;

        public long LastUpdate;
        public long Delay = 1000;

        public Random Random = new Random();

        private UserManager()
        {
            UserCache.Instance.AddHandler(this);
        }

        public void Update()
        {

            if (Program.CommanderForm == null || Program.CommanderForm.UserList == null || !Program.CommanderForm.IsHandleCreated) return;

            long now = Util.Now();

            if (LastUpdate + Delay > now)
            {
                return;
            }

            LastUpdate = now;

            try
            {
                ListBox box = Program.CommanderForm.UserList;

                Dictionary<User, UserStatus> users = GetFilteredStatus(UserStatus.ADD);

                lock (box.Items)
                {
                    foreach (User user in box.Items)
                    {
                        if (users.ContainsKey(user))
                        {
                            users[user] = UserStatus.KEEP;
                        } else
                        {
                            users[user] = UserStatus.DELETE;
                        }
                    }

                    box.BeginInvoke((Action)(() =>
                    {
                        lock (box.Items)
                        {
                            foreach (KeyValuePair<User, UserStatus> pair in users)
                            {
                                if (pair.Value == UserStatus.KEEP) continue;
                                if (pair.Value == UserStatus.ADD)
                                {
                                    if(!box.Items.Contains(pair.Key))
                                        box.Items.Add(pair.Key);
                                }
                                if (pair.Value == UserStatus.DELETE) box.Items.Remove(pair.Key);
                            }
                        }
                    }));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Dictionary<User, UserStatus> GetFilteredStatus(UserStatus status)
        {
            Dictionary<User, UserStatus> users = new Dictionary<User, UserStatus>();
            GetFilteredUsers().ForEach(u =>  users.Add(u, status));
            return users;
        }

        public void ForceUpdate()
        {
            if (Program.CommanderForm == null || Program.CommanderForm.UserList == null) return;

            List<User> users = GetFilteredUsers();
            ListBox box = Program.CommanderForm.UserList;

                box.BeginInvoke((Action)(() => {
                    lock (box.Items)
                    {
                        box.Items.Clear();
                        foreach (User user in users)
                        {
                            box.Items.Add(user);
                        }
                        box.Hide();
                        box.Show();
                    }
                }));
            

        }

        public void OnNewUser(UserEvent userEvent)
        {
            if (MatchesFilter(userEvent.User))
            {
                Console.WriteLine($"{userEvent.User} matches filter!");
                ListBox box = Program.CommanderForm.UserList;
                box.BeginInvoke((Action)(() => {
                    lock (box.Items)
                    {
                        box.Items.Add(userEvent.User);
                        box.Hide();
                        box.Show();
                    }
                }));
            } else
            {
                Console.WriteLine($"{userEvent.User} does not match filter!");
            }
        }

        public void ClearUsers()
        {
            if (Program.CommanderForm == null) return;
            Console.WriteLine("Clearing Users");
            ListBox box = Program.CommanderForm.UserList;
            box.Items.Clear();
        }

        public List<User> GetFilteredUsers()
        {
            return UserCache.Instance.GetAllUsers().FindAll(u => MatchesFilter(u));
        }

        public List<User> GetFilteredUsers(User[] blacklist)
        {
            List<User> users = GetFilteredUsers();
            List<User> list = new List<User>(blacklist);

            users.RemoveAll(u => list.Contains(u));

            return users;
        }

        public bool MatchesFilter(User user)
        {
            if (user.lastMessage == null) return false;
            if (user.Name == "Nightbot") return false;

            bool correctUserType;

            if (user.Broadcaster)
                correctUserType = Broadcaster;
            else if (user.Mod)
                correctUserType = Mods;
            else if (user.Subscriber)
                correctUserType = Subscriber;
            else
                correctUserType = Regular;
            

            if (correctUserType)
            {
                long lastMessage = long.Parse(user.lastMessage.TmiSentTs);
                long elapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastMessage;

                if(elapsed > MessageFilterDuration)
                {
                    return false;
                }
                return true;
            }

            
            return false;
        }

        public void SetMessageFilterDuration(int value, int unit)
        {
            int multiplier = 1;
            if (unit == 0)
                multiplier = 1000; // Seconds
            if (unit == 1)
                multiplier = 1000 * 60; // Minutes
            if (unit == 2)
                multiplier = 1000 * 3600; // Hours

            SetMessageFilterDuration(value * multiplier);
        }

        public void SetMessageFilterDuration(int duration)
        {
            MessageFilterDuration = duration;
            ForceUpdate();
        }

        public User GetRandomUser(User[] blacklist)
        {

            List<User> users = GetFilteredUsers();

            int index = Random.Next(users.Count);

            User result = users.ElementAt(index);

            if (blacklist != null && blacklist.Length > 0)
            {
                for(int i = 0; i < blacklist.Length; i++)
                {
                    if(result == blacklist[i])
                    {
                        return null;
                    }
                }
            }

            return result;
            
        }

        public User GetRandomUserFromList(List<User> users)
        {
            int index = Random.Next(users.Count);

            User result = users.ElementAt(index);

            return result;
        }


    }
}
