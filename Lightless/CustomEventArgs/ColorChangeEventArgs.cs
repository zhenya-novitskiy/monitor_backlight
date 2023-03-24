using System;
using System.Windows.Media;

namespace Lightless.CustomEventArgs
{
    public class ColorChangeEventArgs : EventArgs
    {
        public Color Color;
    }

    public class StringEventArgs : EventArgs
    {
        public string Value;
    }

    public class BoolEventArgs : EventArgs
    {
        public bool Value;
    }
}
