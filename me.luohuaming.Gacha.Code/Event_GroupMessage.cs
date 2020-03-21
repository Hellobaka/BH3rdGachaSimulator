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

namespace me.luohuaming.Gacha.Code
{
    public class Event_GroupMessage : IGroupMessage
    {
        static CQGroupMessageEventArgs cq;
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
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            cq = e;
            CQSave.cq_group = e;
            if (INIhelper.IniRead("接口", "Group", "0", $"{e.CQApi.AppDirectory}Config.ini") == "0") return;
            if (!GroupInini()) return;
            ReadConfig(e);
            bool exist = IDExist(e.FromQQ.Id);
            long controlgroup =Convert.ToInt64( INIhelper.IniRead("后台群", "Id", "0", e.CQApi.AppDirectory + "\\Config.ini"));
            string str = "";
            UI.Gacha gc = new UI.Gacha();
            if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_KC1)
            {
                e.Handler = true;
                if(!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                if (diamond<280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup,KC1);
                gc.Read_Kuochong();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.KC_Gacha(),
                    gc.KC_GachaSub()
                };
                CombinePng cp = new CombinePng();                
                SubDiamond(cq.FromQQ.Id, 280);
                string path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
                if (INIhelper.IniRead("详情", "ResultAt", "0", path) == "0")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 0,0, 1,diamond-280)}]");
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 0,0, 1, diamond - 280)}]");
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个扩充单抽";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_KC10)
            {
                e.Handler = true;
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
                        if (ls[i].name == ls[j].name && ls[i].type !=UI.Gacha.TypeS.Chararcter.ToString())
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
                if (INIhelper.IniRead("详情", "ResultAt", "0", path) == "0")
                {
                   e.CQApi.SendGroupMessage(e.FromGroup,$"[CQ:image,file={cp.Gacha(ls, 0,0, 10, diamond - 2800)}]");
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 0,0, 10, diamond - 2800)}]");
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个扩充十连";

            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#").ToUpper() == order_JZA1)
            {
                e.Handler = true;
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
                e.CQApi.SendGroupMessage(e.FromGroup,JZA1.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                gc.Read_Jingzhun(1);
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.JZ_GachaMain(),
                    gc.JZ_GachaMaterial()
                };
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "A_ResultAt", "0", path) == "0")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1,1, 1, diamond - 280)}]");                    
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 1,1, 1, diamond - 280)}]");

                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准单抽";

            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#").ToUpper() == order_JZA10)
            {
                e.Handler = true;
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
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "A_ResultAt", "0", path) == "0")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1,1, 10, diamond - 2800)}]");                    
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 1,1, 10, diamond - 2800)}]");
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准十连";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#").ToUpper() == order_JZB1)
            {
                e.Handler = true;
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
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "B_ResultAt", "0", path) == "0")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 2, 1, diamond - 280)}]");
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 1, 2, 1, diamond - 280)}]");

                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准单抽";

            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#").ToUpper() == order_JZB10)
            {
                e.Handler = true;
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
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 2800);
                string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "B_ResultAt", "0", path) == "0")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 2, 10, diamond - 2800)}]");
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 1, 2, 10, diamond - 2800)}]");
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准十连";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_BP10)
            {
                e.Handler = true;
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
                if (INIhelper.IniRead("详情", "ResultAt", "0", path) == "0")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 2, 0, 10, diamond - 2800)}]");
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 2, 0, 10, diamond - 2800)}]");
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个标配十连";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_BP1)
            {
                e.Handler = true;
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
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                string path = $@"{cq.CQApi.AppDirectory}\概率\标配概率.txt";
                if (INIhelper.IniRead("详情", "ResultAt", "0", path) == "0")
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 2, 0, 1, diamond - 280)}]");
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}][CQ:image,file={cp.Gacha(ls, 2, 0, 1, diamond - 280)}]");
                }
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个标配单抽";
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_sign)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = Sign(e.FromQQ.Id);
                if(diamond >= 0)
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
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_register)
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
                    e.CQApi.SendGroupMessage(e.FromGroup,mutiRegister.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_querydiamond)
            {
                e.Handler = true;
                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                    return;
                }
                diamond = GetDiamond(e.FromQQ.Id);
                e.CQApi.SendGroupMessage(e.FromGroup, queryDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_signreset)
            {
                e.Handler = true;
                string path = $@"{cq.CQApi.AppDirectory}\Config.ini";
                int count =Convert.ToInt32( INIhelper.IniRead(e.FromGroup.Id.ToString(), "Count", "0", path));
                bool InGroup = false;
                for(int i=0;i<count;i++)
                {
                    if (INIhelper.IniRead(e.FromGroup.Id.ToString(), $"Index{i}", "0", path)==e.FromQQ.Id.ToString())
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
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_help)
            {
                e.Handler = true;
                str = help.Replace(@"\n", "\n");
                e.CQApi.SendGroupMessage(e.FromGroup, str);
                return;
            }
            else if (e.Message.Text.Replace(" ", "").Replace("＃", "#") == order_getpool)
            {
                e.Handler = true;
                string UPS, UPA, UPWeaponA, UPStigmataA,UPWeaponB, UPStigmataB ;
                UPS = INIhelper.IniRead("详情", "UpS", "S角色", e.CQApi.AppDirectory + "\\概率\\扩充概率.txt");
                UPA = INIhelper.IniRead("详情", "UpA", "A角色", e.CQApi.AppDirectory + "\\概率\\扩充概率.txt");
                UPWeaponA = INIhelper.IniRead("详情", "A_UpWeapon", "四星武器", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                UPStigmataA = INIhelper.IniRead("详情", "A_UpStigmata", "四星圣痕", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                UPWeaponB = INIhelper.IniRead("详情", "B_UpWeapon", "四星武器", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                UPStigmataB = INIhelper.IniRead("详情", "B_UpStigmata", "四星圣痕", e.CQApi.AppDirectory + "\\概率\\精准概率.txt");
                e.CQApi.SendGroupMessage(e.FromGroup, $"当前扩充池为 {UPS} {UPA}\n当前精准A池为 {UPWeaponA} {UPStigmataA}\n当前精准B池为 {UPWeaponB} {UPStigmataB}");
                return;
            }
            else if(e.Message.Text.Replace("＃", "#").StartsWith("#氪金"))
            {
                e.Handler = true;
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
                if(temp.Length!=3)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup,$"[CQ:at,qq={e.FromQQ.Id}] 输入的格式不正确！请按照 #氪金 目标QQ号或者at目标 数量 的格式填写");
                    return;
                }
                else
                {
                    try
                    {
                        long targetId = Convert.ToInt64(temp[1].Replace("[CQ:at,qq=","").Replace("]",""));
                        int countdia = Convert.ToInt32(temp[2]);
                        try
                        {
                            if(!IDExist(targetId))
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
                            e.CQApi.SendGroupMessage(e.FromGroup,$"操作成功,为[CQ:at,qq={targetId}]充值{countdia}水晶,剩余{GetDiamond(targetId)}水晶");
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
            else
            {
                return;
            }
            if (controlgroup == 0) return;
            e.CQApi.SendGroupMessage(controlgroup, str);
        }

        bool GroupInini()
        {
            int count = Convert.ToInt32(INIhelper.IniRead("群控", "Count", "0", cq.CQApi.AppDirectory + "\\Config.ini"));
            for(int i=0;i<count;i++)
            {
                if (cq.FromGroup.Id == Convert.ToInt64(INIhelper.IniRead("群控", $"Item{i}", "0", cq.CQApi.AppDirectory + "\\Config.ini")))
                {
                    return true;
                }
            }
            return false;
        }

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

        void Register(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            Random rd = new Random();
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO 'UserData' VALUES({cq.FromGroup.Id},{id},0,0,{rd.Next(registermin,registermax)})", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

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

        int SubDiamond(long id,int num)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"UPDATE UserData SET diamond=@diamond WHERE Fromgroup={cq.FromGroup.Id} and qq='{cq.FromQQ.Id}'", cn);
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

        public static void CreateDB(string path)
        {
            //string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            CreateTable("UserData", cn);
            //SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO 'UserData' VALUES(863450594,{GetTimeStamp()},0,0)", cn);
            //cmd.ExecuteNonQuery();
            cn.Close();
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

        static void CreateTable(string tablename, SQLiteConnection cn)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"CREATE TABLE {tablename}(fromgroup INTEGER not null,qq INTEGER not null,timestamp INTEGER,sign int,diamond int)";
            cmd.ExecuteNonQuery();
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
