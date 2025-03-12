using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Console;

namespace WenElevating.Core.Services
{
    public class DebugWindowService
    {
        /// <summary>
        /// 请求对控制台屏幕缓冲区的读取访问权限，使进程能够从缓冲区读取数据。
        /// </summary>
        private static readonly long GENERIC_READ = 0x80000000L;

        /// <summary>
        /// 请求对控制台屏幕缓冲区的写权限，使进程能够将数据写入缓冲区。
        /// </summary>
        private static readonly long GENERIC_WRITE = 0x40000000L;

        /// <summary>
        /// 可以在控制台屏幕缓冲区上执行其他打开操作，以便进行读取访问。
        /// </summary>
        private static readonly uint FILE_SHARE_READ = 0x00000001;

        /// <summary>
        /// 可以在控制台屏幕缓冲区上执行其他打开操作，以便进行写入访问
        /// </summary>
        private static readonly uint FILE_SHARE_WRITE = 0x00000002;

        /// <summary>
        /// 要创建的控制台屏幕缓冲区的类型。 唯一支持的屏幕缓冲区类型是 CONSOLE_TEXTMODE_BUFFER
        /// </summary>
        private static readonly uint CONSOLE_TEXTMODE_BUFFER = 0x00000001;

        /// <summary>
        /// 控制台输入句柄
        /// </summary>
        private static IntPtr ConsoleInputHandle;

        private static SafeFileHandle SafeConsoleHandle;

        /// <summary>
        /// 输出字符颜色
        /// </summary>
        private enum Foreground
        {
            Blue = 0x0001,
            Green = 0x0002,
            Red = 0x0003,
        }

        /// <summary>
        /// 输出字符背景色
        /// </summary>
        private enum FontBackground
        {
            Blue = 0x0010,
            Green = 0x0020,
            Red = 0x0040
        }

        private static void SetDebugWindowBeautifulTitle()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("      _____                   _______                   _____                   _______         \r\n     /\\    \\                 /::\\    \\                 /\\    \\                 /::\\    \\        \r\n    /::\\    \\               /::::\\    \\               /::\\    \\               /::::\\    \\       \r\n    \\:::\\    \\             /::::::\\    \\             /::::\\    \\             /::::::\\    \\      \r\n     \\:::\\    \\           /::::::::\\    \\           /::::::\\    \\           /::::::::\\    \\     \r\n      \\:::\\    \\         /:::/~~\\:::\\    \\         /:::/\\:::\\    \\         /:::/~~\\:::\\    \\    \r\n       \\:::\\    \\       /:::/    \\:::\\    \\       /:::/  \\:::\\    \\       /:::/    \\:::\\    \\   \r\n       /::::\\    \\     /:::/    / \\:::\\    \\     /:::/    \\:::\\    \\     /:::/    / \\:::\\    \\  \r\n      /::::::\\    \\   /:::/____/   \\:::\\____\\   /:::/    / \\:::\\    \\   /:::/____/   \\:::\\____\\ \r\n     /:::/\\:::\\    \\ |:::|    |     |:::|    | /:::/    /   \\:::\\ ___\\ |:::|    |     |:::|    |\r\n    /:::/  \\:::\\____\\|:::|____|     |:::|    |/:::/____/     \\:::|    ||:::|____|     |:::|    |\r\n   /:::/    \\::/    / \\:::\\    \\   /:::/    / \\:::\\    \\     /:::|____| \\:::\\    \\   /:::/    / \r\n  /:::/    / \\/____/   \\:::\\    \\ /:::/    /   \\:::\\    \\   /:::/    /   \\:::\\    \\ /:::/    /  \r\n /:::/    /             \\:::\\    /:::/    /     \\:::\\    \\ /:::/    /     \\:::\\    /:::/    /   \r\n/:::/    /               \\:::\\__/:::/    /       \\:::\\    /:::/    /       \\:::\\__/:::/    /    \r\n\\::/    /                 \\::::::::/    /         \\:::\\  /:::/    /         \\::::::::/    /     \r\n \\/____/                   \\::::::/    /           \\:::\\/:::/    /           \\::::::/    /      \r\n                            \\::::/    /             \\::::::/    /             \\::::/    /       \r\n                             \\::/____/               \\::::/    /               \\::/____/        \r\n                              ~~                      \\::/____/                 ~~              \r\n                                                       ~~                                       \r\n                                                                                                ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
        }

        public static void InitializeDefaultWindow()
        {
            CreateWindow();
            SetTitle("TODO");
            ConsoleInputHandle = PInvoke.GetStdHandle(STD_HANDLE.STD_OUTPUT_HANDLE);
            SafeConsoleHandle = new(ConsoleInputHandle, true);
            SetDebugWindowBeautifulTitle();
        }

        public static void PrintInformation(string msg, ConsoleColor forground = ConsoleColor.White)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return;
            }

            Console.ForegroundColor = forground;
            Console.WriteLine(msg);
            Console.WriteLine();
        }

        /// <summary>
        /// 创建控制台
        /// </summary>
        /// <returns></returns>
        public static bool CreateWindow()
        {
            // 创建控制台
            Windows.Win32.Foundation.BOOL result = PInvoke.AllocConsole();
            return result;
        }

        /// <summary>
        /// 设置控制台标题
        /// </summary>
        /// <param name="title"></param>
        public static void SetTitle(string title)
        {
            PInvoke.SetConsoleTitle(title);
        }

        /// <summary>
        /// 创建控制台屏幕缓冲区(需要先创建控制台窗体)
        /// </summary>
        /// <returns></returns>
        public static SafeFileHandle CreateScreenCache()
        {
            var handle = PInvoke.CreateConsoleScreenBuffer((uint)(GENERIC_WRITE | GENERIC_READ), FILE_SHARE_WRITE | FILE_SHARE_READ, null, CONSOLE_TEXTMODE_BUFFER);
            return handle;
        }

        /// <summary>
        /// 写入输出缓冲区（会清除控制台）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool InsertText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text), "");
            }

            COORD dwBufferCoord = new() // 缓冲区左上角的起始位置(即控制台左上角的第一个字符)
            {
                X = 0,
                Y = 0
            };

            PInvoke.WriteConsoleOutputCharacter(SafeConsoleHandle, text, (uint)text.Length, dwBufferCoord, out var data);
            return false;
        }

    }
}
