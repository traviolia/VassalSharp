using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using VassalSharp.UiInterfaces;

namespace VassalSharp.UiWinForms
{
    class WinFormsApplication : IUiApplication
    {
        public void Run(IUiWindow window)
        {
            Application.Run((Form)window);
        }
    }
}
