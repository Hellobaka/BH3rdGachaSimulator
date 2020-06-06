using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using me.luohuaming.Gacha.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;
using Native.Sdk.Cqp.Model;
using Native.Tool.IniConfig.Linq;
using Native.Sdk.Cqp;
using Native.Tool.IniConfig;
using static me.luohuaming.Gacha.UI.Gacha;
using System.Web.ModelBinding;
using System.Text.RegularExpressions;
using me.luohuaming.Gacha.Code.Func;
using me.luohuaming.Gacha.Code.CustomPool;

namespace me.luohuaming.Gacha.Code
{
    public class Event_GroupMessage : IGroupMessage
    {
        //INIhelper\.IniRead\((.*?), (.*?), (.*?), .*?\)
        //INIhelper\.IniWrite\((.*?), (.*?), (.*?), .*?\)
        //ini.Object[$1][$2].GetValueOrDefault($3)
        //ini.Object[$1][$2]=new IValue($3)
        //\.GetValueOrDefault\("(.*?)"\)\.ToInt32\(\)
        //.GetValueOrDefault($1)
        //static CQGroupMessageEventArgs cq;
        static IniConfig ini;
        #region --字段--
        public static string order_KC1;
        public static string order_KC10;
        public static string order_JZA1;
        public static string order_JZA10;
        public static string order_JZB1;
        public static string order_JZB10;
        public static string order_BP1;
        public static string order_BP10;

        public static string order_sign;
        public static string order_signreset;
        public static string order_querydiamond;
        public static string order_help;
        public static string order_getpool;
        public static string order_register;
        public static string order_opengacha;
        public static string order_closegacha;

        public static string KC1;
        public static string KC10;
        public static string JZA1;
        public static string JZA10;
        public static string JZB1;
        public static string JZB10;
        public static string BP1;
        public static string BP10;
        public static string sign1;
        public static string sign2;
        public static string mutiSign;
        public static string noReg;
        public static string lowDiamond;
        public static string queryDiamond;
        public static string help;
        public static string register;
        public static string mutiRegister;
        public static string reset1;
        public static string reset2;
        public static string reset3;
        public static string reset4;
        public static string reset5;
        public static string reset6;

        public static int signmin;
        public static int signmax;
        public static int registermin;
        public static int registermax;
        public static int diamond;
        public static long testTimeLingchen;
        #endregion

        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            //cq = e;
            CQSave.cq_group = e;
            string path = $@"{CQSave.AppDirectory}Config.ini";
            ini = new IniConfig(path);
            ini.Load();

            if (ini.Object["接口"]["Group"].GetValueOrDefault("0") == "0") return;
            if (!GroupInini(e) && !e.Message.Text.StartsWith("#抽卡开启")) return;
            ReadConfig();

            bool exist = IDExist(e);
            long controlgroup = Convert.ToInt64(ini.Object["后台群"]["Id"].GetValueOrDefault("0"));
            string str = "";
            UI.Gacha gc = new UI.Gacha();
            e.Message.Text = e.Message.Text.Replace("＃", "#");
            if (e.Message.Text.Replace(" ", "") == order_KC1)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchKC1"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, KC1);
                gc.Read_Kuochong();
                List<GachaResult> ls = new List<GachaResult>
                {
                    gc.KC_Gacha(),
                    gc.KC_GachaSub()
                };
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start(); CombinePng cp = new CombinePng();
                SubDiamond(e, 280);
                AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, 1);
                path = $@"{CQSave.AppDirectory}概率\扩充概率.txt";
                ini = new IniConfig(path);
                ini.Load();
                if (ini.Object["详情"]["ResultAt"].GetValueOrDefault("0") == "0")
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 0, 0, 1, diamond - 280)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, TextGacha(ls));
                    }
                }
                else
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 0, 0, 1, diamond - 280)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]" + TextGacha(ls));
                    }
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个扩充单抽";
            }
            else if (e.Message.Text.Replace(" ", "") == order_KC10)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchKC10"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, KC10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_Kuochong();
                List<GachaResult> ls = new List<GachaResult>();
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
                        if (ls[i].name == ls[j].name && ls[i].type != TypeS.Character.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start(); CombinePng cp = new CombinePng();
                SubDiamond(e, 2800);
                AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, 10);
                path = $@"{CQSave.AppDirectory}概率\扩充概率.txt";
                ini = new IniConfig(path);
                ini.Load();
                if (ini.Object["详情"]["ResultAt"].GetValueOrDefault("0") == "0")
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 0, 0, 10, diamond - 2800)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, TextGacha(ls));
                    }
                }
                else
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 0, 0, 10, diamond - 2800)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]" + TextGacha(ls));
                    }

                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个扩充十连";
            }
            else if (e.Message.Text.Replace(" ", "").ToUpper() == order_JZA1)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchJZA1"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZA1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(1);
                List<GachaResult> ls = new List<GachaResult>
                {
                    gc.JZ_GachaMain(),
                    gc.JZ_GachaMaterial()
                };
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(e, 280);
                AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, 1);

                path = $@"{CQSave.AppDirectory}概率\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();
                if (ini.Object["详情"]["A_ResultAt"].GetValueOrDefault("0") == "0")
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 1, 1, diamond - 280)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, TextGacha(ls));
                    }

                }
                else
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 1, 1, 1, diamond - 280)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]" + TextGacha(ls));
                    }
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准单抽";
            }
            else if (e.Message.Text.Replace(" ", "").ToUpper() == order_JZA10)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchJZA10"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZA10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(1);
                List<GachaResult> ls = new List<GachaResult>();
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
                        if (ls[i].name == ls[j].name && ls[i].type != TypeS.Stigmata.ToString() && ls[i].type != TypeS.Weapon.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(e, 2800);
                AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, 10);

                path = $@"{CQSave.AppDirectory}概率\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();
                if (ini.Object["详情"]["A_ResultAt"].GetValueOrDefault("0") == "0")
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 1, 10, diamond - 2800)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, TextGacha(ls));
                    }

                }
                else
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 1, 1, 10, diamond - 2800)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]" + TextGacha(ls));
                    }
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准十连";
            }
            else if (e.Message.Text.Replace(" ", "").ToUpper() == order_JZB1)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchJZB1"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZB1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(2);
                List<GachaResult> ls = new List<GachaResult>
                {
                    gc.JZ_GachaMain(),
                    gc.JZ_GachaMaterial()
                };
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(e, 280);
                AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, 1);

                path = $@"{CQSave.AppDirectory}概率\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();
                if (ini.Object["详情"]["B_ResultAt"].GetValueOrDefault("0") == "0")
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 2, 1, diamond - 280)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, TextGacha(ls));
                    }
                }
                else
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 1, 2, 1, diamond - 280)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]" + TextGacha(ls));
                    }
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准单抽";
            }
            else if (e.Message.Text.Replace(" ", "").ToUpper() == order_JZB10)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchJZB10"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZB10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(2);
                List<GachaResult> ls = new List<GachaResult>();
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
                        if (ls[i].name == ls[j].name && ls[i].type != TypeS.Stigmata.ToString() && ls[i].type != TypeS.Weapon.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(e, 2800);
                AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, 10);

                path = $@"{CQSave.AppDirectory}概率\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();
                if (ini.Object["详情"]["B_ResultAt"].GetValueOrDefault("0") == "0")
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 2, 10, diamond - 2800)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, TextGacha(ls));
                    }
                }
                else
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 1, 2, 10, diamond - 2800)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]" + TextGacha(ls));
                    }
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准十连";
            }
            else if (e.Message.Text.Replace(" ", "") == order_BP10)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchBP10"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, BP10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_BP();
                List<GachaResult> ls = new List<GachaResult>();
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
                        if (ls[i].name == ls[j].name && ls[i].type != TypeS.Character.ToString())
                        {
                            ls[i].count += ls[j].count;
                            ls.RemoveAt(j);
                            i--; j--;
                            if (i == -1) i = 0;
                        }
                    }
                }
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();

                CombinePng cp = new CombinePng();
                SubDiamond(e, 2800);
                AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, 10);
                path = $@"{CQSave.AppDirectory}概率\标配概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["设置"]["ResultAt"].GetValueOrDefault("0") == "0")
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 2, 0, 10, diamond - 2800)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, TextGacha(ls));
                    }
                }
                else
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 2, 0, 10, diamond - 2800)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]" + TextGacha(ls));
                    }
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个标配十连";
            }
            else if (e.Message.Text.Replace(" ", "") == order_BP1)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchBP1"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, BP1);
                gc.Read_BP();
                List<GachaResult> ls = new List<GachaResult>
                {
                    gc.BP_GachaMain(),
                    gc.BP_GachaSub()
                };
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(e, 280);
                AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, 1);
                path = $@"{CQSave.AppDirectory}概率\标配概率.txt";
                ini = new IniConfig(path);
                ini.Load();
                if (ini.Object["详情"]["ResultAt"].GetValueOrDefault("0") == "0")
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();
                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 2, 0, 1, diamond - 280)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, TextGacha(ls));
                    }
                }
                else
                {
                    path = $@"{CQSave.AppDirectory}Config.ini";
                    ini = new IniConfig(path);
                    ini.Load();

                    if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "0")
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 2, 0, 1, diamond - 280)}]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]" + TextGacha(ls));
                    }
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个标配单抽";
            }
            else if (e.Message.Text.Replace(" ", "") == order_sign)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = Sign(e);
                AddCount_Sign(e);
                if (diamond >= 0)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, sign1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    e.CQApi.SendGroupMessage(e.FromGroup, sign2.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 进行了签到";
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, mutiSign.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
            }
            else if (e.Message.Text.Replace(" ", "") == order_register)
            {
                e.Handler = true;
                if (!exist)
                {
                    Register(e);
                    Random rd = new Random();
                    diamond = GetDiamond(e);
                    e.CQApi.SendGroupMessage(e.FromGroup, register.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 注册了抽卡";
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, mutiRegister.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
            }
            else if (e.Message.Text.Replace(" ", "") == order_querydiamond)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchQueDiamond"].GetValueOrDefault("1") == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e);
                e.CQApi.SendGroupMessage(e.FromGroup, queryDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                return;
            }
            else if (e.Message.Text.Replace(" ", "") == order_signreset)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchResSign"].GetValueOrDefault("1") == "0") return;

                path = $@"{CQSave.AppDirectory}Config.ini";
                ini = new IniConfig(path);
                ini.Load();

                int count = Convert.ToInt32(ini.Object[e.FromGroup.Id.ToString()]["Count"].GetValueOrDefault("0"));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (ini.Object[e.FromGroup.Id.ToString()][$"Index{i}"].GetValueOrDefault("0") == e.FromQQ.Id.ToString())
                    {
                        InGroup = true;
                        break;
                    }
                }
                if (InGroup)
                {
                    SignReset(e);
                    Random rd = new Random();
                    switch (rd.Next(0, 6))
                    {
                        case 0:
                            e.CQApi.SendGroupMessage(e.FromGroup, reset1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                            break;
                        case 1:
                            e.CQApi.SendGroupMessage(e.FromGroup, reset2.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                            break;
                        case 2:
                            e.CQApi.SendGroupMessage(e.FromGroup, reset3.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                            break;
                        case 3:
                            e.CQApi.SendGroupMessage(e.FromGroup, reset4.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                            break;
                        case 4:
                            e.CQApi.SendGroupMessage(e.FromGroup, reset5.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                            break;
                        case 5:
                            e.CQApi.SendGroupMessage(e.FromGroup, reset6.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                            break;
                    }
                    str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 重置了签到";
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, "只有管♂理员才能这么做");
                    return;
                }
            }
            else if (e.Message.Text.Replace(" ", "") == order_help)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchGetHelp"].GetValueOrDefault("1") == "0") return;

                str = help.Replace(@"\n", "\n");
                e.CQApi.SendGroupMessage(e.FromGroup, str);
                return;
            }
            else if (e.Message.Text.Replace(" ", "") == order_getpool)
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchGetPool"].GetValueOrDefault("1") == "0") return;

                string UPS, UPA, UPWeaponA, UPStigmataA, UPWeaponB, UPStigmataB;
                path = e.CQApi.AppDirectory + "\\概率\\扩充概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                UPS = ini.Object["详情"]["UpS"].GetValueOrDefault("S角色");
                UPA = ini.Object["详情"]["UpA"].GetValueOrDefault("A角色");
                path = e.CQApi.AppDirectory + "\\概率\\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();
                UPWeaponA = ini.Object["详情"]["A_UpWeapon"].GetValueOrDefault("四星武器");
                UPStigmataA = ini.Object["详情"]["A_UpStigmata"].GetValueOrDefault("四星圣痕");
                UPWeaponB = ini.Object["详情"]["B_UpWeapon"].GetValueOrDefault("四星武器");
                UPStigmataB = ini.Object["详情"]["B_UpStigmata"].GetValueOrDefault("四星圣痕");
                e.CQApi.SendGroupMessage(e.FromGroup, $"当前扩充池为 {UPS} {UPA}\n当前精准A池为 {UPWeaponA} {UPStigmataA}\n当前精准B池为 {UPWeaponB} {UPStigmataB}");
                return;
            }
            else if (e.Message.Text.StartsWith("#氪金"))
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchKaKin"].GetValueOrDefault("1") == "0") return;
                path = $@"{CQSave.AppDirectory}Config.ini";
                ini = new IniConfig(path);
                ini.Load();
                int count = Convert.ToInt32(ini.Object[e.FromGroup.Id.ToString()]["Count"].GetValueOrDefault("0"));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (ini.Object[e.FromGroup.Id.ToString()][$"Index{i}"].GetValueOrDefault("0") == e.FromQQ.Id.ToString())
                    {
                        InGroup = true;
                        break;
                    }
                }
                if (!InGroup)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, "氪金的权利掌握在管理员手里[CQ:face,id=178]");
                    return;
                }
                string[] temp = e.Message.Text.Split(' ');
                if (temp.Length != 3)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 输入的格式不正确！请按照 #氪金 目标QQ号或者at目标 数量 的格式填写");
                    return;
                }
                else
                {
                    try
                    {
                        long targetId = Convert.ToInt64(temp[1].Replace("[CQ:at,qq=", "").Replace("]", ""));
                        int countdia = Convert.ToInt32(temp[2]);
                        try
                        {
                            if (!IDExist(e,targetId))
                            {
                                e.CQApi.SendGroupMessage(e.FromGroup, "操作对象不存在");
                                return;
                            }
                            path = $@"{CQSave.AppDirectory}data.db";
                            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
                            cn.Open();
                            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET diamond=@diamond WHERE Fromgroup={e.FromGroup.Id} and qq='{targetId}'", cn);
                            cmd.Parameters.Add("diamond", DbType.Int32).Value = GetDiamond(e,targetId) + countdia;
                            cmd.ExecuteNonQuery();
                            e.CQApi.SendGroupMessage(e.FromGroup, $"操作成功,为[CQ:at,qq={targetId}]充值{countdia}水晶,剩余{GetDiamond(e,targetId)}水晶");
                            return;
                        }
                        catch
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, str = "操作失败了……");
                            return;
                        }
                    }
                    catch
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 输入的格式不正确！请按照格式输入纯数字");
                        return;
                    }
                }
            }
            else if (e.Message.Text.ToUpper().StartsWith("#SQL"))
            {
                e.Handler = true;
                path = $@"{CQSave.AppDirectory}Config.ini";
                ini = new IniConfig(path);
                ini.Load();
                if (ini.Object["ExtraConfig"]["ExecuteSql"].GetValueOrDefault("0") == "0")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]此功能未在控制台开启，拒绝操作");
                    return;
                }
                int count = Convert.ToInt32(ini.Object[e.FromGroup.Id.ToString()]["Count"].GetValueOrDefault("0"));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (ini.Object[e.FromGroup.Id.ToString()][$"Index{i}"].GetValueOrDefault("0") == e.FromQQ.Id.ToString())
                    {
                        InGroup = true;
                        break;
                    }
                }
                if (!InGroup)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]权限不足，拒绝操作");
                    return;
                }
                path = $@"{CQSave.AppDirectory}data.db";
                SQLiteConnection cn = new SQLiteConnection("data source=" + path);
                cn.Open();
                SQLiteCommand cmd = new SQLiteCommand(e.Message.Text.Substring(4), cn);
                //cmd.Parameters.Add("diamond", DbType.Int32).Value = GetDiamond(targetId) + countdia;
                try
                {
                    int countSql = cmd.ExecuteNonQuery();
                    e.CQApi.SendGroupMessage(e.FromGroup, $"操作成功，{countSql}行受影响");
                }
                catch (Exception err)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]执行失败:\n{err.Message}");
                    return;
                }
                return;
            }
            else if (e.Message.Text.StartsWith(order_opengacha))
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchOpenGroup"].GetValueOrDefault("1") == "0") return;

                path = $@"{CQSave.AppDirectory}Config.ini";
                ini = new IniConfig(path);
                ini.Load();
                int count = Convert.ToInt32(ini.Object[e.FromGroup.Id.ToString()]["Count"].GetValueOrDefault("0"));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (ini.Object[e.FromGroup.Id.ToString()][$"Index{i}"].GetValueOrDefault("0") == e.FromQQ.Id.ToString())
                    {
                        InGroup = true;
                        break;
                    }
                }
                if (e.FromGroup.Id == controlgroup || InGroup)
                {
                    try
                    {
                        long target = 0;
                        if (e.Message.Text == "#抽卡开启")
                        {
                            target = e.FromGroup.Id;
                        }
                        else
                        {
                            if (e.FromGroup.Id != controlgroup)
                            {
                                e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]控制的群号的操作只允许在后台群，请输入 #抽卡开启");
                                return;
                            }
                            else
                            {
                                target = Convert.ToInt64(e.Message.Text.Substring("#抽卡开启".Length).Trim());
                            }
                        }
                        count = Convert.ToInt32(ini.Object["群控"]["Count"].GetValueOrDefault("0"));
                        path = e.CQApi.AppDirectory + "Config.ini";
                        bool flag = false;
                        for (int i = 0; i < count; i++)
                        {
                            if (ini.Object["群控"][$"Item{i}"].GetValueOrDefault("0") == target.ToString())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 群:{target}已经开启了，不需要重复开启");
                            return;
                        }
                        else
                        {
                            ini.Object["群控"]["Count"] = new IValue((count + 1).ToString());
                            ini.Object["群控"][$"Item{count}"] = new IValue(target.ToString());
                            ini.Save();
                            e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 群:{target}已开启，指定管理员请在控制台完成");
                            str = $"{e.FromQQ.Id}已置群{e.FromGroup.Id}开启";
                        }
                    }
                    catch
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]请输入纯数字");
                        return;
                    }
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]权限不足，拒绝操作");
                    return;
                }
            }
            else if (e.Message.Text.StartsWith(order_closegacha))
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchCloseGroup"].GetValueOrDefault("1") == "0") return;

                path = $@"{CQSave.AppDirectory}Config.ini";
                ini = new IniConfig(path);
                ini.Load();
                int count = Convert.ToInt32(ini.Object[e.FromGroup.Id.ToString()]["Count"].GetValueOrDefault("0"));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (ini.Object[e.FromGroup.Id.ToString()][$"Index{i}"].GetValueOrDefault("0") == e.FromQQ.Id.ToString())
                    {
                        InGroup = true;
                        break;
                    }
                }
                if (e.FromGroup.Id == controlgroup || InGroup)
                {
                    try
                    {
                        long target = 0;
                        if (e.Message.Text == "#抽卡关闭")
                        {
                            target = e.FromGroup.Id;
                        }
                        else
                        {
                            if (e.FromGroup.Id != controlgroup)
                            {
                                e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]控制的群号的操作只允许在后台群，请输入 #抽卡关闭");
                                return;
                            }
                            else
                            {
                                target = Convert.ToInt64(e.Message.Text.Substring("#抽卡关闭".Length).Trim());
                            }
                        }
                        count = Convert.ToInt32(ini.Object["群控"]["Count"].GetValueOrDefault("0"));
                        path = e.CQApi.AppDirectory + "Config.ini";
                        bool flag = false;
                        for (int i = 0; i < count; i++)
                        {
                            if (ini.Object["群控"][$"Item{i}"].GetValueOrDefault("0") == target.ToString())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 群:{target}已经关闭了，不需要重复关闭");
                            return;
                        }
                        else
                        {
                            List<long> grouplist = new List<long>();
                            for (int i = 0; i < count; i++)
                            {
                                long groupid = Convert.ToInt64(ini.Object["群控"][$"Item{i}"].GetValueOrDefault("0"));
                                if (groupid == target) continue;
                                grouplist.Add(groupid);
                            }
                            ini.Object["群控"][$"Count"] = new IValue((count - 1).ToString());
                            for (int i = 0; i < grouplist.Count; i++)
                            {
                                ini.Object["群控"][$"Item{i}"] = new IValue(grouplist[i].ToString());
                            }
                            ini.Save();

                            e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 群:{target}已关闭");
                            str = $"{e.FromQQ.Id}已置群{e.FromGroup.Id}关闭";

                        }
                    }
                    catch (Exception ex)
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]请输入纯数字,info:{ex.Message}");
                        return;
                    }
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]权限不足，拒绝操作");
                    return;

                }

            }
            else if (e.Message.Text.StartsWith("#置抽卡管理"))
            {
                e.Handler = true;
                if (ini.Object["ExtraConfig"]["SwitchOpenAdmin"].GetValueOrDefault("1") == "0") return;

                if (e.FromGroup.Id == controlgroup || CheckAdmin(e))
                {
                    try
                    {
                        string[] targetid = e.Message.Text.Substring("#置抽卡管理".Length).Trim().Replace('，', ',').Split(',');
                        Convert.ToInt64(targetid[1]);
                        if (targetid[2].IndexOf("[CQ:at") != -1)
                        {
                            targetid[2] = targetid[3].Replace("qq=", "").Replace("]", "");
                        }
                        else
                        {
                            Convert.ToInt64(targetid[2]);
                        }
                        if (targetid.Length != 3 && targetid.Length != 4)
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, CQApi.CQCode_At(e.FromQQ), "输入格式非法，例子(依次为群号与QQ号):#置抽卡管理,671467200,2185367837(或者@2185367837)");
                            return;
                        }
                        path = $@"{CQSave.AppDirectory}Config.ini";
                        ini = new IniConfig(path);
                        ini.Load();
                        if (GroupInini(e))
                        {
                            if (CheckAdmin(Convert.ToInt64(targetid[1]), Convert.ToInt64(targetid[2])))
                            {
                                e.CQApi.SendGroupMessage(e.FromGroup, e.FromQQ.CQCode_At(), "目标QQ在目标群已经是管理了，不需要重复设置");
                                return;
                            }
                            else
                            {
                                int count = ini.Object[$"{targetid[1]}"]["Count"].GetValueOrDefault(0);
                                ini.Object[$"{targetid[1]}"][$"Index{count}"] = new IValue(targetid[2]);
                                ini.Object[$"{targetid[1]}"][$"Count"] = new IValue((++count).ToString());
                                ini.Save();

                            }
                        }
                        else
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, CQApi.CQCode_At(e.FromQQ), $"此群未开启，请先输入{order_opengacha}");
                            return;
                        }
                    }
                    catch
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, CQApi.CQCode_At(e.FromQQ), "请保证纯数字输入，例子(依次为群号与QQ号):#置抽卡管理,671467200,2185367837(或者@2185367837)");
                        return;
                    }
                    e.CQApi.SendGroupMessage(e.FromGroup, CQApi.CQCode_At(e.FromQQ), "设置成功");
                    return;
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, CQApi.CQCode_At(e.FromQQ), "权限不足，请在后台群或者开通抽卡机管理员权限");
                    return;
                }
            }
            else if (e.Message.Text.StartsWith("#更换池子"))
            {
                e.Handler = true;
                if (ini.Object["OCR"]["app_id"].GetValueOrDefault("") == "")
                {
                    e.FromGroup.SendGroupMessage("参数缺失，请按照日志提示补全参数");
                    e.CQLog.Warning("参数缺失", $"请到插件数据 Config.ini 下OCR字段填写App_id与App_key。若没有可到插件论坛页面按照提示获取.");
                    return;
                }
                if (CheckAdmin(e))
                {
                    string option = e.Message.Text.Substring("#更换池子".Length).Trim();
                    //ChangePool.PoolName = temp;
                    //e.CQApi.SendGroupMessage(e.FromGroup, ChangePool.GetResult(e));
                    e.FromGroup.SendGroupMessage("获取中……请耐心等待");
                    str = new PaChonger().GetPoolOnline(option);
                    if (string.IsNullOrEmpty(str))
                    {
                        str = "查无此池";
                    }
                    else
                    {
                        str += "立刻更改请回复#now";
                    }
                }
                else
                {
                    str = "权限不足，拒绝操作";
                }
                e.FromGroup.SendGroupMessage(str);
                return;
            }
            else if (e.Message.Text.ToLower() == "#now")
            {
                e.Handler = true;
                e.FromGroup.SendGroupMessage(ChangePool());
                return;
            }
            else if (e.Message.Text.ToLower() == "#time")
            {
                e.Handler = true;
                e.FromGroup.SendGroupMessage("还没开发……");
                return;
            }
            else if (e.Message.Text.StartsWith("#抽干家底"))
            {
                e.Handler = true;
                if (e.Message.Text.Trim() == "#抽干家底")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, CQApi.CQCode_At(e.FromQQ), "请指定要抽取的池子，扩充或者精准A/B");
                    return;
                }
                string order = e.Message.Text.Substring("#抽干家底".Length).Trim().ToUpper().Replace(" ", "");
                if (order == "扩充")
                {
                    if (!exist)
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                        return;
                    }

                    ini = new IniConfig(CQSave.AppDirectory + "概率\\扩充概率.txt");
                    ini.Load();
                    e.CQApi.SendGroupMessage(e.FromGroup, $"正在抽干家底……抽到<{ini.Object["详情"]["UpS"].GetValueOrDefault("UPS角色")}>就会收手");

                    diamond = GetDiamond(e);
                    gc.Read_Kuochong();
                    List<GachaResult> ls = new List<GachaResult>();
                    int count = 0;
                    for (int i = 0; i < diamond / 280; i++)
                    {
                        ls.Add(gc.KC_Gacha());
                        ls.Add(gc.KC_GachaSub());
                        count++;
                        if (ls.FindIndex(x => x.class_ == "S") != -1)
                        {
                            break;
                        }
                    }
                    ls = ls.OrderByDescending(x => x.value).ToList();
                    for (int i = 0; i < ls.Count; i++)
                    {
                        for (int j = i + 1; j < ls.Count; j++)
                        {
                            if (ls[i].name == ls[j].name && ls[i].type != TypeS.Character.ToString())
                            {
                                ls[i].count += ls[j].count;
                                ls.RemoveAt(j);
                                i--; j--;
                                if (i == -1) i = 0;
                            }
                        }
                    }
                    var tasksql = new Task(() =>
                    {
                        AddItem2Repositories(ls, e);
                    }); tasksql.Start();
                    for (int i = 0; i < ls.Count; i++)
                    {
                        for (int j = i + 1; j < ls.Count; j++)
                        {
                            if (ls[i].name == ls[j].name)
                            {
                                ls[i].count += ls[j].count;
                                ls.RemoveAt(j);
                                i--; j--;
                                if (i == -1) i = 0;
                            }
                        }
                    }
                    SubDiamond(e, count * 280);
                    AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, count);
                    if (ls.FindIndex(x => x.class_ == "S") == -1)
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"今天不适合你抽卡……\n合计:抽取{count}次\n消耗了{count * 280}水晶 折合大约是{count * 280 / 7640 + 1}单648[CQ:face,id=67]");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"抽到啦！\n合计:抽取{count}次\n消耗了{count * 280}水晶 折合大约是{count * 280 / 7640 + 1}单648\n٩( 'ω' )و");
                    }
                    string items = "获取到的物品如下:\n";
                    int count_purple = 0;
                    foreach (var item in ls)
                    {
                        if (item.type == TypeS.Character.ToString())
                        {
                            items += $"{item.name} ×{item.count}\n";
                            count_purple += item.count;
                        }
                    }
                    items += $"出货率为{(double)count_purple / count * 100:f2}%\n平均每10发抽到紫{(double)count_purple / count * 10:f2}个";
                    e.CQApi.SendGroupMessage(e.FromGroup, items);
                    GC.Collect();
                    return;
                }
                else if (order.Contains("精准"))
                {
                    if (!exist)
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                        return;
                    }

                    string pool;
                    switch (order.Substring("精准".Length))
                    {
                        case "A":
                            pool = "A";
                            gc.Read_Jingzhun(1);
                            break;
                        case "B":
                            pool = "B";
                            gc.Read_Jingzhun(2);
                            break;
                        default:
                            pool = "A";
                            gc.Read_Jingzhun(1);
                            break;
                    }
                    if (!exist)
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                        return;
                    }
                    ini = new IniConfig(CQSave.AppDirectory + "概率\\精准概率.txt");
                    ini.Load();
                    e.CQApi.SendGroupMessage(e.FromGroup, $"正在抽干家底……抽到{pool}池毕业就会收手");

                    string UPWeapon, UPStigmata;
                    UPWeapon = ini.Object["详情"][$"{pool}_UpWeapon"].GetValueOrDefault("四星武器");
                    UPStigmata = ini.Object["详情"][$"{pool}_UpStigmata"].GetValueOrDefault("四星圣痕");

                    diamond = GetDiamond(e);

                    List<GachaResult> ls = new List<GachaResult>();
                    int count = 0;
                    for (int i = 0; i < diamond / 280; i++)
                    {
                        ls.Add(gc.JZ_GachaMain());
                        ls.Add(gc.JZ_GachaMaterial());
                        count++;
                        if (ls.Exists(x => x.name == UPWeapon) && ls.Exists(x => x.name == UPStigmata + "上")
                            && ls.Exists(x => x.name == UPStigmata + "中") && ls.Exists(x => x.name == UPStigmata + "下"))
                        {
                            break;
                        }
                    }
                    ls = ls.OrderByDescending(x => x.value).ToList();
                    for (int i = 0; i < ls.Count; i++)
                    {
                        for (int j = i + 1; j < ls.Count; j++)
                        {
                            if (ls[i].name == ls[j].name && ls[i].type != TypeS.Stigmata.ToString() && ls[i].type != TypeS.Weapon.ToString())
                            {
                                ls[i].count += ls[j].count;
                                ls.RemoveAt(j);
                                i--; j--;
                                if (i == -1) i = 0;
                            }
                        }
                    }
                    var tasksql = new Task(() =>
                    {
                        AddItem2Repositories(ls, e);
                    });
                    tasksql.Start();
                    ls = ls.OrderByDescending(x => x.name).ToList();
                    for (int i = 0; i < ls.Count; i++)
                    {
                        for (int j = i + 1; j < ls.Count; j++)
                        {
                            if (ls[i].name == ls[j].name)
                            {
                                ls[i].count += ls[j].count;
                                ls.RemoveAt(j);
                                i--; j--;
                                if (i == -1) i = 0;
                            }
                        }
                    }
                    SubDiamond(e, count * 280);
                    AddCount_Gacha(e.FromGroup.Id, e.FromQQ.Id, count);

                    if (ls.Exists(x => x.name == UPWeapon) && ls.Exists(x => x.name == UPStigmata + "上")
                        && ls.Exists(x => x.name == UPStigmata + "中") && ls.Exists(x => x.name == UPStigmata + "下"))
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"抽到啦！\n合计:抽取{count}次\n消耗了{count * 280}水晶 折合大约是{count * 280 / 7640 + 1}单648\n٩( 'ω' )و");
                    }
                    else
                    {
                        e.CQApi.SendGroupMessage(e.FromGroup, $"嘛，精准池不毕业挺正常……\n合计:抽取{count}次\n消耗了{count * 280}水晶 折合大约是{count * 280 / 7640 + 1}单648[CQ:face,id=67]");
                    }
                    string items = "获取到的物品如下:\n";
                    int count_purple = 0;
                    foreach (var item in ls)
                    {
                        if ((item.type == TypeS.Weapon.ToString() && item.quality == 2) || (item.type == TypeS.Stigmata.ToString() && item.quality == 2))
                        {
                            items += $"{item.name} ×{item.count}\n";
                            count_purple += item.count;
                        }
                    }
                    items += $"出货率为{(double)count_purple / count * 100:f2}%\n平均每10发抽到紫{(double)count_purple / count * 10:f2}个";
                    e.CQApi.SendGroupMessage(e.FromGroup, items);

                    GC.Collect();
                    return;

                }
            }
            else if (e.Message.Text == "#排行榜")
            {
                e.Handler = true;
                e.FromGroup.SendGroupMessage(TotalRank.GetRank(e));
                return;
            }
            else if (e.Message.Text == "#周榜")
            {
                e.Handler = true;
                e.FromGroup.SendGroupMessage(WeekRank.GetWeekRank(e));
                return;
            }            
            else
            {
                CustomPoolGacha.CustomPool_GroupMsg(e);
                return;
            }
            ini = new IniConfig(e.CQApi.AppDirectory + "Config.ini"); ini.Load();
            if (ini.Object["ExtraConfig"]["GachaMsg"].GetValueOrDefault("1") == "1")
            {
                if (controlgroup == 0) return;
                e.CQApi.SendGroupMessage(controlgroup, str);
            }
        }
        #region --工具函数--
        /// <summary>
        /// 获取文字抽卡结果
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        string TextGacha(List<GachaResult> ls)
        {
            string str = "";
            foreach (var item in ls)
            {
                string type = item.type;
                switch (type)
                {
                    case "Character":
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

        /// <summary>
        /// 查询群是否在配置中
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool GroupInini(CQGroupMessageEventArgs e)
        {
            ini = new IniConfig(CQSave.AppDirectory + "Config.ini");
            ini.Load();
            int count = Convert.ToInt32(ini.Object["群控"]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                if (e.FromGroup.Id == ini.Object["群控"][$"Item{i}"].GetValueOrDefault((long)0))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 查询发言人是否为发言群指定的管理员
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool CheckAdmin(CQGroupMessageEventArgs e)
        {
            string path = e.CQApi.AppDirectory + "Config.ini";
            ini = new IniConfig(path);
            ini.Load();
            int count = ini.Object[$"{e.FromGroup.Id}"]["Count"].GetValueOrDefault(0);
            bool flag = false;
            for (int i = 0; i < count; i++)
            {
                if (e.FromQQ.Id == ini.Object[$"{e.FromGroup.Id}"][$"Index{i}"].GetValueOrDefault((long)0))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// 使用群与QQ号作为限制，查询是否为管理员
        /// </summary>
        /// <param name="FromGroup"></param>
        /// <param name="FromQQ"></param>
        /// <returns></returns>
        public static bool CheckAdmin(long FromGroup, long FromQQ)
        {
            string path = $@"{CQSave.AppDirectory}Config.ini";
            ini = new IniConfig(path);
            ini.Load();

            int count = ini.Object[$"{FromGroup}"]["Count"].GetValueOrDefault(0);
            bool flag = false;
            for (int i = 0; i < count; i++)
            {
                if (FromQQ == ini.Object[$"{FromGroup}"][$"Index{i}"].GetValueOrDefault((long)0))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// 抽卡指令与回答初始化
        /// </summary>
        /// <param name="e"></param>
        void ReadConfig()
        {
            order_KC1 = ini.Object["Order"]["KC1"].GetValueOrDefault("#扩充单抽");
            order_KC10 = ini.Object["Order"]["KC10"].GetValueOrDefault("#扩充十连");
            order_JZA1 = ini.Object["Order"]["JZA1"].GetValueOrDefault("#精准单抽A");
            order_JZA10 = ini.Object["Order"]["JZA10"].GetValueOrDefault("#精准十连A");
            order_JZB1 = ini.Object["Order"]["JZB1"].GetValueOrDefault("#精准单抽B");
            order_JZB10 = ini.Object["Order"]["JZB10"].GetValueOrDefault("#精准十连B");
            order_BP1 = ini.Object["Order"]["BP1"].GetValueOrDefault("#标配单抽");
            order_BP10 = ini.Object["Order"]["BP10"].GetValueOrDefault("#标配十连");

            order_register = ini.Object["Order"]["Register"].GetValueOrDefault("#抽卡注册");
            order_sign = ini.Object["Order"]["Sign"].GetValueOrDefault("#打扫甲板");
            order_signreset = ini.Object["Order"]["SignReset"].GetValueOrDefault("#甲板积灰");
            order_querydiamond = ini.Object["Order"]["QueryDiamond"].GetValueOrDefault("#我的水晶");
            order_help = ini.Object["Order"]["Help"].GetValueOrDefault("#抽卡帮助");
            order_getpool = ini.Object["Order"]["GetPool"].GetValueOrDefault("#获取池子");
            order_closegacha = ini.Object["Order"]["CloseGacha"].GetValueOrDefault("#抽卡关闭");
            order_opengacha = ini.Object["Order"]["OpenGacha"].GetValueOrDefault("#抽卡开启");

            KC1 = ini.Object["Answer"]["KC1"].GetValueOrDefault("少女祈祷中……").Replace("\\n", "\n");
            KC10 = ini.Object["Answer"]["KC10"].GetValueOrDefault("少女祈祷中……").Replace("\\n", "\n"); ;
            JZA1 = ini.Object["Answer"]["JZA1"].GetValueOrDefault("少女祈祷中……").Replace("\\n", "\n"); ;
            JZA10 = ini.Object["Answer"]["JZA10"].GetValueOrDefault("少女祈祷中……").Replace("\\n", "\n"); ;
            JZB1 = ini.Object["Answer"]["JZB1"].GetValueOrDefault("少女祈祷中……").Replace("\\n", "\n"); ;
            JZB10 = ini.Object["Answer"]["JZB10"].GetValueOrDefault("少女祈祷中……").Replace("\\n", "\n"); ;
            BP1 = ini.Object["Answer"]["BP1"].GetValueOrDefault("少女祈祷中……").Replace("\\n", "\n"); ;
            BP10 = ini.Object["Answer"]["BP10"].GetValueOrDefault("少女祈祷中……").Replace("\\n", "\n"); ;

            register = ini.Object["Answer"]["Register"].GetValueOrDefault("<@>欢迎上舰，这是你的初始资源(<#>)水晶").Replace("\\n", "\n"); ;
            mutiRegister = ini.Object["Answer"]["MutiRegister"].GetValueOrDefault("重复注册是不行的哦").Replace("\\n", "\n"); ;
            sign1 = ini.Object["Answer"]["Sign1"].GetValueOrDefault("大姐你回来了，天气这么好一起多逛逛吧").Replace("\\n", "\n"); ;
            sign2 = ini.Object["Answer"]["Sign2"].GetValueOrDefault("<@>这是你今天清扫甲板的报酬，拿好(<#>水晶)").Replace("\\n", "\n"); ;
            mutiSign = ini.Object["Answer"]["MutiSign"].GetValueOrDefault("今天的甲板挺亮的，擦一遍就行了").Replace("\\n", "\n"); ;
            noReg = ini.Object["Answer"]["NoReg"].GetValueOrDefault("<@>不是清洁工吧？来输入#抽卡注册 来上舰").Replace("\\n", "\n"); ;
            lowDiamond = ini.Object["Answer"]["LowDiamond"].GetValueOrDefault("<@>水晶不足，无法进行抽卡，你还剩余<#>水晶").Replace("\\n", "\n"); ;
            queryDiamond = ini.Object["Answer"]["QueryDiamond"].GetValueOrDefault("<@>你手头还有<#>水晶").Replace("\\n", "\n"); ;
            help = ini.Object["Answer"]["Help"].GetValueOrDefault(@"水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n#氪金 目标账号或者at 数量(管理员限定 暂不支持自定义修改)\n#获取池子\n\n#精准单抽(A/B)大小写随意\n#扩充单抽\n#精准十连(A/B)大小写随意\n#扩充十连\n#标配单抽\n#标配十连\n#抽卡开启(在后台群后面可接群号)\n#抽卡关闭(在后台群后面可接群号)\n#置抽卡管理(示例:#置抽卡管理,群号,QQ或者at)\n#更换池子 查询公告的关键字\n#抽干家底 扩充或者精准A/B")
                .Replace("\\", @"\");

            reset1 = ini.Object["Answer"]["Reset1"].GetValueOrDefault("贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n"); ;
            reset2 = ini.Object["Answer"]["Reset2"].GetValueOrDefault("草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n"); ;
            reset3 = ini.Object["Answer"]["Reset3"].GetValueOrDefault("你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n"); ;
            reset4 = ini.Object["Answer"]["Reset4"].GetValueOrDefault("由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n"); ;
            reset5 = ini.Object["Answer"]["Reset5"].GetValueOrDefault("理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n"); ;
            reset6 = ini.Object["Answer"]["Reset6"].GetValueOrDefault("希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍").Replace("\\n", "\n"); ;

            registermin = Convert.ToInt32(ini.Object["GetDiamond"]["RegisterMin"].GetValueOrDefault("0"));
            registermax = Convert.ToInt32(ini.Object["GetDiamond"]["RegisterMax"].GetValueOrDefault("14000"));
            signmin = Convert.ToInt32(ini.Object["GetDiamond"]["SignMin"].GetValueOrDefault("0"));
            signmax = Convert.ToInt32(ini.Object["GetDiamond"]["SignMax"].GetValueOrDefault("14000"));
        }

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

        /// <summary>
        ///获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary>
        /// 根据获取到的内容更换池子
        /// </summary>
        /// <returns></returns>
        string ChangePool()
        {
            if (PaChonger.JZStigmata.Count == 0 && PaChonger.JZWeapon.Count == 0 &&
                PaChonger.KC.Count == 0 && PaChonger.KC.Count == 0)
            {
                return "未获取公告内容，请先执行#更换池子 关键字 功能";
            }

            string ret_Text = "已更换 ";

            string path = CQSave.AppDirectory + "\\概率\\精准概率.txt";
            ini = new IniConfig(path);
            ini.Load();
            if (PaChonger.JZWeapon.Count != 0 && PaChonger.JZStigmata.Count != 0 &&
               PaChonger.UPAStigmata != "" && PaChonger.UPAWeapon != "")
            {
                ini.Object["详情"]["A_UpWeapon"] = new IValue(PaChonger.UPAWeapon);
                ini.Object["详情"]["A_UpStigmata"] = new IValue(PaChonger.UPAStigmata);

                int count = 0;
                for (int i = 0; i < PaChonger.JZWeapon.Count; i++)
                {
                    if (PaChonger.JZWeapon[i] == PaChonger.UPAWeapon) continue;
                    ini.Object["详情"][$"A_Weapon_Item{count}"] = new IValue(PaChonger.JZWeapon[i]);

                    count++;
                }
                count = 0;
                for (int i = 0; i < PaChonger.JZStigmata.Count; i++)
                {
                    if (PaChonger.JZStigmata[i] == PaChonger.UPAStigmata) continue;
                    ini.Object["详情"][$"A_Stigmata_Item{count}"] = new IValue(PaChonger.JZStigmata[i]);

                    count++;
                }
                ini.Save();
                ret_Text += "精准A ";
            }

            if (PaChonger.JZWeapon.Count != 0 && PaChonger.JZStigmata.Count != 0 &&
               PaChonger.UPBStigmata != "" && PaChonger.UPBWeapon != "")
            {
                ini.Object["详情"]["B_UpWeapon"] = new IValue(PaChonger.UPBWeapon);
                ini.Object["详情"]["B_UpStigmata"] = new IValue(PaChonger.UPBStigmata);

                int count = 0;
                for (int i = 0; i < PaChonger.JZWeapon.Count; i++)
                {
                    if (PaChonger.JZWeapon[i] == PaChonger.UPBWeapon) continue;
                    ini.Object["详情"][$"B_Weapon_Item{count}"] = new IValue(PaChonger.JZWeapon[i]);

                    count++;
                }
                count = 0;
                for (int i = 0; i < PaChonger.JZStigmata.Count; i++)
                {
                    if (PaChonger.JZStigmata[i] == PaChonger.UPBStigmata) continue;
                    ini.Object["详情"][$"B_Stigmata_Item{count}"] = new IValue(PaChonger.JZStigmata[i]);
                    count++;
                }
                ini.Save();
                ret_Text += "精准B ";
            }

            path = CQSave.AppDirectory + "\\概率\\扩充概率.txt";
            ini = new IniConfig(path);
            ini.Load();
            if (PaChonger.KC.Count != 0 && PaChonger.KC.Count != 0)
            {
                ini.Object["详情"]["UpS"] = new IValue(PaChonger.KC[0]);
                ini.Object["详情"]["UpA"] = new IValue(PaChonger.KC[1]);
                ini.Save();

                for (int i = 2; i < PaChonger.KC.Count; i++)
                {
                    ini.Object["详情"][$"Item{i - 2}"] = new IValue(PaChonger.KC[i]);
                    ini.Save();
                }
                ret_Text += "扩充 ";
            }
            new PaChonger().Initialize();
            return ret_Text == "已更换 " ? "获取出错" : ret_Text;
        }
        #endregion

        #region --数据库操作--
        /// <summary>
        /// 签到重置
        /// </summary>
        public static void SignReset(CQGroupMessageEventArgs e)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            Random rd = new Random();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET timestamp=0 WHERE Fromgroup={e.FromGroup.Id}", cn);
            cmd.ExecuteNonQuery();
            cn.Close();

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="id">QQ号</param>
        public static void Register(CQGroupMessageEventArgs e)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            Random rd = new Random();
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO 'UserData' VALUES({e.FromGroup.Id},{e.FromQQ.Id},0,0,{rd.Next(registermin, registermax)},0,0,0,0)", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        /// <summary>
        /// 查询目标QQ号是否已经存在于数据库
        /// </summary>
        /// <param name="id">QQ号</param>
        /// <returns></returns>
        public static bool IDExist(CQGroupMessageEventArgs e)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT count(*) FROM UserData where Fromgroup={e.FromGroup.Id} and qq={e.FromQQ.Id}", cn);
            SQLiteDataReader sr = cmd.ExecuteReader();
            sr.Read();
            int count = sr.GetInt32(0);
            sr.Close();
            return count == 1;
        }

        /// <summary>
        /// 查询目标QQ号是否已经存在于数据库
        /// </summary>
        /// <param name="id">QQ号</param>
        /// <returns></returns>
        public static bool IDExist(CQGroupMessageEventArgs e,long qqid)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT count(*) FROM UserData where Fromgroup={e.FromGroup.Id} and qq={qqid}", cn);
            SQLiteDataReader sr = cmd.ExecuteReader();
            sr.Read();
            int count = sr.GetInt32(0);
            sr.Close();
            return count == 1;
        }


        /// <summary>
        /// 获取水晶数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetDiamond(CQGroupMessageEventArgs e)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            int diamond = 0;
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT diamond FROM UserData WHERE Fromgroup={e.FromGroup.Id} and qq='{e.FromQQ.Id}'", cn);
            SQLiteDataReader sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                diamond = sr.GetInt32(0);
            }
            sr.Close();
            cn.Close();
            return diamond;
        }

        /// <summary>
        /// 获取水晶数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetDiamond(CQGroupMessageEventArgs e,long qqid)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            int diamond = 0;
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT diamond FROM UserData WHERE Fromgroup={e.FromGroup.Id} and qq='{qqid}'", cn);
            SQLiteDataReader sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                diamond = sr.GetInt32(0);
            }
            sr.Close();
            cn.Close();
            return diamond;
        }

        /// <summary>
        /// 使水晶减少
        /// </summary>
        /// <param name="id">QQ号</param>
        /// <param name="num">减少目标数量</param>
        /// <returns></returns>
        public static int SubDiamond(CQGroupMessageEventArgs e,int num)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET diamond=@diamond,total_diamond=total_diamond+{num} WHERE Fromgroup={e.FromGroup.Id} and qq='{e.FromQQ.Id}'", cn);
            cmd.Parameters.Add("diamond", DbType.Int32).Value = GetDiamond(e) - num;
            cmd.ExecuteNonQuery();
            cn.Close();
            return GetDiamond(e);
        }

        /// <summary>
        /// 为数据库 UserData 的 Count_Gacha字段加目标数量
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="id">QQ号</param>
        /// <param name="count">目标数量</param>
        public static void AddCount_Gacha(long group, long id, int count)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET gacha_count=gacha_count+{count} WHERE Fromgroup={group} and qq='{id}'", cn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 为数据库 UserData 的 Count_Sign字段加1
        /// </summary>
        /// <param name="id"></param>
        public static void AddCount_Sign(CQGroupMessageEventArgs e)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET sign_count=sign_count+1 WHERE Fromgroup={e.FromGroup.Id} and qq='{e.FromQQ.Id}'", cn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 向个人仓库加入物品
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="e"></param>
        public static void AddItem2Repositories(List<GachaResult> ls, CQGroupMessageEventArgs e)
        {
            //type 为项目类型（Weapon、Stigmata……；name为名称；class_为A、B或者S；level为等级 ；value为价值；quality为卡片颜色(0=绿，1=蓝，2=紫，3=金；date为项目最后更新时间
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            foreach (var item in ls)
            {
                string str;
                if (item.type == TypeS.debri.ToString() || item.type == TypeS.Material.ToString()) //为碎片与材料，可以叠加
                {
                    str = $"select count(*) from Repositories where name='{item.name}' and fromgroup={e.FromGroup.Id} and qq={e.FromQQ.Id}";
                    SQLiteCommand cmd = new SQLiteCommand(str, cn);
                    SQLiteDataReader sr = cmd.ExecuteReader();
                    sr.Read();
                    if (sr.GetInt32(0) != 0)
                    {
                        str = $"Update Repositories set count=count+{item.count},date='{DateTime.Now.ToString()}' where name='{item.name}' and fromgroup={e.FromGroup.Id} and qq={e.FromQQ.Id}";
                    }
                    else
                    {
                        str = $"INSERT INTO 'Repositories' VALUES({e.FromGroup.Id},{e.FromQQ.Id},'{item.type}','{item.name}','{item.class_}',{item.level},{item.value},{item.quality},{item.count},'{DateTime.Now.ToString()}')";
                    }
                    sr.Close();
                    try
                    {
                        cmd = new SQLiteCommand(str, cn);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e1)
                    {
                        CQSave.CQLog.Info("抽卡机仓库", str);
                        CQSave.CQLog.Info("抽卡机仓库", e1.Message);
                    }
                }
                else//为角色卡，武器与圣痕，不可叠加
                {
                    str = $"INSERT INTO 'Repositories' VALUES({e.FromGroup.Id},{e.FromQQ.Id},'{item.type}','{item.name}','{item.class_}',{item.level},{item.value},{item.quality},{item.count},'{DateTime.Now.ToString()}')";
                    try
                    {
                        SQLiteCommand cmd = new SQLiteCommand(str, cn);
                        cmd.ExecuteNonQuery();
                        //cq.CQLog.Info("抽卡机仓库", str);
                        if (item.quality == 2)
                        {
                            str = $"update UserData set purple_count=purple_count+1 where fromgroup='{e.FromGroup.Id}' and qq='{e.FromQQ.Id}'";
                            cmd = new SQLiteCommand(str, cn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e1)
                    {
                        CQSave.CQLog.Info("抽卡机仓库", str);
                        CQSave.CQLog.Info("抽卡机仓库", e1.Message);
                    }
                }
            }
            cn.Close();
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="id">QQ号</param>
        /// <returns></returns>
        public static int Sign(CQGroupMessageEventArgs e)
        {
            long timestamp = 0; int money = 0;
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT timestamp,diamond,sign FROM UserData WHERE Fromgroup={e.FromGroup.Id} and qq='{e.FromQQ.Id}'", cn);
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
                cmd.CommandText = $"UPDATE UserData SET diamond=@diamond,sign=@sign,timestamp=@timestamp WHERE Fromgroup={e.FromGroup.Id} and qq='{e.FromQQ.Id}'";
                cmd.Parameters.Add("diamond", DbType.Int32).Value = money + signdiamond;
                cmd.Parameters.Add("sign", DbType.Int32).Value = 1;
                cmd.Parameters.Add("timestamp", DbType.Int64).Value = GetTimeStamp();
                cmd.ExecuteNonQuery();
                return signdiamond;
            }
            cn.Close();
            return -1;
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="path">路径</param>
        public static void CreateDB(string path)
        {
            //string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            CreateTable("UserData", cn);

            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = cn;
            //type 为项目类型（Weapon、Stigmata……；name为名称；class_为A、B或者S；level为等级 ；value为价值；quality为卡片颜色(0=绿，1=蓝，2=紫，3=金；date为项目最后更新时间
            cmd.CommandText = $"CREATE TABLE Repositories(fromgroup INTEGER not null,qq INTEGER not null,type TEXT,name TEXT,class_ Text,level INTEGER,value integer,quality Integer,count INTEGER,date TEXT)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"UPDATE UserData SET total_diamond=0";
            cmd.ExecuteNonQuery();

            cn.Close();
        }

        /// <summary>
        /// 删除数据库
        /// </summary>
        static void DeleteDB()
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tablename">目标表名</param>
        static void DeleteTable(string tablename)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
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

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="tablename">目标表名</param>
        /// <param name="cn">SQLiteConnection 类型变量</param>
        static void CreateTable(string tablename, SQLiteConnection cn)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                Connection = cn,
                CommandText = $"CREATE TABLE {tablename}(fromgroup INTEGER not null,qq INTEGER not null,timestamp INTEGER,sign int,diamond int,total_diamond INTEGER,gacha_count int,sign_count int ,purple_count int)"
            };
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 检查total_diamond等字段与表Repositories是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <param name="e"></param>
        public static void CheckDB(string path, CQStartupEventArgs e)
        {
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"select sql from sqlite_master where type = 'table' and name = 'UserData'", cn);
            try
            {
                SQLiteDataReader sr = cmd.ExecuteReader();
                sr.Read();
                try
                {
                    string str = sr.GetString(0);
                    bool flag = false;
                    if (!str.Contains("total_diamond"))
                    {
                        cmd = new SQLiteCommand($"alter table UserData add 'total_diamond' Integer", cn);
                        cmd.ExecuteNonQuery();
                        flag = true;
                    }
                    if (!str.Contains("gacha_count"))
                    {
                        cmd = new SQLiteCommand($"alter table UserData add 'gacha_count' int", cn);
                        cmd.ExecuteNonQuery();
                        flag = true;
                    }
                    if (!str.Contains("sign_count"))
                    {
                        cmd = new SQLiteCommand($"alter table UserData add 'sign_count' int", cn);
                        cmd.ExecuteNonQuery();
                        flag = true;
                    }
                    if (!str.Contains("purple_count"))
                    {
                        cmd = new SQLiteCommand($"alter table UserData add 'purple_count' int", cn);
                        cmd.ExecuteNonQuery();
                        flag = true;
                    }
                    if (flag)
                    {
                        cmd.CommandText = $"UPDATE UserData SET total_diamond=0,gacha_count=0,sign_count=0,purple_count=0";
                        cmd.ExecuteNonQuery();
                        e.CQLog.Info("抽卡机数据库初始化", $"已插入新字段");
                    }
                }
                catch (Exception e2)
                {
                    CQSave.CQLog.Info("抽卡机数据库初始化", $"Error2:插入列失败，信息{e2.Message}");
                }
                sr.Close();
            }
            catch (System.InvalidOperationException e1)
            {
                //if(e1.Message== "No current row")
                e.CQLog.Info("抽卡机数据库初始化", $"Error1:插入列失败，信息{e1.Message}");
            }
            try
            {
                cmd = new SQLiteCommand($"select count(*)  from sqlite_master where type='table' and name = 'Repositories';", cn);
                SQLiteDataReader sqr = cmd.ExecuteReader();
                sqr.Read();
                if (sqr.GetInt32(0) == 0)
                {
                    sqr.Close();
                    cmd.CommandText = $"CREATE TABLE Repositories(fromgroup INTEGER not null,qq INTEGER not null,type TEXT,name TEXT,class_ Text,level INTEGER,value integer,quality Integer,count INTEGER,date TEXT)";
                    cmd.ExecuteNonQuery();

                    e.CQLog.Info("抽卡机数据库初始化", "已创建新表 Repositories");
                }
                sqr.Close();
            }
            catch (Exception e3)
            {
                e.CQLog.Info("抽卡机数据库初始化", $"Error3:创建表失败，信息{e3.Message}");
            }
            cn.Close();
        }
        #endregion
    }
}
