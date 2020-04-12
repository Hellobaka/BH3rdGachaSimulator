using Native.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace me.luohuaming.Gacha.Code
{
    public static class ChangePool
    {
        public static class User
        {
            public static bool UsingState;
            public static long groupid;
            public static long qqid;
        }
        public static string PoolName;
        public static int TalkState=1;
        public static string GetResult(object e)
        {
            Intize(e);
            if (User.UsingState) return "使用中……";
            User.UsingState = true;
            switch(PoolName.ToUpper())
            {
                case "扩充":
                    switch(TalkState)
                    {
                        case 1:
                            TalkState++;
                            return "UPS角色";
                        case 2:
                            TalkState++;
                            return "UPA角色";
                        case 3:
                            TalkState++;
                            return "A1角色";
                        case 4:
                            TalkState++;
                            return "A2角色";
                        case 5:
                            TalkState=1;
                            return "A3角色 完成";
                        default:
                            return $"Defult返回 PoolName:{PoolName} TalkState:{TalkState}";
                    }
                case "精准A":
                    switch (TalkState)
                    {
                        case 1:
                            TalkState++;
                            return "UP武器";
                        case 2:
                            TalkState++;
                            return "UP圣痕";
                        case 3:
                            TalkState++;
                            return "武器1";
                        case 4:
                            TalkState++;
                            return "武器2";
                        case 5:
                            TalkState++;
                            return "武器3";
                        case 6:
                            TalkState++;
                            return "武器4";
                        case 7:
                            TalkState++;
                            return "武器5";
                        case 8:
                            TalkState++;
                            return "武器6";
                        case 9:
                            TalkState++;
                            return "圣痕1";
                        case 10:
                            TalkState++;
                            return "圣痕2";
                        case 11:
                            TalkState++;
                            return "圣痕3";
                        case 12:
                            TalkState = 1;
                            return "圣痕4 完成";
                        default:
                            return $"Defult返回 PoolName:{PoolName} TalkState:{TalkState}";
                    }
                case "精准B":
                    switch (TalkState)
                    {
                        case 1:
                            TalkState ++;
                            return "UP武器";
                        case 2:
                            TalkState ++;
                            return "UP圣痕";
                        case 3:
                            TalkState ++;
                            return "武器1";
                        case 4:
                            TalkState ++;
                            return "武器2";
                        case 5:
                            TalkState ++;
                            return "武器3";
                        case 6:
                            TalkState ++;
                            return "武器4";
                        case 7:
                            TalkState ++;
                            return "武器5";
                        case 8:
                            TalkState ++;
                            return "武器6";
                        case 9:
                            TalkState ++;
                            return "圣痕1";
                        case 10:
                            TalkState ++;
                            return "圣痕2";
                        case 11:
                            TalkState ++;
                            return "圣痕3";
                        case 12:
                            TalkState = 1;
                            return "圣痕4 完成";
                        default:
                            return $"Defult返回 PoolName:{PoolName} TalkState:{TalkState}";
                    }
                default:
                    return $"Defult返回 PoolName:{PoolName} TalkState:{TalkState}";
            }
        }
        static void Intize(object e)
        {
            if (e.GetType().Name == "CQGroupMessageEventArgs")
            {
                CQGroupMessageEventArgs cq = (CQGroupMessageEventArgs)e;
                User.groupid = cq.FromGroup.Id;
                User.qqid = cq.FromQQ.Id;
            }
            else
            {
                CQPrivateMessageEventArgs cq = (CQPrivateMessageEventArgs)e;
                User.groupid = -1;
                User.qqid = cq.FromQQ.Id;
            }
        }
    }
}
