using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp;

namespace me.luohuaming.Gacha.UI
{
    public static class CQSave
    {
        public static CQMenuCallEventArgs cq_menu;
        public static CQGroupMessageEventArgs cq_group;
        public static CQStartupEventArgs cq_start;
        public static CQPrivateMessageEventArgs cq_private;
        public static string AppDirectory;
        public static string ImageDirectory;
        public static CQLog CQLog;
        public static CQApi CQApi;
    }
}
