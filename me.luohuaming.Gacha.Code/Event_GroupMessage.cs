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

namespace me.luohuaming.Gacha.Code
{
    public class Event_GroupMessage : IGroupMessage
    {
        static CQGroupMessageEventArgs cq;
        #region --字段--
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
        string order_opengacha;
        string order_closegacha;

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

        int diamond;
        public static long testTimeLingchen;
        #endregion
        
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            cq = e;
            CQSave.cq_group = e;
            if (INIhelper.IniRead("接口", "Group", "0", $"{e.CQApi.AppDirectory}Config.ini") == "0") return;
            if (!GroupInini(e)&&!e.Message.Text.StartsWith("#抽卡开启")) return;
            ReadConfig(e);
            bool exist = IDExist(e.FromQQ.Id);
            long controlgroup = Convert.ToInt64(INIhelper.IniRead("后台群", "Id", "0", e.CQApi.AppDirectory + "\\Config.ini"));
            string str = "",INIPath=CQSave.AppDirectory+"Config.ini";
            UI.Gacha gc = new UI.Gacha();
            e.Message.Text = e.Message.Text.Replace("＃", "#");
            if (e.Message.Text.Replace(" ", "") == order_KC1)
            {
                e.Handler = true;
                if (INIhelper.IniRead("ExtraConfig", "SwitchKC1", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, KC1);
                gc.Read_Kuochong();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.KC_Gacha(),
                    gc.KC_GachaSub()
                };
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start(); CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                AddCount_Gacha(e.FromGroup.Id,e.FromQQ.Id, 1);
                string path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
                if (INIhelper.IniRead("详情", "ResultAt", "0", path) == "0")
                {
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchKC10", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, KC10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
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
                        if (ls[i].name == ls[j].name && ls[i].type != UI.Gacha.TypeS.Character.ToString())
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
                SubDiamond(cq.FromQQ.Id, 2800);
                AddCount_Gacha(e.FromGroup.Id,e.FromQQ.Id, 10);
                string path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
                if (INIhelper.IniRead("详情", "ResultAt", "0", path) == "0")
                {
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchJZA1", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZA1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(1);
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
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
                SubDiamond(cq.FromQQ.Id, 280);
                AddCount_Gacha(e.FromGroup.Id,e.FromQQ.Id, 1);

                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "A_ResultAt", "0", path) == "0")
                {
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchJZA10", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZA10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
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
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                AddCount_Gacha(e.FromGroup.Id,e.FromQQ.Id, 10);

                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "A_ResultAt", "0", path) == "0")
                {
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchJZB1", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZB1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(2);
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
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
                SubDiamond(cq.FromQQ.Id, 280);
                AddCount_Gacha(e.FromGroup.Id,e.FromQQ.Id, 1);

                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "B_ResultAt", "0", path) == "0")
                {
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchJZB10", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }

                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZB10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
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
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                AddCount_Gacha(e.FromGroup.Id,e.FromQQ.Id, 10);

                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "B_ResultAt", "0", path) == "0")
                {
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchBP10", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, BP10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
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
                        if (ls[i].name == ls[j].name && ls[i].type != UI.Gacha.TypeS.Character.ToString())
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
                    AddItem2Repositories(ls,e);
                });
                tasksql.Start();

                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                AddCount_Gacha(e.FromGroup.Id,e.FromQQ.Id, 10);
                string path = $@"{cq.CQApi.AppDirectory}\概率\标配概率.txt";
                if (INIhelper.IniRead("设置", "ResultAt", "0", path) == "0")
                {
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchBP1", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, BP1);
                gc.Read_BP();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
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
                SubDiamond(cq.FromQQ.Id, 280);
                AddCount_Gacha(e.FromGroup.Id,e.FromQQ.Id, 1);
                string path = $@"{cq.CQApi.AppDirectory}\概率\标配概率.txt";
                if (INIhelper.IniRead("详情", "ResultAt", "0", path) == "0")
                {
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                    if (INIhelper.IniRead("ExtraConfig", "TextGacha", "0", e.CQApi.AppDirectory + "\\Config.ini") == "0")
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
                diamond = Sign(e.FromQQ.Id);
                AddCount_Sign(e.FromQQ.Id);
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
                    Register(e.FromQQ.Id);
                    Random rd = new Random();
                    diamond = GetDiamond(e.FromQQ.Id);
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchQueDiamond", "1", INIPath) == "0") return;

                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                e.CQApi.SendGroupMessage(e.FromGroup, queryDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                return;
            }
            else if (e.Message.Text.Replace(" ", "") == order_signreset)
            {
                e.Handler = true;
                if (INIhelper.IniRead("ExtraConfig", "SwitchResSign", "1", INIPath) == "0") return;

                string path = $@"{cq.CQApi.AppDirectory}\Config.ini";
                int count = Convert.ToInt32(INIhelper.IniRead(e.FromGroup.Id.ToString(), "Count", "0", path));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (INIhelper.IniRead(e.FromGroup.Id.ToString(), $"Index{i}", "0", path) == e.FromQQ.Id.ToString())
                    {
                        InGroup = true;
                        break;
                    }
                }
                if (InGroup)
                {
                    SignReset();
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
                if (INIhelper.IniRead("ExtraConfig", "SwitchGetHelp", "1", INIPath) == "0") return;

                str = help.Replace(@"\n", "\n");
                e.CQApi.SendGroupMessage(e.FromGroup, str);
                return;
            }
            else if (e.Message.Text.Replace(" ", "") == order_getpool)
            {
                e.Handler = true;
                if (INIhelper.IniRead("ExtraConfig", "SwitchGetPool", "1", INIPath) == "0") return;

                string UPS, UPA, UPWeaponA, UPStigmataA, UPWeaponB, UPStigmataB;
                UPS = INIhelper.IniRead("详情", "UpS", "S角色", e.CQApi.AppDirectory + "\\概率\\扩充概率.txt");
                UPA = INIhelper.IniRead("详情", "UpA", "A角色", e.CQApi.AppDirectory + "\\概率\\扩充概率.txt");
                UPWeaponA = INIhelper.IniRead("详情", "A_UpWeapon", "四星武器", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                UPStigmataA = INIhelper.IniRead("详情", "A_UpStigmata", "四星圣痕", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                UPWeaponB = INIhelper.IniRead("详情", "B_UpWeapon", "四星武器", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                UPStigmataB = INIhelper.IniRead("详情", "B_UpStigmata", "四星圣痕", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                e.CQApi.SendGroupMessage(e.FromGroup, $"当前扩充池为 {UPS} {UPA}\n当前精准A池为 {UPWeaponA} {UPStigmataA}\n当前精准B池为 {UPWeaponB} {UPStigmataB}");
                return;
            }
            else if (e.Message.Text.StartsWith("#氪金"))
            {
                e.Handler = true;
                if (INIhelper.IniRead("ExtraConfig", "SwitchKaKin", "1", INIPath) == "0") return;                
                string path = $@"{cq.CQApi.AppDirectory}\Config.ini";
                int count = Convert.ToInt32(INIhelper.IniRead(e.FromGroup.Id.ToString(), "Count", "0", path));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (INIhelper.IniRead(e.FromGroup.Id.ToString(), $"Index{i}", "0", path) == e.FromQQ.Id.ToString())
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
                            if (!IDExist(targetId))
                            {
                                e.CQApi.SendGroupMessage(e.FromGroup, "操作对象不存在");
                                return;
                            }
                            path = $@"{cq.CQApi.AppDirectory}data.db";
                            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
                            cn.Open();
                            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET diamond=@diamond WHERE Fromgroup={cq.FromGroup.Id} and qq='{targetId}'", cn);
                            cmd.Parameters.Add("diamond", DbType.Int32).Value = GetDiamond(targetId) + countdia;
                            cmd.ExecuteNonQuery();
                            e.CQApi.SendGroupMessage(e.FromGroup, $"操作成功,为[CQ:at,qq={targetId}]充值{countdia}水晶,剩余{GetDiamond(targetId)}水晶");
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
                string path = $@"{cq.CQApi.AppDirectory}\Config.ini";
                if (INIhelper.IniRead("ExtraConfig", "ExecuteSql", "0", path) == "0") {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}]此功能未在控制台开启，拒绝操作");
                    return; }
                int count = Convert.ToInt32(INIhelper.IniRead(e.FromGroup.Id.ToString(), "Count", "0", path));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (INIhelper.IniRead(e.FromGroup.Id.ToString(), $"Index{i}", "0", path) == e.FromQQ.Id.ToString())
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
                path = $@"{cq.CQApi.AppDirectory}data.db";
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
            else if(e.Message.Text.StartsWith(order_opengacha))
            {
                e.Handler = true;
                if (INIhelper.IniRead("ExtraConfig", "SwitchOpenGroup", "1", INIPath) == "0") return;

                string path = $@"{cq.CQApi.AppDirectory}\Config.ini";
                int count = Convert.ToInt32(INIhelper.IniRead(e.FromGroup.Id.ToString(), "Count", "0", path));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (INIhelper.IniRead(e.FromGroup.Id.ToString(), $"Index{i}", "0", path) == e.FromQQ.Id.ToString())
                    {
                        InGroup = true;
                        break;
                    }
                }
                if (e.FromGroup.Id==controlgroup || InGroup)
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
                        count =Convert.ToInt32( INIhelper.IniRead("群控", "Count", "0", e.CQApi.AppDirectory + "Config.ini"));
                        path = e.CQApi.AppDirectory + "Config.ini";
                        bool flag = false;
                        for(int i=0;i<count;i++)
                        {
                            if (INIhelper.IniRead("群控", $"Item{i}", "0", path)==target.ToString())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if(flag)
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 群:{target}已经开启了，不需要重复开启");
                            return;
                        }
                        else
                        {
                            INIhelper.IniWrite("群控", "Count", (count+1).ToString(), path);
                            INIhelper.IniWrite("群控", $"Item{count}", target.ToString(), path);
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
            else if(e.Message.Text.StartsWith(order_closegacha))
            {
                e.Handler = true;
                if (INIhelper.IniRead("ExtraConfig", "SwitchCloseGroup", "1", INIPath) == "0") return;

                string path = $@"{cq.CQApi.AppDirectory}\Config.ini";
                int count = Convert.ToInt32(INIhelper.IniRead(e.FromGroup.Id.ToString(), "Count", "0", path));
                bool InGroup = false;
                for (int i = 0; i < count; i++)
                {
                    if (INIhelper.IniRead(e.FromGroup.Id.ToString(), $"Index{i}", "0", path) == e.FromQQ.Id.ToString())
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
                        if (e.Message.Text== "#抽卡关闭")
                        {
                            target = e.FromGroup.Id;
                        }
                        else
                        {
                            if(e.FromGroup.Id!=controlgroup)
                            {
                                e.CQApi.SendGroupMessage(e.FromGroup,$"[CQ:at,qq={e.FromQQ.Id}]控制的群号的操作只允许在后台群，请输入 #抽卡关闭");
                                return;
                            }
                            else
                            {
                                target = Convert.ToInt64(e.Message.Text.Substring("#抽卡关闭".Length).Trim());
                            }
                        }
                        count = Convert.ToInt32(INIhelper.IniRead("群控", "Count", "0", e.CQApi.AppDirectory + "Config.ini"));
                        path = e.CQApi.AppDirectory + "Config.ini";
                        bool flag = false;
                        for (int i = 0; i < count; i++)
                        {
                            if (INIhelper.IniRead("群控", $"Item{i}", "0", path) == target.ToString())
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
                            for(int i=0;i<count;i++)
                            {
                                long groupid =Convert.ToInt64(INIhelper.IniRead("群控", $"Item{i}", "0", path));
                                if (groupid == target) continue;
                                grouplist.Add(groupid);
                            }
                            INIhelper.IniWrite("群控", $"Count",( count - 1).ToString(), path);
                            for(int i=0;i<grouplist.Count;i++)
                            {
                                INIhelper.IniWrite("群控", $"Item{i}", grouplist[i].ToString(), path);
                            }
                            e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 群:{target}已关闭");
                            str = $"{e.FromQQ.Id}已置群{e.FromGroup.Id}关闭";

                        }
                    }
                    catch(Exception ex)
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
            else if(e.Message.Text.StartsWith("#置抽卡管理"))
            {
                e.Handler = true;
                if (INIhelper.IniRead("ExtraConfig", "SwitchOpenAdmin", "1", INIPath) == "0") return;

                if (e.FromGroup.Id == controlgroup || CheckAdmin(e))
                {
                    try
                    {
                        string[] targetid = e.Message.Text.Substring("#置抽卡管理".Length).Trim().Replace('，',',').Split(',');
                        Convert.ToInt64(targetid[1]);
                        if (targetid[2].IndexOf("[CQ:at") != -1)
                        {
                            targetid[2] = targetid[3].Replace("qq=", "").Replace("]", "");
                        }
                        else
                        {
                            Convert.ToInt64(targetid[2]);
                        }
                        if (targetid.Length != 3 && targetid.Length!=4)
                        {
                            e.CQApi.SendGroupMessage(e.FromGroup, CQApi.CQCode_At(e.FromQQ), "输入格式非法，例子(依次为群号与QQ号):#置抽卡管理,671467200,2185367837(或者@2185367837)");
                            return;
                        }
                        string path = e.CQApi.AppDirectory + "Config.ini";
                        if (GroupInini(e))
                        {
                            if (CheckAdmin(Convert.ToInt64(targetid[1]),Convert.ToInt64(targetid[2])))
                            {
                                e.CQApi.SendGroupMessage(e.FromGroup, e.FromQQ.CQCode_At(),"目标QQ在目标群已经是管理了，不需要重复设置");
                                return;
                            }
                            else
                            {
                                int count = INIhelper.IniRead($"{targetid[1]}", "Count", "0", path).ToInt32();
                                INIhelper.IniWrite($"{targetid[1]}", $"Index{count}", targetid[2], path);
                                INIhelper.IniWrite($"{targetid[1]}", $"Count", (++count).ToString(), path);
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
            else if(e.Message.Text.StartsWith("#更换池子"))
            {
                e.Handler = true;
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
            else
            {
                return;
            }
            if (controlgroup == 0) return;
            e.CQApi.SendGroupMessage(controlgroup, str);
        }
        #region --工具函数--
        /// <summary>
        /// 获取文字抽卡结果
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        string TextGacha(List<UI.Gacha.GachaResult> ls)
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
        bool GroupInini(CQGroupMessageEventArgs e)
        {
            int count = Convert.ToInt32(INIhelper.IniRead("群控", "Count", "0", e.CQApi.AppDirectory + "\\Config.ini"));
            for (int i = 0; i < count; i++)
            {
                if (e.FromGroup.Id == Convert.ToInt64(INIhelper.IniRead("群控", $"Item{i}", "0", e.CQApi.AppDirectory + "\\Config.ini")))
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
        bool CheckAdmin(CQGroupMessageEventArgs e)
        {
            string path = e.CQApi.AppDirectory + "Config.ini";
            int count = INIhelper.IniRead($"{e.FromGroup.Id}", "Count", "0", path).ToInt32();
            bool flag = false;
            for (int i = 0; i < count; i++)
            {
                if (e.FromQQ.Id == INIhelper.IniRead($"{e.FromGroup.Id}",$"Index{i}","0",path).ToInt64())
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
        bool CheckAdmin(long FromGroup,long FromQQ)
        {
            string path = cq.CQApi.AppDirectory + "Config.ini";
            int count = INIhelper.IniRead($"{FromGroup}", "Count", "0", path).ToInt32();
            bool flag = false;
            for (int i = 0; i < count; i++)
            {
                if (FromQQ == INIhelper.IniRead($"{FromGroup}", $"Index{i}", "0", path).ToInt64())
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
        void ReadConfig(CQGroupMessageEventArgs e)
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
            order_closegacha = INIhelper.IniRead("Order", "CloseGacha", "#抽卡关闭", path);
            order_opengacha = INIhelper.IniRead("Order", "OpenGacha", "#抽卡开启", path);

            KC1 = INIhelper.IniRead("Answer", "KC1", "少女祈祷中……", path).Replace("\\n", "\n");
            KC10 = INIhelper.IniRead("Answer", "KC10", "少女祈祷中……", path).Replace("\\n", "\n"); ;
            JZA1 = INIhelper.IniRead("Answer", "JZA1", "少女祈祷中……", path).Replace("\\n", "\n"); ;
            JZA10 = INIhelper.IniRead("Answer", "JZA10", "少女祈祷中……", path).Replace("\\n", "\n"); ;
            JZB1 = INIhelper.IniRead("Answer", "JZB1", "少女祈祷中……", path).Replace("\\n", "\n"); ;
            JZB10 = INIhelper.IniRead("Answer", "JZB10", "少女祈祷中……", path).Replace("\\n", "\n"); ;
            BP1 = INIhelper.IniRead("Answer", "BP1", "少女祈祷中……", path).Replace("\\n", "\n"); ;
            BP10 = INIhelper.IniRead("Answer", "BP10", "少女祈祷中……", path).Replace("\\n", "\n"); ;

            register = INIhelper.IniRead("Answer", "Register", "<@>欢迎上舰，这是你的初始资源(<#>)水晶", path).Replace("\\n", "\n"); ;
            mutiRegister = INIhelper.IniRead("Answer", "MutiRegister", "重复注册是不行的哦", path).Replace("\\n", "\n"); ;
            sign1 = INIhelper.IniRead("Answer", "Sign1", "大姐你回来了，天气这么好一起多逛逛吧", path).Replace("\\n", "\n"); ;
            sign2 = INIhelper.IniRead("Answer", "Sign2", "<@>这是你今天清扫甲板的报酬，拿好(<#>水晶)", path).Replace("\\n", "\n"); ;
            mutiSign = INIhelper.IniRead("Answer", "MutiSign", "今天的甲板挺亮的，擦一遍就行了", path).Replace("\\n", "\n"); ;
            noReg = INIhelper.IniRead("Answer", "NoReg", "<@>不是清洁工吧？来输入#抽卡注册 来上舰", path).Replace("\\n", "\n"); ;
            lowDiamond = INIhelper.IniRead("Answer", "LowDiamond", "<@>水晶不足，无法进行抽卡，你还剩余<#>水晶", path).Replace("\\n", "\n"); ;
            queryDiamond = INIhelper.IniRead("Answer", "QueryDiamond", "<@>你手头还有<#>水晶", path).Replace("\\n", "\n"); ;
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
            reset1 = INIhelper.IniRead("Answer", "Reset1", "贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍", path).Replace("\\n", "\n"); ;
            reset2 = INIhelper.IniRead("Answer", "Reset2", "草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍", path).Replace("\\n", "\n"); ;
            reset3 = INIhelper.IniRead("Answer", "Reset3", "你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍", path).Replace("\\n", "\n"); ;
            reset4 = INIhelper.IniRead("Answer", "Reset4", "由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍", path).Replace("\\n", "\n"); ;
            reset5 = INIhelper.IniRead("Answer", "Reset5", "理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍", path).Replace("\\n", "\n"); ;
            reset6 = INIhelper.IniRead("Answer", "Reset6", "希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍", path).Replace("\\n", "\n"); ;

            registermin = Convert.ToInt32(INIhelper.IniRead("GetDiamond", "RegisterMin", "0", path));
            registermax = Convert.ToInt32(INIhelper.IniRead("GetDiamond", "RegisterMax", "14000", path));
            signmin = Convert.ToInt32(INIhelper.IniRead("GetDiamond", "SignMin", "0", path));
            signmax = Convert.ToInt32(INIhelper.IniRead("GetDiamond", "SignMax", "14000", path));
        }

        /// <summary>
        /// 判断两个时间戳是否隔天
        /// </summary>
        /// <param name="dt1">用于判断的时间戳</param>
        /// <param name="dt2">实时时间戳</param>
        public bool JudgeifTimestampOverday(long dt1, long dt2)
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
        public long GetTimeStamp()
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
            if(PaChonger.JZStigmata.Count==0&& PaChonger.JZWeapon.Count==0&&
                PaChonger.KC.Count == 0 && PaChonger.KC.Count == 0)
            {
                return "未获取公告内容，请先执行#更换池子 关键字 功能";
            }

            string ret_Text = "已更换 ";

            string path = CQSave.AppDirectory + "\\概率\\精准概率.txt";
            if(PaChonger.JZWeapon.Count!=0 && PaChonger.JZStigmata.Count != 0&&
               PaChonger.UPAStigmata!="" && PaChonger.UPAWeapon != "" )
            {
                INIhelper.IniWrite("详情", "A_UpWeapon", PaChonger.UPAWeapon, path);
                INIhelper.IniWrite("详情", "A_UpStigmata", PaChonger.UPAStigmata, path);
                int count = 0;
                for(int i = 0; i < PaChonger.JZWeapon.Count; i++)
                {
                    if (PaChonger.JZWeapon[i] == PaChonger.UPAWeapon) continue;
                    INIhelper.IniWrite("详情", $"A_Weapon_Item{count}", PaChonger.JZWeapon[i], path);
                    count++;
                }
                count = 0;
                for (int i = 1; i < PaChonger.JZStigmata.Count; i++)
                {
                    if (PaChonger.JZStigmata[i] == PaChonger.UPAStigmata) continue;
                    INIhelper.IniWrite("详情", $"A_Stigmata_Item{count}", PaChonger.JZStigmata[i], path);
                    count++;
                }
                ret_Text += "精准A ";
            }
            if (PaChonger.JZWeapon.Count != 0 && PaChonger.JZStigmata.Count != 0 &&
               PaChonger.UPBStigmata != "" && PaChonger.UPBWeapon != "")
            {
                INIhelper.IniWrite("详情", "B_UpWeapon", PaChonger.UPBWeapon, path);
                INIhelper.IniWrite("详情", "B_UpStigmata", PaChonger.UPBStigmata, path);
                int count = 0;
                for (int i = 0; i < PaChonger.JZWeapon.Count; i++)
                {
                    if (PaChonger.JZWeapon[i] == PaChonger.UPBWeapon) continue;
                    INIhelper.IniWrite("详情", $"B_Weapon_Item{count}", PaChonger.JZWeapon[i], path);
                    count++;
                }
                count = 0;
                for (int i = 1; i < PaChonger.JZStigmata.Count; i++)
                {
                    if (PaChonger.JZStigmata[i] == PaChonger.UPBStigmata) continue;
                    INIhelper.IniWrite("详情", $"B_Stigmata_Item{count}", PaChonger.JZStigmata[i], path);
                    count++;
                }
                ret_Text += "精准B ";
            }
            path = CQSave.AppDirectory + "\\概率\\扩充概率.txt";
            if (PaChonger.KC.Count != 0 && PaChonger.KC.Count != 0)
            {
                INIhelper.IniWrite("详情", "UpS", PaChonger.KC[0], path);
                INIhelper.IniWrite("详情", "UpA", PaChonger.KC[1], path);

                for (int i = 1; i < PaChonger.KC.Count; i++)
                {
                    INIhelper.IniWrite("详情", $"Item{i - 1}", PaChonger.KC[i], path);
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
        void SignReset()
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            Random rd = new Random();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET timestamp=0 WHERE Fromgroup={cq.FromGroup.Id}", cn);
            cmd.ExecuteNonQuery();
            cn.Close();

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="id">QQ号</param>
        void Register(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            Random rd = new Random();
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO 'UserData' VALUES({cq.FromGroup.Id},{id},0,0,{rd.Next(registermin, registermax)},0,0,0,0)", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        /// <summary>
        /// 查询目标QQ号是否已经存在于数据库
        /// </summary>
        /// <param name="id">QQ号</param>
        /// <returns></returns>
        bool IDExist(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT count(*) FROM UserData where Fromgroup={cq.FromGroup.Id} and qq={id}", cn);
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
        public int GetDiamond(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            int diamond = 0;
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT diamond FROM UserData WHERE Fromgroup={cq.FromGroup.Id} and qq='{id}'", cn);
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
        int SubDiamond(long id, int num)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET diamond=@diamond,total_diamond=total_diamond+{num} WHERE Fromgroup={cq.FromGroup.Id} and qq='{cq.FromQQ.Id}'", cn);
            cmd.Parameters.Add("diamond", DbType.Int32).Value = GetDiamond(cq.FromQQ.Id) - num;
            cmd.ExecuteNonQuery();
            cn.Close();
            return GetDiamond(cq.FromQQ.Id);
        }

        /// <summary>
        /// 为数据库 UserData 的 Count_Gacha字段加目标数量
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="id">QQ号</param>
        /// <param name="count">目标数量</param>
        void AddCount_Gacha(long group,long id, int count)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET gacha_count=gacha_count+{count} WHERE Fromgroup={group} and qq='{id}'", cn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 为数据库 UserData 的 Count_Sign字段加1
        /// </summary>
        /// <param name="id"></param>
        void AddCount_Sign(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET sign_count=sign_count+1 WHERE Fromgroup={cq.FromGroup.Id} and qq='{cq.FromQQ.Id}'", cn);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 向个人仓库加入物品
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="e"></param>
        void AddItem2Repositories(List<UI.Gacha.GachaResult> ls,CQGroupMessageEventArgs e)
        {
            //type 为项目类型（Weapon、Stigmata……；name为名称；class_为A、B或者S；level为等级 ；value为价值；quality为卡片颜色(0=绿，1=蓝，2=紫，3=金；date为项目最后更新时间
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            foreach(var item in ls)
            {                
                string str;
                if(item.type==UI.Gacha.TypeS.debri.ToString() || item.type==UI.Gacha.TypeS.Material.ToString()) //为碎片与材料，可以叠加
                {
                    str = $"select count(*) from Repositories where name='{item.name}' and fromgroup={e.FromGroup.Id} and qq={e.FromQQ.Id}";
                    SQLiteCommand cmd=new SQLiteCommand(str,cn);
                    SQLiteDataReader sr = cmd.ExecuteReader();
                    sr.Read();
                    if(sr.GetInt32(0)!=0)
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
                        cq.CQLog.Info("抽卡机仓库", str);
                        cq.CQLog.Info("抽卡机仓库", e1.Message);
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
                        if (item.quality==2)
                        {
                            str = $"update UserData set purple_count=purple_count+1 where fromgroup='{e.FromGroup.Id}' and qq='{e.FromQQ.Id}'";
                            cmd = new SQLiteCommand(str, cn);
                            cmd.ExecuteNonQuery();  
                        }
                    }
                    catch(Exception e1)
                    {
                        cq.CQLog.Info("抽卡机仓库", str);
                        cq.CQLog.Info("抽卡机仓库", e1.Message);
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
        int Sign(long id)
        {
            long timestamp = 0; int money = 0;
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT timestamp,diamond,sign FROM UserData WHERE Fromgroup={cq.FromGroup.Id} and qq='{cq.FromQQ.Id}'", cn);
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
                cmd.CommandText = $"UPDATE UserData SET diamond=@diamond,sign=@sign,timestamp=@timestamp WHERE Fromgroup={cq.FromGroup.Id} and qq='{cq.FromQQ.Id}'";
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
            //string path = $@"{cq.CQApi.AppDirectory}data.db";
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
            string path = $@"{cq.CQApi.AppDirectory}data.db";
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
        public static void CheckDB(string path,CQStartupEventArgs e)
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
                    if(!str.Contains("total_diamond"))
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
                    if(flag)
                    {
                        cmd.CommandText = $"UPDATE UserData SET total_diamond=0,gacha_count=0,sign_count=0,purple_count=0";
                        cmd.ExecuteNonQuery();
                        e.CQLog.Info("抽卡机数据库初始化", $"已插入新字段");
                    }
                }
                catch(Exception e2)
                {
                    cq.CQLog.Info("抽卡机数据库初始化", $"Error2:插入列失败，信息{e2.Message}");
                }                
                sr.Close();            
            }
            catch(System.InvalidOperationException e1)
            {
                //if(e1.Message== "No current row")
                e.CQLog.Info("抽卡机数据库初始化", $"Error1:插入列失败，信息{e1.Message}");
            }
            try
            {
                cmd = new SQLiteCommand($"select count(*)  from sqlite_master where type='table' and name = 'Repositories';",cn);
                SQLiteDataReader sqr = cmd.ExecuteReader();
                sqr.Read();
                if (sqr.GetInt32(0)==0)
                {
                    sqr.Close();
                    cmd.CommandText = $"CREATE TABLE Repositories(fromgroup INTEGER not null,qq INTEGER not null,type TEXT,name TEXT,class_ Text,level INTEGER,value integer,quality Integer,count INTEGER,date TEXT)";
                    cmd.ExecuteNonQuery();

                    e.CQLog.Info("抽卡机数据库初始化", "已创建新表 Repositories");
                }
                sqr.Close();
            }
            catch(Exception e3)
            {
                e.CQLog.Info("抽卡机数据库初始化", $"Error3:创建表失败，信息{e3.Message}");
            }
            cn.Close();
        }
        #endregion
    }
}
