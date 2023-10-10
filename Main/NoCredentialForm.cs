using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassyTTSTwitch
{
    public partial class NoCredentialForm : Form
    {
        public NoCredentialForm()
        {
            InitializeComponent();
            FormClosed += NoCredentialForm_FormClosed;
        }

        private void NoCredentialForm_FormClosed(object sender, FormClosedEventArgs e)
        { 
            Application.Exit();
        }

        public void SetText(string text, bool invoke = false)
        {
            if (invoke)
            {
                CrashText.BeginInvoke(new Action(() => {
                    CrashText.Text = text;
                }));
            } else
            {
                CrashText.Text = text;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = Directory.GetCurrentDirectory(),
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }
    }
}
