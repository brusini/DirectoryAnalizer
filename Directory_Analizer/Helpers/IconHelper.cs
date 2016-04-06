using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Directory_Analizer.Helpers
{
    // класс для получения иконки объекта, для отображения в treeView. 
    // реализация взята с просторов интернета, и немного переделана под мои интересы :)
    public static class IconHelper
    {
        public static Icon GetSmallIcon(string fileName)
        {
            return GetIcon(fileName, Win32.ShgfiSmallicon);
        }

        public static Icon GetLargeIcon(string fileName)
        {
            return GetIcon(fileName, Win32.ShgfiLargeicon);
        }

        // метод который возвращает иконку нужного размера, try catch добавлен чтобы избежать
        // ситуации, например если файла уже нет, или нет прав у пользователя
        private static Icon GetIcon(string fileName, uint flags)
        {
            try
            {
                var shinfo = new Shfileinfo();
                Win32.SHGetFileInfo(fileName, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.ShgfiIcon | flags);

                var icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
                Win32.DestroyIcon(shinfo.hIcon);
                return icon;
            }
            catch (Exception)
            {
                return SystemIcons.WinLogo;
            }
        }

        // структура которая предоставляет характеристики объекта
        [StructLayout(LayoutKind.Sequential)]
        private struct Shfileinfo
        {
            public readonly IntPtr hIcon;
            private readonly uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            private readonly string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] 
            private readonly string szTypeName;
        };

        // структура которая предоставляет методы для получения информации о объекте
        private static class Win32
        {
            public const uint ShgfiIcon = 0x100;
            public const uint ShgfiLargeicon = 0x0; 
            public const uint ShgfiSmallicon = 0x1; 

            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref Shfileinfo psfi, uint cbSizeFileInfo, uint uFlags);

            [DllImport("User32.dll")]
            public static extern int DestroyIcon(IntPtr hIcon);
        }
    }
}
