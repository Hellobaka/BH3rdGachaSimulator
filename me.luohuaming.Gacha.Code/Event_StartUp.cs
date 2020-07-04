using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Tool;
using System.IO;
using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;
using me.luohuaming.Gacha.UI;

namespace me.luohuaming.Gacha.Code
{
    public class Event_StartUp : ICQStartup
    {
        static IniConfig ini;
        public void CQStartup(object sender, CQStartupEventArgs e)
        {
            CQSave.cq_start = e;
            CQSave.AppDirectory = e.CQApi.AppDirectory;
            CQSave.ImageDirectory = GetAppImageDirectory();
            CQSave.CQLog = e.CQLog;
            CQSave.CQApi = e.CQApi;
            ini = new IniConfig(e.CQApi.AppDirectory + "Config.ini");
            ini.Load();
            string temp = ini.Object["OCR"]["app_id"].GetValueOrDefault("");
            if (temp == "")
            {
                ini.Object["OCR"]["app_id"]=new IValue("");
                ini.Object["OCR"]["app_key"]=new IValue("");
            }
            ini.Save();

            if (!File.Exists($@"{e.CQApi.AppDirectory}装备卡\框\抽卡背景.png"))
            {
                e.CQLog.Warning("错误", "数据包未安装，插件无法运行，请仔细阅读论坛插件说明安装数据包，之后重启酷Q");
            }
            else
            {
                if (!File.Exists($@"{e.CQApi.AppDirectory}data.db"))
                {
                    Event_GroupMessage.CreateDB($@"{e.CQApi.AppDirectory}data.db");
                    e.CQLog.WriteLine(Native.Sdk.Cqp.Enum.CQLogLevel.Info, "已创建数据库");
                }
                else
                {
                    FileInfo info = new FileInfo($@"{e.CQApi.AppDirectory}data.db");
                    if (info.Length == 0)
                    {
                        File.Delete($@"{e.CQApi.AppDirectory}data.db");
                        Event_GroupMessage.CreateDB($@"{e.CQApi.AppDirectory}data.db");
                        e.CQLog.WriteLine(Native.Sdk.Cqp.Enum.CQLogLevel.Info, "已创建数据库");
                        return;
                    }
                    Event_GroupMessage.CheckDB($@"{e.CQApi.AppDirectory}data.db", e);
                }
            }
            AbyssTimerHelper.Start();
        }
        public static string GetAppImageDirectory()
        {
            var ImageDirectory = Path.Combine(Environment.CurrentDirectory, "data", "image");
            return ImageDirectory;
        }
        
    }
}
