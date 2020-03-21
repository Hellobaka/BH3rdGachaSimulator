using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace me.luohuaming.Gacha.UI
{
    public class Event_MenuCall : IMenuCall
    {
        public void MenuCall(object sender, CQMenuCallEventArgs e)
        {
            CQSave.cq_menu = e;
            抽卡 fm = new 抽卡();
            fm.Show();
        }
    }
}
