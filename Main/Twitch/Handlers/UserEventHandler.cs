using BassyTTSTwitch.Twitch.Cache;
using BassyTTSTwitch.Twitch.Events;

namespace BassyTTSTwitch.Twitch.Handlers
{
    public interface UserEventHandler
    {
        void OnNewUser(UserEvent userEvent);
    }
}
