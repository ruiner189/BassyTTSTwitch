using BassyTTSTwitch.Properties;
using BassyTTSTwitch.TTS;
using BassyTTSTwitch.Twitch;
using BassyTTSTwitch.Twitch.Cache;
using BassyTTSTwitch.Widgets;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace BassyTTSTwitch
{
    public class TTSManager
    {
        private static TTSManager _instance;
        public TwitchManager TwitchManager;
        public MessageWidget MessageWidget;
        public UserWidget UserWidget;
        public Filter Filter;

        public User CurrentlySelectedUser;

        public string CurrentChannel;

        private readonly Thread _runThread;

        public long LastUserSelectedTime;
        public long AutoSelectDuration = 5000 * 60;
        public bool DoSelectRandomUser = false;
        public bool DoTTS = false;

        private bool AnnounceUser = true;
        private bool NoRepeats = false;
        private long ChatDelay = 500;
        private long HideMessageDelay = 5000;

        public volatile ConcurrentQueue<MessageSnippet> QueuedMessages = new ConcurrentQueue<MessageSnippet>();
        public Prompt LastPrompt;

        private bool _showWidgets = false;

        public MessageSnippet CurrentSnippet;

        public User[] LastUsers = new User[10];
        public ConcurrentBag<User> AllLastUsers = new ConcurrentBag<User>();
        public string AnnounceText = "{$user} is the next commander!";

        public static void Initialize()
        {
            _instance ??= new TTSManager();
        }

        public static TTSManager GetInstance()
        {
            return _instance;
        }

        private TTSManager()
        {
            LastUserSelectedTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            TwitchManager = new TwitchManager();
            TwitchManager.Client.OnMessageReceived += OnNewChatMessage;
            Filter = new Filter();
            _runThread = new Thread(Run);
            _runThread.IsBackground = true;
            _runThread.Start();
            SettingsManager.OnSave += OnSettingsChanged;
            LoadSettings(Settings.Default);
        }

        private void OnSettingsChanged(object sender, SaveEvent e)
        {
            LoadSettings(e.Settings);
        }

        private void LoadSettings(Settings s)
        {
            SetAutoSelectDuration(s.AutoSelectValue, s.AutoSelectUnit);
            AnnounceUser = s.AnnounceUser;
            AnnounceText = s.AnnouncementText;
            ChatDelay = (long) s.ChatDelay * 1000;
            HideMessageDelay = (long) s.MessageHideDelay * 1000;
        }

        private void SetAutoSelectDuration(long duration)
        {
            AutoSelectDuration = duration;
            LastUserSelectedTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        private void SetAutoSelectDuration(int value, int unit)
        {
            int multiplier = 1;
            if (unit == 0)
                multiplier = 1000; // Seconds
            if (unit == 1)
                multiplier = 1000 * 60; // Minutes
            if (unit == 2)
                multiplier = 1000 * 3600; // Hours

            SetAutoSelectDuration(value * multiplier);
        }

        private void Run()
        {
              while (true)
              {
                try
                {
                    UserManager.Instance.Update();
                    if (LastPrompt == null || LastPrompt.IsCompleted)
                    {
                        RandomUserLoop();
                        NextVoiceLoop();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void RandomUserLoop()
        {
            long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (DoSelectRandomUser && LastUserSelectedTime + AutoSelectDuration <= currentTime)
            {
                ClearQueue();
                SelectRandomUser();                
            }
        }

        private bool IsVoiceComplete = false;
        private long NextMessage;
        private long HideMessage;
        private void NextVoiceLoop()
        {
            if (DoTTS && CloudManager.GetInstance().IsReady())
            {
                long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                if (!IsVoiceComplete)
                {
                    IsVoiceComplete = true;
                    NextMessage = currentTime + ChatDelay;                    
                    return;
                }

                else if (currentTime >= NextMessage )
                {
                    DoNextVoice(currentTime >= HideMessage);
                }
            }
        }

        public void PreventRepeats(bool preventRepeat)
        {
            NoRepeats = preventRepeat;
        }

        public bool SelectRandomUser()
        {
            UserManager manager = UserManager.Instance;

            if (manager.GetFilteredUsers().Count == 0 && CurrentlySelectedUser == null)
            {
                return false;
            }


            Console.WriteLine("Trying to select next user");

            User randomUser = null;

            if (NoRepeats)
            {
                var list = manager.GetFilteredUsers(AllLastUsers.ToArray());
                randomUser = manager.GetRandomUserFromList(list);

                if(randomUser != null)
                {
                    SetUser(randomUser);
                    return true;
                }
            }

            for(int i = 0; i < 20; i++)
            {
                randomUser = manager.GetRandomUser(LastUsers);
                if (randomUser != null) break;
            }

            randomUser ??= manager.GetRandomUser(null);

            SetUser(randomUser);

            return randomUser != null;
        }

        public void StopTTS()
        {
            SetUser(null);
            CloudManager.GetInstance().StopSound();
        }

        public void SetUser(User user)
        {
            CurrentlySelectedUser = user;
            LastUserSelectedTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            if (CurrentlySelectedUser != null)
            {
                if (UserWidget != null && UserWidget.Visible)
                    UserWidget.BeginInvoke((Action)(() => {
                        ChangeTexts(new MessageSnippet("", CurrentlySelectedUser.Name));
                    }));
                CloudManager.GetInstance().StopSound();
                AddUserToWaitQueue(user);
                if (!AllLastUsers.Contains(user))
                    AllLastUsers.Add(user);
                PlayNewUserSound(user);
            }
            else
            {
                if (UserWidget != null && UserWidget.Visible)
                    UserWidget.BeginInvoke((Action)(() => {
                        ChangeTexts(new MessageSnippet("", "")); // TODO CHANGE BEFORE PUBLISH 
                    }));
                CloudManager.GetInstance().StopSound();
            }
        }

        public void PlayNewUserSound(User user)
        {
            if (AnnounceUser)
            {
                string prompt = AnnounceText.Replace("{$user}", user.Name);
                    //$"{user.Name} is the new commander!";
                CloudManager.GetInstance().Speak(prompt);
            }
        }

        private void AddUserToWaitQueue(User user)
        {
            for(int i = 0; i < LastUsers.Length - 1; i++)
            {
                LastUsers[i] = LastUsers[i + 1];
            }
            LastUsers[LastUsers.Length - 1] = user;
        }

        private void DoNextVoice(bool hideTextOnFail)
        {
            if(QueuedMessages.Count > 0)
            {
                if(QueuedMessages.TryDequeue(out MessageSnippet snippet))
                {
                    HideMessage = Util.Now() + HideMessageDelay;
                    IsVoiceComplete = false;
                
                    ChangeTexts(snippet);

                    CloudManager.GetInstance().Speak(snippet.Message);
                }
            } else if (hideTextOnFail)
            {
                if(CurrentlySelectedUser != null)
                {
                    
                    ChangeTexts(new MessageSnippet("", CurrentlySelectedUser.Name));
                    
                } else
                {
                   
                    ChangeTexts(new MessageSnippet("", ""));
                    
                }

            }
        }

        private void ClearQueue()
        {
            QueuedMessages = new ConcurrentQueue<MessageSnippet>();
        }

        public void ChangeTexts(MessageSnippet snippet, bool invoke = true)
        {
            if(snippet != CurrentSnippet)
            {
                CurrentSnippet = snippet;

                if (MessageWidget != null && MessageWidget.IsHandleCreated)
                {
                    if (invoke)
                        MessageWidget.BeginInvoke((Action)(() =>
                        {
                            InternalChangeTexts(snippet);
                        }));
                    else
                        InternalChangeTexts(snippet);
                }
            }
        }

        private void InternalChangeTexts(MessageSnippet snippet)
        {
            UserWidget.Label.Text = snippet.Author;
            MessageWidget.Label.Text = snippet.Message;
            
        }
        public void ForceChangeTexts(MessageSnippet snippet)
        {
            
            if (MessageWidget != null && MessageWidget.IsHandleCreated)
            {
                MessageWidget.BeginInvoke((Action)(() => {
                    UserWidget.Label.Text = snippet.Author;
                    MessageWidget.Label.Text = snippet.Message;
                }));
            }
        }

        private void BuildWidget(bool user = true, bool message = true)
        {
            if(UserWidget == null && user)
                UserWidget = new UserWidget();

            if(MessageWidget == null && message)
                MessageWidget = new MessageWidget();
        }

        public void ShowWidgets()
        {
            BuildWidget();
            UserWidget.Show();
            MessageWidget.Show();

            if(CurrentlySelectedUser != null)
            {
                ChangeTexts(CurrentSnippet);
            }
            _showWidgets = true;
        }

        public void ShowMessageWidget()
        {
            BuildWidget(false, true);
            MessageWidget.Show();

            if (CurrentlySelectedUser != null)
            {
                ChangeTexts(new MessageSnippet("", CurrentlySelectedUser.Name));
            }
            _showWidgets = true;
        }

        public void HideWidget()
        {
            UserWidget?.Hide();
            MessageWidget?.Hide();
            _showWidgets = false;
        }

        public void ToggleWidgets()
        {
            if (_showWidgets)
                HideWidget();
            else
                ShowWidgets();
        }

        public void OnNewChatMessage(object sender, OnMessageReceivedArgs args)
        {
            if (!DoTTS) return;

            ChatMessage message = args.ChatMessage;
            Console.WriteLine(message);

            if (message.Channel.ToLowerInvariant() != TwitchManager.CurrentChannel.ToLowerInvariant())
            {
                Console.WriteLine($"Message is from the incorrect channel! How is this possible? {message.Channel.ToLowerInvariant()} != {CurrentChannel.ToLowerInvariant()}");
                return;
            }
            if(UserCache.Instance.TryGetUser(message.DisplayName, out User user))
            {
                if(CurrentlySelectedUser == user)
                {
                    if(TwitchManager.CommandManager.IsMessageCommand(message.Message)) return;

                    if (Filter.FilterText(message.Message, out string filteredText))
                    {
                        Console.WriteLine($"Adding message to queue: {filteredText}");
                        QueuedMessages.Enqueue(new MessageSnippet(filteredText, user.Name));
                    }
                }
            }
            else
            {
                Console.WriteLine($"Failed to get user {message.Username}");
            }
        }
    }
}
