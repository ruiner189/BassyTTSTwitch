using BassyTTSTwitch.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassyTTSTwitch.Widgets
{
    public partial class UserWidget : Form
    {
        public static readonly int MaxFontSize = 50;
        public static readonly int MinFontSize = 20;
        public UserWidget()
        {
            InitializeComponent();
            Size = Settings.Default.UserWidgetSize;
            SetHooks();
            this.AllowTransparency = true;
            SettingsManager.OnSave += OnSettingsChanged;
            HandleCreated += UserWidget_HandleCreated;
        }

        private void UserWidget_HandleCreated(object sender, EventArgs e)
        {
            LoadSettings(Settings.Default);
        }

        private void SetHooks()
        {
            Label.Paint += new PaintEventHandler(Paint_Transparent);
        }

        private void Paint_Transparent(object sender, PaintEventArgs e)
        {
            Label label = sender as Label;
            e.Graphics.Clear(label.BackColor);

            if (label.Text != null && label.Text != "")
            {
                Font f = GetAdjustedFont(e.Graphics, label.Text, label.Font, label.Width, label.Height, MaxFontSize, MinFontSize, true);
                label.Font = f;
            }

            using var sf = new StringFormat();
            using var br = new SolidBrush(label.ForeColor);
            sf.Alignment = sf.LineAlignment = StringAlignment.Center;
            e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            e.Graphics.DrawString(label.Text, label.Font, br,
                label.ClientRectangle, sf);
        }

        public Font GetAdjustedFont(Graphics g, string graphicString, Font originalFont, int containerWidth, int containerHeight, int maxFontSize, int minFontSize, bool smallestOnFail)
        {
            Font testFont = null;
            // We utilize MeasureString which we get via a control instance           
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

                // Test the string with the new size
                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont);

                if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width))
                {
                    // Good font, return it
                    return testFont;
                }
            }

            // If you get here there was no fontsize that worked
            // return minimumSize or original?
            if (smallestOnFail)
            {
                return testFont;
            }
            else
            {
                return originalFont;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserWidget_Load(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.ApplicationExitCall) return;

            e.Cancel = true;
            Hide();
        }

        private void OnSettingsChanged(object sender, SaveEvent e)
        {
            LoadSettings(e.Settings);
        }

        private void LoadSettings(Settings settings)
        {
            if (IsHandleCreated)
            {
                BeginInvoke(new Action(() => {
                    BackColor = settings.UserBackgroundColor;
                    Label.BackColor = settings.UserBackgroundColor;
                    Label.ForeColor = settings.UserColor;
                    Label.Font = settings.UserFont;
                }));
            }
        }
    }
}
