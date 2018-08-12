using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Leon
{
    public class Win32Helper
    {
        public const int OBJID_CLIENT = -4;
        public delegate bool CallBack(IntPtr hwnd, IntPtr lParam);

        /// <summary>
        /// 通过将句柄传递给每个子窗口，再将其定义为应用程序定义的回调函数，枚举属于指定父窗体的子窗口
        /// </summary>
        /// <param name="hwndParent">要枚举子窗口的父窗口的句柄</param>
        /// <param name="lpEnumFunc">指向应用程序定义的回调函数的指针</param>
        /// <param name="lParam">将要传递给回调函数的应用程序定义的值</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int EnumChildWindows(IntPtr hwndParent, CallBack lpEnumFunc, int lParam);

        /// <summary>
        /// 检索桌面窗口的句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// 检索指定窗口所属的类的名称
        /// </summary>
        /// <param name="hWnd">窗口的句柄</param>
        /// <param name="lpClassName">类名字符串</param>
        /// <param name="nMaxCount">缓冲区的长度</param>
        /// <returns></returns>
        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// 检索指定窗口的标题栏的文本长度
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// 将指定窗口标题栏的文本复制到缓冲区中
        /// </summary>
        /// <param name="hWnd">包含文本的窗口或控件的句柄</param>
        /// <param name="text">将接收文本的缓冲区</param>
        /// <param name="nMaxCount">缓冲区的最大字符数</param>
        /// <returns></returns>
        [DllImport("User32", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);

        /// <summary>
        /// 获取句柄的类名
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <returns></returns>
        public static string GetClassNameFromHandle(IntPtr handle)
        {
            StringBuilder ClassName = new StringBuilder();
            GetClassName(handle, ClassName, 16);
            return ClassName.ToString();
        }

        /// <summary>
        /// 获取句柄的标题文本
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <returns></returns>
        public static string GetTextFromHandle(IntPtr handle)
        {
            int TextLength = GetWindowTextLength(handle);
            StringBuilder WindowName = new StringBuilder(TextLength + 1);
            GetWindowText(handle, WindowName, WindowName.Capacity);
            return WindowName.ToString();
        }
    }
}
