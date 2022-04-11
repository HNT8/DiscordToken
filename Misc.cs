using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace HNT8
{
    public static class Misc
    {
        // Gets external C++ functions from kernel32.dll and user32.dll and makes them usable in our environment
        // to hide the console window from the user's view.

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void HideWindow()
        {
            ShowWindow(GetConsoleWindow(), 0);
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
