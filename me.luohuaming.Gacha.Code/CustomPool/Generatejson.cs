using me.luohuaming.Gacha.Code.CustomPool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Generatejson
    {
        public static int main()
        {
            CustomPool cp = new CustomPool();
            PoolInfo pool = new PoolInfo()
            {
                PoolName = "PCR",
                OneOrder = "1",
                TenOrder = "10",
                HasBaodi = true,
                BaodiNum = 10,
                InfoPicPath = @"G:\酷Q机器人插件开发\CQP-xiaoi\酷Q Pro\data\app\me.cqp.luohuaming.Gacha\装备卡\框\标配十连.png",
                PerGachaConsumption = 150
            };
            List<PoolContent> contents = new List<PoolContent>()
            {
                new PoolContent()
                {
                    Name="佩可",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=1,
                    Count=1,
                    EmptyStarCount=4,
                    BoardPath=BackGroundColor.Blue,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,
                    IsBaodi=false,
                    Probablity=10,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_105801.png"
                } ,
                new PoolContent()
                {
                    Name="凯露",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=1,
                    Count=1,
                    EmptyStarCount=4,
                    BoardPath=BackGroundColor.Blue,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,

                    IsBaodi=false,
                    Probablity=10,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_106001.png"
                } ,
                new PoolContent()
                {
                    Name="宫子",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=2,
                    Count=1,
                    EmptyStarCount=3,
                    BoardPath=BackGroundColor.Purple,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,

                    IsBaodi=true,
                    Probablity=8,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_100701.png"
                } ,new PoolContent()
                {
                    Name="雪",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=2,
                    Count=1,
                    EmptyStarCount=3,
                    BoardPath=BackGroundColor.Purple,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,

                    IsBaodi=true,
                    Probablity=8,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_100801.png"
                } ,
                new PoolContent()
                {
                    Name="可可萝",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=1,
                    Count=1,
                    EmptyStarCount=4,
                    BoardPath=BackGroundColor.Blue,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,

                    IsBaodi=false,
                    Probablity=10,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_105901.png"
                } ,
                new PoolContent()
                {
                    Name="亚丽莎",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=3,
                    Count=1,
                    EmptyStarCount=2,
                    BoardPath=BackGroundColor.MutiColor,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,

                    IsBaodi=true,
                    Probablity=2,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_106301.png"
                } ,
                new PoolContent()
                {
                    Name="璃乃",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=3,
                    Count=1,
                    EmptyStarCount=2,
                    BoardPath=BackGroundColor.MutiColor,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,

                    IsBaodi=true,
                    Probablity=2,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_101101.png"
                } ,
                new PoolContent()
                {
                    Name="真步",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=3,
                    Count=1,
                    EmptyStarCount=2,
                    BoardPath=BackGroundColor.MutiColor,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,
                    IsBaodi=true,
                    Probablity=2,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_101001.png"
                } ,
                new PoolContent()
                {
                    Name="初音",
                    Value=100,
                    CanBeFolded=false,
                    StarCount=3,
                    Count=1,
                    EmptyStarCount=2,
                    BoardPath=BackGroundColor.MutiColor,
                    Type=GachaType.Character,
                    Displaymode1=DisplayMode_1.显示星星,
                    Displaymode2=DisplayMode_2.显示个数,
                    MinNumber=1,
                    MaxNumber=1,
                    IsBaodi=true,
                    Probablity=2,
                    PicPath=@"G:\PCR解包\取\角色小图\fav_push_notif_101201.png"
                } ,
            };
            pool.PoolContents = contents;
            cp.Infos.Add(pool);
            File.WriteAllText("pool.json", JsonConvert.SerializeObject(cp));
            Console.WriteLine("Sucess!");
            return 0;
        }
    }
}
