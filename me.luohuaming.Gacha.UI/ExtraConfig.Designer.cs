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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_TextGacha);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 50);
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
            this.button_Save.Location = new System.Drawing.Point(254, 415);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(292, 23);
            this.button_Save.TabIndex = 1;
            this.button_Save.Text = "保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // ExtraConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExtraConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "扩展设置";
            this.Load += new System.EventHandler(this.ExtraConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_TextGacha;
        private System.Windows.Forms.Button button_Save;
    }
}