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
using Native.Tool.IniConfig;

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
        static IniConfig ini;

        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            cq = e;
            CQSave.cq_private = e;

            string path = $@"{CQSave.AppDirectory}Config.ini";
            ini = new IniConfig(path);
            ini.Load();

            if (ini.Object["接口"]["Private"].GetValueOrDefault("0") == "0") return;
            bool exist = IDExist(e.FromQQ.Id);
            ReadConfig();
            UI.Gacha gc = new UI.Gacha();
            long controlgroup = Convert.ToInt64(ini.Object["后台群"]["Id"].GetValueOrDefault("0"));
            string str = "";

            if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_KC1)
            {
                e.Handler = true;
                if (ini.Object["ExrtaConfig"]["SwitchKC1"].GetValueOrDefault("1") == "0") return;
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
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                path = $@"{CQSave.AppDirectory}\概率\扩充概率.txt"; 
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1")
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
                if (ini.Object["ExrtaConfig"]["SwitchKC10"].GetValueOrDefault("1") == "0") return;

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
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                path = $@"{CQSave.AppDirectory}\概率\扩充概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1")
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
                if (ini.Object["ExrtaConfig"]["SwitchJZA1"].GetValueOrDefault("1") == "0") return;

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
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                path = $@"{CQSave.AppDirectory}\概率\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1")
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
                if (ini.Object["ExrtaConfig"]["SwitchJZA10"].GetValueOrDefault("1") == "0") return;

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
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                path = $@"{CQSave.AppDirectory}\概率\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1")
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
                if (ini.Object["ExrtaConfig"]["SwitchJZB1"].GetValueOrDefault("1") == "0") return;

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
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                path = $@"{CQSave.AppDirectory}\概率\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1")
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
                if (ini.Object["ExrtaConfig"]["SwitchJZB10"].GetValueOrDefault("1") == "0") return;

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
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                path = $@"{CQSave.AppDirectory}\概率\精准概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1")
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
                if (ini.Object["ExrtaConfig"]["SwitchBP10"].GetValueOrDefault("1") == "0") return;

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
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                path = $@"{CQSave.AppDirectory}\概率\标配概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1")
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
                if (ini.Object["ExrtaConfig"]["SwitchBP1"].GetValueOrDefault("1") == "0") return;

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
                var tasksql = new Task(() =>
                {
                    AddItem2Repositories(ls, e);
                });
                tasksql.Start();
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                path = $@"{CQSave.AppDirectory}\概率\标配概率.txt";
                ini = new IniConfig(path);
                ini.Load();

                if (ini.Object["ExtraConfig"]["TextGacha"].GetValueOrDefault("0") == "1")
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
                if (ini.Object["ExrtaConfig"]["SwitchQueDiamond"].GetValueOrDefault("1") == "0") return;

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
                if (ini.Object["ExrtaConfig"]["SwitchGetHelp"].GetValueOrDefault("1") == "0") return;

                str = help;
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_getpool)
            {
                e.Handler = true;
                if (ini.Object["ExrtaConfig"]["SwitchGetPool"].GetValueOrDefault("1") == "0") return;

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
                e.CQApi.SendPrivateMessage(e.FromQQ, $"当前扩充池为 {UPS} {UPA}\n当前精准A池为 {UPWeaponA} {UPStigmataA}\n当前精准B池为 {UPWeaponB} {UPStigmataB}");
                return;
            }
            else if (e.Message.Text.Replace("＃", "#").StartsWith("#氪金"))
            {
                e.Handler = true;
                if (ini.Object["ExrtaConfig"]["SwitchKaKin"].GetValueOrDefault("1") == "0") return;

                path = $@"{CQSave.AppDirectory}\Config.ini";
                ini = new IniConfig(path);
                ini.Load();

                string[] temp = e.Message.Text.Split(' ');
                if (temp.Length != 3)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"输入的格式不正确！请按照 #氪金 目标QQ号或者at目标 数量 的格式填写");
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
                                e.CQApi.SendPrivateMessage(e.FromQQ, "操作对象不存在");
                                return;
                            }
                            path = $@"{CQSave.AppDirectory}data.db";
                            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
                            cn.Open();
                            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET diamond=@diamond WHERE Fromgroup='-1' and qq='{targetId}'", cn);
                            cmd.Parameters.Add("diamond", DbType.Int32).Value = GetDiamond(targetId) + countdia;
                            cmd.ExecuteNonQuery();
                            e.CQApi.SendPrivateMessage(e.FromQQ, $"操作成功,为[CQ:at,qq={targetId}]充值{countdia}水晶,剩余{GetDiamond(targetId)}水晶");
                            return;
                        }
                        catch
                        {
                            e.CQApi.SendPrivateMessage(e.FromQQ, str = "操作失败了……");
                            return;
                        }
                    }
                    catch
                    {
                        e.CQApi.SendPrivateMessage(e.FromQQ, $"输入的格式不正确！请按照格式输入纯数字");
                        return;
                    }
                }
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
            string path = $@"{CQSave.AppDirectory}\Config.ini";
            ini = new IniConfig(path);
            ini.Load();
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

            KC1 = ini.Object["Answer"]["KC1"].GetValueOrDefault("少女祈祷中……");
            KC10 = ini.Object["Answer"]["KC10"].GetValueOrDefault("少女祈祷中……");
            JZA1 = ini.Object["Answer"]["JZA1"].GetValueOrDefault("少女祈祷中……");
            JZA10 = ini.Object["Answer"]["JZA10"].GetValueOrDefault("少女祈祷中……");
            JZB1 = ini.Object["Answer"]["JZB1"].GetValueOrDefault("少女祈祷中……");
            JZB10 = ini.Object["Answer"]["JZB10"].GetValueOrDefault("少女祈祷中……");
            BP1 = ini.Object["Answer"]["BP1"].GetValueOrDefault("少女祈祷中……");
            BP10 = ini.Object["Answer"]["BP10"].GetValueOrDefault("少女祈祷中……");

            register = ini.Object["Answer"]["Register"].GetValueOrDefault("<@>欢迎上舰，这是你的初始资源(<#>)水晶");
            mutiRegister = ini.Object["Answer"]["MutiRegister"].GetValueOrDefault("重复注册是不行的哦");
            sign1 = ini.Object["Answer"]["Sign1"].GetValueOrDefault("大姐你回来了，天气这么好一起多逛逛吧");
            sign2 = ini.Object["Answer"]["Sign2"].GetValueOrDefault("<@>这是你今天清扫甲板的报酬，拿好(<#>水晶)");
            mutiSign = ini.Object["Answer"]["MutiSign"].GetValueOrDefault("今天的甲板挺亮的，擦一遍就行了");
            noReg = ini.Object["Answer"]["NoReg"].GetValueOrDefault("<@>不是清洁工吧？来输入#抽卡注册 来上舰");
            lowDiamond = ini.Object["Answer"]["LowDiamond"].GetValueOrDefault("<@>水晶不足，无法进行抽卡，你还剩余<#>水晶");
            queryDiamond = ini.Object["Answer"]["QueryDiamond"].GetValueOrDefault("<@>你手头还有<#>水晶");
            help = ini.Object["Answer"]["Help"].GetValueOrDefault(@"水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n#氪金 目标账号或者at 数量(管理员限定 暂不支持自定义修改)\n#获取池子\n\n#精准单抽(A/B)大小写随意\n#扩充单抽\n#精准十连(A/B)大小写随意\n#扩充十连\n#标配单抽\n#标配十连\n#抽卡开启(在后台群后面可接群号)\n#抽卡关闭(在后台群后面可接群号)\n#置抽卡管理(示例:#置抽卡管理,群号,QQ或者at)\n#更换池子 查询公告的关键字")
                .Replace("\\", @"\");

            reset1 = ini.Object["Answer"]["Reset1"].GetValueOrDefault("贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍");
            reset2 = ini.Object["Answer"]["Reset2"].GetValueOrDefault("草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍");
            reset3 = ini.Object["Answer"]["Reset3"].GetValueOrDefault("你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍");
            reset4 = ini.Object["Answer"]["Reset4"].GetValueOrDefault("由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍");
            reset5 = ini.Object["Answer"]["Reset5"].GetValueOrDefault("理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍");
            reset6 = ini.Object["Answer"]["Reset6"].GetValueOrDefault("希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍");

            registermin = Convert.ToInt32(ini.Object["GetDiamond"]["RegisterMin"].GetValueOrDefault("0"));
            registermax = Convert.ToInt32(ini.Object["GetDiamond"]["RegisterMax"].GetValueOrDefault("14000"));
            signmin = Convert.ToInt32(ini.Object["GetDiamond"]["SignMin"].GetValueOrDefault("0"));
            signmax = Convert.ToInt32(ini.Object["GetDiamond"]["SignMax"].GetValueOrDefault("14000"));
        }

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

        void Register(long id)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            Random rd = new Random();
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO 'UserData' VALUES(-1,{id},0,0,{rd.Next(registermin, registermax)},0,0,0,0)", cn);
            cq.CQLog.Debug("sqldebug", cmd.CommandText);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        bool IDExist(long id)
        {
            string path = $@"{CQSave.AppDirectory}data.db";
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
            string path = $@"{CQSave.AppDirectory}data.db";
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
            string path = $@"{CQSave.AppDirectory}data.db";
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
            string path = $@"{CQSave.AppDirectory}data.db";
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
            string path = $@"{CQSave.AppDirectory}data.db";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

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

        void AddItem2Repositories(List<UI.Gacha.GachaResult> ls, CQPrivateMessageEventArgs e)
        {
            //type 为项目类型（Weapon、Stigmata……；name为名称；class_为A、B或者S；level为等级 ；value为价值；quality为卡片颜色(0=绿，1=蓝，2=紫，3=金；date为项目最后更新时间
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            foreach (var item in ls)
            {
                string str;
                if (item.type == UI.Gacha.TypeS.debri.ToString() || item.type == UI.Gacha.TypeS.Material.ToString()) //为碎片与材料，可以叠加
                {
                    str = $"select count(*) from Repositories where name='{item.name}' and fromgroup='-1' and qq={e.FromQQ.Id}";
                    SQLiteCommand cmd = new SQLiteCommand(str, cn);
                    SQLiteDataReader sr = cmd.ExecuteReader();
                    sr.Read();
                    if (sr.GetInt32(0) != 0)
                    {
                        str = $"Update Repositories set count=count+{item.count},date='{DateTime.Now.ToString()}' where name='{item.name}' and fromgroup='-1' and qq={e.FromQQ.Id}";
                    }
                    else
                    {
                        str = $"INSERT INTO 'Repositories' VALUES('-1',{e.FromQQ.Id},'{item.type}','{item.name}','{item.class_}',{item.level},{item.value},{item.quality},{item.count},'{DateTime.Now.ToString()}')";
                        //cq.CQLog.Debug("sqldebug", str);
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
                    str = $"INSERT INTO 'Repositories' VALUES('-1',{e.FromQQ.Id},'{item.type}','{item.name}','{item.class_}',{item.level},{item.value},{item.quality},{item.count},'{DateTime.Now.ToString()}')";
                    try
                    {
                        SQLiteCommand cmd = new SQLiteCommand(str, cn);
                        cmd.ExecuteNonQuery();
                        //cq.CQLog.Info("抽卡机仓库", str);
                        if (item.quality == 2)
                        {
                            str = $"update UserData set purple_count=purple_count+1 where fromgroup='-1' and qq='{e.FromQQ.Id}'";
                            cmd = new SQLiteCommand(str, cn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e1)
                    {
                        cq.CQLog.Info("抽卡机仓库", str);
                        cq.CQLog.Info("抽卡机仓库", e1.Message);
                    }
                }
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
