using Native.Csharp.Tool.IniConfig.Linq;
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
    public partial class AnswerDIY : Form
    {
        public AnswerDIY()
        {
            InitializeComponent();
        }

        private void button_Default_Click(object sender, EventArgs e)
        {
            textBox_OrderKC1.Text =  "#扩充单抽";
            textBox_OrderKC10.Text = "#扩充十连";
            textBox_OrderJZ1.Text = "#精准单抽";
            textBox_OrderJZ10.Text = "#精准十连";
            textBox_OrderRegiter.Text ="#抽卡注册";
            textBox_OrderSign.Text = "#打扫甲板";
            textBox_OrderSignReset.Text =  "#甲板积灰";
            textBox_OrderQueryDiamond.Text = "#我的水晶";
            textBox_OrderHelp.Text =  "#抽卡帮助";
            textBox_OrderGetPool.Text = "#获取池子";
            textBox_AnsMutiSign.Text = "今天的甲板挺亮的，擦一遍就行了";
            textBox_AnsKC1.Text = "少女祈祷中……";
            textBox_AnsKC10.Text = "少女祈祷中……";
            textBox_AnsJZ1.Text = "少女祈祷中……";
            textBox_AnsJZ10.Text = "少女祈祷中……";
            textBox_AnsRegister.Text =  "<@>欢迎上舰，这是你的初始资源(<#>)水晶";
            textBox_AnsMutiRegister.Text = "重复注册是不行的哦";
            textBox_AnsSign1.Text = "大姐你回来了，天气这么好一起多逛逛吧";
            textBox_AnsSign2.Text = "<@>这是你今天扫甲板的报酬，拿好(<#>水晶)";
            textBox_AnsQueryDiamond.Text = "<@>你手头还有<#>水晶";
            textBox_AnsNoReg.Text = "<@>不是清洁工吧？来输入#抽卡注册 来上舰";
            textBox_AnsLowDiamond.Text =  "<@>水晶不足，无法进行抽卡，你还剩余<#>水晶";

            textBox_AnsHelp.Text = @"水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n\n#精准单抽\n#扩充单抽\n#精准十连\n#扩充十连";

            textBox_SignReset1.Text = "贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍";
            textBox_SignReset2.Text = "草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍";
            textBox_SignReset3.Text = "你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍";
            textBox_SignReset4.Text ="由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍";
            textBox_SignReset5.Text = "理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍";
            textBox_SignReset6.Text = "希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍";

            textBox_SignMin.Text = "0";textBox_SignMax.Text = "14000";
            textBox_ResisterMin.Text = "0"; textBox_RegisterMax.Text = "14000";
        }

        private void AnswerDIY_Load(object sender, EventArgs e)
        {
            string path = $@"{CQSave.cq_menu.CQApi.AppDirectory}Config.ini";
            textBox_OrderKC1.Text = INIhelper.IniRead("Order", "KC1", "#扩充单抽", path);
            textBox_OrderKC10.Text = INIhelper.IniRead("Order", "KC10", "#扩充十连", path);
            textBox_OrderJZ1.Text = INIhelper.IniRead("Order", "JZ1", "#精准单抽", path);
            textBox_OrderJZ10.Text = INIhelper.IniRead("Order", "JZ10", "#精准十连", path);
            textBox_OrderRegiter.Text = INIhelper.IniRead("Order", "Register", "#抽卡注册", path);
            textBox_OrderSign.Text = INIhelper.IniRead("Order", "Sign", "#打扫甲板", path);
            textBox_OrderSignReset.Text = INIhelper.IniRead("Order", "SignReset", "#甲板积灰", path);
            textBox_OrderQueryDiamond.Text = INIhelper.IniRead("Order", "QueryDiamond", "#我的水晶", path);
            textBox_OrderHelp.Text = INIhelper.IniRead("Order", "Help", "#抽卡帮助", path);
            textBox_OrderGetPool.Text = INIhelper.IniRead("Order", "GetPool", "#获取池子", path);

            textBox_AnsKC1.Text = INIhelper.IniRead("Answer", "KC1", "少女祈祷中……", path);
            textBox_AnsKC10.Text = INIhelper.IniRead("Answer", "KC10", "少女祈祷中……", path);
            textBox_AnsJZ1.Text = INIhelper.IniRead("Answer", "JZ1", "少女祈祷中……", path);
            textBox_AnsJZ10.Text = INIhelper.IniRead("Answer", "JZ10", "少女祈祷中……", path);
            textBox_AnsRegister.Text = INIhelper.IniRead("Answer", "Register", "<@>欢迎上舰，这是你的初始资源(<#>)水晶", path);
            textBox_AnsMutiRegister.Text = INIhelper.IniRead("Answer", "MutiRegister", "重复注册是不行的哦", path);
            textBox_AnsSign1.Text = INIhelper.IniRead("Answer", "Sign1", "大姐你回来了，天气这么好一起多逛逛吧", path);
            textBox_AnsSign2.Text = INIhelper.IniRead("Answer", "Sign2", "<@>这是你今天清扫甲板的报酬，拿好(<#>水晶)", path);
            textBox_AnsMutiSign.Text = INIhelper.IniRead("Answer", "MutiSign", "今天的甲板挺亮的，擦一遍就行了", path);
            textBox_AnsQueryDiamond.Text = INIhelper.IniRead("Answer", "QueryDiamond", "<@>你手头还有<#>水晶", path);
            textBox_AnsNoReg.Text= INIhelper.IniRead("Answer", "NoReg", "<@>不是清洁工吧？来输入#抽卡注册 来上舰", path);
            textBox_AnsLowDiamond.Text = INIhelper.IniRead("Answer", "LowDiamond", "<@>水晶不足，无法进行抽卡，你还剩余<#>水晶", path);
            IniObject iObject = IniObject.Load(path, Encoding.Unicode);     // 从指定的文件中读取 Ini 配置项, 参数1: 文件路径, 参数2: 编码格式 [默认: ANSI]
            try
            {
                IniValue value1 = iObject["Answer"]["Help"];
                textBox_AnsHelp.Text = value1.ToString();
            }
            catch
            {
                textBox_AnsHelp.Text = "";
            }
            if (textBox_AnsHelp.Text == "")
            {
                textBox_AnsHelp.Text = @"水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n#获取池子\n\n#精准单抽\n#扩充单抽\n#精准十连\n#扩充十连";
            }
            textBox_SignReset1.Text = INIhelper.IniRead("Answer", "Reset1", "贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍", path);
            textBox_SignReset2.Text = INIhelper.IniRead("Answer", "Reset2", "草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍", path);
            textBox_SignReset3.Text = INIhelper.IniRead("Answer", "Reset3", "你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍", path);
            textBox_SignReset4.Text = INIhelper.IniRead("Answer", "Reset4", "由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍", path);
            textBox_SignReset5.Text = INIhelper.IniRead("Answer", "Reset5", "理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍", path);
            textBox_SignReset6.Text = INIhelper.IniRead("Answer", "Reset6", "希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍", path);

            textBox_ResisterMin.Text = INIhelper.IniRead("GetDiamond", "RegisterMin", "0", path);
            textBox_RegisterMax.Text = INIhelper.IniRead("GetDiamond", "RegisterMax", "14000", path);
            textBox_SignMin.Text = INIhelper.IniRead("GetDiamond", "SignMin", "0", path);
            textBox_SignMax.Text = INIhelper.IniRead("GetDiamond", "SignMax", "14000", path);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            string path = $@"{CQSave.cq_menu.CQApi.AppDirectory}Config.ini";
            INIhelper.IniWrite("Order", "KC1", textBox_OrderKC1.Text, path);
            INIhelper.IniWrite("Order", "KC10", textBox_OrderKC10.Text, path);
            INIhelper.IniWrite("Order", "JZ1", textBox_OrderJZ1.Text, path);
            INIhelper.IniWrite("Order", "JZ10", textBox_OrderJZ10.Text, path);
            INIhelper.IniWrite("Order", "Register", textBox_OrderRegiter.Text, path);
            INIhelper.IniWrite("Order", "Sign", textBox_OrderSign.Text, path);
            INIhelper.IniWrite("Order", "SignReset", textBox_OrderSignReset.Text, path);
            INIhelper.IniWrite("Order", "QueryDiamond", textBox_OrderQueryDiamond.Text, path);
            INIhelper.IniWrite("Order", "Help", textBox_OrderHelp.Text, path);
            INIhelper.IniWrite("Order", "GetPool", textBox_OrderGetPool.Text, path);

            INIhelper.IniWrite("Answer", "KC1", textBox_AnsKC1.Text, path);
            INIhelper.IniWrite("Answer", "KC10", textBox_AnsKC10.Text, path);
            INIhelper.IniWrite("Answer", "JZ1", textBox_AnsJZ1.Text, path);
            INIhelper.IniWrite("Answer", "JZ10", textBox_AnsJZ10.Text, path);
            INIhelper.IniWrite("Answer", "Register", textBox_AnsRegister.Text, path);
            INIhelper.IniWrite("Answer", "MutiRegister",textBox_AnsMutiRegister.Text, path);
            INIhelper.IniWrite("Answer", "Sign1", textBox_AnsSign1.Text, path);
            INIhelper.IniWrite("Answer", "Sign2", textBox_AnsSign2.Text, path);
            INIhelper.IniWrite("Answer", "MutiSign", textBox_AnsMutiSign.Text, path);
            INIhelper.IniWrite("Answer", "QueryDiamond", textBox_AnsQueryDiamond.Text, path);
            INIhelper.IniWrite("Answer", "NoReg", textBox_AnsNoReg.Text, path);
            INIhelper.IniWrite("Answer", "LowDiamond", textBox_AnsLowDiamond.Text, path);

            INIhelper.IniWrite("Answer", "Reset1", textBox_SignReset1.Text, path);
            INIhelper.IniWrite("Answer", "Reset2", textBox_SignReset2.Text, path);
            INIhelper.IniWrite("Answer", "Reset3", textBox_SignReset3.Text, path);
            INIhelper.IniWrite("Answer", "Reset4", textBox_SignReset4.Text, path);
            INIhelper.IniWrite("Answer", "Reset5", textBox_SignReset5.Text, path);
            INIhelper.IniWrite("Answer", "Reset6", textBox_SignReset6.Text, path);

            INIhelper.IniWrite("Answer", "Help", @textBox_AnsHelp.Text, path);

            INIhelper.IniWrite("GetDiamond", "RegisterMin", textBox_ResisterMin.Text, path);
            INIhelper.IniWrite("GetDiamond", "RegisterMax", textBox_RegisterMax.Text, path);
            INIhelper.IniWrite("GetDiamond", "SignMin", textBox_SignMin.Text, path);
            INIhelper.IniWrite("GetDiamond", "SignMax", textBox_SignMax.Text, path);

            MessageBox.Show("更改已保存");
        }
    }
}
