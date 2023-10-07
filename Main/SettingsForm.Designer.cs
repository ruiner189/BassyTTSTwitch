namespace BassyTTSTwitch
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.WidgetTab = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.messageDelay = new System.Windows.Forms.NumericUpDown();
            this.ChatDelayLabel = new System.Windows.Forms.Label();
            this.chatDelay = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.messageColorBackground = new System.Windows.Forms.Button();
            this.userColorBackground = new System.Windows.Forms.Button();
            this.messageColorButton = new System.Windows.Forms.Button();
            this.messageFontBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userFontBox = new System.Windows.Forms.ComboBox();
            this.userColorButton = new System.Windows.Forms.Button();
            this.TTSTab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.voiceChoice = new System.Windows.Forms.ComboBox();
            this.VolumeTestButton = new System.Windows.Forms.Button();
            this.volumePercentLabel = new System.Windows.Forms.Label();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.VolumeLabel = new System.Windows.Forms.Label();
            this.AnnouncementTextBox = new System.Windows.Forms.TextBox();
            this.shouldAnnounceBox = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveAndCloseButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.WidgetTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatDelay)).BeginInit();
            this.TTSTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // WidgetTab
            // 
            this.WidgetTab.BackColor = System.Drawing.Color.White;
            this.WidgetTab.Controls.Add(this.label4);
            this.WidgetTab.Controls.Add(this.messageDelay);
            this.WidgetTab.Controls.Add(this.ChatDelayLabel);
            this.WidgetTab.Controls.Add(this.chatDelay);
            this.WidgetTab.Controls.Add(this.label7);
            this.WidgetTab.Controls.Add(this.label8);
            this.WidgetTab.Controls.Add(this.label6);
            this.WidgetTab.Controls.Add(this.label5);
            this.WidgetTab.Controls.Add(this.messageColorBackground);
            this.WidgetTab.Controls.Add(this.userColorBackground);
            this.WidgetTab.Controls.Add(this.messageColorButton);
            this.WidgetTab.Controls.Add(this.messageFontBox);
            this.WidgetTab.Controls.Add(this.label2);
            this.WidgetTab.Controls.Add(this.label3);
            this.WidgetTab.Controls.Add(this.userFontBox);
            this.WidgetTab.Controls.Add(this.userColorButton);
            this.WidgetTab.Location = new System.Drawing.Point(104, 4);
            this.WidgetTab.Name = "WidgetTab";
            this.WidgetTab.Size = new System.Drawing.Size(419, 265);
            this.WidgetTab.TabIndex = 3;
            this.WidgetTab.Text = "Widgets";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Message Duration (Seconds)";
            // 
            // messageDelay
            // 
            this.messageDelay.DecimalPlaces = 1;
            this.messageDelay.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.messageDelay.Location = new System.Drawing.Point(230, 235);
            this.messageDelay.Name = "messageDelay";
            this.messageDelay.Size = new System.Drawing.Size(79, 20);
            this.messageDelay.TabIndex = 37;
            // 
            // ChatDelayLabel
            // 
            this.ChatDelayLabel.AutoSize = true;
            this.ChatDelayLabel.Location = new System.Drawing.Point(35, 199);
            this.ChatDelayLabel.Name = "ChatDelayLabel";
            this.ChatDelayLabel.Size = new System.Drawing.Size(156, 13);
            this.ChatDelayLabel.TabIndex = 36;
            this.ChatDelayLabel.Text = "Next Message Delay (Seconds)";
            // 
            // chatDelay
            // 
            this.chatDelay.DecimalPlaces = 1;
            this.chatDelay.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.chatDelay.Location = new System.Drawing.Point(230, 200);
            this.chatDelay.Name = "chatDelay";
            this.chatDelay.Size = new System.Drawing.Size(79, 20);
            this.chatDelay.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(227, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Background Color";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(227, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Text Color";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(227, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Background Color";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Text Color";
            // 
            // messageColorBackground
            // 
            this.messageColorBackground.Location = new System.Drawing.Point(329, 170);
            this.messageColorBackground.Name = "messageColorBackground";
            this.messageColorBackground.Size = new System.Drawing.Size(24, 23);
            this.messageColorBackground.TabIndex = 23;
            this.messageColorBackground.UseVisualStyleBackColor = true;
            this.messageColorBackground.Click += new System.EventHandler(this.messageColorBackground_Click);
            // 
            // userColorBackground
            // 
            this.userColorBackground.Location = new System.Drawing.Point(329, 65);
            this.userColorBackground.Name = "userColorBackground";
            this.userColorBackground.Size = new System.Drawing.Size(24, 23);
            this.userColorBackground.TabIndex = 22;
            this.userColorBackground.UseVisualStyleBackColor = true;
            this.userColorBackground.Click += new System.EventHandler(this.userColorBackground_Click);
            // 
            // messageColorButton
            // 
            this.messageColorButton.Location = new System.Drawing.Point(329, 141);
            this.messageColorButton.Name = "messageColorButton";
            this.messageColorButton.Size = new System.Drawing.Size(24, 23);
            this.messageColorButton.TabIndex = 19;
            this.messageColorButton.UseVisualStyleBackColor = true;
            this.messageColorButton.Click += new System.EventHandler(this.messageColorButton_Click);
            // 
            // messageFontBox
            // 
            this.messageFontBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.messageFontBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.messageFontBox.DropDownWidth = 180;
            this.messageFontBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageFontBox.FormattingEnabled = true;
            this.messageFontBox.Location = new System.Drawing.Point(38, 143);
            this.messageFontBox.Name = "messageFontBox";
            this.messageFontBox.Size = new System.Drawing.Size(121, 21);
            this.messageFontBox.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(156, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Message Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(152, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Username Settings";
            // 
            // userFontBox
            // 
            this.userFontBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.userFontBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userFontBox.DropDownWidth = 180;
            this.userFontBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userFontBox.FormattingEnabled = true;
            this.userFontBox.Location = new System.Drawing.Point(38, 38);
            this.userFontBox.Name = "userFontBox";
            this.userFontBox.Size = new System.Drawing.Size(121, 21);
            this.userFontBox.TabIndex = 17;
            // 
            // userColorButton
            // 
            this.userColorButton.Location = new System.Drawing.Point(329, 36);
            this.userColorButton.Name = "userColorButton";
            this.userColorButton.Size = new System.Drawing.Size(24, 23);
            this.userColorButton.TabIndex = 16;
            this.userColorButton.UseVisualStyleBackColor = true;
            this.userColorButton.Click += new System.EventHandler(this.userColorButton_Click);
            // 
            // TTSTab
            // 
            this.TTSTab.BackColor = System.Drawing.Color.White;
            this.TTSTab.Controls.Add(this.label1);
            this.TTSTab.Controls.Add(this.voiceChoice);
            this.TTSTab.Controls.Add(this.VolumeTestButton);
            this.TTSTab.Controls.Add(this.volumePercentLabel);
            this.TTSTab.Controls.Add(this.volumeBar);
            this.TTSTab.Controls.Add(this.VolumeLabel);
            this.TTSTab.Controls.Add(this.AnnouncementTextBox);
            this.TTSTab.Controls.Add(this.shouldAnnounceBox);
            this.TTSTab.Location = new System.Drawing.Point(104, 4);
            this.TTSTab.Name = "TTSTab";
            this.TTSTab.Padding = new System.Windows.Forms.Padding(3);
            this.TTSTab.Size = new System.Drawing.Size(419, 265);
            this.TTSTab.TabIndex = 0;
            this.TTSTab.Text = "TTS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Voice";
            // 
            // voiceChoice
            // 
            this.voiceChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.voiceChoice.FormattingEnabled = true;
            this.voiceChoice.Location = new System.Drawing.Point(181, 98);
            this.voiceChoice.Name = "voiceChoice";
            this.voiceChoice.Size = new System.Drawing.Size(164, 21);
            this.voiceChoice.TabIndex = 42;
            // 
            // VolumeTestButton
            // 
            this.VolumeTestButton.Location = new System.Drawing.Point(221, 125);
            this.VolumeTestButton.Name = "VolumeTestButton";
            this.VolumeTestButton.Size = new System.Drawing.Size(75, 23);
            this.VolumeTestButton.TabIndex = 40;
            this.VolumeTestButton.Text = "Test";
            this.VolumeTestButton.UseVisualStyleBackColor = true;
            this.VolumeTestButton.Click += new System.EventHandler(this.VolumeTestButton_Click);
            // 
            // volumePercentLabel
            // 
            this.volumePercentLabel.AutoSize = true;
            this.volumePercentLabel.Location = new System.Drawing.Point(275, 67);
            this.volumePercentLabel.Name = "volumePercentLabel";
            this.volumePercentLabel.Size = new System.Drawing.Size(33, 13);
            this.volumePercentLabel.TabIndex = 41;
            this.volumePercentLabel.Text = "100%";
            // 
            // volumeBar
            // 
            this.volumeBar.BackColor = System.Drawing.Color.White;
            this.volumeBar.LargeChange = 10;
            this.volumeBar.Location = new System.Drawing.Point(174, 47);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(230, 45);
            this.volumeBar.SmallChange = 5;
            this.volumeBar.TabIndex = 38;
            this.volumeBar.TickFrequency = 10;
            this.volumeBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.volumeBar.Value = 100;
            // 
            // VolumeLabel
            // 
            this.VolumeLabel.AutoSize = true;
            this.VolumeLabel.Location = new System.Drawing.Point(6, 47);
            this.VolumeLabel.Name = "VolumeLabel";
            this.VolumeLabel.Size = new System.Drawing.Size(42, 13);
            this.VolumeLabel.TabIndex = 39;
            this.VolumeLabel.Text = "Volume";
            // 
            // AnnouncementTextBox
            // 
            this.AnnouncementTextBox.Location = new System.Drawing.Point(181, 14);
            this.AnnouncementTextBox.Name = "AnnouncementTextBox";
            this.AnnouncementTextBox.Size = new System.Drawing.Size(223, 20);
            this.AnnouncementTextBox.TabIndex = 37;
            this.AnnouncementTextBox.Text = "{$user} is the new commander";
            // 
            // shouldAnnounceBox
            // 
            this.shouldAnnounceBox.AutoSize = true;
            this.shouldAnnounceBox.Location = new System.Drawing.Point(9, 14);
            this.shouldAnnounceBox.Name = "shouldAnnounceBox";
            this.shouldAnnounceBox.Size = new System.Drawing.Size(100, 17);
            this.shouldAnnounceBox.TabIndex = 36;
            this.shouldAnnounceBox.Text = "Announce User";
            this.shouldAnnounceBox.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.TTSTab);
            this.tabControl1.Controls.Add(this.WidgetTab);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(25, 100);
            this.tabControl1.Location = new System.Drawing.Point(-2, 2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(527, 273);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(102, 281);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(124, 19);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveAndCloseButton
            // 
            this.SaveAndCloseButton.Location = new System.Drawing.Point(248, 281);
            this.SaveAndCloseButton.Name = "SaveAndCloseButton";
            this.SaveAndCloseButton.Size = new System.Drawing.Size(124, 19);
            this.SaveAndCloseButton.TabIndex = 2;
            this.SaveAndCloseButton.Text = "Save and Close";
            this.SaveAndCloseButton.UseVisualStyleBackColor = true;
            this.SaveAndCloseButton.Click += new System.EventHandler(this.SaveAndCloseButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(388, 281);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(124, 19);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(524, 301);
            this.ControlBox = false;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveAndCloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(540, 340);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(540, 340);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.WidgetTab.ResumeLayout(false);
            this.WidgetTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatDelay)).EndInit();
            this.TTSTab.ResumeLayout(false);
            this.TTSTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage WidgetTab;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown messageDelay;
        public System.Windows.Forms.Label ChatDelayLabel;
        public System.Windows.Forms.NumericUpDown chatDelay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button messageColorBackground;
        public System.Windows.Forms.Button userColorBackground;
        public System.Windows.Forms.Button messageColorButton;
        public System.Windows.Forms.ComboBox messageFontBox;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox userFontBox;
        public System.Windows.Forms.Button userColorButton;
        private System.Windows.Forms.TabPage TTSTab;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox voiceChoice;
        public System.Windows.Forms.Button VolumeTestButton;
        public System.Windows.Forms.Label volumePercentLabel;
        public System.Windows.Forms.TrackBar volumeBar;
        public System.Windows.Forms.Label VolumeLabel;
        private System.Windows.Forms.TextBox AnnouncementTextBox;
        public System.Windows.Forms.CheckBox shouldAnnounceBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button SaveAndCloseButton;
        private System.Windows.Forms.Button CloseButton;
    }
}