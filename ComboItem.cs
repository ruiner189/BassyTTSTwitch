using System;

namespace BassyTTSTwitch
{
    public struct ComboItem
    {
        public int ID;
        public string Text;

        public ComboItem(int ID, String Text)
        {
            this.ID = ID;
            this.Text = Text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
