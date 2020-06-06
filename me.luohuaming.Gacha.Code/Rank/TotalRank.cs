using System;
using System.Text;
using Native.Sdk.Cqp.EventArgs;
using System.Data.SQLite;
using me.luohuaming.Gacha.UI;
using Native.Sdk.Cqp;

namespace me.luohuaming.Gacha.Code.Func
{
    /// <summary>
    /// 总排行榜
    /// </summary>
    public class TotalRank
    {
        /// <summary>
        /// 获取水晶相关排行榜
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static StringBuilder GetDiamondRank(SQLiteConnection cn,CQGroupMessageEventArgs e)
        {
            long groupid = e.FromGroup.Id;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----水晶排行榜----");
            SQLiteCommand cmd = new SQLiteCommand($"SELECT diamond,qq,sign_count FROM UserData WHERE Fromgroup={groupid} order by diamond desc", cn);
            using( SQLiteDataReader sr = cmd.ExecuteReader())
            {
                int count = 1;
                if (!sr.HasRows) return sb;
                while (sr.Read())
                {
                    int diamond = sr.GetInt32(0);
                    try
                    {
                        var temp = e.FromGroup.GetGroupMemberInfo(sr.GetInt64(1));
                        string name = temp.Card ==""? temp.Nick:temp.Card;
                        int sign_count = sr.GetInt32(2);
                        sb.AppendLine($"{count}. {name}->{diamond}水 共计签到{sign_count}次");
                    }
                    catch
                    {
                        int sign_count = sr.GetInt32(2);
                        sb.AppendLine($"{count}. {CQApi.CQCode_At(sr.GetInt64(1)).ToSendString()}->{diamond}水 共计签到{sign_count}次");
                    }
                    if (count == 10) break;
                    count++;
                }
            }
            return sb;
        }
        /// <summary>
        /// 获取抽卡排行榜
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static StringBuilder GetGachaRank(SQLiteConnection cn,CQGroupMessageEventArgs e)
        {
            long groupid = e.FromGroup.Id;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----出货率排行榜----");
            SQLiteCommand cmd = new SQLiteCommand($"select 1.0*purple_count/gacha_count,qq,gacha_count from UserData where fromgroup={groupid} and 1.0*purple_count/gacha_count is not null order by 1.0*purple_count/gacha_count desc", cn);
            using (SQLiteDataReader sr = cmd.ExecuteReader())
            {
                int count = 1;
                if (!sr.HasRows) return sb;
                while (sr.Read())
                {
                    string diamond =(sr.GetDouble(0) *100).ToString("0.000");
                    //string cqcode = CQApi.CQCode_At(sr.GetInt64(1)).ToSendString();
                    try
                    {
                        var temp = e.FromGroup.GetGroupMemberInfo(sr.GetInt64(1));
                        string name = temp.Card == "" ? temp.Nick : temp.Card;
                        int gacha_count = sr.GetInt32(2);
                        sb.AppendLine($"{count}. {name} 共抽卡{gacha_count}次 综合出货率{diamond}%");
                    }
                    catch
                    {
                        int gacha_count = sr.GetInt32(2);
                        sb.AppendLine($"{count}. {sr.GetInt32(1)} 共抽卡{gacha_count}次 综合出货率{diamond}%");
                    }
                    if (count == 10) break;
                    count++;
                }
            }
            return sb;
        }

        public static string GetRank(CQGroupMessageEventArgs e)
        {
            try
            {
                var cn = SqliteHelper.GetConnection();
                StringBuilder str = GetDiamondRank(cn, e);
                str.Append(GetGachaRank(cn, e));
                SqliteHelper.CloseConnection(cn);
                return str.ToString();
            }
            catch(Exception exc)
            {
                string str = "获取出错，错误信息见日志";
                CQSave.CQLog.Info("抽卡排行榜", $"获取出错，错误信息:{exc.Message} 在 {exc.StackTrace}");
                return str;
            }
        }
    }
}
