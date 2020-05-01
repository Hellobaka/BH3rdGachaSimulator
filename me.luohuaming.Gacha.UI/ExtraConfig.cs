using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;
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
        static IniConfig ini;
        private void button_Save_Click(object sender, EventArgs e)
        {
            string path = CQSave.AppDirectory + "Config.ini";
            ini.Object["ExtraConfig"]["TextGacha"]=new IValue((checkBox_TextGacha.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["ExecuteSql"]=new IValue((checkBox_Sql.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["ImageFormat"]=new IValue((radioButton_Picjpg.Checked) ? "jpg" : "png");

            ini.Object["ExtraConfig"]["SwitchBP1"]=new IValue((checkBox_SwBP1.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchBP10"]=new IValue((checkBox_SwBP10.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchJZA1"]=new IValue((checkBox_SwJZA1.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchJZA10"]=new IValue((checkBox_SwJZA10.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchJZB1"]=new IValue((checkBox_SwJZB1.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchJZB10"]=new IValue((checkBox_SwJZB10.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchKC1"]=new IValue((checkBox_SwKC1.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchKC10"]=new IValue((checkBox_SwKC10.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchQueDiamond"]=new IValue((checkBox_SwQueDiamond.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchResSign"]=new IValue((checkBox_SwResSign.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchGetHelp"]=new IValue((checkBox_SwGetHelp.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchGetPool"]=new IValue((checkBox_SwGetPool.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchKaKin"]=new IValue((checkBox_SwKaKin.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchOpenGroup"]=new IValue((checkBox_SwOpenGroup.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchCloseGroup"]=new IValue((checkBox_SwCloseGroup.Checked) ? "1" : "0");
            ini.Object["ExtraConfig"]["SwitchOpenAdmin"]=new IValue((checkBox_SwOpenAdmin.Checked) ? "1" : "0");

            ini.Save();

            MessageBox.Show("更改已保存");
        }

        private void ExtraConfig_Load(object sender, EventArgs e)
        {
            string path = CQSave.AppDirectory + "Config.ini";
            ini = new IniConfig(path);
            ini.Load();
            checkBox_TextGacha.Checked = (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0")) == "1" ? true : false;
            checkBox_Sql.Checked = (ini.Object["ExtraConfig"]["ExecuteSql"].GetValueOrDefault("0")) == "1" ? true : false;
            radioButton_Picjpg.Checked = (ini.Object["ExtraConfig"]["ImageFormat"].GetValueOrDefault("jpg") == "jpg") ? true : false;
            radioButton_Picpng.Checked = (ini.Object["ExtraConfig"]["ImageFormat"].GetValueOrDefault("jpg") == "png") ? true : false;

            checkBox_SwBP1.Checked = (ini.Object["ExtraConfig"]["SwitchBP1"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwBP10.Checked = (ini.Object["ExtraConfig"]["SwitchBP10"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwJZA1.Checked = (ini.Object["ExtraConfig"]["SwitchJZA1"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwJZA10.Checked = (ini.Object["ExtraConfig"]["SwitchJZA10"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwJZB1.Checked = (ini.Object["ExtraConfig"]["SwitchJZB1"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwJZB10.Checked = (ini.Object["ExtraConfig"]["SwitchJZB10"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwKC1.Checked = (ini.Object["ExtraConfig"]["SwitchKC1"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwKC10.Checked = (ini.Object["ExtraConfig"]["SwitchKC10"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwQueDiamond.Checked = (ini.Object["ExtraConfig"]["SwitchQueDiamond"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwResSign.Checked = (ini.Object["ExtraConfig"]["SwitchResSign"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwGetHelp.Checked = (ini.Object["ExtraConfig"]["SwitchGetHelp"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwGetPool.Checked = (ini.Object["ExtraConfig"]["SwitchGetPool"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwKaKin.Checked = (ini.Object["ExtraConfig"]["SwitchKaKin"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwOpenGroup.Checked = (ini.Object["ExtraConfig"]["SwitchOpenGroup"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwCloseGroup.Checked = (ini.Object["ExtraConfig"]["SwitchCloseGroup"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_SwOpenAdmin.Checked = (ini.Object["ExtraConfig"]["SwitchOpenAdmin"].GetValueOrDefault("1") == "1") ? true : false;

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
