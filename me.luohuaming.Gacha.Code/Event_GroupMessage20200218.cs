using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using me.luohuaming.Gacha.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;
using Native.Csharp.Sdk.Cqp.Model;
using Native.Csharp.Tool.IniConfig.Linq;

namespace me.luohuaming.Gacha.Code
{
    public class Event_GroupMessage : IGroupMessage
    {
        static CQGroupMessageEventArgs cq;

        string order_KC1;
        string order_KC10;
        string order_JZ1;
        string order_JZ10;
        string order_sign;
        string order_signreset;
        string order_querydiamond;
        string order_help;
        string order_getpool;
        string order_register;

        string KC1;
        string KC10;
        string JZ1;
        string JZ10;
        string sign1;
        string sign2;
        string querydiamond;
        string help;
        string register;

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

        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            cq = e;
            CQSave.cq_group = e;
            if (!GroupInini()) return;
            bool exist = IDExist(e.FromQQ.Id);
            long controlgroup =Convert.ToInt64( INIhelper.IniRead("后台群", "Id", "0", e.CQApi.AppDirectory + "\\Config.ini"));
            string str = "";
            UI.Gacha gc = new UI.Gacha();
            if (e.Message.Text.Replace(" ","")==order_KC1)
            {
                if(!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }
                int diamond = GetDiamond(e.FromQQ.Id);
                if (diamond<280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $@"[CQ:at,qq={e.FromQQ.Id}] 水晶不足，无法进行抽卡，你还剩余{diamond}水晶");
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
                e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 1,diamond-280)}]");                
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个扩充单抽";
            }
            else if (e.Message.Text.Replace(" ", "") == order_KC10)
            {
                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }
                int diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $@"[CQ:at,qq={e.FromQQ.Id}] 水晶不足，无法进行抽卡，你还剩余{diamond}水晶");
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, KC10);
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

                e.CQApi.SendGroupMessage(e.FromGroup,$"[CQ:image,file={cp.Gacha(ls, 0, 10, diamond - 2800)}]");
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个扩充十连";

            }
            else if (e.Message.Text.Replace(" ", "") == order_JZ1)
            {
                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }

                int diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $@"[CQ:at,qq={e.FromQQ.Id}] 水晶不足，无法进行抽卡，你还剩余{diamond}水晶");
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup,JZ1);
                gc.Read_Jingzhun();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.JZ_GachaMain(),
                    gc.JZ_GachaMaterial()
                };
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);

                e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 1, diamond - 280)}]");
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准单抽";

            }
            else if (e.Message.Text.Replace(" ", "") == order_JZ10)
            {
                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }

                int diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $@"[CQ:at,qq={e.FromQQ.Id}] 水晶不足，无法进行抽卡，你还剩余{diamond}水晶");
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, JZ10);
                gc.Read_Jingzhun();
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
                e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:image,file={cp.Gacha(ls, 1, 10, diamond - 2800)}]");
                cp = null;
                GC.Collect();
                str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 申请了一个精准十连";
            }
            else if (e.Message.Text.Replace(" ", "") == order_sign)
            {
                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }
                int staus = Sign(e.FromQQ.Id);
                if(staus>=0)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, sign1);
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 这是你今天清扫甲板的报酬，拿好 ({staus}水晶)");
                    str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 进行了签到";
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, "今天的甲板挺亮的，擦一遍就行了");
                    return;
                }                
            }
            else if (e.Message.Text.Replace(" ", "") == order_register)
            {
                if (!exist)
                {
                    Register(e.FromQQ.Id);
                    Random rd = new Random();
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 欢迎上舰，这是你的初始资源 {GetDiamond(e.FromQQ.Id)}水晶");
                    str = $"群号:{e.FromGroup.Id} QQ:{e.FromQQ.Id} 注册了抽卡";
                }
                else
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, "重复注册是不行的哦");
                    return;
                }
            }
            else if(e.Message.Text.Replace(" ", "") == order_querydiamond)
            {
                if (!exist)
                {
                    e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }
                e.CQApi.SendGroupMessage(e.FromGroup, $"[CQ:at,qq={e.FromQQ.Id}] 你手头还有{GetDiamond(e.FromQQ.Id)}水晶");
            }
            else if(e.Message.Text.Replace(" ", "") == order_signreset )
            {
                GroupMemberInfo gmi = e.CQApi.GetGroupMemberInfo(e.FromGroup,e.FromQQ,false);
                if (gmi.MemberType==Native.Csharp.Sdk.Cqp.Enum.QQGroupMemberType.Manage || gmi.MemberType == Native.Csharp.Sdk.Cqp.Enum.QQGroupMemberType.Creator)
                {
                    SignReset();
                    Random rd = new Random();
                    switch (rd.Next(0, 6))
                    {
                        case 0:
                            e.CQApi.SendGroupMessage(e.FromGroup, "贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍");
                            break;
                        case 1:
                            e.CQApi.SendGroupMessage(e.FromGroup, "草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍");
                            break;
                        case 2:
                            e.CQApi.SendGroupMessage(e.FromGroup, "你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍");
                            break;
                        case 3:
                            e.CQApi.SendGroupMessage(e.FromGroup, "由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍");
                            break;
                        case 4:
                            e.CQApi.SendGroupMessage(e.FromGroup, "理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍");
                            break;
                        case 5:
                            e.CQApi.SendGroupMessage(e.FromGroup, "希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍");
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
            else if (e.Message.Text.Replace(" ","")==order_help)
            {
                str = "水银抽卡人 给你抽卡的自信(～￣▽￣)～ \n合成图片以及发送图片需要一些时间，请耐心等待\n单抽是没有保底的\n#抽卡注册\n#我的水晶\n#打扫甲板（签到）\n#甲板积灰（重置签到，管理员限定）\n\r#精准单抽\n#扩充单抽\n#精准十连\n#扩充十连";
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
        void ReadConfig()
        {
                string path = $@"{cq.CQApi.AppDirectory}Config.ini";
                order_KC1 = INIhelper.IniRead("Order", "KC1", "#扩充单抽", path);
                order_KC10 = INIhelper.IniRead("Order", "KC10", "#扩充十连", path);
                order_JZ1 = INIhelper.IniRead("Order", "JZ1", "#精准单抽", path);
                order_JZ10 = INIhelper.IniRead("Order", "JZ10", "#精准十连", path);
                order_register = INIhelper.IniRead("Order", "Register", "#抽卡注册", path);
                order_sign = INIhelper.IniRead("Order", "Sign", "#打扫甲板", path);
                order_signreset = INIhelper.IniRead("Order", "SignReset", "#甲板积灰", path);
                order_querydiamond = INIhelper.IniRead("Order", "QueryDiamond", "#我的水晶", path);
                order_help = INIhelper.IniRead("Order", "Help", "#抽卡帮助", path);
                order_getpool = INIhelper.IniRead("Order", "GetPool", "#获取池子", path);

                KC1 = INIhelper.IniRead("Answer", "KC1", "少女祈祷中……", path);
                KC10 = INIhelper.IniRead("Answer", "KC10", "少女祈祷中……", path);
                JZ1 = INIhelper.IniRead("Answer", "JZ1", "少女祈祷中……", path);
                JZ10 = INIhelper.IniRead("Answer", "JZ10", "少女祈祷中……", path);
                register = INIhelper.IniRead("Answer", "Register", "<@>欢迎上舰，这是你的初始资源(<#>)水晶", path);
                sign1 = INIhelper.IniRead("Answer", "Sign1", "大姐你回来了，天气这么好一起多逛逛吧", path);
                sign2 = INIhelper.IniRead("Answer", "Sign2", "<@>这是你今天清扫甲板的报酬，拿好(<#>水晶)", path);
                querydiamond = INIhelper.IniRead("Answer", "QueryDiamond", "<@>你手头还有<#>水晶", path);
                IniObject iObject = IniObject.Load(path, Encoding.Default);     // 从指定的文件中读取 Ini 配置项, 参数1: 文件路径, 参数2: 编码格式 [默认: ANSI]
                IniValue value1 = iObject["Answer"]["Help"];
                help = value1.ToString();
                reset1 = INIhelper.IniRead("Answer", "Reset1", "贝贝龙来甲板找女王♂van，把甲板弄脏了，大家又得打扫一遍", path);
                reset2 = INIhelper.IniRead("Answer", "Reset2", "草履虫非要给鸭子做饭，厨房爆炸了，黑紫色的东西撒了一甲板，把甲板弄脏了，大家又得打扫一遍", path);
                reset3 = INIhelper.IniRead("Answer", "Reset3", "你和女武神们被从深渊扔了回来，来自深渊的炉灰把甲板弄脏了，大家又得打扫一遍", path);
                reset4 = INIhelper.IniRead("Answer", "Reset4", "由于神秘东方村庄的诅咒，你抽卡的泪水把甲板弄脏了，大家又得打扫一遍", path);
                reset5 = INIhelper.IniRead("Answer", "Reset5", "理律疯狂在甲板上逮虾户，把甲板弄脏了，大家又得打扫一遍", path);
                reset6 = INIhelper.IniRead("Answer", "Reset6", "希儿到处找不到鸭子，里人格暴走，把甲板弄脏了，大家又得打扫一遍", path);

                registermin =Convert.ToInt32( INIhelper.IniRead("GetDiamond", "RegisterMin", "0", path));
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
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO 'UserData' VALUES({cq.FromGroup.Id},{id},0,0,{rd.Next(0,14000)})", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        bool IDExist(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT count(*) FROM UserData where Fromgroup={cq.FromGroup.Id} and qq={cq.FromQQ.Id}", cn);
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
                int signdiamond = rd.Next(0, 14000);
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
