using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accessibility;
using Leon;
using static Leon.TIMHelper;

namespace IAccessibleHelper
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshButton.Text = "正在刷新 ...";
            RefreshButton.Enabled = false;

            AccessibleListBox.Items.Clear();
            foreach (var TIMwindow in TIMHelper.GetTIMHandles())
            {
                TIMHelper TIMhelper = new TIMHelper(TIMwindow.Handle);
                AccessibleListBox.Items.Add(TIMhelper);
            }

            RefreshButton.Text = "刷新";
            RefreshButton.Enabled = true;
        }

        private void AccessibleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AccessibleListBox.SelectedItems.Count < 1) return;
            TIMHelper helper = AccessibleListBox.SelectedItem as TIMHelper;
            if (helper == null) return;

            ContactTreeView.Nodes.Clear();
            foreach (string key in helper.Contacts.Keys)
            {
                TreeNode GroupNode = new TreeNode($"{key}  ({helper.Contacts[key].Count} 个联系人)") { Name=key, BackColor=Color.WhiteSmoke};
                foreach (IAccessible Contact in helper.Contacts[key])
                {
                    if (Contact == null)
                        GroupNode.Nodes.Add("<未知联系人>");
                    else
                        GroupNode.Nodes.Add(Contact.accName[0], $"<{Contact.accName[0]}> - 签名: {Contact.accValue[0]}");
                }
                ContactTreeView.Nodes.Add(GroupNode);
            }

            SessionListBox.Items.Clear();
            foreach (IAccessible session in helper.Sessions)
            {
                SessionListBox.Items.Add($"<{session.accName[0]}> - Message: {session.accValue[0]}");
            }
        }
    }
}
