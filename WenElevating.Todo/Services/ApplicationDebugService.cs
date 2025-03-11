using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32;

namespace WenElevating.Todo.Services
{
    public class ApplicationDebugService
    {

        public static void CreateDebugWindow()
        {
            PInvoke.AllocConsole();
            PInvoke.SetConsoleTitle("TODO");
        }
    }
}
