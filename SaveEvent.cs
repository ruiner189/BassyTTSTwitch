using BassyTTSTwitch.Audio;
using BassyTTSTwitch.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassyTTSTwitch
{
    internal class SaveEvent : EventArgs
    {
        internal Settings Settings;
        internal SaveEvent(Settings settings)
        {
            Settings = settings;
        }
    }
}
