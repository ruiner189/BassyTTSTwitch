using BassyTTSTwitch.Twitch.Cache;

namespace BassyTTSTwitch.Twitch.Events
{
    public class UserEvent
    {
        public User User;

        public UserEvent(User user)
        {
            User = user;
        }
    }
}
