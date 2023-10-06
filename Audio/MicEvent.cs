using System;

namespace BassyTTSTwitch.Audio
{
    public class MicEvent : EventArgs
    {
        public readonly SpeechToText SpeechToText;

        internal MicEvent(SpeechToText speechToText)
        {
            SpeechToText = speechToText;
        }

    }
}
