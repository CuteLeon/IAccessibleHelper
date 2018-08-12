using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accessibility;

namespace Leon
{
    public class TIMHelper
    {
        /// <summary>
        /// TIM窗口
        /// </summary>
        public struct TIMWindow
        {
            /// <summary>
            /// TIM窗口句柄
            /// </summary>
            public IntPtr Handle;
            /// <summary>
            /// TIM窗口标题
            /// </summary>
            public string Text;
            /// <summary>
            /// TIM窗口类名
            /// </summary>
            public string ClassName;

            public TIMWindow(IntPtr handle, string text, string classname)
            {
                Handle = handle;
                Text = text;
                ClassName = classname;
            }
            public override string ToString() => $"&H{Handle.ToString("X")} - {Text} ({ClassName})";
        }

        /// <summary>
        /// TIM句柄数组
        /// </summary>
        private static List<TIMWindow> TIMWindows;

        private IntPtr _handle = IntPtr.Zero;
        /// <summary>
        /// 与此类关联的TIM句柄
        /// </summary>
        public IntPtr TIMHandle
        {
            get => _handle;
            private set
            {
                Console.WriteLine($"设置 TIMHelper 句柄 = {value.ToString("X")}");
                _handle = value;
                TIMAccessible = AccessibleHelper.GetAccessibleFromHandle(value);
            }
        }

        private IAccessible _accessible = null;
        /// <summary>
        /// 与此类关联的TIMAccessible
        /// </summary>
        public IAccessible TIMAccessible
        {
            get => _accessible;
            private set
            {
                Console.WriteLine($"设置 TIMHelper 对象为 {value.GetHashCode().ToString("X")}");
                _accessible = value;
                
                //TODO: 更新容器Accessible后，更新对应的子对象
                HeadAccessible = GetHeadAccessible(value);
                MessageTabButton = GetMessageTabButton(value);
                ContactsTabButton = GetContactsTabButton(value);
                CloudFileTabButton = GetCloudFileTabButton(value);
                TodoList = GetTodoList(value);
                Sessions = GetSessions(value);
                Contacts = GetContacts(value);
            }
        }

        /// <summary>
        /// 头像
        /// </summary>
        public IAccessible HeadAccessible { get; private set; } = null;

        /// <summary>
        /// 消息选项卡按钮
        /// </summary>
        public IAccessible MessageTabButton { get; private set; } = null;

        /// <summary>
        /// 联系人选项卡按钮
        /// </summary>
        public IAccessible ContactsTabButton { get; private set; } = null;

        /// <summary>
        /// 云文件选项卡按钮
        /// </summary>
        public IAccessible CloudFileTabButton { get; private set; } = null;

        /// <summary>
        /// 待办事项
        /// </summary>
        public IAccessible TodoList { get; private set; } = null;

        /// <summary>
        /// 会话列表
        /// </summary>
        public List<IAccessible> Sessions = null;

        /// <summary>
        /// 联系人分组字典
        /// </summary>
        public Dictionary<string, List<IAccessible>> Contacts = null;

        private TIMHelper() { }

        public TIMHelper(IntPtr TIMhandle)
        {
            TIMHandle = TIMhandle;
        }

        /// <summary>
        /// 检索所有TIM句柄
        /// </summary>
        /// <returns></returns>
        public static TIMWindow[] GetTIMHandles()
        {
            Console.WriteLine($"开始扫描 TIM 句柄...");
            TIMWindows = new List<TIMWindow>();
            Win32Helper.EnumChildWindows(
                Win32Helper.GetDesktopWindow(),
                new Win32Helper.CallBack(TIMHandleCallBack),
                0);
            return TIMWindows.ToArray<TIMWindow>();
        }

        /// <summary>
        /// 检索TIM句柄回调
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static bool TIMHandleCallBack(IntPtr hwnd, IntPtr lParam)
        {
            string Text = Win32Helper.GetTextFromHandle(hwnd);
            string ClassName = Win32Helper.GetClassNameFromHandle(hwnd);
            if (ClassName == "TXGuiFoundation")
            {
                if (!string.IsNullOrEmpty( Text) && Text != "TXMenuWindow")
                {
                    Console.WriteLine($"发现 TIM 句柄 {hwnd.ToString("X")}");
                    TIMWindow TIMwindow = new TIMWindow(hwnd, Text, ClassName);
                    TIMWindows.Add(TIMwindow);
                }
            }
            return true;
        }

        /// <summary>
        /// 定位头像
        /// </summary>
        /// <param name="TIMaccessible"></param>
        /// <returns></returns>
        private IAccessible GetHeadAccessible(IAccessible TIMaccessible) => TIMaccessible.GetAccessibleByLayers(new int[] { 1, 0, 0, 1, 0, 0 }); //0

        /// <summary>
        /// 定位消息选项卡按钮
        /// </summary>
        /// <param name="TIMaccessible"></param>
        /// <returns></returns>
        private IAccessible GetMessageTabButton(IAccessible TIMaccessible) => TIMaccessible.GetAccessibleByLayers(new int[] { 1, 0, 0, 3 }); //2

        /// <summary>
        /// 定位联系人选项卡按钮
        /// </summary>
        /// <param name="TIMaccessible"></param>
        /// <returns></returns>
        private IAccessible GetContactsTabButton(IAccessible TIMaccessible) => TIMaccessible.GetAccessibleByLayers(new int[] { 1, 0, 0, 3 }); //3

        /// <summary>
        /// 定位云文件选项卡按钮
        /// </summary>
        /// <param name="TIMaccessible"></param>
        /// <returns></returns>
        private IAccessible GetCloudFileTabButton(IAccessible TIMaccessible) => TIMaccessible.GetAccessibleByLayers(new int[] { 1, 0, 0, 3 }); //4

        /// <summary>
        /// 定位待办事项
        /// </summary>
        /// <param name="TIMaccessible"></param>
        /// <returns></returns>
        private IAccessible GetTodoList(IAccessible TIMaccessible) => TIMaccessible.GetAccessibleByLayers(new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0 }); //1

        /// <summary>
        /// 定位所有会话 (此列表需要实时更新)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private List<IAccessible> GetSessions(IAccessible TIMaccessible)
        {
            Console.WriteLine("开始检索会话...");
            IAccessible SessionContainer = TIMaccessible.GetAccessibleByLayers(new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 });
            Sessions = new List<IAccessible>();
            Console.WriteLine("发现会话个数：" + SessionContainer.accChildCount.ToString());
            Sessions.AddRange(SessionContainer.GetChildren());

            for (int index = 0; index < Sessions.Count; index++)
                Console.WriteLine($"{index+1}  <{Sessions[index].accName[0]}> - Message: {Sessions[index].accValue[0]}");
            return Sessions;
        }

        /// <summary>
        /// 定位所有联系人 (此列表需要更新，TIM通过懒加载机制载入分组内的联系人)
        /// </summary>
        /// <param name="TIMaccessible"></param>
        /// <returns></returns>
        private Dictionary<string, List<IAccessible>> GetContacts(IAccessible TIMaccessible)
        {
            Console.WriteLine("开始检索联系人...");
            Contacts = new Dictionary<string, List<IAccessible>>();
            IAccessible GroupContainer = TIMaccessible.GetAccessibleByLayers(new int[] { 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 });
            if (GroupContainer == null) return Contacts;

            Console.WriteLine("发现分组个数：" + GroupContainer.accChildCount.ToString());

            foreach (IAccessible GroupAccessible in GroupContainer.GetChildren())
            {
                Console.WriteLine("遍历分组：{0}", GroupAccessible.accName[1]);
                List<IAccessible> ContactList = new List<IAccessible>();
                Contacts.Add(GroupAccessible.accName[1], ContactList);

                IAccessible InnerGroupAccessible = GroupAccessible.GetChild(1);
                if (InnerGroupAccessible == null) continue;
                Console.WriteLine("———————————— Begin");
                IAccessible[] InnerContacts = InnerGroupAccessible.GetChildren() ?? new IAccessible[0];
                Console.WriteLine("———————————— End");
                Console.WriteLine($"此分组内共有 {InnerContacts.Length} 个联系人");
                if (InnerContacts.Length == 0) continue;

                foreach (IAccessible Contact in InnerContacts)
                {
                    IAccessible InnerContactAccessible = Contact.GetChild(0);
                    ContactList.Add(InnerContactAccessible);
                    if (InnerContactAccessible == null) continue;
                    Console.WriteLine($"发现联系人：<{InnerContactAccessible.accName[0]}> - Sign: {InnerContactAccessible.accValue[0]}");
                }
            }

            return Contacts;
        }

        public override string ToString()
        {
            return $"{TIMHandle.ToString("X")} - {TIMAccessible.accName[0]} ({TIMAccessible.GetStateText()})";
        }
    }
}
