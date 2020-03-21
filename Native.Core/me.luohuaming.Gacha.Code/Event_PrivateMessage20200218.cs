using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using me.luohuaming.Gacha.UI;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace me.luohuaming.Gacha.Code
{
    public class Event_PrivateMessage : IPrivateMessage
    {
        static CQPrivateMessageEventArgs cq;
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            cq = e;
            bool exist = IDExist(e.FromQQ.Id);

            UI.Gacha gc = new UI.Gacha();
            long controlgroup = Convert.ToInt64(INIhelper.IniRead("后台群", "Id", "0", e.CQApi.AppDirectory + "\\Config.ini"));
            string str = "";

            if (e.Message.Text.Replace(" ", "") == "#扩充单抽")
            {
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }
                int diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $@"水晶不足，无法进行抽卡，你还剩余{diamond}水晶");
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, "少女祈祷中……");
                gc.Read_Kuochong();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.KC_Gacha(),
                    gc.KC_GachaSub()
                };
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);
                e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 1, 1, diamond - 280)}]");
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个扩充单抽";
            }
            else if (e.Message.Text.Replace(" ", "") == "#扩充十连")
            {
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }
                int diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $@"水晶不足，无法进行抽卡，你还剩余{diamond}水晶");
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, "少女祈祷中……");
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

                e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 0, 10, diamond - 2800)}]");
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个扩充十连";

            }
            else if (e.Message.Text.Replace(" ", "") == "#精准单抽")
            {
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }

                int diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 280)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $@"水晶不足，无法进行抽卡，你还剩余{diamond}水晶");
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, "少女祈祷中……");
                gc.Read_Jingzhun();
                List<UI.Gacha.GachaResult> ls = new List<UI.Gacha.GachaResult>
                {
                    gc.JZ_GachaMain(),
                    gc.JZ_GachaMaterial()
                };
                CombinePng cp = new CombinePng();
                SubDiamond(cq.FromQQ.Id, 280);

                e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 1, 1, diamond - 280)}]");
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个精准单抽";

            }
            else if (e.Message.Text.Replace(" ", "") == "#精准十连")
            {
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }

                int diamond = GetDiamond(e.FromQQ.Id);
                if (diamond < 2800)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $@"水晶不足，无法进行抽卡，你还剩余{diamond}水晶");
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, "少女祈祷中……");
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
                e.CQApi.SendPrivateMessage(e.FromQQ, $"[CQ:image,file={cp.Gacha(ls, 1, 10, diamond - 2800)}]");
                cp = null;
                GC.Collect();
                str = $"QQ:{e.FromQQ.Id} 申请了一个精准十连";
            }
            else if (e.Message.Text.Replace(" ", "") == "#打扫甲板")
            {
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }
                int staus = Sign(e.FromQQ.Id);
                if (staus >= 0)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, "大姐你回来了，天气这么好一起多逛逛吧");
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"这是你今天清扫甲板的报酬，拿好 ({staus}水晶)");
                    str = $"QQ:{e.FromQQ.Id} 进行了签到";
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, "今天的甲板挺亮的，擦一遍就行了");
                    return;
                }
            }
            else if (e.Message.Text.Replace(" ", "") == "#抽卡注册")
            {
                if (!exist)
                {
                    Register(e.FromQQ.Id);
                    Random rd = new Random();
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"欢迎上舰，这是你的初始资源 {GetDiamond(e.FromQQ.Id)}水晶");
                    str = $"QQ:{e.FromQQ.Id} 注册了抽卡";
                }
                else
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, "重复注册是不行的哦");
                    return;
                }
            }
            else if (e.Message.Text.Replace(" ", "") == "#我的水晶")
            {
                if (!exist)
                {
                    e.CQApi.SendPrivateMessage(e.FromQQ, $"不是清洁工吧？来输入#抽卡注册 来上舰");
                    return;
                }
                e.CQApi.SendPrivateMessage(e.FromQQ, $"你手头还有{GetDiamond(e.FromQQ.Id)}水晶");
            }
            else if (e.Message.Text.Replace(" ", "") == "#抽卡帮助")
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
        void Register(long id)
        {
            string path = $@"{cq.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            Random rd = new Random();
            SQLiteCommand cmd = new SQLiteCommand($"INSERT INTO 'UserData' VALUES(-1,{id},0,0,{rd.Next(0, 14000)})", cn);
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
                int signdiamond = rd.Next(0, 14000);
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
