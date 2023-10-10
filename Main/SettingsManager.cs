using BassyTTSTwitch.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassyTTSTwitch
{
    public static class SettingsManager
    {
        private static SettingsForm _form;
        internal static EventHandler<SaveEvent> OnSave;
        public static void OpenSettings()
        {
            if(_form == null)
            {
                _form = new SettingsForm();
            } 
            
            if(_form.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                _form.Close();
                _form.Dispose();
                _form = null;
            }
            
        }

        public static void SaveSettings()
        {
            if (_form == null)
                return;
            _form.SaveSettings();
        }

        public static void LoadSettingsToForm()
        {
            if (_form == null)
                return;

            _form.LoadSettings();
        }
    }
}
