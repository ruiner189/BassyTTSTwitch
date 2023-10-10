
using System;

namespace BassyTTSTwitch.Keyboard
{
    public class KeyboardManager
    {
        private KeyboardHook _hook;

        private static KeyboardManager _instance;

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public static void Initialize()
        {
            if(_instance == null )
                _instance = new KeyboardManager();
        }

        public static KeyboardManager GetInstance()
        {
            return _instance;
        }
        private KeyboardManager() {
            _hook = new KeyboardHook();
            _hook.RegisterHotKey(ModifierKeys.Control, System.Windows.Forms.Keys.N);
            _hook.RegisterHotKey(ModifierKeys.Control, System.Windows.Forms.Keys.B);
            _hook.KeyPressed += _hook_KeyPressed;
        }

        private void _hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if(KeyPressed != null)
            {
                KeyPressed?.Invoke(this, e);
            }
        }
    }
}
