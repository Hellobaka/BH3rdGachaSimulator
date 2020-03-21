using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.Model;
using Native.Csharp.Sdk.Cqp.EventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace me.luohuaming.Gacha.UI
{
    public partial class 抽卡 : Form
    {
        public 抽卡()
        {
            InitializeComponent();
        }
        private static CQMenuCallEventArgs cq = CQSave.cq_menu;
        public enum TypeS { Chararcter, Weapon, Stigmata, Material, debri };
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

        private void 抽卡_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            tabControl1.SelectedIndex = 0;
            textBox_ControlGroup.Text = INIhelper.IniRead("后台群", "Id", "0", cq.CQApi.AppDirectory + "\\Config.ini");
            int count = Convert.ToInt32(INIhelper.IniRead("群控", "Count", "0", cq.CQApi.AppDirectory + "\\Config.ini"));
            for(int i=0;i<count;i++)
            {
                listBox_Group.Items.Add(INIhelper.IniRead("群控", $"Item{i}", "0", cq.CQApi.AppDirectory + "\\Config.ini"));
            }
            checkBox1.Checked=(INIhelper.IniRead("接口","Private","0", cq.CQApi.AppDirectory + "\\Config.ini")== "1")? true : false;
            checkBox2.Checked = (INIhelper.IniRead("接口", "Group", "0", cq.CQApi.AppDirectory + "\\Config.ini") == "1") ? true : false;

        }

        private void button_GetKuochong_Click(object sender, EventArgs e)
        {
            textBox_KuochongProbablity.Text = Read($@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt");
        }

        public string Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            return sr.ReadToEnd();
        }

        private void button_JingzhunGetProbablity_Click(object sender, EventArgs e)
        {
            textBox_JingzhunProbablity.Text= Read($@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
        }
        private void button_Gacha10_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    List<GachaResult> ls = new List<GachaResult>();
                    for (int i=0;i<10;i++)
                    {
                        ls.Add(KC_Gacha());
                        ls.Add(KC_GachaSub());
                    }
                    ls = ls.OrderByDescending(x => x.value).ToList();
                    for (int i = 0; i < ls.Count; i++)
                    {
                        for (int j = i + 1; j < ls.Count; j++)
                        {
                            if (ls[i].name == ls[j].name && ls[i].type != TypeS.Stigmata.ToString() && ls[i].type != TypeS.Weapon.ToString())
                            {
                                ls[i].count += ls[j].count;
                                ls.RemoveAt(j);
                                i--;j--;
                                if (i == -1) i = 0;

                            }
                        }
                    }
                    foreach (var item in ls)
                    {
                        listBox_Result.Items.Add(item.name + " x" + item.count);
                    }
                    listBox_Result.Items.Add("");
                    listBox_Result.SelectedIndex = listBox_Result.Items.Count - 2;
                    break;
                case 1:
                    List<GachaResult> ls_1 = new List<GachaResult>();
                    for (int i = 0; i < 10; i++)
                    {
                        ls_1.Add(JZ_GachaMain());
                        ls_1.Add(JZ_GachaMaterial());
                    }
                    ls_1 = ls_1.OrderByDescending(x => x.value).ToList();
                    for (int i = 0; i < ls_1.Count; i++)
                    {
                        for (int j = i + 1; j < ls_1.Count; j++)
                        {
                            if (ls_1[i].name == ls_1[j].name && ls_1[i].type != TypeS.Stigmata.ToString() && ls_1[i].type != TypeS.Weapon.ToString())
                            {
                                ls_1[i].count += ls_1[j].count;
                                ls_1.RemoveAt(j);
                                i--; j--;
                                if (i == -1) i = 0;

                            }
                        }
                    }
                    foreach (var item in ls_1)
                    {
                        listBox_Result.Items.Add(item.name + " x" + item.count);
                    }
                    listBox_Result.Items.Add("");
                    listBox_Result.SelectedIndex = listBox_Result.Items.Count - 2;
                    break;
            }

        }
        public bool GetTargetItem;
        public int Count;
        private void button_Gacha1_Click(object sender, EventArgs e)
        {
            switch(tabControl1.SelectedIndex)
            {
                case 0:
                    List<GachaResult> ls = new List<GachaResult>
                    {
                        KC_Gacha(),
                        KC_GachaSub()
                    };
                    listBox_Result.Items.Add(ls[ls.Count - 2].name);
                    listBox_Result.Items.Add(ls[ls.Count-1].name);
                    listBox_Result.SelectedIndex = listBox_Result.Items.Count - 1;
                    break;
                case 1:
                    List<GachaResult> ls_1 = new List<GachaResult>
                    {
                        JZ_GachaMain(),
                        JZ_GachaMaterial()
                    };
                    listBox_Result.Items.Add(ls_1[ls_1.Count - 2].name);
                    listBox_Result.Items.Add(ls_1[ls_1.Count - 1].name);
                    listBox_Result.SelectedIndex = listBox_Result.Items.Count - 1;
                    break;

            }
        }

        public GachaResult KC_Gacha()
        {
            Random rd = new Random(GetRandomSeed());
            double pro_1 = rd.Next(0, 1000000) / (double)10000;
            Count++;
            label13.Text = $"保底剩余{10 - Count}发";
            GachaResult gr = new GachaResult();
            //
            if (Count < 10)
            {
                if (pro_1 < Probablity_KC角色卡)
                {
                    GetTargetItem = true;
                    Count = 0;
                    label13.Text = $"保底剩余{10 - Count}发";
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_KC角色卡) * 1000)) / (double)1000;
                    if (pro_0 < Probablity_UpS)
                    {
                        gr.name = Text_UpS;
                        gr.value = 28000;
                        gr.count = 1;
                        gr.type = TypeS.Chararcter.ToString();
                        gr.class_ = "S";
                        gr.quality = 2;
                       
                        return gr;
                    }
                    else if (pro_0 < Probablity_UpA + Probablity_UpS)
                    {
                        gr.name = Text_UpA; ;
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Chararcter.ToString();
                        gr.class_ = "A";
                        gr.quality = 2;

                        
                        return gr;
                    }
                    else
                    {
                        gr.value = 2800;
                        gr.count = 1;
                        gr.type = TypeS.Chararcter.ToString();
                        gr.class_ = "A";
                        gr.quality = 2;

                        
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
                    if (rd.Next(0, Convert.ToInt32((Probablity_KC角色碎片) * 1000)) / (double)1000 < Probablity_Sdebris)
                    {
                        gr.name = Text_UpS + "碎片";
                        gr.value = 1400;
                        gr.count = rd.Next(7, 10);
                        gr.type = TypeS.debri.ToString();
                        gr.quality = 2;

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
                        gr.quality = 2;

                        return gr;
                    }
                }
                else
                {
                    Thread.Sleep(10);
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
            //
            else
            {
                Count = 0;
                label13.Text = $"保底剩余{10 - Count}发";
                GetTargetItem = false;
                Thread.Sleep(10);
                double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_KC角色卡) * 1000)) / (double)1000;
                if (pro_0 < Probablity_UpS)
                {
                    gr.name = Text_UpS; ;
                    gr.value = 28000;
                    gr.count = 1;
                    gr.class_ = "S";
                    gr.quality = 2;

                    gr.type = TypeS.Chararcter.ToString();
                    return gr;
                }
                else if (pro_0 < Probablity_UpA + Probablity_UpS)
                {
                    gr.name = Text_UpA; ;
                    gr.value = 2800;
                    gr.count = 1;
                    gr.class_ = "A";
                    gr.quality = 2;

                    gr.type = TypeS.Chararcter.ToString();
                    return gr;
                }
                else
                {
                    gr.value = 2800;
                    gr.count = 1;
                    gr.class_ = "A";
                    gr.quality = 2;

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
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Weapon2", "0", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Weapon2_Item{rd.Next(0, count)}", "2星武器", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
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
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Stigmata2", "0", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Stigmata2_Item{rd.Next(0, count)}", "2星圣痕", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
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

        public static int Count_JZ;
        /// <summary>
        /// 使用RNGCryptoServiceProvider生成种子
        /// </summary>
        /// <returns></returns>
        public int GetRandomSeed()
        {
            Random rd = new Random();
            byte[] bytes = new byte[rd.Next(100,10000000)];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public GachaResult JZ_GachaMain()
        {
            //Thread.Sleep(100);
            //Random rd = new Random();
            Random rd = new Random(GetRandomSeed());
            double pro_1 = rd.Next(0, 10000000) / (double)100000;
            Count_JZ++;
            label13.Text = $"保底剩余{10 - Count_JZ}发";
            GachaResult gr = new GachaResult();
            //
            if (Count_JZ < 10)
            {
                if (pro_1 < Probablity_Weapon4Total)
                {
                    GetTargetItem = true;
                    Count_JZ = 0;
                    label13.Text = $"保底剩余{10 - Count_JZ}发";
                   
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
                    label13.Text = $"保底剩余{10 - Count_JZ}发";
                    
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
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Weapon3", "0", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Weapon3_Item{rd.Next(0, count)}", "3星武器", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
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
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Stigmata3", "0", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt"));
                    gr.name = INIhelper.IniRead("详情", $"Stigmata3_Item{rd.Next(0, count)}", "3星圣痕", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
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
                        int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Weapon2", "0", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt"));
                        gr.name = INIhelper.IniRead("详情", $"Weapon2_Item{rd.Next(0, count)}", "2星武器", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
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
                        int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count_Stigmata2", "0", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt"));
                        gr.name = INIhelper.IniRead("详情", $"Stigmata2_Item{rd.Next(0, count)}", "2星圣痕", $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt");
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
                Count_JZ = 0;
                label13.Text = $"保底剩余{10 - Count_JZ}发";
                GetTargetItem = false;
                Thread.Sleep(10);
                double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_Weapon4Total + Probablity_Stigmata4Total) * 1000000)) / (double)1000000;
                if (pro_0 < Probablity_Weapon4Total)
                {
                    Thread.Sleep(10);
                   

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
                    Thread.Sleep(10);
                    

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
        }
        public GachaResult JZ_GachaMaterial()
        {
            //Thread.Sleep(20);
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
                Thread.Sleep(10);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label12.Text ="当前池:"+ tabControl1.TabPages[tabControl1.SelectedIndex].Text;
            string path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
                    Probablity_KC角色卡 =Convert.ToDouble(INIhelper.IniRead("综合概率", "角色卡", "15.00", path).Replace("%", ""));
                    Probablity_KC角色碎片 = Convert.ToDouble(INIhelper.IniRead("综合概率", "角色碎片", "26.25", path).Replace("%", ""));
                    Probablity_KC材料 = Convert.ToDouble(INIhelper.IniRead("综合概率", "材料", "58.75", path).Replace("%", ""));
                    Probablity_UpS=Convert.ToDouble(INIhelper.IniRead("详细概率", "UpS角色", "1.50", path).Replace("%", ""));
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

                    listBox_Kuochong.Items.Clear();
                    textBox_KuochongUpS.Text = INIhelper.IniRead("详情", "UpS", "", path);
                    textBox_KuochongUpA.Text = INIhelper.IniRead("详情", "UpA", "", path);
                    int count = Convert.ToInt32(INIhelper.IniRead("详情", "Count", "0", path));
                    for(int i=0;i<count;i++)
                    {
                        listBox_Kuochong.Items.Add(INIhelper.IniRead("详情", $"Item{i}", "", path));
                    }

                    break;
                case 1:
                    path = $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
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

                    listBox_JingzhunWeapon.Items.Clear();
                    listBox_JingzhunStigmata.Items.Clear();
                    textBox_JingzhunUpWeapon.Text = INIhelper.IniRead("详情", "UpWeapon", "", path);
                    textBox_JingzhunUpStigmata.Text = INIhelper.IniRead("详情", "UpStigmata", "", path);
                    for (int i = 0; i < 6; i++)
                    {
                        listBox_JingzhunWeapon.Items.Add(INIhelper.IniRead("详情", $"Weapon_Item{i}", "", path));
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        listBox_JingzhunStigmata.Items.Add(INIhelper.IniRead("详情", $"Stigmata_Item{i}", "", path));
                    }
                    break;
            }
        }

        private void button_KuochongPlus_Click(object sender, EventArgs e)
        {
            if (listBox_Kuochong.Items.Count >= 3) return;
            listBox_Kuochong.Items.Add(textBox_KuochongA.Text);
            textBox_KuochongA.Text = "";
        }

        private void button_KuochongSub_Click(object sender, EventArgs e)
        {
            listBox_Kuochong.Items.RemoveAt(listBox_Kuochong.SelectedIndex);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt64(textBox_ControlGroup.Text);
            }
            catch
            {
                MessageBox.Show("后台群输入格式有误");
                return;
            }
            string path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
            INIhelper.IniWrite("详情", "Count",listBox_Kuochong.Items.Count.ToString(), path);
            INIhelper.IniWrite("详情", "UpS", textBox_KuochongUpS.Text, path);
            INIhelper.IniWrite("详情", "UpA", textBox_KuochongUpA.Text, path);
            for(int i=0;i<listBox_Kuochong.Items.Count;i++)
            {
                INIhelper.IniWrite("详情", $"Item{i}", listBox_Kuochong.Items[i].ToString(), path);
            }
            path= $@"{cq.CQApi.AppDirectory}\概率\精准概率.txt";
            INIhelper.IniWrite("详情", "Count_Weapon", listBox_JingzhunWeapon.Items.Count.ToString(), path);
            INIhelper.IniWrite("详情", "Count_Stigmata", listBox_JingzhunStigmata.Items.Count.ToString(), path);
            INIhelper.IniWrite("详情", "UpWeapon", textBox_JingzhunUpWeapon.Text, path);
            INIhelper.IniWrite("详情", "UpStigmata", textBox_JingzhunUpStigmata.Text, path);
            for (int i = 0; i < listBox_JingzhunWeapon.Items.Count; i++)
            {
                INIhelper.IniWrite("详情", $"Weapon_Item{i}", listBox_JingzhunWeapon.Items[i].ToString(), path);
            }
            for (int i = 0; i < listBox_JingzhunStigmata.Items.Count; i++)
            {
                INIhelper.IniWrite("详情", $"Stigmata_Item{i}", listBox_JingzhunStigmata.Items[i].ToString(), path);
            }
            //File.Delete(cq.CQApi.AppDirectory + "\\Config.ini");
            INIhelper.IniWrite("后台群", "Id", textBox_ControlGroup.Text, cq.CQApi.AppDirectory + "\\Config.ini");
            INIhelper.IniWrite("群控", "Count", listBox_Group.Items.Count.ToString(), cq.CQApi.AppDirectory + "\\Config.ini");
            for(int i=0;i<listBox_Group.Items.Count;i++)
            {
                INIhelper.IniWrite("群控", $"Item{i}", listBox_Group.Items[i].ToString(), cq.CQApi.AppDirectory + "\\Config.ini");
            }
            INIhelper.IniWrite("接口", "Private",(checkBox1.Checked)?"1":"0", cq.CQApi.AppDirectory + "\\Config.ini");
            INIhelper.IniWrite("接口", "Group", (checkBox2.Checked) ? "1" : "0", cq.CQApi.AppDirectory + "\\Config.ini");
            MessageBox.Show("更改已保存");
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            listBox_Result.Items.Clear();
        }

        private void button_KuochongUpSPic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_KuochongUpS.Text)) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\角色卡";
            try
            {                
                pictureBox1.Image = Image.FromFile(path+$"\\{textBox_KuochongUpS.Text}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\角色卡 下找到 {textBox_KuochongUpS.Text}.png 文件");                
            }
        }

        private void button_KuochongUpAPic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_KuochongUpA.Text)) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox1.Image = Image.FromFile(path + $"\\{textBox_KuochongUpA.Text}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\角色卡 下找到 {textBox_KuochongUpA.Text}.png 文件");
            }
        }

        private void listBox_Kuochong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Kuochong.SelectedIndex < 0) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox1.Image = Image.FromFile(path + $"\\{listBox_Kuochong.Items[listBox_Kuochong.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\角色卡 下找到 {listBox_Kuochong.Items[listBox_Kuochong.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void button_Gacha10Pic_Click(object sender, EventArgs e)
        {
            List<GachaResult> ls = new List< GachaResult>();
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    for (int i = 0; i < 10; i++)
                    {
                        ls.Add(KC_Gacha());
                        ls.Add(KC_GachaSub());
                    }
                    ls = ls.OrderByDescending(x => x.value).ToList();
                    for (int i = 0; i < ls.Count; i++)
                    {
                        for (int j = i + 1; j < ls.Count; j++)
                        {
                            if (ls[i].name == ls[j].name && ls[i].type != TypeS.Chararcter.ToString())
                            {
                                ls[i].count += ls[j].count;
                                ls.RemoveAt(j);
                                i--; j--;
                                if (i == -1) i = 0;

                            }
                        }
                    }
                    foreach (var item in ls)
                    {
                        listBox_Result.Items.Add(item.name + " x" + item.count);
                    }
                    break;
                case 1:
                    for (int i = 0; i < 10; i++)
                    {
                        ls.Add(JZ_GachaMain());
                        ls.Add(JZ_GachaMaterial());
                    }
                    ls = ls.OrderByDescending(x => x.value).ToList();
                    for (int i = 0; i < ls.Count; i++)
                    {
                        for (int j = i + 1; j < ls.Count; j++)
                        {
                            if (ls[i].name == ls[j].name && ls[i].type != TypeS.Stigmata.ToString() && ls[i].type != TypeS.Weapon.ToString())
                            {
                                ls[i].count += ls[j].count;
                                ls.RemoveAt(j);
                                i--; j--;
                                if (i == -1) i = 0;
                            }
                        }
                    }
                    foreach (var item in ls)
                    {
                        listBox_Result.Items.Add(item.name + " x" + item.count);
                    }
                    break;
            }
            CombinePng cp = new CombinePng();
            MessageBox.Show(cp.Gacha(ls, tabControl1.SelectedIndex,10));
            cp = null;
            GC.Collect();
        }

        private void button_JingzhunWeaponPlus_Click(object sender, EventArgs e)
        {
            if (listBox_JingzhunWeapon.Items.Count >= 6) return;
            listBox_JingzhunWeapon.Items.Add(textBox_JingzhunWeapon.Text); 
            textBox_JingzhunWeapon.Text = "";
        }

        private void button_JingzhunWeaponSub_Click(object sender, EventArgs e)
        {
            if (listBox_JingzhunWeapon.SelectedIndex < 0) return;
            listBox_JingzhunWeapon.Items.RemoveAt(listBox_JingzhunWeapon.SelectedIndex);
        }

        private void button_JingzhunStigmataPlus_Click(object sender, EventArgs e)
        {
            if (listBox_JingzhunStigmata.Items.Count >= 4) return;
            listBox_JingzhunStigmata.Items.Add(textBox_JingzhunStigmata.Text);
            textBox_JingzhunStigmata.Text = "";
        }

        private void button_JingzhunStigmataSub_Click(object sender, EventArgs e)
        {
            if (listBox_JingzhunStigmata.SelectedIndex < 0) return;
            listBox_JingzhunStigmata.Items.RemoveAt(listBox_JingzhunStigmata.SelectedIndex);
        }

        private void listBox_JingzhunWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_JingzhunWeapon.SelectedIndex < 0) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\武器";
            try
            {
                pictureBox2.Image = Image.FromFile(path + $"\\{listBox_JingzhunWeapon.Items[listBox_JingzhunWeapon.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\武器 下找到 {listBox_JingzhunWeapon.Items[listBox_JingzhunWeapon.SelectedIndex].ToString()}.png 文件");
            }
        }

        private void listBox_JingzhunStigmata_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox_JingzhunStigmata.SelectedIndex < 0) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\圣痕卡";
            try
            {
                pictureBox2.Image = Image.FromFile(path + $"\\{listBox_JingzhunStigmata.Items[listBox_JingzhunStigmata.SelectedIndex].ToString()}上.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\圣痕卡 下找到 {listBox_JingzhunStigmata.Items[listBox_JingzhunStigmata.SelectedIndex].ToString()}上.png 文件");
            }

        }

        private void button_JingzhunUpStigmataPic_Click(object sender, EventArgs e)
        {
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\圣痕卡\{textBox_JingzhunUpStigmata.Text}上.png";

            try
            {
                pictureBox2.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\圣痕卡 下找到 {textBox_JingzhunUpStigmata.Text}上.png 文件");
            }
        }

        private void button_JingzhunUpWeaponPic_Click(object sender, EventArgs e)
        {
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\武器\{textBox_JingzhunUpWeapon.Text}.png";
            try
            {
                pictureBox2.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\武器 下找到 {textBox_JingzhunUpWeapon.Text}.png 文件");
            }
        }

        private void button_GroupPlus_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt64(textBox_Group.Text);
            }
            catch
            {
                MessageBox.Show("群格式有误");
                return;
            }
            listBox_Group.Items.Add(textBox_Group.Text);
            textBox_Group.Text = "";
        }

        private void button_GroupSub_Click(object sender, EventArgs e)
        {
            if (listBox_Group.SelectedIndex < 0) return;
            listBox_Group.Items.RemoveAt(listBox_Group.SelectedIndex);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnswerDIY fm = new AnswerDIY();
            fm.Show();
        }
    }
}
