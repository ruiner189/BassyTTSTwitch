using BassyTTSTwitch.Audio;
using BassyTTSTwitch.Keyboard;
using BassyTTSTwitch.OpenAI;
using BassyTTSTwitch.Properties;
using BassyTTSTwitch.TTS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassyTTSTwitch
{
    public partial class ChatGPTForm : Form
    {
        private string CurrentText = "";
        private bool _recordReady = true;
        private bool _recording = false;

        private AudioState _currentAudio;
        private SpeechToText _speechToText;

        public ChatGPTForm()
        {
            Mic.Initialize();

            OpenAIManager.Initialize();
            OpenAIManager.GetInstance().SetCapacity(3);
            OpenAIManager.GetInstance().OnConversation += ChatGPTForm_OnConversation;

            Mic.MicStartEvent += OnMicStart;
            Mic.MicStopEvent += OnMicStop;

            InitializeComponent();
            PostInitializeComponent();

            FormClosing += OnClosed;
            KeyboardManager.GetInstance().KeyPressed += OnMicHotkey;

        }

        private void OnMicHotkey(object sender, KeyPressedEventArgs e)
        {
            if(e.Modifier == Keyboard.ModifierKeys.Control && e.Key == Keys.B)
            {
                BeginInvoke(new Action(Send));
            }
        }

        public void PostInitializeComponent()
        {


            DefaultSettings();
        }


        public void DefaultSettings()
        {
            Settings settings = Settings.Default;

            string preferedDevice = settings.InputDevice;
            int deviceId = 0;

            if(!(preferedDevice == null || preferedDevice.Length == 0))
            {
                foreach(DeviceInfo info in Mic.InputDevices)
                {
                    if(info.DeviceName == preferedDevice)
                    {
                        deviceId = info.DeviceID;
                        break;
                    }
                }
            }

            InputDevice.DataSource = Mic.InputDevices;
            InputDevice.SelectedIndex = deviceId;

            FontFamily messageFont = FontFamily.Families.FirstOrDefault(font => font.Name == settings.MessageFont.Name);
            FontFamily userFont = FontFamily.Families.FirstOrDefault(font => font.Name == settings.UserFont.Name);




            BackgroundText.Text = settings.CurrentBackgroundInformation;

            AutoButton.Checked = settings.AutoSend;
        }

        private void SaveSettings()
        {
            Settings settings = Settings.Default;


            if(TTSManager.GetInstance().MessageWidget != null && TTSManager.GetInstance().MessageWidget.IsHandleCreated)
                settings.MessageWidgetSize = TTSManager.GetInstance().MessageWidget.Size;

            settings.InputDevice = InputDevice.SelectedItem.ToString();
            settings.CurrentBackgroundInformation = BackgroundText.Text;

            settings.AutoSend = AutoButton.Checked;

            settings.Save();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            SaveSettings();
            Application.Exit();
        }



        private void ComboBoxFonts_DrawItem(object sender, DrawItemEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var fontFamily = (FontFamily)comboBox.Items[e.Index];
            var font = new Font(fontFamily, comboBox.Font.SizeInPoints);

            e.DrawBackground();
            e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
        }


        private void VolumeTestButton_Click(object sender, EventArgs e)
        {
            string shortstring = "This is a test message";

            CloudManager.GetInstance().Speak(shortstring);
        }

        /*
        private void messageColorButton_Click(object sender, EventArgs e)
        {
            if (messageColorDialog.ShowDialog() == DialogResult.OK)
            {
                TTSManager.GetInstance().MessageWidget.Label.ForeColor = messageColorButton.BackColor;
            };
        }
        */


        private void OBSButton_Click(object sender, EventArgs e)
        {
            TTSManager.GetInstance().ShowMessageWidget();
        }

        private void record_Click(object sender, EventArgs e)
        {
            if (!_recording)
            {
                Mic.StartRecording(30 * 1000);
                CurrentText = CurrentPrompt.Text;
            } else
            {
                Mic.StopRecording();
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void Send()
        {
            if (CurrentPrompt.Text.Trim() == "")
            {
                return;
            }
            _recordReady = false;
            _currentAudio = null;
            CurrentPrompt.Enabled = false;
            RecordButton.Enabled = false;
            SendButton.Enabled = false;
            Mic.SetUseHotKey(false);
            OpenAIManager.GetInstance().SendMessage(BackgroundText.Text, CurrentPrompt.Text);
            CurrentPrompt.Text = "";
        }

        private void InputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(InputDevice.SelectedItem is DeviceInfo info)
            {
                Console.WriteLine($"Changing to {info.DeviceName}");
                Mic.SetMicId(info.DeviceID);
            }
        }
        

        // Missing the final string. Needs to wait to get result after recording is done.
        private void RecordTimer_Tick(object sender, EventArgs e)
        {
            bool sttFinal =  _speechToText == null || _speechToText.IsFinalized();

            CurrentPrompt.Enabled = !_recording && sttFinal;
            SendButton.Enabled = !_recording && _recordReady;
            RecordButton.Enabled = _recording || _recordReady;


            if (Mic.IsRecording())
            {

                if (_speechToText != null)
                {

                    CurrentPrompt.Text = $"{CurrentText} {_speechToText.GetCurrentResultString()}";
                }
            }

            if (!_recordReady)
            {
                if (_currentAudio == null)
                    return;


                _recordReady = _currentAudio.CurrentState == AudioState.State.FINISHED && sttFinal ;

                if(_recordReady)
                    Mic.SetUseHotKey(true);
            }

            if (_speechToText != null && _speechToText.IsFinalized())
            {
                CurrentPrompt.Text = $"{CurrentText} {_speechToText.GetCurrentResultString()}";
                _speechToText = null;

                if (AutoButton.Checked)
                {
                    Send();
                }
            }

        }

        private void ChatGPTForm_OnConversation(object sender, ConversationEventArgs e)
        {
            Console.WriteLine($"ChatGPT: {e.Conversation.Reply}");

            ConversationBox.BeginInvoke(new Action(() => SynchChatBox()));
            _currentAudio = CloudManager.GetInstance().Speak(e.Conversation.Reply);
            TTSManager.GetInstance().ChangeTexts(new MessageSnippet(e.Conversation.Reply, "ChatGPT"));
        }

        private void SynchChatBox()
        {
            string chat = "";

            Conversation[] conversations = OpenAIManager.GetInstance().Conversations;

            for(int i = conversations.Length - 1; i >= 0; i--)
            {
                Conversation conversation = conversations[i];

                if (conversation != null)
                {
                    chat += $"You: {conversation.UserPrompt}" +
                        $"\n\nChatGPT: {conversation.Reply}\n\n";
                }
            }
            ConversationBox.Text = chat;
        }

        private void OnMicStart(object sender, MicEvent e)
        {
            CurrentPrompt.Enabled = false;
            RecordButton.Enabled = false;
            SendButton.Enabled = false;
            CurrentText = CurrentPrompt.Text;
            _speechToText = e.SpeechToText;
            _recording = true;
            RecordButton.BeginInvoke(new Action(() =>
            {
                RecordButton.Text = "Stop Recording";

            }));
        }

        private void OnMicStop(object sender, MicEvent e)
        {
            _recording = false;
            RecordButton.BeginInvoke(new Action(() =>
            {
                RecordButton.Text = "Record";

            }));
        }

        private void ChatClearButton_Click(object sender, EventArgs e)
        {
            OpenAIManager.GetInstance().ClearChat();
            SynchChatBox();
        }

        private void AutoButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsManager.OpenSettings();
        }
    }
}
