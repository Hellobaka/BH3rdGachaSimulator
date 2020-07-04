using System.Collections.Generic;

namespace me.luohuaming.Gacha.UI
{   
    public class AbyssTimer
    {
        public AbyssTimer(bool enabled, List<long> groupList, string remindText, int dayofWeek, int hour, int minute)
        {
            Enabled = enabled;
            GroupList = groupList;
            RemindText = remindText;
            DayofWeek = dayofWeek;
            Hour = hour;
            Minute = minute;
        }

        public bool Enabled { get; set; }
        public List<long> GroupList { get; set; }
        public string RemindText { get; set; }
        public int DayofWeek { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
