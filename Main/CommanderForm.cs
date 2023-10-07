using BassyTTSTwitch.Properties;
using BassyTTSTwitch.TTS;
using BassyTTSTwitch.Twitch.Cache;
using BassyTTSTwitch.Widgets;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassyTTSTwitch
{
    public partial class CommanderForm : Form
    {
        public static CommanderForm Instance;
        public ComboItem[] voices;

        public static readonly string[] WhiteListChannels = new string[]
        {
            "ruiner189",
            "axialmatt"
        };

        public CommanderForm()
        {
            Instance = this;
            InitializeComponent();
            PostInitializeComponent();
            FormClosing += OnClosed;
            UserManager.Instance.ClearUsers();
        }

        public void PostInitializeComponent()
        {
            UserList.Visible = true;

            DefaultSettings();
        }


        public void DefaultSettings()
        {
            Settings settings = Settings.Default;


            chatterSelectionUnit.DataSource = new ComboItem[] {
                new ComboItem(0, "Seconds"),
                new ComboItem(1, "Minutes"),
                new ComboItem(2, "Hours")
            };
            chatterSelectionUnit.SelectedIndex = settings.LastMessageUnit;
            chatterSelectionAmount.Value = settings.LastMessageSelectValue;

            autoSelectUnit.DataSource = new ComboItem[] {
                new ComboItem(0, "Seconds"),
                new ComboItem(1, "Minutes"),
                new ComboItem(2, "Hours")
            };
            autoSelectUnit.SelectedIndex = settings.AutoSelectUnit;
            autoSelectAmount.Value = settings.AutoSelectValue;

            Controls.Add(UserList);

            TwitchChannel.Text = settings.Channel;

            commandCheck.Checked = settings.ModCommands;

            NoRepeatCheckbox.Checked = settings.PreventDuplicates;
        }

        public void SaveSettings()
        {
            Settings settings = Settings.Default;

            settings.LastMessageUnit = chatterSelectionUnit.SelectedIndex;
            settings.LastMessageSelectValue = (int) chatterSelectionAmount.Value;

            settings.AutoSelectUnit = autoSelectUnit.SelectedIndex;
            settings.AutoSelectValue = (int) autoSelectAmount.Value;

            settings.Channel = TwitchChannel.Text;

            StringCollection collection = new StringCollection();

            for (int i = 0; i < filteredUsers.CheckedItems.Count; i++)
            {
                collection.Add(filteredUsers.CheckedItems[i].ToString());
            }

            settings.FilterViwers = collection;

            if(TTSManager.GetInstance().MessageWidget != null)
                settings.MessageWidgetSize = TTSManager.GetInstance().MessageWidget.Size;
            if(TTSManager.GetInstance().UserWidget != null)
                settings.UserWidgetSize = TTSManager.GetInstance().UserWidget.Size;

            settings.ModCommands = commandCheck.Checked;
            settings.Save();
        }



        private void OnClosed(object sender, EventArgs e)
        {
            SaveSettings();
            Application.Exit();
        }

        private void twitchChannel_TextChanged(object sender, EventArgs e)
        {
            connectLabel.Text = "Not Connected!";
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if(connectButton.Text == "Connect")
            {
                bool isWhitelist = false;
                string currentChannel = TwitchChannel.Text.ToLowerInvariant();

                if (currentChannel == null || currentChannel.Length == 0)
                {
                    connectLabel.Text = "Not Connected!";
                }

                foreach (string whitelist in WhiteListChannels)
                {
                    if (whitelist == currentChannel)
                    {
                        isWhitelist = true;
                        break;
                    }
                }


                if (isWhitelist)
                {
                    TTSManager.GetInstance().TwitchManager.JoinChannel(TwitchChannel.Text);
                    connectLabel.Text = "Connected!";

                    StartTTS.Enabled = true;
                    StartStopButton.Enabled = true;
                    SelectedViewerButton.Enabled = true;
                    RandomViewerButton.Enabled = true;
                    TwitchChannel.Enabled = false;
                    connectButton.Text = "Disconnect";
                }
                else
                {
                    connectLabel.Text = "Failed to connect. Channel not on whitelist";
                }
            } else
            {
                TTSManager.GetInstance().TwitchManager.PartChannel();
                StartTTS.Enabled = false;
                StartStopButton.Enabled = false;
                SelectedViewerButton.Enabled = false;
                RandomViewerButton.Enabled = false;
                TwitchChannel.Enabled = true;
                connectButton.Text = "Connect";
            }


        }

        private void VolumeTestButton_Click(object sender, EventArgs e)
        {
            string shortstring = "This is a test message";

            CloudManager.GetInstance().Speak(shortstring);
        }


        private void SelectedViewerButton_Click(object sender, EventArgs e)
        {
            if (UserList.SelectedItem == null) return;
            String username = UserList.SelectedItem.ToString();

            if(UserCache.Instance.TryGetUser(username, out User user))
            {
                TTSManager.GetInstance().SetUser(user);
            }
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            bool selectRandom = !TTSManager.GetInstance().DoSelectRandomUser;
            TTSManager.GetInstance().DoSelectRandomUser = selectRandom;

            if (selectRandom)
            {
                StartStopButton.Text = "Stop";
                TTSManager.GetInstance().SelectRandomUser();
            }
            else
            {
                StartStopButton.Text = "Start";
            }
        }

        private void StartTTS_Click(object sender, EventArgs e)
        {
            bool doTTS = !TTSManager.GetInstance().DoTTS;
            TTSManager.GetInstance().DoTTS = doTTS;

            if (doTTS)
            {
                StartTTS.Text = "Stop TTS";
            } else
            {
                StartTTS.Text = "Start TTS";
                TTSManager.GetInstance().StopTTS();
            }
        }

        private void OBSButton_Click(object sender, EventArgs e)
        {
            TTSManager.GetInstance().ShowWidgets();
        }

        private void filteredUsers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            bool value = e.NewValue == CheckState.Checked;
            string key = filteredUsers.Items[e.Index].ToString();

            Console.WriteLine($"{key} = {value}");

            if(key == "Mod")
            {
                UserManager.Instance.Mods = value;
            } else if (key == "Subscriber")
            {
                UserManager.Instance.Subscriber = value;
            } else if (key == "Regular User")
            {
                UserManager.Instance.Regular = value;
            } else if (key == "Broadcaster")
            {
                UserManager.Instance.Broadcaster = value;
            }

            UserManager.Instance.ForceUpdate();
        }

        private void chatterSelectionAmount_ValueChanged(object sender, EventArgs e)
        {
            UserManager.Instance.SetMessageFilterDuration((int)chatterSelectionAmount.Value, chatterSelectionUnit.SelectedIndex);
            UserManager.Instance.ForceUpdate();
        }

        private void chatterSelectionUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserManager.Instance.SetMessageFilterDuration((int)chatterSelectionAmount.Value, chatterSelectionUnit.SelectedIndex);
            UserManager.Instance.ForceUpdate();
        }

        private void RandomViewerButton_Click(object sender, EventArgs e)
        {
            TTSManager.GetInstance().SelectRandomUser();
        }


        private void commandCheck_CheckedChanged(object sender, EventArgs e)
        {
            TTSManager.GetInstance().TwitchManager.CommandManager.DoCommands = commandCheck.Checked;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsManager.OpenSettings();
        }

        private void NoRepeatCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            TTSManager.GetInstance().PreventRepeats(NoRepeatCheckbox.Checked);
        }
    }
}
