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
    public partial class SelectionForm : Form
    {
        public SelectionForm()
        {
            InitializeComponent();

            VersionLabel.Text = $"Version: {Program.VERSION}";

            if (Updater.CheckForUpdate())
            {
                UpdateForm form = new UpdateForm();
                if(form.ShowDialog() == DialogResult.OK)
                {
                    Visible = false;
                }
            }

            if (!CredentialManager.HasAllCredentials())
            {
                Console.WriteLine(CredentialManager.GetMissingCredentials());
            }
        }

        private void TTS_Click(object sender, EventArgs e)
        {
            Program.CommanderForm ??= new CommanderForm();

            Program.CommanderForm.Show();
            Hide();
        }

        private void ChatGPTButton_Click(object sender, EventArgs e)
        {
            Program.ChatGPTForm ??= new ChatGPTForm();

            Program.ChatGPTForm.Show();
            Hide();
        }

        private void toolTip2_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
