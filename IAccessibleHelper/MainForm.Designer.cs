namespace IAccessibleHelper
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.AccessibleListBox = new System.Windows.Forms.ListBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ContactTreeView = new System.Windows.Forms.TreeView();
            this.TIMInfoSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SessionListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TIMInfoSplitContainer)).BeginInit();
            this.TIMInfoSplitContainer.Panel1.SuspendLayout();
            this.TIMInfoSplitContainer.Panel2.SuspendLayout();
            this.TIMInfoSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.AccessibleListBox);
            this.MainSplitContainer.Panel1.Controls.Add(this.RefreshButton);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.TIMInfoSplitContainer);
            this.MainSplitContainer.Size = new System.Drawing.Size(613, 436);
            this.MainSplitContainer.SplitterDistance = 204;
            this.MainSplitContainer.TabIndex = 1;
            // 
            // AccessibleListBox
            // 
            this.AccessibleListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AccessibleListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccessibleListBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AccessibleListBox.FormattingEnabled = true;
            this.AccessibleListBox.ItemHeight = 20;
            this.AccessibleListBox.Location = new System.Drawing.Point(0, 28);
            this.AccessibleListBox.Name = "AccessibleListBox";
            this.AccessibleListBox.Size = new System.Drawing.Size(204, 408);
            this.AccessibleListBox.TabIndex = 1;
            this.AccessibleListBox.SelectedIndexChanged += new System.EventHandler(this.AccessibleListBox_SelectedIndexChanged);
            // 
            // RefreshButton
            // 
            this.RefreshButton.BackColor = System.Drawing.Color.White;
            this.RefreshButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.RefreshButton.FlatAppearance.BorderSize = 0;
            this.RefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RefreshButton.Location = new System.Drawing.Point(0, 0);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(204, 28);
            this.RefreshButton.TabIndex = 0;
            this.RefreshButton.Text = "刷新";
            this.RefreshButton.UseVisualStyleBackColor = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ContactTreeView
            // 
            this.ContactTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ContactTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContactTreeView.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ContactTreeView.FullRowSelect = true;
            this.ContactTreeView.Location = new System.Drawing.Point(0, 0);
            this.ContactTreeView.Name = "ContactTreeView";
            this.ContactTreeView.ShowLines = false;
            this.ContactTreeView.Size = new System.Drawing.Size(208, 436);
            this.ContactTreeView.TabIndex = 0;
            // 
            // TIMInfoSplitContainer
            // 
            this.TIMInfoSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TIMInfoSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.TIMInfoSplitContainer.Name = "TIMInfoSplitContainer";
            // 
            // TIMInfoSplitContainer.Panel1
            // 
            this.TIMInfoSplitContainer.Panel1.Controls.Add(this.ContactTreeView);
            // 
            // TIMInfoSplitContainer.Panel2
            // 
            this.TIMInfoSplitContainer.Panel2.Controls.Add(this.SessionListBox);
            this.TIMInfoSplitContainer.Size = new System.Drawing.Size(405, 436);
            this.TIMInfoSplitContainer.SplitterDistance = 208;
            this.TIMInfoSplitContainer.TabIndex = 1;
            // 
            // SessionListBox
            // 
            this.SessionListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SessionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SessionListBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SessionListBox.FormattingEnabled = true;
            this.SessionListBox.ItemHeight = 20;
            this.SessionListBox.Location = new System.Drawing.Point(0, 0);
            this.SessionListBox.Name = "SessionListBox";
            this.SessionListBox.Size = new System.Drawing.Size(193, 436);
            this.SessionListBox.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 436);
            this.Controls.Add(this.MainSplitContainer);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.TIMInfoSplitContainer.Panel1.ResumeLayout(false);
            this.TIMInfoSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TIMInfoSplitContainer)).EndInit();
            this.TIMInfoSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ListBox AccessibleListBox;
        private System.Windows.Forms.TreeView ContactTreeView;
        private System.Windows.Forms.SplitContainer TIMInfoSplitContainer;
        private System.Windows.Forms.ListBox SessionListBox;
    }
}

