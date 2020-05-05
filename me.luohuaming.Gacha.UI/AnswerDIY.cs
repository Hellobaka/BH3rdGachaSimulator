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
    public partial class AnswerDIY : Form
    {
        public AnswerDIY()
        {
            InitializeComponent();
        }
        static IniConfig ini;
        private void button_Default_Click(object sender, EventArgs e)
        {
            textBox_OrderKC1.Text =  "#扩充单抽";
            textBox_OrderKC10.Text = "#扩充十连";
            textBox_OrderJZA1.Text = "#精准单抽A";
            textBox_OrderJZA10.Text = "#精准十连A";
            textBox_OrderJZB1.Text = "#精准单抽B";
            textBox_OrderJZB10.Text = "#精准十连B";
            textBox_OrderBP1.Text = "#标配单抽";
            textBox_OrderBP10.Text = "#标配十连";

            textBox_OrderRegiter.Text ="#抽卡注册";
            textBox_OrderSign.Text = "#打扫甲板";
            textBox_OrderSignReset.Text =  "#甲板积灰";
            textBox_OrderQueryDiamond.Text = "#我的水晶";
            textBox_OrderHelp.Text =  "#抽卡帮助";
            textBox_OrderGetPool.Text = "#获取池子";
            textBox_OrderCloseGacha.Text = "#抽卡关闭";
            textBox_OrderOpenGacha.Text = "#抽卡开启";


            textBox_AnsMutiSign.Text = "今天的甲板挺亮的，擦一遍就行了";
            textBox_AnsKC1.Text = "少女祈祷中……";
            textBox_AnsKC10.Text = "少女祈祷中……";
            textBox_AnsJZA1.Text = "少女祈祷中……";
            textBox_AnsJZA10.Text = "少女祈祷中……";
            textBox_AnsJZB1.Text = "少女祈祷中……";
            textBox_AnsJZB10.Text = "少女祈祷中……";
            textBox_AnsBP1.Text = "少女祈祷中……";
            textBox_AnsBP10.Text = "少女祈祷中……";

            textBox_AnsRegister.Text =  "<@>欢迎上舰，这是你的初始资源(<#>水晶)";
            textBox_AnsMutiRegister.Text = "重复注册是不行的哦";
            textBox_AnsSign1.Text = "大姐你回来了，天气这么好一起多逛逛吧";
            textBox_AnsSign2.Text = "<@>这是你今天扫甲板的报酬，拿好(<#>水晶)";
            textBox_AnsQueryDiamond.Text = "<@>你手头还有<#>水晶";
            textBox_AnsNoReg.Text = "<@>不是清洁工吧？来输入#抽卡注册 来上舰";
            textBox_AnsLowDiamond.Text =  "<@>水晶不足，无法进行抽卡，你还剩余<#>水晶";

            textBox_AnsHelp.Text = @"水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n#氪金 目标账号或者at 数量(管理员限定 暂不支持自定义修改)\n#获取池子\n\n#精准单抽(A/B)大小写随意\n#扩充单抽\n#精准十连(A/B)大小写随意\n#扩充十连\n#标配单抽\n#标配十连\n#抽卡开启(在后台群后面可接群号)\n#抽卡关闭(在后台群后面可接群号)\n#置抽卡管理(示例:#置抽卡管理,群号,QQ或者at)\n#更换池子 查询公告的关键字\n#抽干家底 扩充或者精准A/B";

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
            string path = $@"{CQSave.AppDirectory}Config.ini";
            ini = new IniConfig(path);
            ini.Load();
            textBox_OrderKC1.Text = ini.Object["Order"]["KC1"].GetValueOrDefault("#扩充单抽");
            textBox_OrderKC10.Text = ini.Object["Order"]["KC10"].GetValueOrDefault("#扩充十连");
            textBox_OrderJZA1.Text = ini.Object["Order"]["JZA1"].GetValueOrDefault("#精准单抽A");
            textBox_OrderJZA10.Text = ini.Object["Order"]["JZA10"].GetValueOrDefault("#精准十连A");
            textBox_OrderJZB1.Text = ini.Object["Order"]["JZB1"].GetValueOrDefault("#精准单抽B");
            textBox_OrderJZB10.Text = ini.Object["Order"]["JZB10"].GetValueOrDefault("#精准十连B");
            textBox_OrderBP1.Text = ini.Object["Order"]["BP1"].GetValueOrDefault("#标配单抽");
            textBox_OrderBP10.Text = ini.Object["Order"]["BP10"].GetValueOrDefault("#标配十连");

            textBox_OrderCloseGacha.Text = ini.Object["Order"]["CloseGacha"].GetValueOrDefault("#抽卡关闭");
            textBox_OrderOpenGacha.Text = ini.Object["Order"]["OpenGacha"].GetValueOrDefault("#抽卡开启");
            textBox_OrderRegiter.Text = ini.Object["Order"]["Register"].GetValueOrDefault("#抽卡注册");
            textBox_OrderSign.Text = ini.Object["Order"]["Sign"].GetValueOrDefault("#打扫甲板");
            textBox_OrderSignReset.Text = ini.Object["Order"]["SignReset"].GetValueOrDefault("#甲板积灰");
            textBox_OrderQueryDiamond.Text = ini.Object["Order"]["QueryDiamond"].GetValueOrDefault("#我的水晶");
            textBox_OrderHelp.Text = ini.Object["Order"]["Help"].GetValueOrDefault("#抽卡帮助");
            textBox_OrderGetPool.Text = ini.Object["Order"]["GetPool"].GetValueOrDefault("#获取池子");

            textBox_AnsKC1.Text = ini.Object["Answer"]["KC1"].GetValueOrDefault("少女祈祷中……");
            textBox_AnsKC10.Text = ini.Object["Answer"]["KC10"].GetValueOrDefault("少女祈祷中……");
            textBox_AnsJZA1.Text = ini.Object["Answer"]["JZA1"].GetValueOrDefault("少女祈祷中……");
            textBox_AnsJZA10.Text = ini.Object["Answer"]["JZA10"].GetValueOrDefault("少女祈祷中……");
            textBox_AnsJZB1.Text = ini.Object["Answer"]["JZB1"].GetValueOrDefault("少女祈祷中……");
            textBox_AnsJZB10.Text = ini.Object["Answer"]["JZB10"].GetValueOrDefault("少女祈祷中……");
            textBox_AnsBP1.Text = ini.Object["Answer"]["BP1"].GetValueOrDefault("少女祈祷中……");
            textBox_AnsBP10.Text = ini.Object["Answer"]["BP10"].GetValueOrDefault("少女祈祷中……");

            textBox_AnsRegister.Text = ini.Object["Answer"]["Register"].GetValueOrDefault("<@>欢迎上舰，这是你的初始资源(<#>水晶)");
            textBox_AnsMutiRegister.Text = ini.Object["Answer"]["MutiRegister"].GetValueOrDefault("重复注册是不行的哦");
            textBox_AnsSign1.Text = ini.Object["Answer"]["Sign1"].GetValueOrDefault("大姐你回来了，天气这么好一起多逛逛吧");
            textBox_AnsSign2.Text = ini.Object["Answer"]["Sign2"].GetValueOrDefault("<@>这是你今天清扫甲板的报酬，拿好(<#>水晶)");
            textBox_AnsMutiSign.Text = ini.Object["Answer"]["MutiSign"].GetValueOrDefault("今天的甲板挺亮的，擦一遍就行了");
            textBox_AnsQueryDiamond.Text = ini.Object["Answer"]["QueryDiamond"].GetValueOrDefault("<@>你手头还有<#>水晶");
            textBox_AnsNoReg.Text= ini.Object["Answer"]["NoReg"].GetValueOrDefault("<@>不是清洁工吧？来输入#抽卡注册 来上舰");
            textBox_AnsLowDiamond.Text = ini.Object["Answer"]["LowDiamond"].GetValueOrDefault("<@>水晶不足，无法进行抽卡，你还剩余<#>水晶");
            textBox_AnsHelp.Text = ini.Object["Answer"]["Help"].GetValueOrDefault(@"水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n#氪金 目标账号或者at 数量(管理员限定 暂不支持自定义修改)\n#获取池子\n\n#精准单抽(A/B)大小写随意\n#扩充单抽\n#精准十连(A/B)大小写随意\n#扩充十连\n#标配单抽\n#标配十连\n#抽卡开启(在后台群后面可接群号)\n#抽卡关闭(在后台群后面可接群号)\n#置抽卡管理(示例:#置抽卡管理,群号,QQ或者at)\n#更换池子 查询公告的关键字\n#抽干家底 扩充或者精准A/B")
                .Replace("\\", @"\");

            textBox_SignReset1.Text = ini.Object["Answer"]["Reset1"].GetValueOrDefault("贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍");
            textBox_SignReset2.Text = ini.Object["Answer"]["Reset2"].GetValueOrDefault("草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍");
            textBox_SignReset3.Text = ini.Object["Answer"]["Reset3"].GetValueOrDefault("你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍");
            textBox_SignReset4.Text = ini.Object["Answer"]["Reset4"].GetValueOrDefault("由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍");
            textBox_SignReset5.Text = ini.Object["Answer"]["Reset5"].GetValueOrDefault("理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍");
            textBox_SignReset6.Text = ini.Object["Answer"]["Reset6"].GetValueOrDefault("希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍");

            textBox_ResisterMin.Text = ini.Object["GetDiamond"]["RegisterMin"].GetValueOrDefault("0");
            textBox_RegisterMax.Text = ini.Object["GetDiamond"]["RegisterMax"].GetValueOrDefault("14000");
            textBox_SignMin.Text = ini.Object["GetDiamond"]["SignMin"].GetValueOrDefault("0");
            textBox_SignMax.Text = ini.Object["GetDiamond"]["SignMax"].GetValueOrDefault("14000");
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            string path = $@"{CQSave.cq_menu.CQApi.AppDirectory}Config.ini";
            ini.Object["Order"]["KC1"]=new IValue(textBox_OrderKC1.Text);
            ini.Object["Order"]["KC10"]=new IValue(textBox_OrderKC10.Text);
            ini.Object["Order"]["JZA1"]=new IValue(textBox_OrderJZA1.Text);
            ini.Object["Order"]["JZA10"]=new IValue(textBox_OrderJZA10.Text);
            ini.Object["Order"]["JZB1"]=new IValue(textBox_OrderJZB1.Text);
            ini.Object["Order"]["JZB10"]=new IValue(textBox_OrderJZB10.Text);
            ini.Object["Order"]["BP1"]=new IValue(textBox_OrderBP1.Text);
            ini.Object["Order"]["BP10"]=new IValue(textBox_OrderBP10.Text);

            ini.Object["Order"]["Register"]=new IValue(textBox_OrderRegiter.Text);
            ini.Object["Order"]["Sign"]=new IValue(textBox_OrderSign.Text);
            ini.Object["Order"]["SignReset"]=new IValue(textBox_OrderSignReset.Text);
            ini.Object["Order"]["QueryDiamond"]=new IValue(textBox_OrderQueryDiamond.Text);
            ini.Object["Order"]["Help"]=new IValue(textBox_OrderHelp.Text);
            ini.Object["Order"]["GetPool"]=new IValue(textBox_OrderGetPool.Text);
            ini.Object["Order"]["OpenGacha"]=new IValue(textBox_OrderOpenGacha.Text);
            ini.Object["Order"]["CloseGacha"]=new IValue(textBox_OrderCloseGacha.Text);

            ini.Object["Answer"]["KC1"]=new IValue(textBox_AnsKC1.Text);
            ini.Object["Answer"]["KC10"]=new IValue(textBox_AnsKC10.Text);
            ini.Object["Answer"]["JZA1"]=new IValue(textBox_AnsJZA1.Text);
            ini.Object["Answer"]["JZA10"]=new IValue(textBox_AnsJZA10.Text);
            ini.Object["Answer"]["JZB1"]=new IValue(textBox_AnsJZB1.Text);
            ini.Object["Answer"]["JZB10"]=new IValue(textBox_AnsJZB10.Text);
            ini.Object["Answer"]["BP1"]=new IValue(textBox_AnsBP1.Text);
            ini.Object["Answer"]["BP10"]=new IValue(textBox_AnsBP10.Text);

            ini.Object["Answer"]["Register"]=new IValue(textBox_AnsRegister.Text);
            ini.Object["Answer"]["MutiRegister"]=new IValue(textBox_AnsMutiRegister.Text);
            ini.Object["Answer"]["Sign1"]=new IValue(textBox_AnsSign1.Text);
            ini.Object["Answer"]["Sign2"]=new IValue(textBox_AnsSign2.Text);
            ini.Object["Answer"]["MutiSign"]=new IValue(textBox_AnsMutiSign.Text);
            ini.Object["Answer"]["QueryDiamond"]=new IValue(textBox_AnsQueryDiamond.Text);
            ini.Object["Answer"]["NoReg"]=new IValue(textBox_AnsNoReg.Text);
            ini.Object["Answer"]["LowDiamond"]=new IValue(textBox_AnsLowDiamond.Text);

            ini.Object["Answer"]["Reset1"]=new IValue(textBox_SignReset1.Text);
            ini.Object["Answer"]["Reset2"]=new IValue(textBox_SignReset2.Text);
            ini.Object["Answer"]["Reset3"]=new IValue(textBox_SignReset3.Text);
            ini.Object["Answer"]["Reset4"]=new IValue(textBox_SignReset4.Text);
            ini.Object["Answer"]["Reset5"]=new IValue(textBox_SignReset5.Text);
            ini.Object["Answer"]["Reset6"]=new IValue(textBox_SignReset6.Text);

            ini.Object["Answer"]["Help"]=new IValue(@textBox_AnsHelp.Text);

            ini.Object["GetDiamond"]["RegisterMin"]=new IValue(textBox_ResisterMin.Text);
            ini.Object["GetDiamond"]["RegisterMax"]=new IValue(textBox_RegisterMax.Text);
            ini.Object["GetDiamond"]["SignMin"]=new IValue(textBox_SignMin.Text);
            ini.Object["GetDiamond"]["SignMax"]=new IValue(textBox_SignMax.Text);

            ini.Save();
            MessageBox.Show("更改已保存");
        }
    }
}
