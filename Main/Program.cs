using BassyTTSTwitch.Keyboard;
using BassyTTSTwitch.TTS;
using System;
using System.Windows.Forms;

namespace BassyTTSTwitch
{
    static class Program
    {

        public static CommanderForm CommanderForm;
        public static ChatGPTForm ChatGPTForm;
        public static SelectionForm SelectionForm;

        public const string VERSION = "1.0.1.3";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            KeyboardManager.Initialize();
            CloudManager.Initialize();
            TTSManager.Initialize();

            SelectionForm = new SelectionForm();

            Application.Run(SelectionForm);

        }
    }
}
