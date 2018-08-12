using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Accessibility;

namespace Leon
{
    public static class AccessibleHelper
    {
        /// <summary>
        /// COM GUID
        /// </summary>
        public static Guid COMGuid = new Guid(0x618736E0, 0x3C3D, 0x11CF, 0x81, 0xC, 0x0, 0xAA, 0x0, 0x38, 0x9B, 0x71);

        /// <summary>
        /// 将指定接口的地址检索到与给定窗口关联的对象
        /// </summary>
        /// <param name="hwnd">指定要检索对象的窗口的句柄</param>
        /// <param name="dwObjectID">指定对象ID</param>
        /// <param name="refID">指定请求接口的引用标识符</param>
        /// <param name="ppvObject">接收指定接口地址的指针变量的地址</param>
        /// <returns></returns>
        [DllImport("Oleacc")]
        private static extern int AccessibleObjectFromWindow(IntPtr hwnd, int dwObjectID, ref Guid refID, ref IAccessible ppvObject);

        /// <summary>
        /// 检索对应于IAccess接口的特定实例的窗口句柄
        /// </summary>
        /// <param name="pacc"></param>
        /// <param name="phwnd"></param>
        /// <returns></returns>
        [DllImport("Oleacc")]
        private static extern int WindowFromAccessibleObject(IAccessible pacc, out IntPtr phwnd);

        /// <summary>
        /// 检索可访问容器对象中每个子对象
        /// </summary>
        /// <param name="paccContainer">指向容器对象的可访问接口的指针</param>
        /// <param name="iChildStart">指定的第一个孩子，是基于0的索引检索</param>
        /// <param name="cChildren">指定要检索的子的数目</param>
        /// <param name="rgvarChildren">接收容器的子对象信息的数组的指针</param>
        /// <param name="pcObtained">子元素数组元素个数</param>
        /// <returns></returns>
        [DllImport("Oleacc")]
        private static extern int AccessibleChildren(IAccessible paccContainer, int iChildStart, int cChildren, [Out] object[] rgvarChildren, out int pcObtained);

        /// <summary>
        /// 检索一个本地化字符串描述单个预定义状态位标志的对象状态
        /// </summary>
        /// <param name="dwStateBit">一个对象状态常量</param>
        /// <param name="lpszStateBit">接收状态文本字符串的缓冲区地址</param>
        /// <param name="cchStateBitMax">由LPSZSTATEBIT参数指向的缓冲器的大小</param>
        /// <returns></returns>
        [DllImport("Oleacc")]
        private static extern int GetStateText(int dwStateBit, StringBuilder lpszStateBit, int cchStateBitMax);

        /// <summary>
        /// 将指定对象角色文本复制到缓冲区中
        /// </summary>
        /// <param name="lRole">指定对象</param>
        /// <param name="lpszStateBit">文本缓冲区</param>
        /// <param name="cchStateBitMax">缓冲区最大字符数</param>
        /// <returns></returns>
        [DllImport("Oleacc")]
        private static extern int GetRoleText(int lRole, StringBuilder lpszStateBit, int cchStateBitMax);

        /// <summary>
        /// 获取状态文本
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetStateText(this IAccessible target)
        {
            Console.WriteLine($"获取对象 {target.GetHashCode().ToString("X")} 状态...");
            StringBuilder state = new StringBuilder();
            int result = GetStateText(target.accState[0], state, 256);
            return state.ToString();
        }

        /// <summary>
        /// 获取角色文本
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetRoleText(this IAccessible target)
        {
            Console.WriteLine($"获取对象 {target.GetHashCode().ToString("X")} 角色...");
            StringBuilder role = new StringBuilder();
            int result = GetRoleText(target.accRole[0], role, 256);
            return role.ToString();
        }

        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Rectangle GetRectangle(this IAccessible target)
        {
            target.accLocation(out int x, out int y, out int w, out int h, 0);
            return new Rectangle(x,y,w,h);
        }

        /// <summary>
        /// 将指定接口的地址检索到与给定窗口关联的对象
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static IAccessible GetAccessibleFromHandle(IntPtr handle)
        {
            Console.WriteLine($"从句柄 {handle.ToString("X")} 获取对象...");
            IAccessible IACurrent = null;
            AccessibleObjectFromWindow(handle, Win32Helper.OBJID_CLIENT, ref COMGuid, ref IACurrent);
            return IACurrent;
        }

        /// <summary>
        /// 检索对应于IAccess接口的特定实例的窗口句柄
        /// </summary>
        /// <param name="accessible"></param>
        /// <returns></returns>
        public static IntPtr GetWindowFromAccessible(IAccessible accessible)
        {
            Console.WriteLine($"从对象 {accessible.GetHashCode().ToString("X")} 获取句柄...");
            WindowFromAccessibleObject(accessible, out IntPtr Handle);
            return Handle;
        }

        /// <summary>
        /// 获取父级对象
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IAccessible GetParent(this IAccessible target) => target.accParent as IAccessible;

        /// <summary>
        /// 获取子对象
        /// 获取倒数第二级的子对象时会返回 (int32)1 ???
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IAccessible[] GetChildren(this IAccessible target)
        {
            Console.WriteLine($"获取对象 {target.GetHashCode().ToString("X")} 的子对象集合...");
            try
            {
                IAccessible[] Children = new IAccessible[target.accChildCount];
                AccessibleChildren(target, 0, target.accChildCount, Children, out int pcObtained);
                return Children;
            }
            catch { return null; }
        }

        /// <summary>
        /// 获取指定ID的子对象
        /// 获取倒数第二级的子对象时会返回 (int32)1 ???
        /// </summary>
        /// <param name="target"></param>
        /// <param name="ChildID">子对象的ID</param>
        /// <returns></returns>
        public static IAccessible GetChild(this IAccessible target, int ChildID)
        {
            Console.WriteLine($"获取对象 {target.GetHashCode().ToString("X")} 指定 ID= {ChildID} 的子对象...");
            try
            {
                IAccessible[] Children = new IAccessible[target.accChildCount];
                AccessibleChildren(target, ChildID, 1, Children, out int pcObtained);
                return Children.FirstOrDefault();
            }
            catch { return null; }
        }

        /// <summary>
        /// 根据层级数组检索子对象
        /// 获取倒数第二级的子对象时会返回 (int32)1 ???
        /// </summary>
        /// <param name="target">容器对象</param>
        /// <param name="layers">层级数组</param>
        /// <returns></returns>
        public static IAccessible GetAccessibleByLayers(this IAccessible target, int[] layers)
        {
            Console.WriteLine($"获取对象 {target.GetHashCode().ToString("X")} 指定层级 {string.Join(", ", layers)} 的对象...");
            IAccessible CurrentAccessible = target;
            foreach (int layer in layers)
            {
                CurrentAccessible = CurrentAccessible.GetChild(layer);
                if (CurrentAccessible == null) return null;
            }
            return CurrentAccessible;
        }

    }
}
