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
    public partial class Aboutme : Form
    {
        public Aboutme()
        {
            InitializeComponent();
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_Sponsor_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("如果你觉得这个插件挺好，而且想作者个奖励的话，看着给就好\n感谢感谢(〃'▽'〃)","感谢感谢",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                Sponsor fm = new Sponsor();
                fm.Show();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start( linkLabel2.Text);
        }
    }
}
