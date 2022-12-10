using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VassalSharp.launch;

namespace VassalSharp.UiInterfaces
{
    public interface IUiProvider
    {
        IUiApplication NewUiApplication();

        IModuleManagerWindow NewModuleManagerWindow();
    }
}
