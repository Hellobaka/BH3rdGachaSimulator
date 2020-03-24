using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using me.luohuaming.Gacha.UI;
using System.Data.SQLite;
using System.Data;
using System.IO;
using Native.Tool.IniConfig.Linq;

namespace me.luohuaming.Gacha.Code
{
    public class Event_PrivateMessage : IPrivateMessage
    {
        static CQPrivateMessageEventArgs cq;
        #region 字段
        string order_KC1;
        string order_KC10;
        string order_JZA1;
        string order_JZA10;
        string order_JZB1;
        string order_JZB10;
        string order_BP1;
        string order_BP10;

        string order_sign;
        string order_signreset;
        string order_querydiamond;
        string order_help;
        string order_getpool;
        string order_register;

        string KC1;
        string KC10;
        string JZA1;
        string JZA10;
        string JZB1;
        string JZB10;
        string BP1;
        string BP10;
        string sign1;
        string sign2;
        string mutiSign;
        string noReg;
        string lowDiamond;
        string queryDiamond;
        string help;
        string register;
        string mutiRegister;

        string reset1;
        string reset2;
        string reset3;
        string reset4;
        string reset5;
        string reset6;

        int signmin;
        int signmax;
        int registermin;
        int registermax;
        #endregion
        int diamond;

        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            cq = e;
            CQSave.cq_private = e;
            if (INIhelper.IniRead("接口", "Private", "0", $"{e.CQApi.AppDirectory}Config.ini") == "0") return;
            bool exist = IDExist(e.FromQQ.Id);
            ReadConfig();
            UI.Gacha gc = new UI.Gacha();
            long controlgroup = Convert.ToInt64(INIhelper.IniRead("后台群", "Id", "0", e.CQApi.AppDirectory + "\\Config.ini"));
            string str = "";
            if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_KC1)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, lowDiamond.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, KC1);
                gc.Read_Kuochong();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.KC_Gacha(),
                    gc.KC_GachaSub()
                };
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                string path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
                if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "1")
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, TextGacha(ls));
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 0, 0, 1, diamond - 280)}]");
                }
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个扩充单抽";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_KC10)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, lowDiamond.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, KC10.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                gc.Read_Kuochong();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>();
                for (int i = 0; i < 10; i++)
                {
                    ls.Add(gc.KC_Gacha());
                    ls.Add(gc.KC_GachaSub());
                }
                ls = ls.OrderByDescending(x => x.value).ToList();
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = i + 1; j < ls.Count; j++)
                    {
                        if (ls[i].name == ls[j].name && ls[i].type != UI.Gacha.TypeS.Chararcter.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                string path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
                if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "1")
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, TextGacha(ls));
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 0, 0, 10, diamond - 2800)}]");
                }
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个扩充十连";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#").ToUpper() == order_JZA1)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, lowDiamond.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, JZA1.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(1);
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.JZ_GachaMain(),
                    gc.JZ_GachaMaterial()
                };
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "1")
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, TextGacha(ls));
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 1, 1, 1, diamond - 280)}]");
                }
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个精准单抽";

            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#").ToUpper() == order_JZA10)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, lowDiamond.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, JZA10.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(1);
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>();
                for (int i = 0; i < 10; i++)
                {
                    ls.Add(gc.JZ_GachaMain());
                    ls.Add(gc.JZ_GachaMaterial());
                }
                ls = ls.OrderByDescending(x => x.value).ToList();
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = i + 1; j < ls.Count; j++)
                    {
                        if (ls[i].name == ls[j].name && ls[i].type != UI.Gacha.TypeS.Stigmata.ToString() && ls[i].type != UI.Gacha.TypeS.Weapon.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "1")
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, TextGacha(ls));
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 1, 1, 10, diamond - 2800)}]");
                }
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个精准十连";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#").ToUpper() == order_JZB1)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, lowDiamond.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, JZB1.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(2);
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.JZ_GachaMain(),
                    gc.JZ_GachaMaterial()
                };
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "1")
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, TextGacha(ls));
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 1, 2, 1, diamond - 280)}]");
                }
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个精准单抽";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#").ToUpper() == order_JZB10)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, lowDiamond.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, JZB10.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(2);
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>();
                for (int i = 0; i < 10; i++)
                {
                    ls.Add(gc.JZ_GachaMain());
                    ls.Add(gc.JZ_GachaMaterial());
                }
                ls = ls.OrderByDescending(x => x.value).ToList();
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = i + 1; j < ls.Count; j++)
                    {
                        if (ls[i].name == ls[j].name && ls[i].type != UI.Gacha.TypeS.Stigmata.ToString() && ls[i].type != UI.Gacha.TypeS.Weapon.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "1")
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, TextGacha(ls));
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 1, 2, 10, diamond - 2800)}]");
                }
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个精准十连";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_BP10)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, lowDiamond.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, BP10.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                gc.Read_BP();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>();
                for (int i = 0; i < 10; i++)
                {
                    ls.Add(gc.BP_GachaMain());
                    ls.Add(gc.BP_GachaSub());
                }
                ls = ls.OrderByDescending(x => x.value).ToList();
                for (int i = 0; i < ls.Count; i++)
                {
                    for (int j = i + 1; j < ls.Count; j++)
                    {
                        if (ls[i].name == ls[j].name && ls[i].type != UI.Gacha.TypeS.Chararcter.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                string path = $@"{cq.CQApi.AppDirectory}\概率\标配概率.txt";
                if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "1")
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, TextGacha(ls));
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 2, 0, 10, diamond - 2800)}]");
                }
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个标配十连";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_BP1)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, lowDiamond.Replace("<@>", $"").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, BP1);
                gc.Read_BP();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.BP_GachaMain(),
                    gc.BP_GachaSub()
                };
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                string path = $@"{cq.CQApi.AppDirectory}\概率\标配概率.txt";
                if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "1")
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, TextGacha(ls));
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 2, 0, 1, diamond - 280)}]");
                }
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个标配单抽";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_sign)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>","").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = Sign(e.FromQQ.Id);
                if (diamond >= 0)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, sign1.Replace("<@>","").Replace("<#>", diamond.ToString()));
                    e.CQApi.SendPrivateMessage(e.FromQQ, sign2.Replace("<@>","").Replace("<#>", diamond.ToString()));
                    str = $"QQ:{e.FromQQ.Id} 进行了签到";
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, mutiSign.Replace("<@>","").Replace("<#>", diamond.ToString()));
                    return;
                }
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_register)
            {
                e.Handler = true;
                if (!exist)
                {
                    Register(e.FromQQ.Id);
                    Random rd = new Random();
                    diamond = GetDiamond(e.FromQQ.Id);
                    e.CQApi.SendPrivateMessage(e.FromQQ, register.Replace("<@>","").Replace("<#>", diamond.ToString()));
                    str = $"QQ:{e.FromQQ.Id} 注册了抽卡";
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, mutiRegister.Replace("<@>","").Replace("<#>", diamond.ToString()));
                    return;
                }
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_querydiamond)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, noReg.Replace("<@>","").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                e.CQApi.SendPrivateMessage(e.FromQQ, queryDiamond.Replace("<@>","").Replace("<#>", diamond.ToString()));
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_help)
            {
                e.Handler = true;
                str = help;
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_getpool)
            {
                e.Handler = true;
                string UPS, UPA, UPWeapon, UPStigmata;
                UPS = INIhelper.IniRead("详情", "UpS", "S角色", e.CQApi.AppDirectory + "\\概率\\扩充概率.txt");
                UPA = INIhelper.IniRead("详情", "UpA", "A角色", e.CQApi.AppDirectory + "\\概率\\扩充概率.txt");
                UPWeapon = INIhelper.IniRead("详情", "UpWeapon", "四星武器", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                UPStigmata = INIhelper.IniRead("详情", "UpStigmata", "四星圣痕", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                e.CQApi.SendPrivateMessage(e.FromQQ, $"当前扩充池为 {UPS} {UPA}\n当前精准池为 {UPWeapon} {UPStigmata}");
                return;
            }
            else
            {
                return;
            }
            if (controlgroup == 0) return;
            e.CQApi.SendGroupMessage(controlgroup, str);
        }

        void ReadConfig()
        {
            string path = $@"{cq.CQApi.AppDirectory}Config.ini";
            order_KC1 = INIhelper.IniRead("Order", "KC1", "#扩充单抽", path);
            order_KC10 = INIhelper.IniRead("Order", "KC10", "#扩充十连", path);
            order_JZA1 = INIhelper.IniRead("Order", "JZA1", "#精准单抽A", path);
            order_JZA10 = INIhelper.IniRead("Order", "JZA10", "#精准十连A", path);
            order_JZB1 = INIhelper.IniRead("Order", "JZB1", "#精准单抽B", path);
            order_JZB10 = INIhelper.IniRead("Order", "JZB10", "#精准十连B", path);
            order_BP1 = INIhelper.IniRead("Order", "BP1", "#标配单抽", path);
            order_BP10 = INIhelper.IniRead("Order", "BP10", "#标配十连", path);

            order_register = INIhelper.IniRead("Order", "Register", "#抽卡注册", path);
            order_sign = INIhelper.IniRead("Order", "Sign", "#打扫甲板", path);
            order_signreset = INIhelper.IniRead("Order", "SignReset", "#甲板积灰", path);
            order_querydiamond = INIhelper.IniRead("Order", "QueryDiamond", "#我的水晶", path);
            order_help = INIhelper.IniRead("Order", "Help", "#抽卡帮助", path);
            order_getpool = INIhelper.IniRead("Order", "GetPool", "#获取池子", path);

            KC1 = INIhelper.IniRead("Answer", "KC1", "少女祈祷中……", path);
            KC10 = INIhelper.IniRead("Answer", "KC10", "少女祈祷中……", path);
            JZA1 = INIhelper.IniRead("Answer", "JZA1", "少女祈祷中……", path);
            JZA10 = INIhelper.IniRead("Answer", "JZA10", "少女祈祷中……", path);
            JZB1 = INIhelper.IniRead("Answer", "JZB1", "少女祈祷中……", path);
            JZB10 = INIhelper.IniRead("Answer", "JZB10", "少女祈祷中……", path);
            BP1 = INIhelper.IniRead("Answer", "BP1", "少女祈祷中……", path);
            BP10 = INIhelper.IniRead("Answer", "BP10", "少女祈祷中……", path);

            register = INIhelper.IniRead("Answer", "Register", "<@>欢迎上舰，这是你的初始资源(<#>)水晶", path);
            mutiRegister = INIhelper.IniRead("Answer", "MutiRegister", "重复注册是不行的哦", path);
            sign1 = INIhelper.IniRead("Answer", "Sign1", "大姐你回来了，天气这么好一起多逛逛吧", path);
            sign2 = INIhelper.IniRead("Answer", "Sign2", "<@>这是你今天清扫甲板的报酬，拿好(<#>水晶)", path);
            mutiSign = INIhelper.IniRead("Answer", "MutiSign", "今天的甲板挺亮的，擦一遍就行了", path);
            noReg = INIhelper.IniRead("Answer", "NoReg", "<@>不是清洁工吧？来输入#抽卡注册 来上舰", path);
            lowDiamond = INIhelper.IniRead("Answer", "LowDiamond", "<@>水晶不足，无法进行抽卡，你还剩余<#>水晶", path);
            queryDiamond = INIhelper.IniRead("Answer", "QueryDiamond", "<@>你手头还有<#>水晶", path);
            IniObject iObject = IniObject.Load(path, Encoding.Default);     // 从指定的文件中读取 Ini 配置项, 参数1: 文件路径, 参数2: 编码格式 [默认: ANSI]
            try
            {
                IniValue value1 = iObject["Answer"]["Help"];
                help = value1.ToString().Replace("\\", @"\");
            }
            catch
            {
                help = "";
            }
            if (help == "")
            {
                help = "水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n#氪金 目标账号 数量(管理员限定 暂不支持自定义修改)\n\n#精准单抽(A/B)大小写随意\n#扩充单抽\n#精准十连(A/B)大小写随意\n#扩充十连\n#标配单抽\n#标配十连";
            }
            reset1 = INIhelper.IniRead("Answer", "Reset1", "贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍", path);
            reset2 = INIhelper.IniRead("Answer", "Reset2", "草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍", path);
            reset3 = INIhelper.IniRead("Answer", "Reset3", "你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍", path);
            reset4 = INIhelper.IniRead("Answer", "Reset4", "由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍", path);
            reset5 = INIhelper.IniRead("Answer", "Reset5", "理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍", path);
            reset6 = INIhelper.IniRead("Answer", "Reset6", "希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍", path);

            registermin = Convert.ToInt32(INIhelper.IniRead("GetDiamond", "RegisterMin", "0", path));
            registermax = Convert.ToInt32(INIhelper.IniRead("GetDiamond", "RegisterMax", "14000", path));
            signmin = Convert.ToInt32(INIhelper.IniRead("GetDiamond", "SignMin", "0", path));
            signmax = Convert.ToInt32(INIhelper.IniRead("GetDiamond", "SignMax", "14000", path));
        }

        string TextGacha(List<UI.Gacha.GachaResult> ls)
        {
            string str = "";
            foreach (var item in ls)
            {
                string type = item.type;
                switch (type)
                {
                    case "Chararcter":
                        switch (item.class_)
                        {
                            case "S":
                                type = "[S角色卡]";
                                break;
                            case "A":
                                type = "[A角色卡]";
                                break;
                            case "B":
                                type = "[B角色卡]";
                                break;
                        }
                        break;
                    case "Weapon":
                        switch (item.evaluation)
                        {
                            case 4:
                                type = "[四星武器]";
                                break;
                            case 3:
                                type = "[三星武器]";
                                break;
                            case 2:
                                type = "[二星武器]";
                                break;
                        }
                        break;
                    case "Stigmata":
                        switch (item.evaluation)
                        {
                            case 4:
                                type = "[四星圣痕]";
                                break;
                            case 3:
                                type = "[三星圣痕]";
                                break;
                            case 2:
                                type = "[二星圣痕]";
                                break;
                        }
                        break;
                    case "Material":
                        type = "[材料]";
                        break;
                    case "debri":
                        type = "[碎片]";
                        break;
                }
                str += type + item.name + $"x{item.count}\n";
            }
            return str;
        }

        void Register(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            Random rd = new Random();
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO 'UserData' VALUES(-1,{id},0,0,{rd.Next(registermin, registermax)})", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        bool IDExist(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT count(*) FROM UserData where Fromgroup=-1 and qq={cq.FromQQ.Id}", cn);
            SQLiteDataReader sr = cmd.ExecuteReader();
            sr.Read();
            int count = sr.GetInt32(0);
            sr.Close();
            return count == 1;
        }

        public int GetDiamond(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            int diamond = 0;
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT diamond FROM UserData WHERE Fromgroup=-1 and qq='{id}'", cn);
            SQLiteDataReader sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                diamond = sr.GetInt32(0);
            }
            sr.Close();
            cn.Close();
            return diamond;
        }

        int SubDiamond(long id, int num)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET diamond=@diamond WHERE Fromgroup=-1 and qq='{cq.FromQQ.Id}'", cn);
            cmd.Parameters.Add("diamond", DbType.Int32).Value = GetDiamond(cq.FromQQ.Id) - num;
            cmd.ExecuteNonQuery();
            return GetDiamond(cq.FromQQ.Id);
        }

        int Sign(long id)
        {
            long timestamp = 0; int money = 0;
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT timestamp,diamond,sign FROM UserData WHERE Fromgroup=-1 and qq='{cq.FromQQ.Id}'", cn);
            SQLiteDataReader sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                timestamp = sr.GetInt64(0);
                money = sr.GetInt32(1);
            }
            sr.Close();
            if (JudgeifTimestampOverday(timestamp, GetTimeStamp()))
            {
                Random rd = new Random();
                int signdiamond = rd.Next(signmin, signmax);
                cmd.CommandText = $"UPDATE UserData SET diamond=@diamond,sign=@sign,timestamp=@timestamp WHERE Fromgroup=-1 and qq='{cq.FromQQ.Id}'";
                cmd.Parameters.Add("diamond", DbType.Int32).Value = money + signdiamond;
                cmd.Parameters.Add("sign", DbType.Int32).Value = 1;
                cmd.Parameters.Add("timestamp", DbType.Int64).Value = GetTimeStamp();
                cmd.ExecuteNonQuery();
                return signdiamond;
            }
            cn.Close();
            return -1;
        }

        static void DeleteDB()
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        static void DeleteTable(string tablename)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = cn;
                cmd.CommandText = $"DROP TABLE IF EXISTS {tablename}";
                cmd.ExecuteNonQuery();
            }
            cn.Close();
        }

        public static long testTimeLingchen;
        /// <summary>
        /// 判断两个时间戳是否隔天
        /// </summary>
        /// <param name="dt1">用于判断的时间戳</param>
        /// <param name="dt2">实时时间戳</param>
        public static bool JudgeifTimestampOverday(long dt1, long dt2)
        {
            testTimeLingchen = dt1 - ((dt1 + 8 * 3600) % 86400);
            if (dt2 > dt1)
            {
                if (dt2 - testTimeLingchen > 24 * 60 * 60)
                {
                    return true;//是明天
                }
                else
                {
                    return false;//是今天
                }
            }
            else
            {
                return false;//是昨天
            }
        }
        //获取时间戳
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}
