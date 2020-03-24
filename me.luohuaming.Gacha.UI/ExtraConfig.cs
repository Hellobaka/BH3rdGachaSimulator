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
            INIhelper.IniWrite("ExtraConfig", "TextGacha", (checkBox_TextGacha.Checked) ? "1" : "0", CQSave.cq_menu.CQApi.AppDirectory + "\\Config.ini");
            MessageBox.Show("更改已保存");
        }

        private void ExtraConfig_Load(object sender, EventArgs e)
        {
            checkBox_TextGacha.Checked= (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", CQSave.cq_menu.CQApi.AppDirectory + "\\Config.ini"))=="1"?true:false;
        }
    }
}
