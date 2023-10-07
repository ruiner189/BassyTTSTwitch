using BassyTTSTwitch.Twitch.Events;
using BassyTTSTwitch.Twitch.Handlers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace BassyTTSTwitch.Twitch
{
    public class ChatListener
    {
        public static readonly Uri SocketAddress = new Uri("wss://irc-ws.chat.twitch.tv:443");
        public static readonly Uri AuthorizationAddress = new Uri("https://id.twitch.tv/oauth2/validate");
        public List<ChatMessageEventHandler> ChatMessageHandlers = new List<ChatMessageEventHandler>();

        private WebsocketClient client;
        public bool connected = false;

        public ChatListener()
        {
            Connect();
        }

        public void AddListener(ChatMessageEventHandler handler)
        {
            ChatMessageHandlers.Add(handler);
        }

        public void Connect()
        {
            if(client != null)
            {
                Disconnect();
            }
            client = new WebsocketClient(SocketAddress);
            client.MessageReceived.Subscribe(OnMessage);
            client.Start();
        }

        public void Login()
        {
            String token = "12c3jetaep1frrd8tydg71w7o5qepe";
            HttpClient client = new HttpClient();
            client.BaseAddress = AuthorizationAddress;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", token);

            System.Console.WriteLine("Sending request ");
            HttpResponseMessage response = client.GetAsync("").Result;
            // Parse the response body.
            Task<String> result = response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result.Result);

            String login = (String)json["login"];

            SendMessage($"PASS oauth:{token}");
            SendMessage($"NICK {login.ToLower()}");

            RequestPrivledges();
        }

        public void RequestPrivledges()
        {
            SendMessage("CAP REQ :twitch.tv/membership");
            SendMessage("CAP REQ :twitch.tv/tags");
            SendMessage("CAP REQ :twitch.tv/commands");
        }

        public void Disconnect()
        {
            client.Dispose();
            client = null;
            connected = false;
        }

        public void SendMessage(String message)
        {
            if (!connected) return;
            Console.WriteLine($"Sending: {message}");
            client.Send(message);
        }

        public void JoinChannel(string channel)
        {
            if (!connected && client != null)
            {
                connected = true;
                Login();
            }
            SendMessage($"JOIN #{channel.ToLower()}");
        }

        public void PartChannel(string channel)
        {
            SendMessage($"PART #{channel.ToLower()}");
        }


        public void OnMessage(ResponseMessage message)
        {
            String s = message.Text;
            Console.WriteLine($"|TWITCH| {s}");
            if (IsStartMessage(s)) return;
            if (IsPingMessage(s)) return;
            if (s.StartsWith(":tmi.twitch.tv CAP * ACK")) return;

/*            TwitchMessage tm = TwitchMessage.From(s);

            if(tm is ChatMessage cm)
            {
                Console.WriteLine(cm.Content);
                ChatMessageEvent cme = new ChatMessageEvent(cm);
                foreach(ChatMessageEventHandler handler in ChatMessageHandlers)
                {
                    handler.OnNewChatMessage(cme);
                }
            }*/
        }

        public bool IsStartMessage(String message)
        {
            if (message.StartsWith(":tmi.twitch.tv 001 businessbass :Welcome, GLHF!"))
                return true;
            return false;
        }

        public bool IsPingMessage(String message)
        {
            if(message.StartsWith("PING"))
            {
                Console.WriteLine($"Received Ping! {message}\n Ponging back");
                SendMessage("PONG :tmi.twitch.tv");
                return true;
            }
            return false;
        }

    }
}
