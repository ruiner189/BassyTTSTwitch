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

        public const string VERSION = "1.0.2.1";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CloudManager.Initialize();
            SelectionForm = new SelectionForm();

            Application.Run(SelectionForm);
        }

        internal static void InitializeManagers()
        {
            KeyboardManager.Initialize();
            TTSManager.Initialize();
        }
    }
}
