using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Native.Sdk.Cqp.EventArgs;
using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;

namespace me.luohuaming.Gacha.UI
{
    public partial class ImportGroupList : Form
    {
        public ImportGroupList()
        {
            InitializeComponent();
        }
        static CQMenuCallEventArgs cq;
        static IniConfig ini;
        private void ImportGroupList_Load(object sender, EventArgs e)
        {
            cq = CQSave.cq_menu;
            string path = CQSave.AppDirectory + "Config.ini";
            ini = new IniConfig(path);
            ini.Load();
            var ls = cq.CQApi.GetGroupList();
            foreach (var item in ls)
            {
                dataGridView1.Rows.Add(item.Group.Id, item.Name.ToString());
            }
            Label_Status.Text = "就绪     |";
            Label_Text.Text = $"已载入{ls.Count}个群...       ";

            int count = Convert.ToInt32(ini.Object["群控"]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                listBox_Group.Items.Add(ini.Object["群控"][$"Item{i}"].GetValueOrDefault("0"));
            }
            label_Count.Text = $"计数:{listBox_Group.Items.Count}个";
        }

        private void button_AllExport_Click(object sender, EventArgs e)
        {
            listBox_Group.Items.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                listBox_Group.Items.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
            label_Count.Text = $"计数:{listBox_Group.Items.Count}个";
        }

        private void button_SelectExport_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                bool flag = true;
                foreach (var item in listBox_Group.Items)
                {
                    if (item.ToString() == dataGridView1.SelectedRows[i].Cells[0].Value.ToString())
                    {
                        Label_Error.Visible = true;
                        Label_Error.Text = $"错误:项目 {item.ToString()} 已存在";
                        Delay(2000);
                        Label_Error.Visible = false;
                        flag = false;
                        break;
                    }
                }
                if (flag) listBox_Group.Items.Add(dataGridView1.SelectedRows[i].Cells[0].Value.ToString());
            }
            label_Count.Text = $"计数:{listBox_Group.Items.Count}个";
        }
        [DllImport("kernel32.dll")]
        static extern uint GetTickCount();
        static void Delay(uint ms)//这就是不假死的延时命令，单位为毫秒
        {
            uint start = GetTickCount();
            while (GetTickCount() - start < ms)
            {
                Application.DoEvents();
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            listBox_Group.Items.Clear();
            label_Count.Text = $"计数:{listBox_Group.Items.Count}个";

        }

        private void listBox_Group_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox_Group.SelectedIndex < 0) return;
            if (e.KeyData == Keys.Delete)
            {
                listBox_Group.Items.Remove(listBox_Group.SelectedItem);
                listBox_Group.SelectedItem = 0;
            }
            label_Count.Text = $"计数:{listBox_Group.Items.Count}个";
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            long admin = 0;
            try
            {
                if (!string.IsNullOrEmpty(textBox_Admin.Text)) admin = Convert.ToInt64(textBox_Admin.Text);
            }
            catch
            {
                MessageBox.Show("数字输入格式有误");
                return;
            }
            string path = cq.CQApi.AppDirectory + "\\Config.ini";

            if (listBox_Group.Items.Count == 0)
            {
                if (MessageBox.Show("当前没有群在配置中，是否继续保存？", "提示", MessageBoxButtons.YesNo) == DialogResult.Cancel)
                {
                    return;
                }
            }

            ini.Object["群控"]["Count"]=new IValue(listBox_Group.Items.Count.ToString());
            for (int i = 0; i < listBox_Group.Items.Count; i++)
            {
                ini.Object["群控"][$"Item{i}"]=new IValue(listBox_Group.Items[i].ToString());
            }
            if (!string.IsNullOrEmpty(textBox_Admin.Text))
            {
                foreach (var item in listBox_Group.Items)
                {
                    string groupid = item.ToString();
                    if (ini.Object[groupid]["Count"].GetValueOrDefault("0") != "0")
                    {
                        if (checkBox_CoverSetting.Checked)
                        {
                            if (!string.IsNullOrEmpty(textBox_Admin.Text))
                            {
                                ini.Object[groupid]["Count"]=new IValue("1");
                                ini.Object[groupid][$"Index0"]=new IValue(textBox_Admin.Text);
                            }
                            else
                            {
                                ini.Object[groupid]["Count"]=new IValue("0");
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(textBox_Admin.Text))
                        {
                            ini.Object[groupid]["Count"]=new IValue("1");
                            ini.Object[groupid][$"Index0"]=new IValue(textBox_Admin.Text);
                        }
                        else
                        {
                            ini.Object[groupid]["Count"]=new IValue("0");
                        }
                    }
                }
            }
            ini.Save();

            MessageBox.Show("更改已保存");
        }
    }
}
