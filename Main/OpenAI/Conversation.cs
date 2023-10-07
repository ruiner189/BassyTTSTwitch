namespace BassyTTSTwitch.OpenAI
{
    public class Conversation
    {
        public readonly string UserPrompt;
        public readonly string Reply;

        public Conversation(string userPrompt, string reply)
        {
            UserPrompt = userPrompt;
            Reply = reply;
        }
    }
}
