using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32;

namespace WenElevating.Core.Services
{
    public class DebugWindowService
    {
        public static void CreateWindow()
        {
            PInvoke.AllocConsole();
            PInvoke.SetConsoleTitle("TODO");
        }
    }
}
