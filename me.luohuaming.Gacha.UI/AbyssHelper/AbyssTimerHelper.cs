using Native.Tool.IniConfig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace me.luohuaming.Gacha.UI
{
    public class AbyssTimerHelper
    {
        private static Timer remindTimer = new Timer();
        private static List<AbyssTimer> abyssTimers = new List<AbyssTimer>();
        public static void Start()
        {
            remindTimer.Elapsed -= RemindTimer_Elapsed;
            remindTimer.Stop();
            IniConfig ini = new IniConfig(CQSave.AppDirectory + "Config.ini"); ini.Load();

            if (File.Exists(CQSave.AppDirectory + "AbyssHelper.json"))
                abyssTimers = JsonConvert.DeserializeObject<List<AbyssTimer>>(File.ReadAllText(CQSave.AppDirectory + "AbyssHelper.json"));
            remindTimer.Interval = Convert.ToDouble(ini.Object["ExtraConfig"]["TimerInterval"].GetValueOrDefault("20"))*1000;
            remindTimer.Elapsed += RemindTimer_Elapsed;
            if (abyssTimers.Count != 0) 
            { 
                remindTimer.Start();
                CQSave.CQLog.Info("深渊提醒助手", $"定时生效,周期{remindTimer.Interval/1000}秒");
            }                
        }

        private static void RemindTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                List<AbyssTimer> ls = new List<AbyssTimer>();
                foreach(var item in abyssTimers)
                {
                    if (!item.Enabled) continue;
                    if ((int)DateTime.Now.DayOfWeek == item.DayofWeek && DateTime.Now.Hour==item.Hour && DateTime.Now.Minute==item.Minute)
                    {
                        foreach(var group in item.GroupList)
                        {
                            CQSave.CQApi.SendGroupMessage(group, item.RemindText);
                        }
                        ls.Add(item);
                    }
                }
                foreach(var item in ls)
                {
                    abyssTimers.Remove(item);
                }
                if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                {
                    Start();
                }
            }
            catch(Exception exc)
            {
                CQSave.CQLog.Info("深渊提醒助手", $"Timer出现错误，错误信息:{exc.Message}");
            }
        }
    }
}
