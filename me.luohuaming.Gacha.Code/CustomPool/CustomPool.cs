using System;
using System.Collections.Generic;

namespace me.luohuaming.Gacha.Code.CustomPool
{
    /// <summary>
    /// 自定义池
    /// </summary>
    public class CustomPool
    {
        /// <summary>
        /// 卡池信息数组
        /// </summary>
        public List<PoolInfo> Infos = new List<PoolInfo>();
    }

    /// <summary>
    /// 自定义池信息
    /// </summary>
    public class PoolInfo:ICloneable
    {
        /// <summary>
        /// 卡池名称
        /// </summary>
        public string PoolName { get; set; }
        /// <summary>
        /// 单抽指令
        /// </summary>
        public string OneOrder { get; set; }
        /// <summary>
        /// 十连指令
        /// </summary>
        public string TenOrder { get; set; }
        /// <summary>
        /// 每抽消耗的水晶
        /// </summary>
        public int PerGachaConsumption { get; set; }
        /// <summary>
        /// 结果是否at
        /// </summary>
        public bool ResultAt { get; set; }
        /// <summary>
        /// 是否拥有保底
        /// </summary>
        public bool HasBaodi { get; set; }
        /// <summary>
        /// 保底所需要的数量
        /// </summary>
        public int BaodiNum { get; set; }
        /// <summary>
        /// 左下角的图片路径
        /// </summary>
        public string InfoPicPath { get; set; }
        /// <summary>
        /// 背景图片
        /// </summary>
        public string BackgroundImg { get; set; }
        /// <summary>
        /// 卡池内容
        /// </summary>
        public List<PoolContent> PoolContents { get; set; }

        public PoolInfo Clone()
        {
            return (PoolInfo)this.MemberwiseClone();
        }


        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 卡池内容
    /// </summary>
    public class PoolContent:ICloneable
    {
        /// <summary>
        /// 内容名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 内容价值,用于最终结果排列
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// 边框图片路径
        /// </summary>
        public string BoardPath { get; set; }
        /// <summary>
        /// 是否为保底项目
        /// </summary>
        public bool IsBaodi { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 星的数量
        /// </summary>
        public int StarCount { get; set; }
        /// <summary>
        /// 空星的数量
        /// </summary>
        public int EmptyStarCount { get; set; }
        /// <summary>
        /// 概率,百分比,无%
        /// </summary>
        public double Probablity { get; set; }
        /// <summary>
        /// 类型,加入数据库时需要使用
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 能否被折叠
        /// </summary>
        public bool CanBeFolded { get; set; }
        /// <summary>
        /// 最小数量
        /// </summary>
        public int MinNumber { get; set; }
        /// <summary>
        /// 最大数量
        /// </summary>
        public int MaxNumber { get; set; }
        /// <summary>
        /// 结果显示方式1，显示星星或评级（B、A、S……）
        /// </summary>
        public string Displaymode1 { get; set; }
        /// <summary>
        /// 结果显示方式2，显示等级或数量
        /// </summary>
        public string Displaymode2 { get; set; }        
        /// <summary>
        /// 不使用星的图标显示，A、S、SS那些
        /// </summary>
        public string BigIcon { get; set; }

        public PoolContent Clone()
        {
            return (PoolContent)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
    }

    public enum DisplayMode_1 { 显示星星, 显示评级 };
    public enum DisplayMode_2 { 显示等级,显示个数,无};
    public enum GachaType { Character, Weapon, Stigmata, Material, debri };
    
}

