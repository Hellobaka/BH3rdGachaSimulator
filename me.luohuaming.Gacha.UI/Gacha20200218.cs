using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;

namespace me.luohuaming.Gacha.UI
{
    public class Gacha
    {
        public enum TypeS { Chararcter, Weapon, Stigmata, Material, debri };
        public class GachaResult
        {
            public string name;
            public string type;
            public int count;
            public int value;
        }
        #region 扩充
        public double Probablity_KC角色卡;
        public double Probablity_KC角色碎片;
        public double Probablity_KC材料;
        public double Probablity_UpS;
        public double Probablity_UpA;
        public double Probablity_A;
        public double Probablity_Sdebris;
        public double Probablity_Adebris;

        public string Text_UpS;
        public string Text_UpA;
        public string Text_A1;
        public string Text_A2;
        public string Text_A3;
        #endregion
        #region 精准
        public double Probablity_JZUpWeapon4;
        public double Probablity_JZUpStigmata4;
        public double Probablity_JZWeapon4;
        public double Probablity_JZStigmata4;
        public double Probablity_JZWeapon3;
        public double Probablity_JZStigmata3;
        public double Probablity_JZ通用进化材料;
        public double Probablity_JZ装备经验;
        public double Probablity_JZGold;
        public double Probablity_Weapon4Up;
        public double Probablity_Weapon4Total;
        public double Probablity_Weapon3Total;
        public double Probablity_Stigmata3Total;
        public double Probablity_Weapon4;
        public double Probablity_Weapon3;
        public double Probablity_Weapon2;
        public double Probablity_Stigmata4Up;
        public double Probablity_Stigmata4Total;
        public double Probablity_Stigmata4;
        public double Probablity_Stigmata3;
        public double Probablity_Stigmata2;


        public string Text_UpWeapon;
        public string Text_UpStigmata;
        public string Text_Weapon1;
        public string Text_Weapon2;
        public string Text_Weapon3;
        public string Text_Weapon4;
        public string Text_Weapon5;
        public string Text_Weapon6;
        public string Text_Stigmata1;
        public string Text_Stigmata2;
        public string Text_Stigmata3;
        public string Text_Stigmata4;
        #endregion

        //private CQMenuCallEventArgs cq = CQSave.cq_menu;
        private CQGroupMessageEventArgs cq = CQSave.cq_group;

        public double Probablity_Material镜面;
        public double Probablity_Material紫色经验材料;
        public double Probablity_Material蓝色经验材料;
        public double Probablity_Material反应炉;
        public double Probablity_Material紫色角色经验;
        public double Probablity_Material蓝色角色经验;
        public double Probablity_Material技能材料;
        public double Probablity_Material吼咪宝藏;
        public double Probablity_Material吼美宝藏;
        public double Probablity_Material吼姆宝藏;

        public static int Count_JZ;
        public static int Count;
        public static bool GetTargetItem;

        /// <summary>
        /// 使用RNGCryptoServiceProvider生成种子
        /// </summary>
        /// <returns></returns>
        public int GetRandomSeed()
        {
            Random rd = new Random();
            byte[] bytes = new byte[rd.Next(0, 10000000)];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public GachaResult KC_Gacha()
        {
            Random rd = new Random(GetRandomSeed());
            double pro_1 = rd.Next(0, 1000000) / (double)10000;
            Count++;
            GachaResult gr = new GachaResult();
            //
            if (Count < 10 )
            {
                if (pro_1 < Probablity_KC角色卡)
                {
                    GetTargetItem = true;
                    Count = 0;
                    rd = new Random(GetRandomSeed());
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_KC角色卡) * 100000)) / (double)100000;
                    if (pro_0 < Probablity_UpS)
                    {
                        gr.name = Text_UpS; ;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.type = TypeS.Chararcter.ToString();
                        return gr;
                    }
                    else if (pro_0 < Probablity_UpA + Probablity_UpS)
                    {
                        gr.name = Text_UpA; ;
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Chararcter.ToString();
                        return gr;
                    }
                    else
                    {
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Chararcter.ToString();
                        int temp = rd.Next(3);
                        if (temp == 0)
                        {
                            gr.name = Text_A1; ;
                            return gr;
                        }
                        if (temp == 1)
                        {
                            gr.name = Text_A2;
                            return gr;
                        }
                        gr.name = Text_A3; ;
                        return gr;
                    }
                }
                else if (pro_1 < Probablity_KC角色卡 + Probablity_KC角色碎片)
                {
                    rd = new Random(GetRandomSeed());
                    if (rd.Next(0, Convert.ToInt32((Probablity_KC角色碎片) * 100000)) / (double)100000 < Probablity_Sdebris)
                    {
                        gr.name = Text_UpS + "碎片";
                        gr.value = 1400;
                        gr.count = rd.Next(7, 10);
                        gr.type = TypeS.debri.ToString();
                        return gr;
                    }
                    else
                    {
                        gr.value = 1400;
                        gr.count = rd.Next(4, 9);
                        gr.type = TypeS.debri.ToString();
                        int temp = rd.Next(4);
                        if (temp == 0) { gr.name = Text_UpA + "碎片"; return gr; }
                        if (temp == 1) { gr.name = Text_A1 + "碎片"; return gr; }
                        if (temp == 2) { gr.name = Text_A2 + "碎片"; return gr; }
                        gr.name = Text_A3 + "碎片";
                        return gr;
                    }
                }
                else
                {
                    rd = new Random(GetRandomSeed());
                    double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_KC材料) * 100000)) / (double)100000;
                    if (pro_2 < Probablity_Material技能材料)
                    {
                        gr.name = "高级技能材料";
                        gr.value = 1000;
                        gr.count = rd.Next(4, 7);
                        gr.type = TypeS.Material.ToString();
                        return gr;
                    }
                    else if (pro_2 < Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "蓝色经验材料";
                        gr.value = 800;
                        gr.count = rd.Next(4, 7);
                        gr.type = TypeS.Material.ToString();
                        return gr;
                    }
                    else if (pro_2 < Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        double pro_4 = rd.NextDouble();
                        if (pro_4 <= 0.33)
                        {
                            gr.name = "紫色角色经验";
                            gr.value = 900;
                            gr.count = rd.Next(5, 9);
                            gr.type = TypeS.Material.ToString();
                            return gr;
                        }
                        else
                        {
                            gr.name = "蓝色角色经验";
                            gr.value = 800;
                            gr.count = rd.Next(5, 9);
                            gr.type = TypeS.Material.ToString();
                            return gr;
                        }
                    }
                    else if (pro_2 < Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "吼咪宝藏";
                        gr.value = 700;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        return gr;
                    }
                    else if (pro_2 < Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "吼美宝藏";
                        gr.value = 600;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        return gr;
                    }
                    else
                    {
                        gr.name = "吼姆宝藏";
                        gr.value = 500;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        return gr;
                    }
                }
            }
            //
            else
            {
                Count = 0;
                GetTargetItem = false;
                rd = new Random(GetRandomSeed());
                double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_KC角色卡) * 100000)) / (double)100000;
                if (pro_0 < Probablity_UpS)
                {
                    gr.name = Text_UpS; ;
                    gr.value = 28000;
                    gr.count = 1;
                    gr.type = TypeS.Chararcter.ToString();
                    return gr;
                }
                else if (pro_0 < Probablity_UpA + Probablity_UpS)
                {
                    gr.name = Text_UpA; ;
                    gr.value = 2800;
                    gr.count = 1;
                    gr.type = TypeS.Chararcter.ToString();
                    return gr;
                }
                else
                {
                    gr.value = 2800;
                    gr.count = 1;
                    gr.type = TypeS.Chararcter.ToString();
                    int temp = rd.Next(3);
                    if (temp == 0)
                    {
                        gr.name = Text_A1; ;
                        return gr;
                    }
                    if (temp == 1)
                    {
                        gr.name = Text_A2;
                        return gr;
                    }
                    gr.name = Text_A3; ;
                    return gr;
                }
            }
        }

        public GachaResult JZ_GachaMain()
        {
            Random rd = new Random(GetRandomSeed());
            //初始概率1 初始值0-100.00000
            double pro_1 = rd.Next(0, 10000000) / (double)100000;
            Count_JZ++;
            GachaResult gr = new GachaResult();
            //保底标志为false且保底次数小于10
            if (Count_JZ < 10)
            {
                //概率1命中四星武器
                if (pro_1 < Probablity_Weapon4Total)
                {
                    //保底标志为真 保底次数归零
                    GetTargetItem = true;
                    Count_JZ = 0;
                    rd = new Random(GetRandomSeed());
                    //初始概率2 初始值0-四星武器概率(4.95800)                    
                    double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_Weapon4Total) * 100000)) / (double)100000;
                    //概率2命中up武器(2.479)
                    if (pro_2 < Probablity_JZUpWeapon4)
                    {
                        gr.name = Text_UpWeapon; ;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.type = TypeS.Weapon.ToString();
                        return gr;
                    }
                    //概率2命中up外武器4.958-2.479=2.479 2.479/6=0.413
                    else
                    {
                        //概率3初始化
                        double pro_3 = rd.Next(0, 6);
                        gr.value = 27000;
                        gr.count = 1;
                        gr.type = TypeS.Weapon.ToString();
                        switch (pro_3)
                        {
                            case 0:
                                gr.name = Text_Weapon1;
                                break;
                            case 1:
                                gr.name = Text_Weapon2;
                                break;
                            case 2:
                                gr.name = Text_Weapon3;
                                break;
                            case 3:
                                gr.name = Text_Weapon4;
                                break;
                            case 4:
                                gr.name = Text_Weapon5;
                                break;
                            case 5:
                                gr.name = Text_Weapon6;
                                break;
                        }
                        return gr;
                    }
                }
                //概率1命中四星圣痕 7.437+4.958=12.395   4.958-12.395
                else if (pro_1 < Probablity_Stigmata4Total + Probablity_Weapon4Total)
                {
                    //保底标志为真 保底次数归零
                    GetTargetItem = true;
                    Count_JZ = 0;
                    rd = new Random(GetRandomSeed());
                    //概率2初始化 0-(1.24000+0.31000=1.55000)
                    double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_JZUpStigmata4 + Probablity_JZStigmata4) * 100000)) / (double)100000;
                    //概率2命中Up圣痕单件(1.240) 1.240*3=3.72 7.437-3.72=3.717 3.717/12=0.30975
                    //与乘以三应该相同
                    if (pro_2 < Probablity_JZUpStigmata4)
                    {
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name = Text_UpStigmata + " 上";
                                break;
                            case 1:
                                gr.name = Text_UpStigmata + " 中";
                                break;
                            case 2:
                                gr.name = Text_UpStigmata + " 下";
                                break;
                        }
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Stigmata.ToString();
                        return gr;
                    }
                    //概率2命中普通圣痕单件(0.310)
                    else
                    {
                        string temp = "";
                        switch (rd.Next(0, 4))
                        {
                            case 0:
                                temp = Text_Stigmata1;
                                break;
                            case 1:
                                temp = Text_Stigmata2;
                                break;
                            case 2:
                                temp = Text_Stigmata3;
                                break;
                            case 3:
                                temp = Text_Stigmata4;
                                break;
                        }
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name = temp + " 上";
                                break;
                            case 1:
                                gr.name = temp + " 中";
                                break;
                            case 2:
                                gr.name = temp + " 下";
                                break;
                        }
                        gr.value = 2700;
                        gr.count = 1;
                        gr.type = TypeS.Stigmata.ToString();
                        return gr;
                    }
                }
                //概率1命中三星武器 4.958+7.437+11.231=23.626 12.395-23.626
                else if (pro_1 < Probablity_Weapon3Total + Probablity_Weapon4Total + Probablity_Stigmata4Total)
                {
                    gr.name = INIhelper.IniRead("详情", $"Weapon3_Item{rd.Next(0, 18)}", "3星武器", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
                    gr.count = 1;
                    gr.type = TypeS.Weapon.ToString();
                    gr.value = 26000;
                    return gr;
                }
                //概率1命中三星圣痕 4.958+7.437+11.231+33.694=57.32 23.626-57.32
                else if (pro_1 < Probablity_Stigmata3Total + Probablity_Weapon3Total + Probablity_Weapon4Total + Probablity_Stigmata4Total)
                {
                    gr.name = INIhelper.IniRead("详情", $"Stigmata3_Item{rd.Next(0, 12)}", "3星圣痕", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
                    switch (rd.Next(0, 3))
                    {
                        case 0:
                            gr.name += "上";
                            break;
                        case 1:
                            gr.name += "中";
                            break;
                        case 2:
                            gr.name += "下";
                            break;
                    }
                    gr.count = 1;
                    gr.type = TypeS.Stigmata.ToString();
                    gr.value = 2600;
                    return gr;
                }
                //逻辑1命中二星装备与二星圣痕 4.958+7.437+11.231+33.694+17.072+17.072+8.536=100
                //感觉逻辑上没什么问题……
                else
                {
                    rd = new Random(GetRandomSeed());
                    double pro_0 = rd.Next(0, 10000) / (double)100;
                    if (pro_0 <= 50)
                    {
                        gr.name = INIhelper.IniRead("详情", $"Weapon2_Item{rd.Next(0, 20)}", "2星武器", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
                        gr.value = 25000;
                        gr.count = 1;
                        gr.type = TypeS.Weapon.ToString();
                        return gr;
                    }
                    else
                    {
                        gr.name = INIhelper.IniRead("详情", $"Stigmata2_Item{rd.Next(0, 5)}", "2星圣痕", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name += "上";
                                break;
                            case 1:
                                gr.name += "中";
                                break;
                            case 2:
                                gr.name += "下";
                                break;
                        }
                        gr.value = 2500;
                        gr.count = 1;
                        gr.type = TypeS.Stigmata.ToString();
                        return gr;
                    }
                }
            }
            //
            else
            {
                //保底
                Count_JZ = 0;
                GetTargetItem = false;
                rd = new Random(GetRandomSeed());
                //概率0初始化 0-（4星武器+4星圣痕）
                double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_Weapon4Total + Probablity_Stigmata4Total) * 1000000)) / (double)1000000;
                //概率0命中四星武器
                if (pro_0 < Probablity_Weapon4Total)
                {
                    double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_Weapon4Total) * 1000000)) / (double)1000000;
                    if (pro_2 < Probablity_JZUpWeapon4)
                    {
                        gr.name = Text_UpWeapon; ;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.type = TypeS.Weapon.ToString();
                        return gr;
                    }
                    else
                    {
                        double pro_3 = rd.Next(0, 6);
                        gr.value = 27000;
                        gr.count = 1;
                        gr.type = TypeS.Chararcter.ToString();
                        switch (pro_3)
                        {
                            case 0:
                                gr.name = Text_Weapon1;
                                break;
                            case 1:
                                gr.name = Text_Weapon2;
                                break;
                            case 2:
                                gr.name = Text_Weapon3;
                                break;
                            case 3:
                                gr.name = Text_Weapon4;
                                break;
                            case 4:
                                gr.name = Text_Weapon5;
                                break;
                            case 5:
                                gr.name = Text_Weapon6;
                                break;
                        }
                        return gr;
                    }
                }
                else
                {
                    rd = new Random(GetRandomSeed());
                    if (rd.Next(0, Convert.ToInt32((Probablity_Stigmata4Total) * 1000000)) / (double)1000000 < Probablity_JZUpWeapon4 * 3)
                    {
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name = Text_UpStigmata + " 上";
                                break;
                            case 1:
                                gr.name = Text_UpStigmata + " 中";
                                break;
                            case 2:
                                gr.name = Text_UpStigmata + " 下";
                                break;
                        }
                        gr.value = 20000;
                        gr.count = 1;
                        gr.type = TypeS.Stigmata.ToString();
                        return gr;
                    }
                    else
                    {
                        string temp = "";
                        switch (rd.Next(0, 4))
                        {
                            case 0:
                                temp = Text_Stigmata1;
                                break;
                            case 1:
                                temp = Text_Stigmata2;
                                break;
                            case 2:
                                temp = Text_Stigmata3;
                                break;
                            case 3:
                                temp = Text_Stigmata4;
                                break;
                        }
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name = temp + " 上";
                                break;
                            case 1:
                                gr.name = temp + " 中";
                                break;
                            case 2:
                                gr.name = temp + " 下";
                                break;
                        }
                        gr.value = 19000;
                        gr.count = 1;
                        gr.type = TypeS.Stigmata.ToString();
                        return gr;
                    }
                }
            }
        }

        public GachaResult JZ_GachaMaterial()
        {
            //概率1命中通用进化材料 装备经验 金币
            Random rd = new Random(GetRandomSeed());
            double pro_0 = Probablity_JZ装备经验 + Probablity_JZ通用进化材料 + Probablity_JZGold;
            double pro_1 = rd.Next(0, Convert.ToInt32(pro_0 * 100)) / (double)100;
            GachaResult gr = new GachaResult();
            if (pro_1 < Probablity_JZ通用进化材料)
            {
                gr.name = "镜面";
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.value = 1200;
                return gr;
            }
            else if (pro_1 < Probablity_JZ装备经验 + Probablity_JZ通用进化材料)
            {
                double db = rd.NextDouble();
                gr.name = (db <= 0.5) ? "紫色经验材料" : "蓝色经验材料";
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.value = (db <= 0.5) ? 1100 : 1000;
                return gr;
            }
            else
            {
                rd = new Random(GetRandomSeed());
                double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_JZGold) * 1000)) / (double)1000;
                if (pro_2 < Probablity_Material吼咪宝藏)
                {
                    gr.name = "吼咪宝藏";
                    gr.value = 700;
                    gr.count = 1;
                    gr.type = TypeS.Material.ToString();
                    return gr;
                }
                else if (pro_2 < Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏)
                {
                    gr.name = "吼美宝藏";
                    gr.value = 600;
                    gr.count = 1;
                    gr.type = TypeS.Material.ToString();
                    return gr;
                }
                else
                {
                    gr.name = "吼姆宝藏";
                    gr.value = 500;
                    gr.count = 1;
                    gr.type = TypeS.Material.ToString();
                    return gr;
                }
            }
        }

        public void Read_Kuochong()
        {
            string path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
            Probablity_KC角色卡 = Convert.ToDouble(INIhelper.IniRead("综合概率", "角色卡", "15.00", path).Replace("%", ""));
            Probablity_KC角色碎片 = Convert.ToDouble(INIhelper.IniRead("综合概率", "角色碎片", "26.25", path).Replace("%", ""));
            Probablity_KC材料 = Convert.ToDouble(INIhelper.IniRead("综合概率", "材料", "58.75", path).Replace("%", ""));
            Probablity_UpS = Convert.ToDouble(INIhelper.IniRead("详细概率", "UpS角色", "1.50", path).Replace("%", ""));
            Probablity_UpA = Convert.ToDouble(INIhelper.IniRead("详细概率", "UpA角色", "4.50", path).Replace("%", ""));
            Probablity_A = Convert.ToDouble(INIhelper.IniRead("详细概率", "A角色", "3.00", path).Replace("%", ""));
            Probablity_Adebris = Convert.ToDouble(INIhelper.IniRead("详细概率", "UpA角色碎片", "15.00", path).Replace("%", ""));
            Probablity_Sdebris = Convert.ToDouble(INIhelper.IniRead("详细概率", "UpS角色碎片", "2.25", path).Replace("%", ""));
            Probablity_Material技能材料 = Convert.ToDouble(INIhelper.IniRead("详细概率", "技能材料", "10.00", path).Replace("%", ""));
            Probablity_Material反应炉 = Convert.ToDouble(INIhelper.IniRead("详细概率", "低星装备材料", "26.41", path).Replace("%", ""));
            Probablity_Material紫色角色经验 = Convert.ToDouble(INIhelper.IniRead("详细概率", "角色经验", "11.17", path).Replace("%", ""));
            Probablity_Material吼咪宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼咪宝藏", "2.232", path).Replace("%", ""));
            Probablity_Material吼美宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼美宝藏", "3.334", path).Replace("%", ""));
            Probablity_Material吼姆宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼姆宝藏", "5.556", path).Replace("%", ""));

            Text_UpS = INIhelper.IniRead("详情", "UpS", "S角色卡", path);
            Text_UpA = INIhelper.IniRead("详情", "UpA", "UpA角色卡", path);
            Text_A1 = INIhelper.IniRead("详情", "Item0", "A角色卡1", path);
            Text_A2 = INIhelper.IniRead("详情", "Item1", "A角色卡2", path);
            Text_A3 = INIhelper.IniRead("详情", "Item2", "A角色卡3", path);
        }
        public void Read_Jingzhun()
        {
            string path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
            Probablity_Weapon4Total = Convert.ToDouble(INIhelper.IniRead("综合概率", "4星武器", "4.958", path).Replace("%", ""));
            Probablity_Stigmata4Total = Convert.ToDouble(INIhelper.IniRead("综合概率", "4星圣痕", "7.437", path).Replace("%", ""));
            Probablity_Weapon3Total = Convert.ToDouble(INIhelper.IniRead("综合概率", "3星武器", "11.231", path).Replace("%", ""));
            Probablity_Stigmata3Total = Convert.ToDouble(INIhelper.IniRead("综合概率", "3星圣痕", "33.694", path).Replace("%", ""));
            Probablity_JZ通用进化材料 = Convert.ToDouble(INIhelper.IniRead("综合概率", "通用进化材料", "17.072", path).Replace("%", ""));
            Probablity_JZ装备经验 = Convert.ToDouble(INIhelper.IniRead("综合概率", "装备经验", "17.072", path).Replace("%", ""));
            Probablity_JZGold = Convert.ToDouble(INIhelper.IniRead("综合概率", "金币", "8.536", path).Replace("%", ""));

            Probablity_JZUpWeapon4 = Convert.ToDouble(INIhelper.IniRead("详细概率", "Up武器", "2.479", path).Replace("%", ""));
            Probablity_JZUpStigmata4 = Convert.ToDouble(INIhelper.IniRead("详细概率", "Up圣痕单件", "1.240", path).Replace("%", ""));
            Probablity_JZWeapon4 = Convert.ToDouble(INIhelper.IniRead("详细概率", "四星武器", "0.413", path).Replace("%", ""));
            Probablity_JZStigmata4 = Convert.ToDouble(INIhelper.IniRead("详细概率", "四星圣痕单件", "0.310", path).Replace("%", ""));
            Probablity_Weapon3 = Convert.ToDouble(INIhelper.IniRead("详细概率", "三星武器", "0.511", path).Replace("%", ""));
            Probablity_Stigmata3 = Convert.ToDouble(INIhelper.IniRead("详细概率", "三星圣痕单件", "0.936", path).Replace("%", ""));
            Probablity_Material镜面 = Convert.ToDouble(INIhelper.IniRead("详细概率", "镜面", "6.828", path).Replace("%", ""));
            Probablity_Material反应炉 = Convert.ToDouble(INIhelper.IniRead("详细概率", "反应炉", "10.242", path).Replace("%", ""));
            Probablity_Material紫色经验材料 = Convert.ToDouble(INIhelper.IniRead("详细概率", "紫色经验材料", "8.536", path).Replace("%", ""));
            Probablity_Material蓝色经验材料 = Convert.ToDouble(INIhelper.IniRead("详细概率", "蓝色经验材料", "8.536", path).Replace("%", ""));
            Probablity_Material吼咪宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼咪宝藏", "1.707", path).Replace("%", ""));
            Probablity_Material吼美宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼美宝藏", "2.561", path).Replace("%", ""));
            Probablity_Material吼姆宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼姆宝藏", "4.267", path).Replace("%", ""));

            Text_UpWeapon = INIhelper.IniRead("详情", "UpWeapon", "Up四星武器", path);
            Text_UpStigmata = INIhelper.IniRead("详情", "UpStigmata", "Up四星圣痕", path);
            Text_Weapon1 = INIhelper.IniRead("详情", "Weapon_Item0", "四星武器1", path);
            Text_Weapon2 = INIhelper.IniRead("详情", "Weapon_Item1", "四星武器2", path);
            Text_Weapon3 = INIhelper.IniRead("详情", "Weapon_Item2", "四星武器3", path);
            Text_Weapon4 = INIhelper.IniRead("详情", "Weapon_Item3", "四星武器4", path);
            Text_Weapon5 = INIhelper.IniRead("详情", "Weapon_Item4", "四星武器5", path);
            Text_Weapon6 = INIhelper.IniRead("详情", "Weapon_Item5", "四星武器6", path);
            Text_Stigmata1 = INIhelper.IniRead("详情", "Stigmata_Item0", "四星圣痕1", path);
            Text_Stigmata2 = INIhelper.IniRead("详情", "Stigmata_Item1", "四星圣痕2", path);
            Text_Stigmata3 = INIhelper.IniRead("详情", "Stigmata_Item2", "四星圣痕3", path);
            Text_Stigmata4 = INIhelper.IniRead("详情", "Stigmata_Item3", "四星圣痕4", path);
        }
    }
}
