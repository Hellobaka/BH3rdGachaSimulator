using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace me.luohuaming.Gacha.UI
{
    public partial class ExtraConfig : Form
    {
        public ExtraConfig()
        {
            InitializeComponent();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            INIhelper.IniWrite("ExtraConfig", "TextGacha", (checkBox_TextGacha.Checked) ? "1" : "0", CQSave.cq_menu.CQApi.AppDirectory + "Config.ini");
            INIhelper.IniWrite("ExtraConfig", "ExecuteSql", (checkBox_Sql.Checked) ? "1" : "0", CQSave.cq_menu.CQApi.AppDirectory + "Config.ini");
            INIhelper.IniWrite("ExtraConfig", "ImageFormat", (radioButton_Picjpg.Checked) ? "jpg" : "png", CQSave.cq_menu.CQApi.AppDirectory + "Config.ini");
            MessageBox.Show("更改已保存");
        }

        private void ExtraConfig_Load(object sender, EventArgs e)
        {
            checkBox_TextGacha.Checked= (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", CQSave.cq_menu.CQApi.AppDirectory + "Config.ini"))=="1"?true:false;
            checkBox_Sql.Checked = (INIhelper.IniRead("ExtraConfig", "ExecuteSql", "0", CQSave.cq_menu.CQApi.AppDirectory + "Config.ini")) == "1" ? true : false;
            radioButton_Picjpg.Checked = (INIhelper.IniRead("ExtraConfig", "ImageFormat", "jpg", CQSave.cq_menu.CQApi.AppDirectory + "Config.ini") == "jpg") ? true : false;
            radioButton_Picpng.Checked = (INIhelper.IniRead("ExtraConfig", "ImageFormat", "jpg", CQSave.cq_menu.CQApi.AppDirectory + "Config.ini") == "png") ? true : false;
            flag = true;
        }
        bool flag = false;
        private void checkBox_Sql_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_Sql.Checked&&flag)
            {
                MessageBox.Show("开启表示你接受开启此功能可能带来的后果，比如有人衫裤跑路之类的，作者不承担后果","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void radioButton_Picjpg_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton_Picpng_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
