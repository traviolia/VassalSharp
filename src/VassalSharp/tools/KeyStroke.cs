using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VassalSharp.tools
{
    public class KeyStroke
    {
        public KeyStroke()
        {
        }

        public KeyStroke(int v)
        {
        }

        public int KeyValue { get; internal set; }
        public int Modifiers { get; internal set; }

        public static int getKeyStrokeForEvent(System.Windows.Forms.KeyEventArgs e)
        {
            return e.KeyValue;
        }
    }
}
