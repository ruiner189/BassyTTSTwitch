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
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
            VersionLabel.Text = $"Current Version: {Updater.CurrentVersion}\r\n\r\nLatest Version: {Updater.LatestVersion}\r\n";
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Updater.DownloadLatest();
            DialogResult = DialogResult.OK;
        }
    }
}
