using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VassalSharp.launch;
using VassalSharp.UiInterfaces;

namespace VassalSharp.UiWinForms
{
    public class WinFormsUiProvider : IUiProvider
    {
        public IModuleManagerWindow NewModuleManagerWindow()
        {
           return new ModuleManagerForm();
        }

        public IUiApplication NewUiApplication()
        {
            return new WinFormsApplication();
        }
    }
}
