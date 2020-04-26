namespace me.luohuaming.Gacha.UI
{
    partial class ExtraConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_Picpng = new System.Windows.Forms.RadioButton();
            this.radioButton_Picjpg = new System.Windows.Forms.RadioButton();
            this.checkBox_TextGacha = new System.Windows.Forms.CheckBox();
            this.button_Save = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_Sql = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox_SwOpenAdmin = new System.Windows.Forms.CheckBox();
            this.checkBox_SwCloseGroup = new System.Windows.Forms.CheckBox();
            this.checkBox_SwOpenGroup = new System.Windows.Forms.CheckBox();
            this.checkBox_SwKaKin = new System.Windows.Forms.CheckBox();
            this.checkBox_SwGetPool = new System.Windows.Forms.CheckBox();
            this.checkBox_SwGetHelp = new System.Windows.Forms.CheckBox();
            this.checkBox_SwResSign = new System.Windows.Forms.CheckBox();
            this.checkBox_SwQueDiamond = new System.Windows.Forms.CheckBox();
            this.checkBox_SwKC10 = new System.Windows.Forms.CheckBox();
            this.checkBox_SwKC1 = new System.Windows.Forms.CheckBox();
            this.checkBox_SwJZB10 = new System.Windows.Forms.CheckBox();
            this.checkBox_SwJZB1 = new System.Windows.Forms.CheckBox();
            this.checkBox_SwJZA10 = new System.Windows.Forms.CheckBox();
            this.checkBox_SwJZA1 = new System.Windows.Forms.CheckBox();
            this.checkBox_SwBP10 = new System.Windows.Forms.CheckBox();
            this.checkBox_SwBP1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Picpng);
            this.groupBox1.Controls.Add(this.radioButton_Picjpg);
            this.groupBox1.Controls.Add(this.checkBox_TextGacha);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "抽卡";
            // 
            // radioButton_Picpng
            // 
            this.radioButton_Picpng.AutoSize = true;
            this.radioButton_Picpng.Location = new System.Drawing.Point(257, 49);
            this.radioButton_Picpng.Name = "radioButton_Picpng";
            this.radioButton_Picpng.Size = new System.Drawing.Size(233, 16);
            this.radioButton_Picpng.TabIndex = 2;
            this.radioButton_Picpng.Text = "输出png格式(大 2.3m左右 但清晰度高)";
            this.radioButton_Picpng.UseVisualStyleBackColor = true;
            this.radioButton_Picpng.CheckedChanged += new System.EventHandler(this.radioButton_Picpng_CheckedChanged);
            // 
            // radioButton_Picjpg
            // 
            this.radioButton_Picjpg.AutoSize = true;
            this.radioButton_Picjpg.Checked = true;
            this.radioButton_Picjpg.Location = new System.Drawing.Point(6, 49);
            this.radioButton_Picjpg.Name = "radioButton_Picjpg";
            this.radioButton_Picjpg.Size = new System.Drawing.Size(245, 16);
            this.radioButton_Picjpg.TabIndex = 1;
            this.radioButton_Picjpg.TabStop = true;
            this.radioButton_Picjpg.Text = "输出jpg格式(小 700k左右 但清晰度一般)";
            this.radioButton_Picjpg.UseVisualStyleBackColor = true;
            this.radioButton_Picjpg.CheckedChanged += new System.EventHandler(this.radioButton_Picjpg_CheckedChanged);
            // 
            // checkBox_TextGacha
            // 
            this.checkBox_TextGacha.AutoSize = true;
            this.checkBox_TextGacha.Location = new System.Drawing.Point(6, 20);
            this.checkBox_TextGacha.Name = "checkBox_TextGacha";
            this.checkBox_TextGacha.Size = new System.Drawing.Size(162, 16);
            this.checkBox_TextGacha.TabIndex = 0;
            this.checkBox_TextGacha.Text = "使用文字版抽卡(CQA适用)";
            this.checkBox_TextGacha.UseVisualStyleBackColor = true;
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(252, 415);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(292, 23);
            this.button_Save.TabIndex = 1;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_Sql);
            this.groupBox2.Location = new System.Drawing.Point(12, 169);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(775, 53);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "实验性功能";
            // 
            // checkBox_Sql
            // 
            this.checkBox_Sql.AutoSize = true;
            this.checkBox_Sql.Location = new System.Drawing.Point(10, 24);
            this.checkBox_Sql.Name = "checkBox_Sql";
            this.checkBox_Sql.Size = new System.Drawing.Size(276, 16);
            this.checkBox_Sql.TabIndex = 0;
            this.checkBox_Sql.Text = "允许管理员在群内执行sql语句(以#sql开头即可";
            this.checkBox_Sql.UseVisualStyleBackColor = true;
            this.checkBox_Sql.CheckedChanged += new System.EventHandler(this.checkBox_Sql_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox_SwOpenAdmin);
            this.groupBox3.Controls.Add(this.checkBox_SwCloseGroup);
            this.groupBox3.Controls.Add(this.checkBox_SwOpenGroup);
            this.groupBox3.Controls.Add(this.checkBox_SwKaKin);
            this.groupBox3.Controls.Add(this.checkBox_SwGetPool);
            this.groupBox3.Controls.Add(this.checkBox_SwGetHelp);
            this.groupBox3.Controls.Add(this.checkBox_SwResSign);
            this.groupBox3.Controls.Add(this.checkBox_SwQueDiamond);
            this.groupBox3.Controls.Add(this.checkBox_SwKC10);
            this.groupBox3.Controls.Add(this.checkBox_SwKC1);
            this.groupBox3.Controls.Add(this.checkBox_SwJZB10);
            this.groupBox3.Controls.Add(this.checkBox_SwJZB1);
            this.groupBox3.Controls.Add(this.checkBox_SwJZA10);
            this.groupBox3.Controls.Add(this.checkBox_SwJZA1);
            this.groupBox3.Controls.Add(this.checkBox_SwBP10);
            this.groupBox3.Controls.Add(this.checkBox_SwBP1);
            this.groupBox3.Location = new System.Drawing.Point(15, 95);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(772, 68);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "功能开关";
            // 
            // checkBox_SwOpenAdmin
            // 
            this.checkBox_SwOpenAdmin.AutoSize = true;
            this.checkBox_SwOpenAdmin.Checked = true;
            this.checkBox_SwOpenAdmin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwOpenAdmin.Location = new System.Drawing.Point(575, 42);
            this.checkBox_SwOpenAdmin.Name = "checkBox_SwOpenAdmin";
            this.checkBox_SwOpenAdmin.Size = new System.Drawing.Size(108, 16);
            this.checkBox_SwOpenAdmin.TabIndex = 15;
            this.checkBox_SwOpenAdmin.Text = "指令开管理权限";
            this.checkBox_SwOpenAdmin.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwCloseGroup
            // 
            this.checkBox_SwCloseGroup.AutoSize = true;
            this.checkBox_SwCloseGroup.Checked = true;
            this.checkBox_SwCloseGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwCloseGroup.Location = new System.Drawing.Point(575, 20);
            this.checkBox_SwCloseGroup.Name = "checkBox_SwCloseGroup";
            this.checkBox_SwCloseGroup.Size = new System.Drawing.Size(84, 16);
            this.checkBox_SwCloseGroup.TabIndex = 14;
            this.checkBox_SwCloseGroup.Text = "指令关闭群";
            this.checkBox_SwCloseGroup.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwOpenGroup
            // 
            this.checkBox_SwOpenGroup.AutoSize = true;
            this.checkBox_SwOpenGroup.Checked = true;
            this.checkBox_SwOpenGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwOpenGroup.Location = new System.Drawing.Point(491, 43);
            this.checkBox_SwOpenGroup.Name = "checkBox_SwOpenGroup";
            this.checkBox_SwOpenGroup.Size = new System.Drawing.Size(84, 16);
            this.checkBox_SwOpenGroup.TabIndex = 13;
            this.checkBox_SwOpenGroup.Text = "指令开启群";
            this.checkBox_SwOpenGroup.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwKaKin
            // 
            this.checkBox_SwKaKin.AutoSize = true;
            this.checkBox_SwKaKin.Checked = true;
            this.checkBox_SwKaKin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwKaKin.Location = new System.Drawing.Point(491, 21);
            this.checkBox_SwKaKin.Name = "checkBox_SwKaKin";
            this.checkBox_SwKaKin.Size = new System.Drawing.Size(48, 16);
            this.checkBox_SwKaKin.TabIndex = 12;
            this.checkBox_SwKaKin.Text = "氪金";
            this.checkBox_SwKaKin.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwGetPool
            // 
            this.checkBox_SwGetPool.AutoSize = true;
            this.checkBox_SwGetPool.Checked = true;
            this.checkBox_SwGetPool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwGetPool.Location = new System.Drawing.Point(413, 43);
            this.checkBox_SwGetPool.Name = "checkBox_SwGetPool";
            this.checkBox_SwGetPool.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SwGetPool.TabIndex = 11;
            this.checkBox_SwGetPool.Text = "获取池子";
            this.checkBox_SwGetPool.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwGetHelp
            // 
            this.checkBox_SwGetHelp.AutoSize = true;
            this.checkBox_SwGetHelp.Checked = true;
            this.checkBox_SwGetHelp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwGetHelp.Location = new System.Drawing.Point(413, 21);
            this.checkBox_SwGetHelp.Name = "checkBox_SwGetHelp";
            this.checkBox_SwGetHelp.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SwGetHelp.TabIndex = 10;
            this.checkBox_SwGetHelp.Text = "获取帮助";
            this.checkBox_SwGetHelp.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwResSign
            // 
            this.checkBox_SwResSign.AutoSize = true;
            this.checkBox_SwResSign.Checked = true;
            this.checkBox_SwResSign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwResSign.Location = new System.Drawing.Point(330, 43);
            this.checkBox_SwResSign.Name = "checkBox_SwResSign";
            this.checkBox_SwResSign.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SwResSign.TabIndex = 9;
            this.checkBox_SwResSign.Text = "签到重置";
            this.checkBox_SwResSign.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwQueDiamond
            // 
            this.checkBox_SwQueDiamond.AutoSize = true;
            this.checkBox_SwQueDiamond.Checked = true;
            this.checkBox_SwQueDiamond.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwQueDiamond.Location = new System.Drawing.Point(330, 21);
            this.checkBox_SwQueDiamond.Name = "checkBox_SwQueDiamond";
            this.checkBox_SwQueDiamond.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SwQueDiamond.TabIndex = 8;
            this.checkBox_SwQueDiamond.Text = "查询水晶";
            this.checkBox_SwQueDiamond.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwKC10
            // 
            this.checkBox_SwKC10.AutoSize = true;
            this.checkBox_SwKC10.Checked = true;
            this.checkBox_SwKC10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwKC10.Location = new System.Drawing.Point(252, 43);
            this.checkBox_SwKC10.Name = "checkBox_SwKC10";
            this.checkBox_SwKC10.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SwKC10.TabIndex = 7;
            this.checkBox_SwKC10.Text = "扩充十连";
            this.checkBox_SwKC10.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwKC1
            // 
            this.checkBox_SwKC1.AutoSize = true;
            this.checkBox_SwKC1.Checked = true;
            this.checkBox_SwKC1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwKC1.Location = new System.Drawing.Point(252, 21);
            this.checkBox_SwKC1.Name = "checkBox_SwKC1";
            this.checkBox_SwKC1.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SwKC1.TabIndex = 6;
            this.checkBox_SwKC1.Text = "扩充单抽";
            this.checkBox_SwKC1.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwJZB10
            // 
            this.checkBox_SwJZB10.AutoSize = true;
            this.checkBox_SwJZB10.Checked = true;
            this.checkBox_SwJZB10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwJZB10.Location = new System.Drawing.Point(168, 42);
            this.checkBox_SwJZB10.Name = "checkBox_SwJZB10";
            this.checkBox_SwJZB10.Size = new System.Drawing.Size(78, 16);
            this.checkBox_SwJZB10.TabIndex = 5;
            this.checkBox_SwJZB10.Text = "精准B十连";
            this.checkBox_SwJZB10.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwJZB1
            // 
            this.checkBox_SwJZB1.AutoSize = true;
            this.checkBox_SwJZB1.Checked = true;
            this.checkBox_SwJZB1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwJZB1.Location = new System.Drawing.Point(168, 20);
            this.checkBox_SwJZB1.Name = "checkBox_SwJZB1";
            this.checkBox_SwJZB1.Size = new System.Drawing.Size(78, 16);
            this.checkBox_SwJZB1.TabIndex = 4;
            this.checkBox_SwJZB1.Text = "精准B单抽";
            this.checkBox_SwJZB1.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwJZA10
            // 
            this.checkBox_SwJZA10.AutoSize = true;
            this.checkBox_SwJZA10.Checked = true;
            this.checkBox_SwJZA10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwJZA10.Location = new System.Drawing.Point(84, 42);
            this.checkBox_SwJZA10.Name = "checkBox_SwJZA10";
            this.checkBox_SwJZA10.Size = new System.Drawing.Size(78, 16);
            this.checkBox_SwJZA10.TabIndex = 3;
            this.checkBox_SwJZA10.Text = "精准A十连";
            this.checkBox_SwJZA10.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwJZA1
            // 
            this.checkBox_SwJZA1.AutoSize = true;
            this.checkBox_SwJZA1.Checked = true;
            this.checkBox_SwJZA1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwJZA1.Location = new System.Drawing.Point(84, 20);
            this.checkBox_SwJZA1.Name = "checkBox_SwJZA1";
            this.checkBox_SwJZA1.Size = new System.Drawing.Size(78, 16);
            this.checkBox_SwJZA1.TabIndex = 2;
            this.checkBox_SwJZA1.Text = "精准A单抽";
            this.checkBox_SwJZA1.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwBP10
            // 
            this.checkBox_SwBP10.AutoSize = true;
            this.checkBox_SwBP10.Checked = true;
            this.checkBox_SwBP10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwBP10.Location = new System.Drawing.Point(6, 42);
            this.checkBox_SwBP10.Name = "checkBox_SwBP10";
            this.checkBox_SwBP10.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SwBP10.TabIndex = 1;
            this.checkBox_SwBP10.Text = "标配十连";
            this.checkBox_SwBP10.UseVisualStyleBackColor = true;
            // 
            // checkBox_SwBP1
            // 
            this.checkBox_SwBP1.AutoSize = true;
            this.checkBox_SwBP1.Checked = true;
            this.checkBox_SwBP1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SwBP1.Location = new System.Drawing.Point(6, 20);
            this.checkBox_SwBP1.Name = "checkBox_SwBP1";
            this.checkBox_SwBP1.Size = new System.Drawing.Size(72, 16);
            this.checkBox_SwBP1.TabIndex = 0;
            this.checkBox_SwBP1.Text = "标配单抽";
            this.checkBox_SwBP1.UseVisualStyleBackColor = true;
            // 
            // ExtraConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExtraConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "扩展设置";
            this.Load += new System.EventHandler(this.ExtraConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_TextGacha;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_Sql;
        private System.Windows.Forms.RadioButton radioButton_Picpng;
        private System.Windows.Forms.RadioButton radioButton_Picjpg;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox_SwBP1;
        private System.Windows.Forms.CheckBox checkBox_SwJZB10;
        private System.Windows.Forms.CheckBox checkBox_SwJZB1;
        private System.Windows.Forms.CheckBox checkBox_SwJZA10;
        private System.Windows.Forms.CheckBox checkBox_SwJZA1;
        private System.Windows.Forms.CheckBox checkBox_SwBP10;
        private System.Windows.Forms.CheckBox checkBox_SwGetPool;
        private System.Windows.Forms.CheckBox checkBox_SwGetHelp;
        private System.Windows.Forms.CheckBox checkBox_SwResSign;
        private System.Windows.Forms.CheckBox checkBox_SwQueDiamond;
        private System.Windows.Forms.CheckBox checkBox_SwKC10;
        private System.Windows.Forms.CheckBox checkBox_SwKC1;
        private System.Windows.Forms.CheckBox checkBox_SwOpenGroup;
        private System.Windows.Forms.CheckBox checkBox_SwKaKin;
        private System.Windows.Forms.CheckBox checkBox_SwOpenAdmin;
        private System.Windows.Forms.CheckBox checkBox_SwCloseGroup;
    }
}