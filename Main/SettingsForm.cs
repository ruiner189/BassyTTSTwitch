using BassyTTSTwitch.Properties;
using BassyTTSTwitch.TTS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassyTTSTwitch
{
    public partial class SettingsForm : Form
    {
        public ComboItem[] voices;
        public SettingsForm()
        {
            InitializeComponent();
            PostInitializeComponent();
            LoadSettings();
            tabControl1.DrawItem += tabControl1_DrawItem;
        }

        private void PostInitializeComponent()
        {
            messageFontBox.DrawMode = DrawMode.OwnerDrawFixed;
            messageFontBox.DrawItem += ComboBoxFonts_DrawItem;
            messageFontBox.DataSource = FontFamily.Families.ToList();

            userFontBox.DrawMode = DrawMode.OwnerDrawFixed;
            userFontBox.DrawItem += ComboBoxFonts_DrawItem;
            userFontBox.DataSource = FontFamily.Families.ToList();
        }

        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", 10.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }




        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        internal void LoadSettings()
        {
            Settings settings = Settings.Default;

            CloudManager cloud = CloudManager.GetInstance();

            voices = new ComboItem[cloud.Voices.Count];

            int selectedVoiceIndex = 0;
            for (int i = 0; i < voices.Length; i++)
            {
                voices[i] = new ComboItem(i, cloud.Voices[i].Name);
                if (cloud.Voices[i].Name == settings.CurrentVoice)
                    selectedVoiceIndex = i;
            }

            voiceChoice.DataSource = voices;
            voiceChoice.SelectedIndex = selectedVoiceIndex;

            userColorButton.BackColor = settings.UserColor;
            userColorBackground.BackColor = settings.UserBackgroundColor;

            messageColorButton.BackColor = settings.MessageColor;
            messageColorBackground.BackColor = settings.MessageBackgroundColor;

            FontFamily messageFont = FontFamily.Families.FirstOrDefault(font => font.Name == settings.MessageFont.Name);
            FontFamily userFont = FontFamily.Families.FirstOrDefault(font => font.Name == settings.UserFont.Name);
            messageFontBox.SelectedItem = messageFont;
            userFontBox.SelectedItem = userFont;


            volumePercentLabel.Text = $"{settings.Volume}%";
            volumeBar.Value = settings.Volume;

            chatDelay.Value = settings.ChatDelay;
            messageDelay.Value = settings.MessageHideDelay;

            shouldAnnounceBox.Checked = settings.AnnounceUser;
            AnnouncementTextBox.Text = settings.AnnouncementText;
        }

        internal void SaveSettings()
        {
            Settings settings = Settings.Default;

            settings.UserColor = userColorButton.BackColor;
            settings.MessageColor = messageColorButton.BackColor;
            settings.UserBackgroundColor = userColorBackground.BackColor;
            settings.MessageBackgroundColor = messageColorBackground.BackColor;

            settings.MessageFont = new Font((FontFamily)messageFontBox.SelectedItem, 8.25f);
            settings.UserFont = new Font((FontFamily)userFontBox.SelectedItem, 8.25f);

            settings.Volume = volumeBar.Value;

            settings.CurrentVoice = voices[voiceChoice.SelectedIndex].Text;

            if (TTSManager.GetInstance().MessageWidget != null)
                settings.MessageWidgetSize = TTSManager.GetInstance().MessageWidget.Size;
            if (TTSManager.GetInstance().UserWidget != null)
                settings.UserWidgetSize = TTSManager.GetInstance().UserWidget.Size;

            settings.ChatDelay = chatDelay.Value;
            settings.MessageHideDelay = messageDelay.Value;
            settings.AnnounceUser = shouldAnnounceBox.Checked;
            settings.AnnouncementText = AnnouncementTextBox.Text;
            settings.Save();
            SettingsManager.OnSave?.Invoke(this, new SaveEvent(Settings.Default));
        }

        private void ComboBoxFonts_DrawItem(object sender, DrawItemEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var fontFamily = (FontFamily)comboBox.Items[e.Index];
            var font = new Font(fontFamily, comboBox.Font.SizeInPoints);

            e.DrawBackground();
            e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
        }

        private void SaveAndCloseButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            DialogResult = DialogResult.Cancel;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloudManager.GetInstance().SetVolume(Settings.Default.Volume);
            DialogResult = DialogResult.Cancel;
        }

        private void userColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userColorButton.BackColor = colorDialog1.Color;
            };
        }

        private void userColorBackground_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userColorBackground.BackColor = colorDialog1.Color;
            };
        }

        private void messageColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                messageColorButton.BackColor = colorDialog1.Color;
            };
        }

        private void messageColorBackground_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                messageColorBackground.BackColor = colorDialog1.Color;
            };
        }

        private void VolumeTestButton_Click(object sender, EventArgs e)
        {
            string voice = voices[voiceChoice.SelectedIndex].Text;
            CloudManager.GetInstance().Speak("This is a test message", voice);
        }

        private void volumeBar_Scroll(object sender, EventArgs e)
        {
            volumePercentLabel.Text = $"{volumeBar.Value}%";
            CloudManager.GetInstance().SetVolume(volumeBar.Value);
        }

        private void volumePercentLabel_Click(object sender, EventArgs e)
        {

        }
    }


}
