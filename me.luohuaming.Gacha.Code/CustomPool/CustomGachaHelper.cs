using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static me.luohuaming.Gacha.UI.Gacha;


namespace me.luohuaming.Gacha.Code.CustomPool
{
    /// <summary>
    /// 自定义池,抽卡帮助类
    /// </summary>
    public class CustomGachaHelper
    {
        /// <summary>
        /// 标记保底的变量
        /// </summary>
        public static int Gacha_Baodi=1;

        /// <summary>
        /// 进行随机抽卡,有保底机制
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public static int RandomGacha(PoolInfo cp)
        {
            int probablity_total = 0;
            //计算概率和,作为取随机数的上限
            //乘10000为了加大精度,不使用NextDouble是因为NextDouble局限于0.0-1.0,对填入概率有要求,友好度down
            foreach(var item in cp.PoolContents)
            {
                probablity_total +=Convert.ToInt32(item.Probablity*10000);
            }
            int pro = new Random(GetRandomSeed()).Next(probablity_total+1);
            //用于标记遍历元素的概率之和
            //若取到的随机数小于temp值,说明概率落在了上一个元素与这个元素之间
            double temp = 0;
            //用于标记是第几个元素,由于foreach无标记进度的变量,所以需要构造一个变量
            int count = 0;
            foreach(var item in cp.PoolContents)
            {
                //保底系统
                if (Gacha_Baodi == cp.BaodiNum)
                {
                    //取出所有是保底的元素
                    var c = cp.PoolContents.Where(x => x.IsBaodi == true).ToList();
                    int probablity_Baodi = 0;
                    foreach (var item_Baodi in c)
                    {
                        probablity_total += Convert.ToInt32(item_Baodi.Probablity * 10000);
                    }
                    pro = new Random(GetRandomSeed()).Next(probablity_total + 1);
                    double temp2 = 0;
                    //从保底中再次按照概率选取一个
                    foreach(var item2 in c)
                    {
                        temp2 += item2.Probablity;

                        if ((double)pro / 10000 <= temp2)
                        {
                            //达到保底,重置保底进度
                            Gacha_Baodi = 1;
                            return cp.PoolContents.IndexOf(item2);
                        }
                    }                            
                }
                temp += item.Probablity;
                if ((double)pro / 10000 <= temp)
                {                    
                    //抽到角色，保底数目+1                    
                    Gacha_Baodi++;
                    //对象是保底,保底进度清空
                    if (item.IsBaodi)
                        Gacha_Baodi = 1;
                    return count;
                }
                count++;
            }
            return cp.PoolContents.Count - 1;
        }

        /// <summary>
        /// 反序列化获取池子对象
        /// </summary>
        /// <returns></returns>
        public static CustomPool GetCustomPool()
        {
            return JsonConvert.DeserializeObject<CustomPool>(File.ReadAllText("pool.json"));
        }

        /// <summary>
        /// 按照抽卡结果生成图片
        /// </summary>
        /// <param name="count">抽卡次数</param>
        /// <returns></returns>
        public static string GetPicPath(int count,PoolInfo poolInfo,CQGroupMessageEventArgs e)
        {
            List<PoolContent> ls = new List<PoolContent>();
            for(int i = 0; i < count; i++)
            {
                var item = poolInfo.PoolContents[RandomGacha(poolInfo)];
                ls = FoldItem(item, ls);
            }
            Event_GroupMessage.SubDiamond(e, poolInfo.PerGachaConsumption*count);
            PicHelper combine = new PicHelper();
            ls= ls.OrderByDescending(x => x.Value).ToList();
            string CQAt = poolInfo.ResultAt ? CQApi.CQCode_At(e.FromQQ).ToSendString() : string.Empty;
            return CQAt+combine.GeneratePic(poolInfo,ls, Event_GroupMessage.GetDiamond(e));
        }

        /// <summary>
        /// 判断能否抽卡，并发送一定的自定义信息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="poolInfo"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool CanGacha(CQGroupMessageEventArgs e, PoolInfo poolInfo, int count)
        {
            if (!Event_GroupMessage.GroupInini(e))
                return false;
            if (!Event_GroupMessage.IDExist(e))
            { 
                e.FromGroup.SendGroupMessage(Event_GroupMessage.noReg.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
                return false; 
            }
            int diamond = Event_GroupMessage.GetDiamond(e);
            if (diamond < count * poolInfo.PerGachaConsumption)
            {
                e.FromGroup.SendGroupMessage(Event_GroupMessage.lowDiamond.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]").Replace("<#>", diamond.ToString()));
                return false;
            }
            e.FromGroup.SendGroupMessage(Event_GroupMessage.BP10.Replace("<@>", $"[CQ:at,qq={e.FromQQ.Id}]"));
            return true;
        }
        /// <summary>
        /// 按照数目随机取，若项目可以重叠，则重叠，否则添加
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ls"></param>
        /// <returns></returns>
        public static List<PoolContent> FoldItem(PoolContent item,List<PoolContent> ls)
        {
            if (!item.CanBeFolded)
            {
                item.Count = new Random(GetRandomSeed()).Next(item.MinNumber, item.MaxNumber + 1);
                ls.Add(item);
                return ls;
            }
            else
            {
                item.Count = new Random(GetRandomSeed()).Next(item.MinNumber, item.MaxNumber + 1);
                if (ls.Where(x => x.Name == item.Name).Count() == 0)
                    ls.Add(item);
                else
                    ls.Where(x => x.Name == item.Name).FirstOrDefault().Count += item.Count;
                return ls;
            }
        }
    }
}
