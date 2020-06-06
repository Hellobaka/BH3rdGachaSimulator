using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Native.Sdk.Cqp.EventArgs;
using System.Data.SQLite;
using System.Data;
using me.luohuaming.Gacha.UI;
using Native.Sdk.Cqp;

namespace me.luohuaming.Gacha.Code.Func
{
    /// <summary>
    /// 周榜
    /// </summary>
    public class WeekRank
    {
        public class Member
        {
            public long qqid;
            public long purple_count;
            public long gacha_count;
        }
        /// <summary>
        /// 获取上一个周日
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastSunday()
        {
            //不是周日,找上个周日
            DateTime dt = DateTime.Now;
            if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                while (dt.DayOfWeek != DayOfWeek.Sunday)
                {
                    dt = dt.AddDays(-1);
                }
            }
            return dt;
        }

        /// <summary>
        /// 获取SQL查询语句
        /// </summary>
        /// <param name="order">需要Select的内容</param>
        /// <param name="condition">需要Where的内容</param>
        /// <param name="orderby">需要最后Group by 或者 Order by的内容</param>
        /// <returns></returns>
        public static string GetQueryStr(string order, string condition, string orderby = "")
        {
            DateTime dt = GetLastSunday();
            StringBuilder sb = new StringBuilder();
            sb.Append($"select {order} from Repositories where {condition} and(");
            sb.Append($"date like '{dt.ToString("yyyy/M/d")}%'");
            dt = dt.AddDays(-1);
            for (int i = 0; i < 6; i++)
            {
                sb.Append($" or date like '{dt:yyyy/M/d}%'");
                dt = dt.AddDays(-1);
            }
            sb.Append(") " + orderby);
            return sb.ToString();
        }

        /// <summary>
        /// 获取周榜文本
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static StringBuilder GetWeekRankText(SQLiteConnection cn, CQGroupMessageEventArgs e)
        {
            long groupid = e.FromGroup.Id;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----抽卡周榜({GetLastSunday().AddDays(-6):yyyy-M-d} - {GetLastSunday():yyyy-M-d})----");

            SQLiteCommand cmd = new SQLiteCommand(GetQueryStr("count(*)", $"fromgroup={groupid}"), cn);
            SQLiteDataReader sr = cmd.ExecuteReader();
            sr.Read();
            int count = sr.GetInt32(0);
            sb.AppendLine($"上周本群共抽卡:{count}次 共消耗水晶:{count * 280} 大约为大伟哥捐了{count * 280 / 7640 + 1}个648");

            cmd = new SQLiteCommand(GetQueryStr("count(*)", $"fromgroup={groupid} and type='Weapon' and quality=2"), cn); sr = cmd.ExecuteReader();
            sr.Read();
            sb.AppendLine($"抽到了四星武器:{sr.GetInt32(0)}个");

            cmd = new SQLiteCommand(GetQueryStr("count(*)", $"fromgroup={groupid} and type='Stigmata' and quality=2"), cn); sr = cmd.ExecuteReader();
            sr.Read();
            sb.AppendLine($"抽到了四星圣痕:{sr.GetInt32(0)}个");

            cmd = new SQLiteCommand(GetQueryStr("count(*)", $"fromgroup={groupid} and type='Character' and class_='S'"), cn); sr = cmd.ExecuteReader();
            sr.Read();
            sb.AppendLine($"抽到了S角色:{sr.GetInt32(0)}个");

            cmd = new SQLiteCommand(GetQueryStr("count(*)", $"fromgroup={groupid} and type='Character' and class_='A'"), cn); sr = cmd.ExecuteReader();
            sr.Read();
            sb.AppendLine($"抽到了A角色:{sr.GetInt32(0)}个");


            cmd = new SQLiteCommand(GetQueryStr("count(qq),qq", $"fromgroup={groupid}", "group by qq order by count(qq) desc"), cn); sr = cmd.ExecuteReader();
            sr.Read();

            var temp = e.FromGroup.GetGroupMemberInfo(sr.GetInt64(1));
            string name = temp.Card == "" ? temp.Nick : temp.Card;
            sb.AppendLine($"抽卡次数最多的是:{name} 一共抽了{sr.GetInt32(0)}次 共花费了{sr.GetInt32(0) * 280}水晶");


            List<Member> ls = new List<Member>();
            cmd = new SQLiteCommand(GetQueryStr("count(qq),qq", $"fromgroup={groupid} and (quality=2 or class_='S')", "group by qq"), cn); sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                Member member = new Member()
                {
                    qqid = sr.GetInt64(1),
                    purple_count = sr.GetInt32(0)
                };
                ls.Add(member);
            }

            cmd = new SQLiteCommand(GetQueryStr("count(*),qq", $"fromgroup={groupid}", "group by qq"), cn); sr = cmd.ExecuteReader();
            while (sr.Read())
            {
                foreach (var item in ls)
                {
                    if (item.qqid == sr.GetInt64(1))
                        item.gacha_count = sr.GetInt32(0);
                }
            }
            ls.OrderByDescending(x => x.purple_count / (double)x.gacha_count);
            sb.AppendLine($"最欧的是:{CQApi.CQCode_At(ls[0].qqid).ToSendString()} 综合出货率为{(ls[0].purple_count / (double)ls[0].gacha_count * 100):0.000}%");

            return sb;
        }

        public static string GetWeekRank(CQGroupMessageEventArgs e)
        {
            try
            {
                var cn = SqliteHelper.GetConnection();
                var sb = GetWeekRankText(cn, e);
                SqliteHelper.CloseConnection(cn);
                return sb.ToString();

            }
            catch (Exception exc)
            {
                string str = "获取出错，错误信息见日志";
                CQSave.CQLog.Info("抽卡周榜", $"获取出错,错误信息:{exc.Message} 在 {exc.StackTrace}");
                return str;
            }
        }
    }
}
