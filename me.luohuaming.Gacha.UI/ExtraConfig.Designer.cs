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
            this.checkBox_TextGacha = new System.Windows.Forms.CheckBox();
            this.button_Save = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_Sql = new System.Windows.Forms.CheckBox();
            this.radioButton_Picjpg = new System.Windows.Forms.RadioButton();
            this.radioButton_Picpng = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
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
            // ExtraConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}