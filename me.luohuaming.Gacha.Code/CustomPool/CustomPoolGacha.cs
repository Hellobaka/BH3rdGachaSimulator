using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Newtonsoft.Json;
using System;
using System.IO;

namespace me.luohuaming.Gacha.Code.CustomPool
{
    public static class CustomPoolGacha
    {
        public static void CustomPool_GroupMsg(CQGroupMessageEventArgs e)
        {
            try
            {
                if (!File.Exists(Path.Combine(e.CQApi.AppDirectory, "CustomPool", "pool.json")))
                    return;
                CustomPool pool = JsonConvert.DeserializeObject<CustomPool>(File.ReadAllText(Path.Combine(e.CQApi.AppDirectory, "CustomPool", "pool.json")));
                string str = e.Message.Text;
                foreach (var item in pool.Infos)
                {
                    if (str == item.OneOrder)
                    {
                        e.Handler = true;
                        if (CustomGachaHelper.CanGacha(e, item, 1))
                            e.FromGroup.SendGroupMessage(CustomGachaHelper.GetPicPath(1, item, e));
                    }
                    else if (str == item.TenOrder)
                    {
                        e.Handler = true;
                        if (CustomGachaHelper.CanGacha(e, item, 10))
                            e.FromGroup.SendGroupMessage(CustomGachaHelper.GetPicPath(10, item, e));
                    }
                }
            }
            catch(Exception exc)
            {
                e.CQLog.Info("自定义抽卡", $"发生错误，错误信息:{exc.Message} 在 {exc.StackTrace}");
            }            
        }
    }
}
