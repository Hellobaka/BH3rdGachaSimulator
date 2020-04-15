using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;

namespace me.luohuaming.Gacha.UI
{
    public class Gacha
    {
        public enum TypeS { Character, Weapon, Stigmata, Material, debri };
        public class GachaResult
        {
            /// <summary>
            /// 抽卡项目名称
            /// </summary>
            public string name;
            /// <summary>
            /// 抽卡项目类型
            /// </summary>
            public string type;
            /// <summary>
            /// 抽卡项目等级
            /// </summary>
            public int level;
            /// <summary>
            /// 抽卡项目阶级 A与S
            /// </summary>
            public string class_;
            /// <summary>
            /// 抽卡项目品质 1为蓝色2为紫色
            /// </summary>
            public int quality;
            /// <summary>
            /// 抽卡项目计数
            /// </summary>
            public int count;
            /// <summary>
            /// 抽卡项目价值
            /// </summary>
            public int value;
            /// <summary>
            /// 抽卡项目星级
            /// </summary>
            public int evaluation;
            public bool isnew;
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
        #region 标配
        public double Probablity_BP角色卡;
        public double Probablity_BP角色碎片;
        public double Probablity_BP装备;
        public double Probablity_BP材料;

        public double Probablity_BPS;
        public double Probablity_BPA;
        public double Probablity_BPB;
        public double Probablity_BPSdebris;
        public double Probablity_BPAdebris;
        public double Probablity_BPWeapon4;
        public double Probablity_BPStigmata4;
        #endregion
        //private CQMenuCallEventArgs cq = CQSave.cq_menu;
        private CQGroupMessageEventArgs cq = CQSave.cq_group;
        #region 材料
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
        #endregion
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
            jumpin:
            GachaResult gr = new GachaResult();
            //
            if (Count < 10)
            {
                if (pro_1 < Probablity_KC角色卡)
                {
                    GetTargetItem = true;
                    Count = 0;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_UpS+Probablity_UpA+Probablity_A) * 1000)) / (double)1000;
                    if (pro_0 < Probablity_UpS)
                    {
                        gr.name = Text_UpS;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.type = TypeS.Character.ToString();
                        gr.class_ = "S";
                        gr.quality = 2;
                        return gr;
                    }
                    else if (pro_0 < Probablity_UpA + Probablity_UpS)
                    {
                        gr.name = Text_UpA; ;
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Character.ToString();
                        gr.class_ = "A";
                        gr.quality = 2;

                        return gr;
                    }
                    else
                    {
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Character.ToString();
                        gr.class_ = "A";
                        gr.quality = 2;
                        int temp = rd.Next(0,4);
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
                    if (rd.Next(0, Convert.ToInt32((Probablity_UpS + Probablity_UpA + Probablity_A) * 1000)) / (double)1000 < Probablity_Sdebris)
                    {
                        gr.name = Text_UpS + "碎片";
                        gr.value = 2700;
                        gr.count = rd.Next(7, 10);
                        gr.type = TypeS.debri.ToString();
                        gr.quality = 2;

                        return gr;
                    }
                    else
                    {
                        gr.value = 2600;
                        gr.count = rd.Next(4, 9);
                        gr.type = TypeS.debri.ToString();
                        int temp = rd.Next(4);
                        if (temp == 0) { gr.name = Text_UpA + "碎片"; return gr; }
                        if (temp == 1) { gr.name = Text_A1 + "碎片"; return gr; }
                        if (temp == 2) { gr.name = Text_A2 + "碎片"; return gr; }
                        gr.name = Text_A3 + "碎片";
                        gr.quality = 2;

                        return gr;
                    }
                }
                else
                {
                    double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_KC材料) * 1000)) / (double)1000;
                    if (pro_2 < Probablity_Material技能材料)
                    {
                        gr.name = "高级技能材料";
                        gr.value = 1000;
                        gr.count = rd.Next(4, 7);
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;
                        return gr;
                    }
                    else if (pro_2 < Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "超小型反应炉";
                        gr.value = 800;
                        gr.count = rd.Next(4, 7);
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 1;
                        gr.evaluation = 3;

                        return gr;
                    }
                    else if (pro_2 < Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        double pro_4 = rd.NextDouble();
                        if (pro_4 <= 0.33)
                        {
                            gr.name = "特级学习材料";
                            gr.value = 900;
                            gr.count = rd.Next(5, 9);
                            gr.type = TypeS.Material.ToString();
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                        else
                        {
                            gr.name = "高级学习材料";
                            gr.value = 800;
                            gr.count = rd.Next(5, 9);
                            gr.type = TypeS.Material.ToString();
                            gr.quality = 1;
                            gr.evaluation = 3;

                            return gr;
                        }
                    }
                    else if (pro_2 < Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "吼咪宝藏";
                        gr.value = 700;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else if (pro_2 < Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "吼美宝藏";
                        gr.value = 600;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else
                    {
                        gr.name = "吼里宝藏";
                        gr.value = 500;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 1;
                        gr.evaluation = 3;

                        return gr;
                    }
                }
            }
            else
            {
                string path = $@"{CQSave.AppDirectory}\概率\扩充概率.txt";
                if (INIhelper.IniRead("详情", "Baodi",  "1", path)=="1")
                {
                    Count = 0;
                    GetTargetItem = false;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_KC角色卡) * 1000)) / (double)1000;
                    if (pro_0 < Probablity_UpS)
                    {
                        gr.name = Text_UpS; ;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.class_ = "S";
                        gr.quality = 2;

                        gr.type = TypeS.Character.ToString();
                        return gr;
                    }
                    else if (pro_0 < Probablity_UpA + Probablity_UpS)
                    {
                        gr.name = Text_UpA; ;
                        gr.value = 2800;
                        gr.count = 1;
                        gr.class_ = "A";
                        gr.quality = 2;

                        gr.type = TypeS.Character.ToString();
                        return gr;
                    }
                    else
                    {
                        gr.value = 2800;
                        gr.count = 1;
                        gr.class_ = "A";
                        gr.quality = 2;

                        gr.type = TypeS.Character.ToString();
                        int temp = rd.Next(0,4);
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
                else
                {
                    Count--;
                    goto jumpin;
                }
            }
        }

        public GachaResult KC_GachaSub()
        {
            GachaResult gr = new GachaResult();
            Random rd = new Random(GetRandomSeed());
            double pro_2 = rd.Next(0, 100000) / (double)1000;
            if (pro_2 < Probablity_Material技能材料)
            {
                gr.name = "高级技能材料";
                gr.value = 1000;
                gr.count = rd.Next(2, 4);
                gr.type = TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;
                return gr;
            }
            else if (pro_2 < Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                gr.name = "超小型反应炉";
                gr.value = 800;
                gr.count = rd.Next(2, 4);
                gr.type = TypeS.Material.ToString();
                gr.quality = 1;
                gr.evaluation = 3;

                return gr;
            }
            else if (pro_2 < Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                double pro_4 = rd.NextDouble();
                if (pro_4 <= 0.33)
                {
                    gr.name = "特级学习材料";
                    gr.value = 900;
                    gr.count = rd.Next(3, 6);
                    gr.type = TypeS.Material.ToString();
                    gr.quality = 2;
                    gr.evaluation = 4;

                    return gr;
                }
                else
                {
                    gr.name = "高级学习材料";
                    gr.value = 800;
                    gr.count = rd.Next(3, 6);
                    gr.type = TypeS.Material.ToString();
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
            }
            else if (pro_2 < Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                gr.name = "吼咪宝藏";
                gr.value = 700;
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_2 < Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                gr.name = "吼美宝藏";
                gr.value = 600;
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_2 < Probablity_Material吼姆宝藏 + Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                gr.name = "吼里宝藏";
                gr.value = 500;
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.quality = 1;
                gr.evaluation = 3;
                return gr;
            }
            else
            {
                double pro_0 = rd.Next(0, 10000) / (double)100;
                if (pro_0 <= 50)
                {
                    int count = Convert.ToInt32(INIhelper.IniRead( "详情", "Count_Weapon2","0", $@"{CQSave.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Weapon2_Item{rd.Next(0, count)}", "2星武器", $@"{CQSave.AppDirectory}\概率\精准概率.txt");
                    gr.value = 2500;
                    gr.count = 1;
                    gr.type = TypeS.Weapon.ToString();
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 2;

                    return gr;
                }
                else
                {
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Stigmata2", "0", $@"{CQSave.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Stigmata2_Item{rd.Next(0, count)}", "2星圣痕", $@"{CQSave.AppDirectory}\概率\精准概率.txt");
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
                    gr.value = 2400;
                    gr.count = 1;
                    gr.type = TypeS.Stigmata.ToString();
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 2;

                    return gr;
                }
            }

        }

        public GachaResult JZ_GachaMain()
        {
            Random rd = new Random(GetRandomSeed());
            double pro_1 = rd.Next(0, 10000000) / (double)100000;
            Count_JZ++;
        jumpin:
            GachaResult gr = new GachaResult();

            if (Count_JZ < 10)
            {
                if (pro_1 < Probablity_Weapon4Total)
                {
                    GetTargetItem = true;
                    Count_JZ = 0;
                    double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_Weapon4Total) * 100000)) / (double)100000;
                    if (pro_2 < Probablity_JZUpWeapon4)
                    {
                        gr.name = Text_UpWeapon; ;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.level = 10;
                        gr.type = TypeS.Weapon.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;
                        return gr;
                    }
                    else
                    {
                        double pro_3 = rd.Next(0, 6);
                        gr.value = 27000;
                        gr.count = 1;
                        gr.level = 1;
                        gr.type = TypeS.Weapon.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

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
                else if (pro_1 < Probablity_Stigmata4Total + Probablity_Weapon4Total)
                {
                    GetTargetItem = true;
                    Count_JZ = 0;
                    rd = new Random(GetRandomSeed());
                    double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_JZUpStigmata4 + Probablity_JZStigmata4) * 100000)) / (double)100000;
                    if (pro_2 < Probablity_JZUpStigmata4)
                    {
                        switch (rd.Next(0, 3))
                        {
                            case 0:
                                gr.name = Text_UpStigmata + "上";
                                break;
                            case 1:
                                gr.name = Text_UpStigmata + "中";
                                break;
                            case 2:
                                gr.name = Text_UpStigmata + "下";
                                break;
                        }
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Stigmata.ToString();
                        gr.level = 10;
                        gr.quality = 2;
                        gr.evaluation = 4;

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
                                gr.name = temp + "上";
                                break;
                            case 1:
                                gr.name = temp + "中";
                                break;
                            case 2:
                                gr.name = temp + "下";
                                break;
                        }
                        gr.value = 2700;
                        gr.count = 1;
                        gr.type = TypeS.Stigmata.ToString();
                        gr.level = 1;
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                }
                else if (pro_1 < Probablity_Weapon3Total + Probablity_Weapon4Total + Probablity_Stigmata4Total)
                {
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Weapon3", "0", $@"{CQSave.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Weapon3_Item{rd.Next(0, count)}", "3星武器", $@"{CQSave.AppDirectory}\概率\精准概率.txt");
                    gr.count = 1;
                    gr.type = TypeS.Weapon.ToString();
                    gr.value = 26000;
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
                else if (pro_1 < Probablity_Stigmata3Total + Probablity_Weapon3Total + Probablity_Weapon4Total + Probablity_Stigmata4Total)
                {
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Stigmata3", "0", $@"{CQSave.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Stigmata3_Item{rd.Next(0, count)}", "3星圣痕", $@"{CQSave.AppDirectory}\概率\精准概率.txt");
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
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
                else
                {
                    double pro_0 = rd.Next(0, 10000) / (double)100;
                    if (pro_0 <= 50)
                    {
                        int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Weapon2", "0", $@"{CQSave.AppDirectory}\概率\精准概率.txt"));
                        gr.name = INIhelper.IniRead("详情", $"Weapon2_Item{rd.Next(0, count)}", "2星武器", $@"{CQSave.AppDirectory}\概率\精准概率.txt");
                        gr.value = 25000;
                        gr.count = 1;
                        gr.type = TypeS.Weapon.ToString();
                        gr.level = 1;
                        gr.quality = 1;
                        gr.evaluation = 2;

                        return gr;
                    }
                    else
                    {
                        int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Stigmata2", "0", $@"{CQSave.AppDirectory}\概率\精准概率.txt"));
                        gr.name = INIhelper.IniRead("详情", $"Stigmata2_Item{rd.Next(0, count)}", "2星圣痕", $@"{CQSave.AppDirectory}\概率\精准概率.txt");
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
                        gr.level = 1;
                        gr.quality = 1;
                        gr.evaluation = 2;

                        return gr;
                    }
                }
            }
            else
            {
                string path = $@"{CQSave.AppDirectory}\概率\精准概率.txt";
                if (INIhelper.IniRead("详情", "Baodi", "1", path) == "1")
                {
                    Count_JZ = 0;
                    GetTargetItem = false;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_Weapon4Total + Probablity_Stigmata4Total) * 1000000)) / (double)1000000;
                    if (pro_0 < Probablity_Weapon4Total)
                    {
                        double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_Weapon4Total) * 1000000)) / (double)1000000;
                        if (pro_2 < Probablity_JZUpWeapon4)
                        {
                            gr.name = Text_UpWeapon; ;
                            gr.value = 28000;
                            gr.count = 1;
                            gr.type = TypeS.Weapon.ToString();
                            gr.level = 10;
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                        else
                        {
                            double pro_3 = rd.Next(0, 6);
                            gr.value = 27000;
                            gr.count = 1;
                            gr.type = TypeS.Weapon.ToString();
                            gr.level = 1;
                            gr.quality = 2;
                            gr.evaluation = 4;

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
                        if (rd.Next(0, Convert.ToInt32((Probablity_Stigmata4Total) * 1000000)) / (double)1000000 < Probablity_JZUpWeapon4)
                        {
                            switch (rd.Next(0, 3))
                            {
                                case 0:
                                    gr.name = Text_UpStigmata + "上";
                                    break;
                                case 1:
                                    gr.name = Text_UpStigmata + "中";
                                    break;
                                case 2:
                                    gr.name = Text_UpStigmata + "下";
                                    break;
                            }
                            gr.value = 20000;
                            gr.count = 1;
                            gr.type = TypeS.Stigmata.ToString();
                            gr.level = 10;
                            gr.quality = 2;
                            gr.evaluation = 4;

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
                                    gr.name = temp + "上";
                                    break;
                                case 1:
                                    gr.name = temp + "中";
                                    break;
                                case 2:
                                    gr.name = temp + "下";
                                    break;
                            }
                            gr.value = 19000;
                            gr.count = 1;
                            gr.type = TypeS.Stigmata.ToString();
                            gr.level = 1;
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                    }
                }
                else
                {
                    Count_JZ--;
                    goto jumpin;
                }
            }
        }

        public GachaResult JZ_GachaMaterial()
        {
            Random rd = new Random(GetRandomSeed());
            double pro_0 = Probablity_JZ装备经验 + Probablity_JZ通用进化材料 + Probablity_JZGold;
            double pro_1 = rd.Next(0, Convert.ToInt32(pro_0 * 100)) / (double)100;
            GachaResult gr = new GachaResult();
            if (pro_1 < Probablity_JZ通用进化材料)
            {
                gr.name = "相转移镜面";
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.value = 1200;
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_1 < Probablity_JZ装备经验 + Probablity_JZ通用进化材料)
            {
                double db = rd.NextDouble();
                gr.name = (db <= 0.5) ? "双子灵魂结晶" : "灵魂结晶";
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.value = (db <= 0.5) ? 1100 : 1000;
                gr.quality = (db <= 0.5) ? 2 : 1;
                gr.evaluation = (db <= 0.5) ? 4 : 3;
                return gr;
            }
            else
            {
                double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_JZGold) * 1000)) / (double)1000;
                if (pro_2 < Probablity_Material吼咪宝藏)
                {
                    gr.name = "吼咪宝藏";
                    gr.value = 700;
                    gr.count = 1;
                    gr.type = TypeS.Material.ToString();
                    gr.quality = 2;
                    gr.evaluation = 4;

                    return gr;
                }
                else if (pro_2 < Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏)
                {
                    gr.name = "吼美宝藏";
                    gr.value = 600;
                    gr.count = 1;
                    gr.type = TypeS.Material.ToString();
                    gr.quality = 2;
                    gr.evaluation = 4;

                    return gr;
                }
                else
                {
                    gr.name = "吼里宝藏";
                    gr.value = 500;
                    gr.count = 1;
                    gr.type = TypeS.Material.ToString();
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
            }
        }

        public GachaResult BP_GachaMain()
        {
            Random rd = new Random(GetRandomSeed());
            double pro_1 = rd.Next(0, 1000000) / (double)10000;
            Count++;
        jumpin:
            GachaResult gr = new GachaResult();
            if (Count < 10)
            {
                if (pro_1 < Probablity_BP角色卡)
                {
                    GetTargetItem = true;                    
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_BP角色卡) * 1000)) / (double)1000;
                    if (pro_0 < Probablity_BPS)
                    {
                        Count = 0;
                        gr.name = GetBPCharacter("S");
                        gr.value = 28000;
                        gr.count = 1;
                        gr.type = TypeS.Character.ToString();
                        gr.class_ = "S";
                        gr.quality = 2;
                        return gr;
                    }
                    else if (pro_0 < Probablity_BPA + Probablity_BPS)
                    {
                        Count = 0;
                        gr.name = GetBPCharacter("A");
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Character.ToString();
                        gr.class_ = "A";
                        gr.quality = 2;
                        return gr;
                    }
                    else if (pro_1 < Probablity_BP角色卡 + Probablity_BP角色碎片 + Probablity_BP装备)
                    {
                        double pro_2 = rd.Next(0, (int)(10000 * (Probablity_BPWeapon4 + Probablity_BPStigmata4))) / (double)10000;
                        string path = $@"{CQSave.AppDirectory}概率\标配概率.txt";
                        if (pro_2 < Probablity_BPWeapon4)
                        {
                            gr.name = GetBPWeapon();
                            gr.value = 2400;
                            gr.count = 1;
                            gr.type = TypeS.Weapon.ToString();
                            gr.quality = 2;
                            gr.evaluation = 4;
                            return gr;
                        }
                        else
                        {
                            gr.name = GetBPStigmata();
                            gr.value = 2400;
                            gr.count = 1;
                            gr.type = TypeS.Stigmata.ToString();
                            gr.quality = 2;
                            gr.evaluation = 4;
                            return gr;
                        }
                    }
                    else
                    {
                        gr.name = GetBPCharacter("B");
                        gr.value = 2750;
                        gr.count = 1;
                        gr.type = TypeS.Character.ToString();
                        gr.class_ = "B";
                        gr.quality = 2;
                        return gr;
                    }
                }
                else if (pro_1 < Probablity_BP角色卡 + Probablity_BP角色碎片)
                {
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_BP角色碎片) * 1000)) / (double)1000;
                    if (pro_0 < Probablity_BPSdebris)
                    {
                        gr.name = GetBPCharacter("S") + "碎片";
                        gr.value = 2700;
                        gr.count = rd.Next(4, 9);
                        gr.type = TypeS.debri.ToString();
                        gr.quality = 2;
                        return gr;
                    }
                    else if(pro_0<Probablity_BPSdebris+Probablity_BPAdebris)
                    {
                        gr.value = 2600;
                        gr.count = rd.Next(4, 9);
                        gr.type = TypeS.debri.ToString();
                        gr.name = GetBPCharacter("A") + "碎片";
                        gr.quality = 2;
                        return gr;
                    }
                    else
                    {
                        gr.value = 2500;
                        gr.count = rd.Next(4, 9);
                        gr.type = TypeS.debri.ToString();
                        gr.name = GetBPCharacter("B") + "碎片";
                        gr.quality = 2;
                        return gr;
                    }
                }
                else
                {
                    double pro_2 = rd.Next(0, Convert.ToInt32((Probablity_BP材料) * 1000)) / (double)1000;
                    if (pro_2 < Probablity_Material技能材料)
                    {
                        gr.name = "高级技能材料";
                        gr.value = 1000;
                        gr.count = rd.Next(4, 7);
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;
                        return gr;
                    }
                    else if (pro_2 < Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "超小型反应炉";
                        gr.value = 800;
                        gr.count = rd.Next(4, 7);
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 1;
                        gr.evaluation = 3;

                        return gr;
                    }
                    else if (pro_2 < Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        double pro_4 = rd.NextDouble();
                        if (pro_4 <= 0.33)
                        {
                            gr.name = "特级学习材料";
                            gr.value = 900;
                            gr.count = rd.Next(5, 9);
                            gr.type = TypeS.Material.ToString();
                            gr.quality = 2;
                            gr.evaluation = 4;

                            return gr;
                        }
                        else
                        {
                            gr.name = "高级学习材料";
                            gr.value = 800;
                            gr.count = rd.Next(5, 9);
                            gr.type = TypeS.Material.ToString();
                            gr.quality = 1;
                            gr.evaluation = 3;

                            return gr;
                        }
                    }
                    else if (pro_2 < Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "吼咪宝藏";
                        gr.value = 700;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else if (pro_2 < Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
                    {
                        gr.name = "吼美宝藏";
                        gr.value = 600;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 2;
                        gr.evaluation = 4;

                        return gr;
                    }
                    else
                    {
                        gr.name = "吼里宝藏";
                        gr.value = 500;
                        gr.count = 1;
                        gr.type = TypeS.Material.ToString();
                        gr.quality = 1;
                        gr.evaluation = 3;

                        return gr;
                    }
                }
            }
            else
            {
                string path = $@"{CQSave.AppDirectory}\概率\标配概率.txt";
                if (INIhelper.IniRead("设置", "Baodi", "1", path) == "1")
                {
                    Count = 0;
                    GetTargetItem = false;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_BP角色卡-Probablity_BPB) * 1000)) / (double)1000;
                    if (pro_0 < Probablity_BPS)
                    {
                        gr.name = GetBPCharacter("S");
                        gr.value = 28000;
                        gr.count = 1;
                        gr.class_ = "S";
                        gr.quality = 2;
                        gr.type = TypeS.Character.ToString();
                        return gr;
                    }
                    else
                    {
                        gr.name = GetBPCharacter("A");
                        gr.value = 2800;
                        gr.count = 1;
                        gr.class_ = "A";
                        gr.quality = 2;
                        gr.type = TypeS.Character.ToString();
                        return gr;
                    }
                }
                else
                {
                    Count--;
                    goto jumpin;
                }
            }
        }

        public GachaResult BP_GachaSub()
        {
            GachaResult gr = new GachaResult();
            Random rd = new Random(GetRandomSeed());
            double pro_2 = rd.Next(0, 100000) / (double)1000;
            if (pro_2 < Probablity_Material技能材料)
            {
                gr.name = "高级技能材料";
                gr.value = 1000;
                gr.count = rd.Next(2, 4);
                gr.type = TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;
                return gr;
            }
            else if (pro_2 < Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                gr.name = "超小型反应炉";
                gr.value = 800;
                gr.count = rd.Next(2, 4);
                gr.type = TypeS.Material.ToString();
                gr.quality = 1;
                gr.evaluation = 3;

                return gr;
            }
            else if (pro_2 < Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                double pro_4 = rd.NextDouble();
                if (pro_4 <= 0.33)
                {
                    gr.name = "特级学习材料";
                    gr.value = 900;
                    gr.count = rd.Next(3, 6);
                    gr.type = TypeS.Material.ToString();
                    gr.quality = 2;
                    gr.evaluation = 4;

                    return gr;
                }
                else
                {
                    gr.name = "高级学习材料";
                    gr.value = 800;
                    gr.count = rd.Next(3, 6);
                    gr.type = TypeS.Material.ToString();
                    gr.quality = 1;
                    gr.evaluation = 3;

                    return gr;
                }
            }
            else if (pro_2 < Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                gr.name = "吼咪宝藏";
                gr.value = 700;
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_2 < Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                gr.name = "吼美宝藏";
                gr.value = 600;
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.quality = 2;
                gr.evaluation = 4;

                return gr;
            }
            else if (pro_2 < Probablity_Material吼姆宝藏 + Probablity_Material吼美宝藏 + Probablity_Material吼咪宝藏 + Probablity_Material紫色角色经验 + Probablity_Material反应炉 + Probablity_Material技能材料)
            {
                gr.name = "吼里宝藏";
                gr.value = 500;
                gr.count = 1;
                gr.type = TypeS.Material.ToString();
                gr.quality = 1;
                gr.evaluation = 3;
                return gr;
            }
            else
            {
                double pro_0 = rd.Next(0, 10000) / (double)100;
                if (pro_0 <= 50)
                {
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Weapon2", "0", $@"{CQSave.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Weapon2_Item{rd.Next(0, count)}", "2星武器", $@"{CQSave.AppDirectory}\概率\精准概率.txt");
                    gr.value = 2500;
                    gr.count = 1;
                    gr.type = TypeS.Weapon.ToString();
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 2;

                    return gr;
                }
                else
                {
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Stigmata2", "0", $@"{CQSave.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Stigmata2_Item{rd.Next(0, count)}", "2星圣痕", $@"{CQSave.AppDirectory}\概率\精准概率.txt");
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
                    gr.value = 2400;
                    gr.count = 1;
                    gr.type = TypeS.Stigmata.ToString();
                    gr.level = 1;
                    gr.quality = 1;
                    gr.evaluation = 2;
                    return gr;
                }
            }
        }

        string GetBPCharacter (string pos)
        {
            int count;
            string path = $@"{CQSave.AppDirectory}\概率\标配概率.txt";
            Random rd = new Random(GetRandomSeed());
            string result = "";
            switch (pos)
            {
                case "S":
                    count = Convert.ToInt32(INIhelper.IniRead("详情_S", "Count", "0", path));
                    result = INIhelper.IniRead("详情_S", $"Index{rd.Next(1, count + 1)}", $"S角色{count}", path);                    
                    break;
                case "A":
                    count = Convert.ToInt32(INIhelper.IniRead("详情_A", "Count", "0", path));
                    result = INIhelper.IniRead("详情_A", $"Index{rd.Next(1, count + 1)}", $"A角色{count}", path);
                    break;
                case "B":
                    count = Convert.ToInt32(INIhelper.IniRead("详情_B", "Count", "0", path));
                    result = INIhelper.IniRead("详情_B", $"Index{rd.Next(1, count + 1)}", $"B角色{count}", path);
                    break;
            }
            return result;
        }

        string GetBPWeapon()
        {
            int count;
            string path = $@"{CQSave.AppDirectory}\概率\标配概率.txt";
            Random rd = new Random(GetRandomSeed());
            string result;
            count = Convert.ToInt32(INIhelper.IniRead("详情_Weapon", "Count", "0", path));
            result = INIhelper.IniRead("详情_Weapon", $"Index{rd.Next(1, count + 1)}", $"四星武器{count}", path);
            return result;
        }

        string GetBPStigmata()
        {
            int count;
            string path = $@"{CQSave.AppDirectory}\概率\标配概率.txt";
            Random rd = new Random(GetRandomSeed());
            string result, pos = "";
            switch (rd.Next(0, 3))
            {
                case 0:
                    pos = "上";
                    break;
                case 1:
                    pos = "中";
                    break;
                case 2:
                    pos = "下";
                    break;
            }
            count = Convert.ToInt32(INIhelper.IniRead("详情_Stigmata", "Count", "0", path));
            result = INIhelper.IniRead("详情_Stigmata", $"Index{rd.Next(1, count + 1)}", $"四星圣痕{count}{pos}", path);
            return result;
        }

        public void Read_Kuochong()
        {
            string path;
            if(cq != null)
            {
                path= $@"{CQSave.AppDirectory}\概率\扩充概率.txt";
            }
            else
            {
                path= $@"{CQSave.cq_private.CQApi.AppDirectory}\概率\扩充概率.txt";
            }
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

        public void Read_Jingzhun(int pos)
        {
            string path = "";
            if (cq!=null)
            {
                path = $@"{CQSave.AppDirectory}\概率\精准概率.txt";
            }
            else
            {
                path = $@"{CQSave.cq_private.CQApi.AppDirectory}\概率\精准概率.txt";
            }
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


            switch (pos)
            {
                case 1:
                    Text_UpWeapon = INIhelper.IniRead("详情", "A_UpWeapon", "Up四星武器", path);
                    Text_UpStigmata = INIhelper.IniRead("详情", "A_UpStigmata", "Up四星圣痕", path);
                    Text_Weapon1 = INIhelper.IniRead("详情", "A_Weapon_Item0", "四星武器1", path);
                    Text_Weapon2 = INIhelper.IniRead("详情", "A_Weapon_Item1", "四星武器2", path);
                    Text_Weapon3 = INIhelper.IniRead("详情", "A_Weapon_Item2", "四星武器3", path);
                    Text_Weapon4 = INIhelper.IniRead("详情", "A_Weapon_Item3", "四星武器4", path);
                    Text_Weapon5 = INIhelper.IniRead("详情", "A_Weapon_Item4", "四星武器5", path);
                    Text_Weapon6 = INIhelper.IniRead("详情", "A_Weapon_Item5", "四星武器6", path);
                    Text_Stigmata1 = INIhelper.IniRead("详情", "A_Stigmata_Item0", "四星圣痕1", path);
                    Text_Stigmata2 = INIhelper.IniRead("详情", "A_Stigmata_Item1", "四星圣痕2", path);
                    Text_Stigmata3 = INIhelper.IniRead("详情", "A_Stigmata_Item2", "四星圣痕3", path);
                    Text_Stigmata4 = INIhelper.IniRead("详情", "A_Stigmata_Item3", "四星圣痕4", path);
                    break;
                case 2:
                    Text_UpWeapon = INIhelper.IniRead("详情", "B_UpWeapon", "Up四星武器", path);
                    Text_UpStigmata = INIhelper.IniRead("详情", "B_UpStigmata", "Up四星圣痕", path);
                    if(string.IsNullOrEmpty(Text_UpWeapon)||string.IsNullOrEmpty(Text_UpStigmata))
                    {
                        Text_UpWeapon = INIhelper.IniRead("详情", "B_UpWeaponBackup", "Up四星武器", path);
                        Text_UpStigmata = INIhelper.IniRead("详情", "B_UpStigmataBackup", "Up四星圣痕", path);

                    }
                    Text_Weapon1 = INIhelper.IniRead("详情", "B_Weapon_Item0", "四星武器1", path);
                    Text_Weapon2 = INIhelper.IniRead("详情", "B_Weapon_Item1", "四星武器2", path);
                    Text_Weapon3 = INIhelper.IniRead("详情", "B_Weapon_Item2", "四星武器3", path);
                    Text_Weapon4 = INIhelper.IniRead("详情", "B_Weapon_Item3", "四星武器4", path);
                    Text_Weapon5 = INIhelper.IniRead("详情", "B_Weapon_Item4", "四星武器5", path);
                    Text_Weapon6 = INIhelper.IniRead("详情", "B_Weapon_Item5", "四星武器6", path);
                    Text_Stigmata1 = INIhelper.IniRead("详情", "B_Stigmata_Item0", "四星圣痕1", path);
                    Text_Stigmata2 = INIhelper.IniRead("详情", "B_Stigmata_Item1", "四星圣痕2", path);
                    Text_Stigmata3 = INIhelper.IniRead("详情", "B_Stigmata_Item2", "四星圣痕3", path);
                    Text_Stigmata4 = INIhelper.IniRead("详情", "B_Stigmata_Item3", "四星圣痕4", path);
                    break;
            }

        }

        public void Read_BP()
        {
            string path = "";
            if (cq!=null)
            {
                path = $@"{CQSave.AppDirectory}\概率\标配概率.txt";
            }
            else
            {
                path = $@"{CQSave.cq_private.CQApi.AppDirectory}\概率\标配概率.txt";
            }
            Probablity_BP角色卡 = Convert.ToDouble(INIhelper.IniRead("综合概率", "角色卡", "6.00", path).Replace("%", ""));
            Probablity_BP角色碎片 = Convert.ToDouble(INIhelper.IniRead("综合概率", "角色碎片", "17.25", path).Replace("%", ""));
            Probablity_BP装备 = Convert.ToDouble(INIhelper.IniRead("综合概率", "四星装备", "1.19", path).Replace("%", ""));
            Probablity_BP材料 = Convert.ToDouble(INIhelper.IniRead("综合概率", "材料", "75.56", path).Replace("%", ""));
            Probablity_BPS = Convert.ToDouble(INIhelper.IniRead("详细概率", "S角色", "1.50", path).Replace("%", ""));
            Probablity_BPA = Convert.ToDouble(INIhelper.IniRead("详细概率", "A角色", "4.50", path).Replace("%", ""));
            Probablity_BPB = Convert.ToDouble(INIhelper.IniRead("详细概率", "B角色", "1.00", path).Replace("%", ""));
            Probablity_BPSdebris = Convert.ToDouble(INIhelper.IniRead("详细概率", "S角色碎片", "2.25", path).Replace("%", ""));
            Probablity_BPAdebris = Convert.ToDouble(INIhelper.IniRead("详细概率", "A角色碎片", "15.00", path).Replace("%", ""));
            Probablity_BPWeapon4 = Convert.ToDouble(INIhelper.IniRead("详细概率", "4星武器", "0.46", path).Replace("%", ""));
            Probablity_BPStigmata4 = Convert.ToDouble(INIhelper.IniRead("详细概率", "4星圣痕", "0.73", path).Replace("%", ""));

            Probablity_Material技能材料 = Convert.ToDouble(INIhelper.IniRead("详细概率", "技能材料", "10.00", path).Replace("%", ""));
            Probablity_Material反应炉 = Convert.ToDouble(INIhelper.IniRead("详细概率", "低星装备材料", "26.41", path).Replace("%", ""));
            Probablity_Material紫色角色经验 = Convert.ToDouble(INIhelper.IniRead("详细概率", "角色经验", "11.17", path).Replace("%", ""));
            Probablity_Material吼咪宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼咪宝藏", "2.232", path).Replace("%", ""));
            Probablity_Material吼美宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼美宝藏", "3.334", path).Replace("%", ""));
            Probablity_Material吼姆宝藏 = Convert.ToDouble(INIhelper.IniRead("详细概率", "吼姆宝藏", "5.556", path).Replace("%", ""));
        }
    }
}
