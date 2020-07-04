using Native.Tool.IniConfig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace me.luohuaming.Gacha.UI
{
    public partial class AbyssHelper : Form
    {
        public AbyssHelper()
        {
            InitializeComponent();
        }

        private readonly List<long> grouplist = new List<long>();
        private List<AbyssTimer> abyssTimers = new List<AbyssTimer>();

        private void AbyssHelper_Load(object sender, EventArgs e)
        {
            if (CQSave.CQApi != null)
            {
                foreach (var item in CQSave.CQApi.GetGroupList())
                {
                    grouplist.Add(item.Group.Id);
                    checkedListBox_Group.Items.Add($"{item.Name}({item.Group.Id})");
                }
            }
            else
            {
                Random rd = new Random();
                for (int i = 0; i < 10; i++)
                {
                    long groupid = rd.Next();
                    grouplist.Add(groupid);
                    checkedListBox_Group.Items.Add($"名称{i + 1}({groupid})");
                }
            }
            IniConfig ini = new IniConfig(CQSave.AppDirectory + "Config.ini"); ini.Load();
            textBox_timerInterval.Text = ini.Object["ExtraConfig"]["TimerInterval"].GetValueOrDefault("20");
            if (File.Exists(CQSave.AppDirectory + "AbyssHelper.json"))
                abyssTimers = JsonConvert.DeserializeObject<List<AbyssTimer>>(File.ReadAllText(CQSave.AppDirectory + "AbyssHelper.json"));

            foreach (var item in abyssTimers)
            {
                string grouptext = string.Empty;
                int count = 0;
                foreach (var group in item.GroupList)
                {
                    count++;
                    grouptext += group + (count == item.GroupList.Count ? "" : ",");
                    if (grouplist.IndexOf(group) != -1)
                        checkedListBox_Group.SetItemChecked(grouplist.IndexOf(group), true);
                }
                dataGridView_Details.Rows.Add(item.Enabled, comboBox_Week.Items[item.DayofWeek].ToString()
                    , $"{item.Hour}:{item.Minute}", item.RemindText, grouptext);
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            string grouptext = string.Empty;
            List<long> groups = new List<long>();
            int count = 0;
            foreach (var item in checkedListBox_Group.CheckedItems)
            {
                count++;
                groups.Add(grouplist[checkedListBox_Group.Items.IndexOf(item)]);
                grouptext += grouplist[checkedListBox_Group.Items.IndexOf(item)] + (count == checkedListBox_Group.CheckedItems.Count ? "" : ",");
            }
            abyssTimers.Add(new AbyssTimer(true,
               groups, richTextBox_RemindText.Text, comboBox_Week.SelectedIndex,
                Convert.ToInt32(textBox_Hours.Text), Convert.ToInt32(textBox_Minutes.Text)));
            dataGridView_Details.Rows.Add(true, comboBox_Week.SelectedItem.ToString(), textBox_Hours.Text + ":" + textBox_Minutes.Text, richTextBox_RemindText.Text, grouptext);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            File.WriteAllText(CQSave.AppDirectory + "AbyssHelper.json", JsonConvert.SerializeObject(abyssTimers));
            AbyssTimerHelper.Start();
            IniConfig ini = new IniConfig(CQSave.AppDirectory + "Config.ini"); ini.Load();
            ini.Object["ExtraConfig"]["TimerInterval"] = textBox_timerInterval.Text;
            ini.Save();
            MessageBox.Show("保存成功");
        }

        private void dataGridView_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex>=0)
            {
                dataGridView_Details.ReadOnly = false;
                (dataGridView_Details.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell ).Value=!Convert.ToBoolean(dataGridView_Details.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue);
                abyssTimers[e.RowIndex].Enabled = Convert.ToBoolean((dataGridView_Details.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell).Value);
                dataGridView_Details.ReadOnly = true;
            }
        }

        private void dataGridView_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0) return;
            textBox_Hours.Text = abyssTimers[index].Hour.ToString();
            textBox_Minutes.Text = abyssTimers[index].Minute.ToString();
            comboBox_Week.SelectedIndex = abyssTimers[index].DayofWeek;
            richTextBox_RemindText.Text = abyssTimers[index].RemindText.ToString();
            foreach (int item in checkedListBox_Group.CheckedIndices)
            {
                checkedListBox_Group.SetItemChecked(item, false);
            }
            foreach (var item in abyssTimers[index].GroupList)
            {
                if (grouplist.IndexOf(item) == -1) break;
                checkedListBox_Group.SetItemChecked(grouplist.IndexOf(item), true);
            }
        }

        private void button_AllChecked_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox_Group.Items.Count; i++)
            {
                checkedListBox_Group.SetItemChecked(i, true);
            }
        }

        private void button_AntiChecked_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox_Group.Items.Count; i++)
            {
                checkedListBox_Group.SetItemChecked(i, checkedListBox_Group.CheckedItems.IndexOf(checkedListBox_Group.Items[i]) == -1);
            }
        }

        private void button_NonChecked_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox_Group.Items.Count; i++)
            {
                checkedListBox_Group.SetItemChecked(i, false);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (dataGridView_Details.SelectedRows.Count == 0) { MessageBox.Show("右侧表格未选中任何数据"); return; }
            dataGridView_Details.SelectedRows[0].Cells[1].Value = comboBox_Week.SelectedItem.ToString();
            abyssTimers[dataGridView_Details.SelectedRows[0].Index].DayofWeek = comboBox_Week.SelectedIndex;
            dataGridView_Details.SelectedRows[0].Cells[2].Value = $"{textBox_Hours.Text}:{textBox_Minutes.Text}";
            abyssTimers[dataGridView_Details.SelectedRows[0].Index].Hour = Convert.ToInt32(textBox_Hours.Text);
            abyssTimers[dataGridView_Details.SelectedRows[0].Index].Minute = Convert.ToInt32(textBox_Minutes.Text);
            dataGridView_Details.SelectedRows[0].Cells[3].Value = richTextBox_RemindText.Text;
            abyssTimers[dataGridView_Details.SelectedRows[0].Index].RemindText = richTextBox_RemindText.Text;
            string grouptext = string.Empty;
            List<long> group = new List<long>();
            int count = 0;
            foreach (var item in checkedListBox_Group.CheckedItems)
            {
                count++;
                group.Add(grouplist[checkedListBox_Group.Items.IndexOf(item)]);
                grouptext += grouplist[checkedListBox_Group.Items.IndexOf(item)] + (count == checkedListBox_Group.CheckedItems.Count ? "" : ",");
            }
            dataGridView_Details.SelectedRows[0].Cells[4].Value = grouptext;
            abyssTimers[dataGridView_Details.SelectedRows[0].Index].GroupList = group;
        }
    }
}
