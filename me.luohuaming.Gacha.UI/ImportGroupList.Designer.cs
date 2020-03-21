namespace me.luohuaming.Gacha.UI
{
    partial class ImportGroupList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listBox_Group = new System.Windows.Forms.ListBox();
            this.button_SelectExport = new System.Windows.Forms.Button();
            this.button_AllExport = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Label_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_Text = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_Error = new System.Windows.Forms.ToolStripStatusLabel();
            this.label_Count = new System.Windows.Forms.Label();
            this.button_Clear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Admin = new System.Windows.Forms.TextBox();
            this.button_Save = new System.Windows.Forms.Button();
            this.checkBox_CoverSetting = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GroupId,
            this.GroupName});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(234, 371);
            this.dataGridView1.TabIndex = 0;
            // 
            // GroupId
            // 
            this.GroupId.HeaderText = "群号";
            this.GroupId.Name = "GroupId";
            this.GroupId.ReadOnly = true;
            // 
            // GroupName
            // 
            this.GroupName.HeaderText = "群名称";
            this.GroupName.Name = "GroupName";
            this.GroupName.ReadOnly = true;
            this.GroupName.Width = 130;
            // 
            // listBox_Group
            // 
            this.listBox_Group.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_Group.FormattingEnabled = true;
            this.listBox_Group.ItemHeight = 17;
            this.listBox_Group.Location = new System.Drawing.Point(405, 36);
            this.listBox_Group.Name = "listBox_Group";
            this.listBox_Group.ScrollAlwaysVisible = true;
            this.listBox_Group.Size = new System.Drawing.Size(149, 344);
            this.listBox_Group.TabIndex = 1;
            this.listBox_Group.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_Group_KeyDown);
            // 
            // button_SelectExport
            // 
            this.button_SelectExport.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_SelectExport.Location = new System.Drawing.Point(283, 102);
            this.button_SelectExport.Name = "button_SelectExport";
            this.button_SelectExport.Size = new System.Drawing.Size(96, 23);
            this.button_SelectExport.TabIndex = 2;
            this.button_SelectExport.Text = "导入选中";
            this.button_SelectExport.UseVisualStyleBackColor = true;
            this.button_SelectExport.Click += new System.EventHandler(this.button_SelectExport_Click);
            // 
            // button_AllExport
            // 
            this.button_AllExport.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_AllExport.Location = new System.Drawing.Point(283, 176);
            this.button_AllExport.Name = "button_AllExport";
            this.button_AllExport.Size = new System.Drawing.Size(96, 23);
            this.button_AllExport.TabIndex = 3;
            this.button_AllExport.Text = "全部导入";
            this.button_AllExport.UseVisualStyleBackColor = true;
            this.button_AllExport.Click += new System.EventHandler(this.button_AllExport_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Label_Status,
            this.Label_Text,
            this.toolStripStatusLabel1,
            this.Label_Error});
            this.statusStrip1.Location = new System.Drawing.Point(0, 407);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(757, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Label_Status
            // 
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(64, 17);
            this.Label_Status.Text = "载入中……";
            // 
            // Label_Text
            // 
            this.Label_Text.Name = "Label_Text";
            this.Label_Text.Size = new System.Drawing.Size(62, 17);
            this.Label_Text.Text = "载入x个群";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(163, 17);
            this.toolStripStatusLabel1.Text = "|                                      ";
            // 
            // Label_Error
            // 
            this.Label_Error.Name = "Label_Error";
            this.Label_Error.Size = new System.Drawing.Size(32, 17);
            this.Label_Error.Text = "错误";
            this.Label_Error.Visible = false;
            // 
            // label_Count
            // 
            this.label_Count.AutoSize = true;
            this.label_Count.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Count.Location = new System.Drawing.Point(403, 12);
            this.label_Count.Name = "label_Count";
            this.label_Count.Size = new System.Drawing.Size(53, 17);
            this.label_Count.TabIndex = 5;
            this.label_Count.Text = "计数:x个";
            // 
            // button_Clear
            // 
            this.button_Clear.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Clear.Location = new System.Drawing.Point(283, 251);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(96, 23);
            this.button_Clear.TabIndex = 6;
            this.button_Clear.Text = "清空";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(560, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 85);
            this.label1.TabIndex = 7;
            this.label1.Text = "提供简易的批量导入，群的详细\r\n\r\n设置请在主界面进行修改\r\n\r\n清除某个项目请按Delete键";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(561, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "为批量导入的群设置统一的管理员:";
            // 
            // textBox_Admin
            // 
            this.textBox_Admin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Admin.Location = new System.Drawing.Point(563, 230);
            this.textBox_Admin.Name = "textBox_Admin";
            this.textBox_Admin.Size = new System.Drawing.Size(182, 23);
            this.textBox_Admin.TabIndex = 9;
            // 
            // button_Save
            // 
            this.button_Save.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Save.Location = new System.Drawing.Point(563, 300);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(188, 79);
            this.button_Save.TabIndex = 10;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // checkBox_CoverSetting
            // 
            this.checkBox_CoverSetting.AutoSize = true;
            this.checkBox_CoverSetting.Location = new System.Drawing.Point(567, 265);
            this.checkBox_CoverSetting.Name = "checkBox_CoverSetting";
            this.checkBox_CoverSetting.Size = new System.Drawing.Size(156, 16);
            this.checkBox_CoverSetting.TabIndex = 11;
            this.checkBox_CoverSetting.Text = "覆盖已存在的管理员设置";
            this.checkBox_CoverSetting.UseVisualStyleBackColor = true;
            // 
            // ImportGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 429);
            this.Controls.Add(this.checkBox_CoverSetting);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.textBox_Admin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.label_Count);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_AllExport);
            this.Controls.Add(this.button_SelectExport);
            this.Controls.Add(this.listBox_Group);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ImportGroupList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportGroupList";
            this.Load += new System.EventHandler(this.ImportGroupList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.ListBox listBox_Group;
        private System.Windows.Forms.Button button_SelectExport;
        private System.Windows.Forms.Button button_AllExport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Label_Status;
        private System.Windows.Forms.ToolStripStatusLabel Label_Text;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel Label_Error;
        private System.Windows.Forms.Label label_Count;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Admin;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.CheckBox checkBox_CoverSetting;
    }
}