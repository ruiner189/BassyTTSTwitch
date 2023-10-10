
namespace BassyTTSTwitch
{
    partial class ChatGPTForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatGPTForm));
            this.CurrentPrompt = new System.Windows.Forms.RichTextBox();
            this.ConversationBox = new System.Windows.Forms.RichTextBox();
            this.BackgroundText = new System.Windows.Forms.RichTextBox();
            this.OBSButton = new System.Windows.Forms.Button();
            this.RecordButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.InputDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RecordTimer = new System.Windows.Forms.Timer(this.components);
            this.messageColorDialog = new System.Windows.Forms.ColorDialog();
            this.ChatClearButton = new System.Windows.Forms.Button();
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.AutoButton = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SettingsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentPrompt
            // 
            this.CurrentPrompt.Location = new System.Drawing.Point(519, 47);
            this.CurrentPrompt.Name = "CurrentPrompt";
            this.CurrentPrompt.Size = new System.Drawing.Size(269, 148);
            this.CurrentPrompt.TabIndex = 1;
            this.CurrentPrompt.Text = "";
            // 
            // ConversationBox
            // 
            this.ConversationBox.Location = new System.Drawing.Point(12, 257);
            this.ConversationBox.Name = "ConversationBox";
            this.ConversationBox.ReadOnly = true;
            this.ConversationBox.Size = new System.Drawing.Size(776, 200);
            this.ConversationBox.TabIndex = 2;
            this.ConversationBox.TabStop = false;
            this.ConversationBox.Text = "";
            // 
            // BackgroundText
            // 
            this.BackgroundText.Location = new System.Drawing.Point(12, 47);
            this.BackgroundText.Name = "BackgroundText";
            this.BackgroundText.Size = new System.Drawing.Size(416, 148);
            this.BackgroundText.TabIndex = 3;
            this.BackgroundText.Text = "";
            // 
            // OBSButton
            // 
            this.OBSButton.Location = new System.Drawing.Point(12, 217);
            this.OBSButton.Name = "OBSButton";
            this.OBSButton.Size = new System.Drawing.Size(147, 23);
            this.OBSButton.TabIndex = 30;
            this.OBSButton.Text = "OBS Widget";
            this.OBSButton.UseVisualStyleBackColor = true;
            this.OBSButton.Click += new System.EventHandler(this.OBSButton_Click);
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(535, 199);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(75, 23);
            this.RecordButton.TabIndex = 31;
            this.RecordButton.Text = "Record";
            this.MainToolTip.SetToolTip(this.RecordButton, resources.GetString("RecordButton.ToolTip"));
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.record_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(438, 64);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 32;
            this.SendButton.Text = "Send";
            this.MainToolTip.SetToolTip(this.SendButton, "Sends the current prompt to Chat-GPT.\r\n\r\nPress \"CTRL+B\" as a hotkey to send.\r\n\r\n");
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // InputDevice
            // 
            this.InputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.InputDevice.FormattingEnabled = true;
            this.InputDevice.Location = new System.Drawing.Point(634, 201);
            this.InputDevice.MaxLength = 1000;
            this.InputDevice.Name = "InputDevice";
            this.InputDevice.Size = new System.Drawing.Size(154, 21);
            this.InputDevice.TabIndex = 33;
            this.InputDevice.SelectedIndexChanged += new System.EventHandler(this.InputDevice_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Chat GPT Background";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(621, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Current Prompt";
            // 
            // RecordTimer
            // 
            this.RecordTimer.Enabled = true;
            this.RecordTimer.Tick += new System.EventHandler(this.RecordTimer_Tick);
            // 
            // messageColorDialog
            // 
            this.messageColorDialog.SolidColorOnly = true;
            // 
            // ChatClearButton
            // 
            this.ChatClearButton.Location = new System.Drawing.Point(438, 102);
            this.ChatClearButton.Name = "ChatClearButton";
            this.ChatClearButton.Size = new System.Drawing.Size(75, 23);
            this.ChatClearButton.TabIndex = 36;
            this.ChatClearButton.Text = "Clear History";
            this.ChatClearButton.UseVisualStyleBackColor = true;
            this.ChatClearButton.Click += new System.EventHandler(this.ChatClearButton_Click);
            // 
            // AutoButton
            // 
            this.AutoButton.AutoSize = true;
            this.AutoButton.Location = new System.Drawing.Point(537, 227);
            this.AutoButton.Name = "AutoButton";
            this.AutoButton.Size = new System.Drawing.Size(116, 17);
            this.AutoButton.TabIndex = 37;
            this.AutoButton.Text = "Automatically Send";
            this.MainToolTip.SetToolTip(this.AutoButton, "Sends the current prompt automatically after you are done recording.");
            this.AutoButton.UseVisualStyleBackColor = true;
            this.AutoButton.CheckedChanged += new System.EventHandler(this.AutoButton_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 38;
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
            // ChatGPTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 462);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.AutoButton);
            this.Controls.Add(this.ChatClearButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InputDevice);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.RecordButton);
            this.Controls.Add(this.OBSButton);
            this.Controls.Add(this.BackgroundText);
            this.Controls.Add(this.ConversationBox);
            this.Controls.Add(this.CurrentPrompt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatGPTForm";
            this.RightToLeftLayout = true;
            this.Text = "BassyTTSTwitch";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox CurrentPrompt;
        private System.Windows.Forms.RichTextBox ConversationBox;
        private System.Windows.Forms.RichTextBox BackgroundText;
        public System.Windows.Forms.Button OBSButton;
        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.Button SendButton;
        public System.Windows.Forms.ComboBox InputDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer RecordTimer;
        public System.Windows.Forms.ColorDialog messageColorDialog;
        private System.Windows.Forms.Button ChatClearButton;
        private System.Windows.Forms.ToolTip MainToolTip;
        private System.Windows.Forms.CheckBox AutoButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SettingsButton;
    }
}