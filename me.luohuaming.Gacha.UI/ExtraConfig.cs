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
            string path = CQSave.AppDirectory + "Config.ini";
            INIhelper.IniWrite("ExtraConfig", "TextGacha", (checkBox_TextGacha.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "ExecuteSql", (checkBox_Sql.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "ImageFormat", (radioButton_Picjpg.Checked) ? "jpg" : "png", path);

            INIhelper.IniWrite("ExtraConfig", "SwitchBP1", (checkBox_SwBP1.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchBP10", (checkBox_SwBP10.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchJZA1", (checkBox_SwJZA1.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchJZA10", (checkBox_SwJZA10.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchJZB1", (checkBox_SwJZB1.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchJZB10", (checkBox_SwJZB10.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchKC1", (checkBox_SwKC1.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchKC10", (checkBox_SwKC10.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchQueDiamond", (checkBox_SwQueDiamond.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchResSign", (checkBox_SwResSign.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchGetHelp", (checkBox_SwGetHelp.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchGetPool", (checkBox_SwGetPool.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchKaKin", (checkBox_SwKaKin.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchOpenGroup", (checkBox_SwOpenGroup.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchCloseGroup", (checkBox_SwCloseGroup.Checked) ? "1" : "0", path);
            INIhelper.IniWrite("ExtraConfig", "SwitchOpenAdmin", (checkBox_SwOpenAdmin.Checked) ? "1" : "0", path);

            MessageBox.Show("更改已保存");
        }

        private void ExtraConfig_Load(object sender, EventArgs e)
        {
            string path = CQSave.AppDirectory + "Config.ini";

            checkBox_TextGacha.Checked = (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", path)) == "1" ? true : false;
            checkBox_Sql.Checked = (INIhelper.IniRead("ExtraConfig", "ExecuteSql", "0", path)) == "1" ? true : false;
            radioButton_Picjpg.Checked = (INIhelper.IniRead("ExtraConfig", "ImageFormat", "jpg", path) == "jpg") ? true : false;
            radioButton_Picpng.Checked = (INIhelper.IniRead("ExtraConfig", "ImageFormat", "jpg", path) == "png") ? true : false;

            checkBox_SwBP1.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchBP1", "1", path) == "1") ? true : false;
            checkBox_SwBP10.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchBP10", "1", path) == "1") ? true : false;
            checkBox_SwJZA1.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchJZA1", "1", path) == "1") ? true : false;
            checkBox_SwJZA10.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchJZA10", "1", path) == "1") ? true : false;
            checkBox_SwJZB1.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchJZB1", "1", path) == "1") ? true : false;
            checkBox_SwJZB10.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchJZB10", "1", path) == "1") ? true : false;
            checkBox_SwKC1.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchKC1", "1", path) == "1") ? true : false;
            checkBox_SwKC10.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchKC10", "1", path) == "1") ? true : false;
            checkBox_SwQueDiamond.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchQueDiamond", "1", path) == "1") ? true : false;
            checkBox_SwResSign.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchResSign", "1", path) == "1") ? true : false;
            checkBox_SwGetHelp.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchGetHelp", "1", path) == "1") ? true : false;
            checkBox_SwGetPool.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchGetPool", "1", path) == "1") ? true : false;
            checkBox_SwKaKin.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchKaKin", "1", path) == "1") ? true : false;
            checkBox_SwOpenGroup.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchOpenGroup", "1", path) == "1") ? true : false;
            checkBox_SwCloseGroup.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchCloseGroup", "1", path) == "1") ? true : false;
            checkBox_SwOpenAdmin.Checked = (INIhelper.IniRead("ExtraConfig", "SwitchOpenAdmin", "1", path) == "1") ? true : false;

            flag = true;
        }
        bool flag = false;
        private void checkBox_Sql_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Sql.Checked && flag)
            {
                MessageBox.Show("开启表示你接受开启此功能可能带来的后果，比如有人衫裤跑路之类的，作者不承担后果", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
