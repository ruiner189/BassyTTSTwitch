
using System.Linq;

namespace BassyTTSTwitch
{
    partial class CommanderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommanderForm));
            this.OBSButton = new System.Windows.Forms.Button();
            this.userColorDialog = new System.Windows.Forms.ColorDialog();
            this.messageColorDialog = new System.Windows.Forms.ColorDialog();
            this.StartTTS = new System.Windows.Forms.Button();
            this.TwitchChannelLabel = new System.Windows.Forms.Label();
            this.TwitchChannel = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.connectLabel = new System.Windows.Forms.Label();
            this.UserList = new System.Windows.Forms.ListBox();
            this.restrictedRoleLabel = new System.Windows.Forms.Label();
            this.chatterSelectionAmount = new System.Windows.Forms.NumericUpDown();
            this.SelectedViewerButton = new System.Windows.Forms.Button();
            this.chatterSelectionUnit = new System.Windows.Forms.ComboBox();
            this.ActiveTimeLabel = new System.Windows.Forms.Label();
            this.AutoLabel = new System.Windows.Forms.Label();
            this.filteredUsers = new System.Windows.Forms.CheckedListBox();
            this.autoSelectAmount = new System.Windows.Forms.NumericUpDown();
            this.ViewerLabel = new System.Windows.Forms.Label();
            this.RandomViewerButton = new System.Windows.Forms.Button();
            this.autoSelectUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.commandCheck = new System.Windows.Forms.CheckBox();
            this.MainTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.NoRepeatCheckbox = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SettingsButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.chatterSelectionAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoSelectAmount)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OBSButton
            // 
            this.OBSButton.Location = new System.Drawing.Point(49, 349);
            this.OBSButton.Name = "OBSButton";
            this.OBSButton.Size = new System.Drawing.Size(147, 23);
            this.OBSButton.TabIndex = 6;
            this.OBSButton.Text = "OBS Widgets";
            this.OBSButton.UseVisualStyleBackColor = true;
            this.OBSButton.Click += new System.EventHandler(this.OBSButton_Click);
            // 
            // userColorDialog
            // 
            this.userColorDialog.SolidColorOnly = true;
            // 
            // messageColorDialog
            // 
            this.messageColorDialog.SolidColorOnly = true;
            // 
            // StartTTS
            // 
            this.StartTTS.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.StartTTS.Enabled = false;
            this.StartTTS.Location = new System.Drawing.Point(49, 397);
            this.StartTTS.Name = "StartTTS";
            this.StartTTS.Size = new System.Drawing.Size(147, 23);
            this.StartTTS.TabIndex = 9;
            this.StartTTS.Text = "Start TTS";
            this.MainTooltip.SetToolTip(this.StartTTS, "Enables Text-To-Speech. When disabled, removes currently selected user.");
            this.StartTTS.UseVisualStyleBackColor = false;
            this.StartTTS.Click += new System.EventHandler(this.StartTTS_Click);
            // 
            // TwitchChannelLabel
            // 
            this.TwitchChannelLabel.AutoSize = true;
            this.TwitchChannelLabel.Location = new System.Drawing.Point(90, 30);
            this.TwitchChannelLabel.Name = "TwitchChannelLabel";
            this.TwitchChannelLabel.Size = new System.Drawing.Size(81, 13);
            this.TwitchChannelLabel.TabIndex = 0;
            this.TwitchChannelLabel.Text = "Twitch Channel";
            // 
            // TwitchChannel
            // 
            this.TwitchChannel.Location = new System.Drawing.Point(18, 55);
            this.TwitchChannel.MaxLength = 100;
            this.TwitchChannel.Name = "TwitchChannel";
            this.TwitchChannel.Size = new System.Drawing.Size(100, 20);
            this.TwitchChannel.TabIndex = 1;
            this.TwitchChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TwitchChannel.TextChanged += new System.EventHandler(this.twitchChannel_TextChanged);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(147, 54);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 19;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // connectLabel
            // 
            this.connectLabel.AutoSize = true;
            this.connectLabel.Location = new System.Drawing.Point(26, 83);
            this.connectLabel.Name = "connectLabel";
            this.connectLabel.Size = new System.Drawing.Size(82, 13);
            this.connectLabel.TabIndex = 20;
            this.connectLabel.Text = "Not Connected!";
            this.connectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserList
            // 
            this.UserList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.UserList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserList.FormattingEnabled = true;
            this.UserList.ItemHeight = 16;
            this.UserList.Location = new System.Drawing.Point(304, 42);
            this.UserList.Name = "UserList";
            this.UserList.Size = new System.Drawing.Size(140, 388);
            this.UserList.Sorted = true;
            this.UserList.TabIndex = 23;
            // 
            // restrictedRoleLabel
            // 
            this.restrictedRoleLabel.AutoSize = true;
            this.restrictedRoleLabel.BackColor = System.Drawing.SystemColors.Window;
            this.restrictedRoleLabel.Location = new System.Drawing.Point(491, 120);
            this.restrictedRoleLabel.Name = "restrictedRoleLabel";
            this.restrictedRoleLabel.Size = new System.Drawing.Size(69, 13);
            this.restrictedRoleLabel.TabIndex = 11;
            this.restrictedRoleLabel.Text = "Filter Viewers";
            // 
            // chatterSelectionAmount
            // 
            this.chatterSelectionAmount.Location = new System.Drawing.Point(462, 248);
            this.chatterSelectionAmount.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.chatterSelectionAmount.Name = "chatterSelectionAmount";
            this.chatterSelectionAmount.Size = new System.Drawing.Size(40, 20);
            this.chatterSelectionAmount.TabIndex = 13;
            this.chatterSelectionAmount.ValueChanged += new System.EventHandler(this.chatterSelectionAmount_ValueChanged);
            // 
            // SelectedViewerButton
            // 
            this.SelectedViewerButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SelectedViewerButton.Enabled = false;
            this.SelectedViewerButton.Location = new System.Drawing.Point(473, 45);
            this.SelectedViewerButton.Name = "SelectedViewerButton";
            this.SelectedViewerButton.Size = new System.Drawing.Size(108, 23);
            this.SelectedViewerButton.TabIndex = 8;
            this.SelectedViewerButton.Text = "Selected Viewer";
            this.SelectedViewerButton.UseVisualStyleBackColor = false;
            this.SelectedViewerButton.Click += new System.EventHandler(this.SelectedViewerButton_Click);
            // 
            // chatterSelectionUnit
            // 
            this.chatterSelectionUnit.DisplayMember = "Text";
            this.chatterSelectionUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chatterSelectionUnit.FormattingEnabled = true;
            this.chatterSelectionUnit.Items.AddRange(new object[] {
            "Seconds",
            "Minutes"});
            this.chatterSelectionUnit.Location = new System.Drawing.Point(515, 248);
            this.chatterSelectionUnit.Name = "chatterSelectionUnit";
            this.chatterSelectionUnit.Size = new System.Drawing.Size(72, 21);
            this.chatterSelectionUnit.TabIndex = 15;
            this.chatterSelectionUnit.ValueMember = "ID";
            this.chatterSelectionUnit.SelectedIndexChanged += new System.EventHandler(this.chatterSelectionUnit_SelectedIndexChanged);
            // 
            // ActiveTimeLabel
            // 
            this.ActiveTimeLabel.AutoSize = true;
            this.ActiveTimeLabel.BackColor = System.Drawing.SystemColors.Window;
            this.ActiveTimeLabel.Location = new System.Drawing.Point(470, 223);
            this.ActiveTimeLabel.Name = "ActiveTimeLabel";
            this.ActiveTimeLabel.Size = new System.Drawing.Size(108, 13);
            this.ActiveTimeLabel.TabIndex = 14;
            this.ActiveTimeLabel.Text = "Viewer Last Message";
            this.MainTooltip.SetToolTip(this.ActiveTimeLabel, "How long ago the user has sent a message to be considered in auto-selection");
            // 
            // AutoLabel
            // 
            this.AutoLabel.AutoSize = true;
            this.AutoLabel.BackColor = System.Drawing.SystemColors.Window;
            this.AutoLabel.Location = new System.Drawing.Point(478, 335);
            this.AutoLabel.Name = "AutoLabel";
            this.AutoLabel.Size = new System.Drawing.Size(92, 13);
            this.AutoLabel.TabIndex = 17;
            this.AutoLabel.Text = "Autoselect Viewer";
            this.MainTooltip.SetToolTip(this.AutoLabel, "How long it takes for the next user to be selected.");
            // 
            // filteredUsers
            // 
            this.filteredUsers.FormattingEnabled = true;
            this.filteredUsers.Items.AddRange(new object[] {
            "Regular User",
            "Subscriber",
            "Mod",
            "Broadcaster"});
            this.filteredUsers.Location = new System.Drawing.Point(479, 146);
            this.filteredUsers.Name = "filteredUsers";
            this.filteredUsers.Size = new System.Drawing.Size(91, 64);
            this.filteredUsers.TabIndex = 10;
            this.filteredUsers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.filteredUsers_ItemCheck);
            // 
            // autoSelectAmount
            // 
            this.autoSelectAmount.Location = new System.Drawing.Point(462, 362);
            this.autoSelectAmount.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.autoSelectAmount.Name = "autoSelectAmount";
            this.autoSelectAmount.Size = new System.Drawing.Size(40, 20);
            this.autoSelectAmount.TabIndex = 16;
            this.autoSelectAmount.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // ViewerLabel
            // 
            this.ViewerLabel.AutoSize = true;
            this.ViewerLabel.Location = new System.Drawing.Point(346, 26);
            this.ViewerLabel.Name = "ViewerLabel";
            this.ViewerLabel.Size = new System.Drawing.Size(44, 13);
            this.ViewerLabel.TabIndex = 4;
            this.ViewerLabel.Text = "Viewers";
            // 
            // RandomViewerButton
            // 
            this.RandomViewerButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RandomViewerButton.Enabled = false;
            this.RandomViewerButton.Location = new System.Drawing.Point(473, 80);
            this.RandomViewerButton.Name = "RandomViewerButton";
            this.RandomViewerButton.Size = new System.Drawing.Size(108, 23);
            this.RandomViewerButton.TabIndex = 7;
            this.RandomViewerButton.Text = "Random Viewer";
            this.RandomViewerButton.UseVisualStyleBackColor = false;
            this.RandomViewerButton.Click += new System.EventHandler(this.RandomViewerButton_Click);
            // 
            // autoSelectUnit
            // 
            this.autoSelectUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.autoSelectUnit.FormattingEnabled = true;
            this.autoSelectUnit.Items.AddRange(new object[] {
            "Seconds",
            "Minutes"});
            this.autoSelectUnit.Location = new System.Drawing.Point(515, 362);
            this.autoSelectUnit.Name = "autoSelectUnit";
            this.autoSelectUnit.Size = new System.Drawing.Size(72, 21);
            this.autoSelectUnit.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(482, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Select TTS User";
            // 
            // StartStopButton
            // 
            this.StartStopButton.Enabled = false;
            this.StartStopButton.Location = new System.Drawing.Point(485, 398);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(75, 23);
            this.StartStopButton.TabIndex = 2;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(257, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 420);
            this.panel1.TabIndex = 24;
            // 
            // commandCheck
            // 
            this.commandCheck.AutoSize = true;
            this.commandCheck.Location = new System.Drawing.Point(18, 119);
            this.commandCheck.Name = "commandCheck";
            this.commandCheck.Size = new System.Drawing.Size(138, 17);
            this.commandCheck.TabIndex = 26;
            this.commandCheck.Text = "Enable Mod Commands";
            this.MainTooltip.SetToolTip(this.commandCheck, "!random => Select a random viewer\r\n!stop => Remove current user from selection an" +
        "d stops all audio");
            this.commandCheck.UseVisualStyleBackColor = true;
            this.commandCheck.CheckedChanged += new System.EventHandler(this.commandCheck_CheckedChanged);
            // 
            // MainTooltip
            // 
            this.MainTooltip.AutoPopDelay = 5000;
            this.MainTooltip.InitialDelay = 100;
            this.MainTooltip.ReshowDelay = 100;
            // 
            // NoRepeatCheckbox
            // 
            this.NoRepeatCheckbox.AutoSize = true;
            this.NoRepeatCheckbox.Location = new System.Drawing.Point(485, 288);
            this.NoRepeatCheckbox.Name = "NoRepeatCheckbox";
            this.NoRepeatCheckbox.Size = new System.Drawing.Size(83, 17);
            this.NoRepeatCheckbox.TabIndex = 32;
            this.NoRepeatCheckbox.Text = "No Repeats";
            this.MainTooltip.SetToolTip(this.NoRepeatCheckbox, "Only used for Random Viewer and Autoselect Viewer\r\n\r\nEnabled this makes it so tha" +
        "t the next person selected is not a repeat.\r\n\r\nIf there is no non-repeats left, " +
        "it will then pick a repeat");
            this.NoRepeatCheckbox.UseVisualStyleBackColor = true;
            this.NoRepeatCheckbox.CheckedChanged += new System.EventHandler(this.NoRepeatCheckbox_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(601, 25);
            this.toolStrip1.TabIndex = 34;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SettingsButton
            // 
            this.SettingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SettingsButton.Image = ((System.Drawing.Image)(resources.GetObject("SettingsButton.Image")));
            this.SettingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(53, 22);
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.ToolTipText = "Settings";
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // CommanderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(601, 437);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.NoRepeatCheckbox);
            this.Controls.Add(this.commandCheck);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.OBSButton);
            this.Controls.Add(this.connectLabel);
            this.Controls.Add(this.StartTTS);
            this.Controls.Add(this.TwitchChannelLabel);
            this.Controls.Add(this.TwitchChannel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.StartStopButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.autoSelectUnit);
            this.Controls.Add(this.RandomViewerButton);
            this.Controls.Add(this.ViewerLabel);
            this.Controls.Add(this.autoSelectAmount);
            this.Controls.Add(this.filteredUsers);
            this.Controls.Add(this.AutoLabel);
            this.Controls.Add(this.ActiveTimeLabel);
            this.Controls.Add(this.chatterSelectionUnit);
            this.Controls.Add(this.SelectedViewerButton);
            this.Controls.Add(this.chatterSelectionAmount);
            this.Controls.Add(this.restrictedRoleLabel);
            this.Controls.Add(this.UserList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CommanderForm";
            this.Text = "BassyTTSTwitch";
            ((System.ComponentModel.ISupportInitialize)(this.chatterSelectionAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoSelectAmount)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button OBSButton;
        public System.Windows.Forms.ColorDialog userColorDialog;
        public System.Windows.Forms.ColorDialog messageColorDialog;
        public System.Windows.Forms.Label connectLabel;
        public System.Windows.Forms.Button connectButton;
        public System.Windows.Forms.TextBox TwitchChannel;
        public System.Windows.Forms.Label TwitchChannelLabel;
        public System.Windows.Forms.Button StartTTS;
        public System.Windows.Forms.ListBox UserList;
        public System.Windows.Forms.Label restrictedRoleLabel;
        public System.Windows.Forms.NumericUpDown chatterSelectionAmount;
        public System.Windows.Forms.Button SelectedViewerButton;
        public System.Windows.Forms.ComboBox chatterSelectionUnit;
        public System.Windows.Forms.Label ActiveTimeLabel;
        public System.Windows.Forms.Label AutoLabel;
        public System.Windows.Forms.CheckedListBox filteredUsers;
        public System.Windows.Forms.NumericUpDown autoSelectAmount;
        public System.Windows.Forms.Label ViewerLabel;
        public System.Windows.Forms.Button RandomViewerButton;
        public System.Windows.Forms.ComboBox autoSelectUnit;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button StartStopButton;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.CheckBox commandCheck;
        public System.Windows.Forms.ToolTip MainTooltip;
        public System.Windows.Forms.CheckBox NoRepeatCheckbox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SettingsButton;
    }
}