using System;

namespace BassyTTSTwitch
{
    public struct MessageSnippet
    {
        public readonly string Message;
        public readonly string Author;

        public MessageSnippet(string message, string author)
        {
            Message = message;
            Author = author;
        }

        public static bool operator == (MessageSnippet s1, MessageSnippet s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator != (MessageSnippet s1, MessageSnippet s2)
        {
            return !s1.Equals(s2);
        }

        public override bool Equals(object obj)
        {
            if(obj is MessageSnippet snippet)
            {
                return Message == snippet.Message && Author == snippet.Author;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Message, Author);
        }
    }
}
