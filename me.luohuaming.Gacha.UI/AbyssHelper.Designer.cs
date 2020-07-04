namespace me.luohuaming.Gacha.UI
{
    partial class AbyssHelper
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
            this.comboBox_Week = new System.Windows.Forms.ComboBox();
            this.textBox_Hours = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Minutes = new System.Windows.Forms.TextBox();
            this.richTextBox_RemindText = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_update = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_NonChecked = new System.Windows.Forms.Button();
            this.button_AntiChecked = new System.Windows.Forms.Button();
            this.button_AllChecked = new System.Windows.Forms.Button();
            this.checkedListBox_Group = new System.Windows.Forms.CheckedListBox();
            this.label_RemindText = new System.Windows.Forms.Label();
            this.dataGridView_Details = new System.Windows.Forms.DataGridView();
            this.Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DayOfWeek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemindText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_timerInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Details)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_Week
            // 
            this.comboBox_Week.FormattingEnabled = true;
            this.comboBox_Week.Items.AddRange(new object[] {
            "周日",
            "周一",
            "周二",
            "周三",
            "周四",
            "周五",
            "周六"});
            this.comboBox_Week.Location = new System.Drawing.Point(7, 18);
            this.comboBox_Week.Name = "comboBox_Week";
            this.comboBox_Week.Size = new System.Drawing.Size(71, 20);
            this.comboBox_Week.TabIndex = 0;
            this.comboBox_Week.Text = "周三";
            // 
            // textBox_Hours
            // 
            this.textBox_Hours.Location = new System.Drawing.Point(84, 18);
            this.textBox_Hours.Name = "textBox_Hours";
            this.textBox_Hours.Size = new System.Drawing.Size(33, 21);
            this.textBox_Hours.TabIndex = 3;
            this.textBox_Hours.Text = "18";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = ":";
            // 
            // textBox_Minutes
            // 
            this.textBox_Minutes.Location = new System.Drawing.Point(133, 18);
            this.textBox_Minutes.Name = "textBox_Minutes";
            this.textBox_Minutes.Size = new System.Drawing.Size(33, 21);
            this.textBox_Minutes.TabIndex = 5;
            this.textBox_Minutes.Text = "30";
            // 
            // richTextBox_RemindText
            // 
            this.richTextBox_RemindText.Location = new System.Drawing.Point(6, 62);
            this.richTextBox_RemindText.Name = "richTextBox_RemindText";
            this.richTextBox_RemindText.Size = new System.Drawing.Size(314, 43);
            this.richTextBox_RemindText.TabIndex = 6;
            this.richTextBox_RemindText.Text = "今天是深渊结算的日子，舰长要不要去挑战试试~";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox_RemindText);
            this.groupBox1.Controls.Add(this.button_update);
            this.groupBox1.Controls.Add(this.button_Add);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label_RemindText);
            this.groupBox1.Controls.Add(this.comboBox_Week);
            this.groupBox1.Controls.Add(this.textBox_Minutes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_Hours);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 426);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "提醒设置";
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(172, 377);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(161, 43);
            this.button_update.TabIndex = 13;
            this.button_update.Text = "更新右侧选中项";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(9, 377);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(157, 43);
            this.button_Add.TabIndex = 12;
            this.button_Add.Text = "添加";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_NonChecked);
            this.groupBox2.Controls.Add(this.button_AntiChecked);
            this.groupBox2.Controls.Add(this.button_AllChecked);
            this.groupBox2.Controls.Add(this.checkedListBox_Group);
            this.groupBox2.Location = new System.Drawing.Point(7, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 245);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择生效的群";
            // 
            // button_NonChecked
            // 
            this.button_NonChecked.Location = new System.Drawing.Point(296, 168);
            this.button_NonChecked.Name = "button_NonChecked";
            this.button_NonChecked.Size = new System.Drawing.Size(25, 23);
            this.button_NonChecked.TabIndex = 13;
            this.button_NonChecked.Text = "否";
            this.button_NonChecked.UseVisualStyleBackColor = true;
            this.button_NonChecked.Click += new System.EventHandler(this.button_NonChecked_Click);
            // 
            // button_AntiChecked
            // 
            this.button_AntiChecked.Location = new System.Drawing.Point(296, 119);
            this.button_AntiChecked.Name = "button_AntiChecked";
            this.button_AntiChecked.Size = new System.Drawing.Size(25, 23);
            this.button_AntiChecked.TabIndex = 12;
            this.button_AntiChecked.Text = "反";
            this.button_AntiChecked.UseVisualStyleBackColor = true;
            this.button_AntiChecked.Click += new System.EventHandler(this.button_AntiChecked_Click);
            // 
            // button_AllChecked
            // 
            this.button_AllChecked.Location = new System.Drawing.Point(296, 68);
            this.button_AllChecked.Name = "button_AllChecked";
            this.button_AllChecked.Size = new System.Drawing.Size(25, 23);
            this.button_AllChecked.TabIndex = 11;
            this.button_AllChecked.Text = "全";
            this.button_AllChecked.UseVisualStyleBackColor = true;
            this.button_AllChecked.Click += new System.EventHandler(this.button_AllChecked_Click);
            // 
            // checkedListBox_Group
            // 
            this.checkedListBox_Group.CheckOnClick = true;
            this.checkedListBox_Group.FormattingEnabled = true;
            this.checkedListBox_Group.Location = new System.Drawing.Point(6, 21);
            this.checkedListBox_Group.Name = "checkedListBox_Group";
            this.checkedListBox_Group.Size = new System.Drawing.Size(284, 212);
            this.checkedListBox_Group.TabIndex = 10;
            // 
            // label_RemindText
            // 
            this.label_RemindText.AutoSize = true;
            this.label_RemindText.Location = new System.Drawing.Point(7, 43);
            this.label_RemindText.Name = "label_RemindText";
            this.label_RemindText.Size = new System.Drawing.Size(53, 12);
            this.label_RemindText.TabIndex = 7;
            this.label_RemindText.Text = "提醒文本";
            // 
            // dataGridView_Details
            // 
            this.dataGridView_Details.AllowUserToAddRows = false;
            this.dataGridView_Details.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Details.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Enabled,
            this.DayOfWeek,
            this.Time,
            this.RemindText,
            this.Column1});
            this.dataGridView_Details.Location = new System.Drawing.Point(357, 12);
            this.dataGridView_Details.MultiSelect = false;
            this.dataGridView_Details.Name = "dataGridView_Details";
            this.dataGridView_Details.ReadOnly = true;
            this.dataGridView_Details.RowTemplate.Height = 23;
            this.dataGridView_Details.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Details.Size = new System.Drawing.Size(431, 407);
            this.dataGridView_Details.TabIndex = 8;
            this.dataGridView_Details.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Details_CellClick);
            this.dataGridView_Details.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Details_CellContentClick);
            // 
            // Enabled
            // 
            this.Enabled.HeaderText = "启用";
            this.Enabled.Name = "Enabled";
            this.Enabled.ReadOnly = true;
            this.Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Enabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Enabled.Width = 40;
            // 
            // DayOfWeek
            // 
            this.DayOfWeek.HeaderText = "星期";
            this.DayOfWeek.Name = "DayOfWeek";
            this.DayOfWeek.ReadOnly = true;
            this.DayOfWeek.Width = 60;
            // 
            // Time
            // 
            this.Time.HeaderText = "提醒时间";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 80;
            // 
            // RemindText
            // 
            this.RemindText.HeaderText = "提醒文本";
            this.RemindText.Name = "RemindText";
            this.RemindText.ReadOnly = true;
            this.RemindText.Width = 200;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "开启的群";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(679, 444);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(109, 32);
            this.button_Save.TabIndex = 9;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(358, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "时钟周期:";
            // 
            // textBox_timerInterval
            // 
            this.textBox_timerInterval.Location = new System.Drawing.Point(423, 449);
            this.textBox_timerInterval.Name = "textBox_timerInterval";
            this.textBox_timerInterval.Size = new System.Drawing.Size(35, 21);
            this.textBox_timerInterval.TabIndex = 11;
            this.textBox_timerInterval.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(464, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "(秒)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 422);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "按Del键删除行";
            // 
            // AbyssHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 484);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_timerInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.dataGridView_Details);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "AbyssHelper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "深渊提醒助手";
            this.Load += new System.EventHandler(this.AbyssHelper_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Details)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Week;
        private System.Windows.Forms.TextBox textBox_Hours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Minutes;
        private System.Windows.Forms.RichTextBox richTextBox_RemindText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_RemindText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox_Group;
        private System.Windows.Forms.DataGridView dataGridView_Details;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayOfWeek;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemindText;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button button_NonChecked;
        private System.Windows.Forms.Button button_AntiChecked;
        private System.Windows.Forms.Button button_AllChecked;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_timerInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}