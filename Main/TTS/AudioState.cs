namespace BassyTTSTwitch.TTS
{
    public class AudioState
    {
        public readonly string Prompt;
        public State CurrentState;

        public AudioState(string prompt, State state = State.PREPARING)
        {
            Prompt = prompt;
            CurrentState = state;
        }

        public enum State {
            PREPARING,
            READY_TO_PLAY,
            PLAYING,
            FINISHED
        };
    }
}
