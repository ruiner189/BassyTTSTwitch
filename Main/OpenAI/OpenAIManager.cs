using OpenAI_API;
using OpenAI_API.Completions;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassyTTSTwitch.OpenAI
{
    public class OpenAIManager
    {
        private static OpenAIManager _instance;
        private OpenAIAPI Api;

        public Conversation[] Conversations;
        public int Capacity;

        private readonly Thread OpenAIThread;

        public delegate void ConversationEventHandler(object sender, ConversationEventArgs e);
        public event ConversationEventHandler OnConversation;

        public bool Running = true;

        public ConcurrentQueue<Tuple<string,Task<CompletionResult>>> TaskQueue = new ConcurrentQueue<Tuple<string,Task<CompletionResult>>>();

        public static void Initialize()
        {
            _instance ??= new OpenAIManager();
        }
        public static OpenAIManager GetInstance()
        {
            return _instance;
        }

        private OpenAIManager()
        {
            OpenAIThread = new Thread(Run);
            OpenAIThread.IsBackground = true;
            OpenAIThread.Start();
            Login();
        }


        private void Run()
        {
            while (Running)
            {
                if(TaskQueue.TryDequeue(out Tuple<string,Task<CompletionResult>> tuple))
                {
                    try
                    {
                        Task<CompletionResult> result = tuple.Item2;
                        result.Wait();

                        if (result.IsCompleted)
                        {
                            CompletionResult completionResult = result.Result;
                            Choice choice = completionResult.Completions[0];
                            Conversation conversation = new Conversation(tuple.Item1.Trim('\n'), choice.Text.Trim('\n'));
                            AddChat(conversation);
                            OnConversation?.Invoke(this, new ConversationEventArgs(conversation));
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }


        public void SetCapacity(int capacity)
        {
            Capacity = capacity;
            if(Conversations == null)
            {
                Conversations = new Conversation[capacity];
            } else if (Conversations.Length > capacity)
            {
                Conversation[] newArray = new Conversation[capacity];

                for(int i = 0; i < newArray.Length; i++)
                {
                    newArray[i] = Conversations[i];
                }
                Conversations = newArray;
            } else if (Conversations.Length < capacity)
            {
                Conversation[] newArray = new Conversation[capacity];
                for (int i = 0; i < Conversations.Length; i++)
                {
                    newArray[i] = Conversations[i];
                }
                Conversations = newArray;
            }
        }

        public void AddChat(Conversation conversation)
        {
            if (Conversations == null || Conversations.Length == 0)
                return;

            for(int i = Conversations.Length - 1; i > 0; i--)
            {
                Conversations[i] = Conversations[i-1];
            }

            Conversations[0] = conversation;
        }

        public void AddChat(string prompt, string result)
        {
            AddChat(new Conversation(prompt, result));
        }

        public void ClearChat()
        {
            Conversations = new Conversation[Capacity];
        }


        private void Login()
        {
            if(CredentialManager.TryGetData(CredentialManager.GetChatGPTPath(), out string token))
            {
                Api = new OpenAIAPI(token);
            } else
            {
                Api = null;
            }
        }

        public string GetCurrentPrompt(string scenario, string request)
        {
            string prompt = scenario;

            if (Conversations != null && Conversations.Length > 0)
            {
                for (int i = Conversations.Length - 1; i >= 0; i--)
                {
                    Conversation conversation = Conversations[i];
                    if (conversation == null) continue;

                    prompt += $"\nUser: {conversation.UserPrompt}" +
                        $"\nYou: {conversation.Reply}";
                }
            }

            prompt += $"\nUser: {request}" +
                $"\nYou: ";

            return prompt;
        }

        public void SendMessage(string scenario, string message)
        {
            CompletionRequest request = new CompletionRequest(GetCurrentPrompt(scenario, message));
            request.Model = OpenAI_API.Models.Model.DavinciText;
            request.MaxTokens = 100;

            TaskQueue.Enqueue(new Tuple<string, Task<CompletionResult>>(message, Api.Completions.CreateCompletionAsync(request)));
        }
    }
}
