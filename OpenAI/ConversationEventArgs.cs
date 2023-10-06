namespace BassyTTSTwitch.OpenAI
{
    public class ConversationEventArgs
    {
        public Conversation Conversation;

        public ConversationEventArgs(Conversation conversation)
        {
            Conversation = conversation;
        }
    }
}
