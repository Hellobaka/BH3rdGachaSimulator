using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;

namespace me.luohuaming.Gacha.UI
{
    public static class CQSave
    {
        public static CQMenuCallEventArgs cq_menu;
        public static CQGroupMessageEventArgs cq_group;
        public static CQStartupEventArgs cq_start;
    }
}
