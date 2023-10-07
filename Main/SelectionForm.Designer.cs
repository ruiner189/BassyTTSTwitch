namespace BassyTTSTwitch
{
    partial class SelectionForm
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
            this.TTSButton = new System.Windows.Forms.Button();
            this.ChatGPTButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // TTSButton
            // 
            this.TTSButton.Location = new System.Drawing.Point(37, 40);
            this.TTSButton.Name = "TTSButton";
            this.TTSButton.Size = new System.Drawing.Size(290, 41);
            this.TTSButton.TabIndex = 0;
            this.TTSButton.Text = "Chat Text To Speech";
            this.TTSButton.UseVisualStyleBackColor = true;
            this.TTSButton.Click += new System.EventHandler(this.TTS_Click);
            // 
            // ChatGPTButton
            // 
            this.ChatGPTButton.Location = new System.Drawing.Point(37, 139);
            this.ChatGPTButton.Name = "ChatGPTButton";
            this.ChatGPTButton.Size = new System.Drawing.Size(290, 41);
            this.ChatGPTButton.TabIndex = 1;
            this.ChatGPTButton.Text = "ChatGPT";
            this.ChatGPTButton.UseVisualStyleBackColor = true;
            this.ChatGPTButton.Click += new System.EventHandler(this.ChatGPTButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(149, 217);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(69, 13);
            this.VersionLabel.TabIndex = 2;
            this.VersionLabel.Text = "Version 0.0.0";
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.AutoSize = true;
            this.AuthorLabel.Location = new System.Drawing.Point(131, 204);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(110, 13);
            this.AuthorLabel.TabIndex = 3;
            this.AuthorLabel.Text = "Created by Ruiner189";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 239);
            this.Controls.Add(this.AuthorLabel);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.ChatGPTButton);
            this.Controls.Add(this.TTSButton);
            this.Name = "SelectionForm";
            this.Text = "BassySelector";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TTSButton;
        private System.Windows.Forms.Button ChatGPTButton;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}