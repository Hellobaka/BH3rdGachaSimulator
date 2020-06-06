using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using me.luohuaming.Gacha.Code.CustomPool;
using me.luohuaming.Gacha.UI;

namespace me.cqp.luohuaming.Gacha.WPFUI
{
    public class Translation
    {
        public static readonly Dictionary<string, Infos> NameTranslation = new Dictionary<string, Infos>()
        {
            { "PoolName",new Infos{ChineseName="池名称",UIElementType=new TextBox(),DefaultValue="新元素..." } },
            { "OneOrder",new Infos{ChineseName="单抽指令",UIElementType=new TextBox(),DefaultValue="#单抽" } },
            { "TenOrder",new Infos{ChineseName="十连指令",UIElementType=new TextBox(),DefaultValue="#十连" } },
            { "PerGachaConsumption",new Infos{ChineseName="每抽消耗的水晶",UIElementType=new TextBox(),DefaultValue="280" } },
            { "HasBaodi",new Infos{ChineseName="是否拥有保底",UIElementType=new ComboBox(),EnmuContent= new List<string>(){"True","False"},DefaultValue="True" } },
            { "BaodiNum",new Infos{ChineseName="保底所需要的数量",UIElementType=new TextBox(),DefaultValue="10" }  },
            { "PoolContents",new Infos{ChineseName="卡池内容",UIElementType=new TextBox(),DefaultValue=new List<PoolContent>(),visible=false } },
            { "BackgroundImg",new Infos{ChineseName="背景图片",UIElementType=new TextBoxWithImg(),DefaultValue=CQSave.AppDirectory+@"装备卡\框\抽卡背景.png" } },
            { "InfoPicPath",new Infos{ChineseName="左下角的图片路径",UIElementType=new TextBoxWithImg(),DefaultValue=CQSave.AppDirectory+@"装备卡\框\标配十连.png" } },
            { "ResultAt",new Infos{ChineseName="结果是否at",UIElementType=new ComboBox(),EnmuContent= new List<string>(){"True","False"},DefaultValue="False"  } },
            { "Name",new Infos{ChineseName="内容名称",UIElementType=new TextBox(),DefaultValue="新元素..." } },
            { "Value",new Infos{ChineseName="内容价值",UIElementType=new TextBox(),DefaultValue="100" } },
            { "BoardPath",new Infos{ChineseName="边框图片路径",UIElementType=new TextBoxWithImg(),DefaultValue=CQSave.AppDirectory+@"装备卡\框\框蓝.png"  } },
            { "Level",new Infos{ChineseName="等级",UIElementType=new TextBox(),DefaultValue="1" } },
            { "Count",new Infos{ChineseName="数量",UIElementType=new TextBox(),DefaultValue="1",visible=false } },
            { "StarCount",new Infos{ChineseName="星的数量",UIElementType=new TextBox(),DefaultValue="3" } },
            { "EmptyStarCount",new Infos{ChineseName="空星的数量",UIElementType=new TextBox(),DefaultValue="1" } },
            { "BigIcon",new Infos{ChineseName="大图标(S,SS,SSS)路径",UIElementType=new TextBoxWithImg(),DefaultValue=CQSave.AppDirectory+@"装备卡\框\Star_Avatar_2M.png" } },
            { "Probablity",new Infos{ChineseName="概率,百分比,无%",UIElementType=new TextBox(),DefaultValue="10.57" } },
            { "Type",new Infos{ChineseName="类型",UIElementType=new ComboBox(),EnmuContent= new List<string>(){ "Character", "Weapon", "Stigmata", "Material", "debri" },DefaultValue="Character" } },
            { "CanBeFolded",new Infos{ChineseName="能否被折叠",UIElementType=new ComboBox(),EnmuContent= new List<string>(){"True","False"},DefaultValue="False"  } },
            { "MinNumber",new Infos{ChineseName="最小数量",UIElementType=new TextBox(),DefaultValue="1" } },
            { "MaxNumber",new Infos{ChineseName="最大数量",UIElementType=new TextBox(),DefaultValue="1" } },
            { "Displaymode1",new Infos{ChineseName="结果显示方式1，显示星星或评级（B、A、S……）",UIElementType=new ComboBox(),EnmuContent= new List<string>(){ "显示星星", "显示评级" },DefaultValue="显示星星"  } },
            { "Displaymode2",new Infos{ChineseName="结果显示方式2，显示等级或数量",UIElementType=new ComboBox(),EnmuContent= new List<string>(){ "显示等级", "显示个数","无" },DefaultValue="显示个数"  } },
            { "PicPath",new Infos{ChineseName="图片路径",UIElementType=new TextBoxWithImg(),DefaultValue=CQSave.AppDirectory } },
            { "IsBaodi",new Infos{ChineseName="是否为保底项目",UIElementType=new ComboBox(),EnmuContent= new List<string>(){"True","False"},DefaultValue="False"   } },
        };
    }
    public class Infos
    {
        public string ChineseName { get; set; }
        public UIElement UIElementType { get; set; }
        public List<string> EnmuContent { get; set; }
        public bool visible = true;
        public object DefaultValue { get; set; }
    }
}
