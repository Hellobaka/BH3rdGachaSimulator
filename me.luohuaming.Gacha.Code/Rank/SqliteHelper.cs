using me.luohuaming.Gacha.UI;
using System;
using System.Data.SQLite;

namespace me.luohuaming.Gacha.Code.Func
{
    public static class SqliteHelper
    {
        public static SQLiteConnection GetConnection()
        {
            string path = $@"{CQSave.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            return cn;
        }
        public static bool CloseConnection(SQLiteConnection cn)
        {
            try
            {
                cn.Close();
                return true;
            }
            catch (Exception e)
            {
                CQSave.CQLog.Info("数据库关闭", $"关闭失败，错误信息:{e.Message}");
            }
            return false;
        }
    }
}
