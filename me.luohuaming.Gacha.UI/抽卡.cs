using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;
using Native.Sdk.Cqp.EventArgs;
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
using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;

namespace me.luohuaming.Gacha.UI
{
    public partial class 抽卡 : Form
    {
        public 抽卡()
        {
            InitializeComponent();
        }
        private static CQMenuCallEventArgs cq = CQSave.cq_menu;
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
        static IniConfig ini;
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

        public bool UnsaveFlag = false;
        public int LastGroupChoice;
        static string path;
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
        #endregion
        private void 抽卡_Load(object sender, EventArgs e)
        {
            if (!File.Exists($@"{cq.CQApi.AppDirectory}装备卡\框\标配十连.png"))
            {
                MessageBox.Show("数据包未更新!前往论坛更新吧", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            tabControl1.SelectedIndex = 1;
            tabControl1.SelectedIndex = 2;
            tabControl1.SelectedIndex = 3;
            tabControl1.SelectedIndex = 0;

            path = Path.Combine(CQSave.AppDirectory, "Config.ini");
            ini = new IniConfig(path);
            ini.Load();

            textBox_ControlGroup.Text = ini.Object["后台群"]["Id"].GetValueOrDefault("0");
            int count = Convert.ToInt32(ini.Object["群控"]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                listBox_Group.Items.Add(ini.Object["群控"][$"Item{i}"].GetValueOrDefault("0"));
            }
            checkBox1.Checked = (ini.Object["接口"]["Private"].GetValueOrDefault("0") == "1") ? true : false;
            checkBox2.Checked = (ini.Object["接口"]["Group"].GetValueOrDefault("0") == "1") ? true : false;

            path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();
            checkBox_JZABaodi.Checked = (ini.Object["详情"]["A_Baodi"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_JZAAt.Checked = (ini.Object["详情"]["A_ResultAt"].GetValueOrDefault("0") == "1") ? true : false;

            checkBox_JingzhunBaodi.Checked = (ini.Object["详情"]["B_Baodi"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_JingzhunAt.Checked = (ini.Object["详情"]["B_ResultAt"].GetValueOrDefault("0") == "1") ? true : false;

            listBox_Group.SelectedIndex = (listBox_Group.Items.Count != 0) ? 0 : -1;

            path = $@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt";
            ini = new IniConfig(path);
            ini.Load();
            checkBox_KuochongBaodi.Checked = (ini.Object["详情"]["Baodi"].GetValueOrDefault("1") == "1") ? true : false;
            checkBox_KuochongAt.Checked = (ini.Object["详情"]["ResultAt"].GetValueOrDefault("0") == "1") ? true : false;

            AppInfo appInfo = cq.CQApi.AppInfo;
            label_NowVersion.Text = $"当前版本:{appInfo.Version.ToString()}";
        }

        private void button_GetKuochong_Click(object sender, EventArgs e)
        {
            textBox_KuochongProbablity.Text = Read($@"{cq.CQApi.AppDirectory}\概率\扩充概率.txt");
        }

        public string Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string text = sr.ReadToEnd();
            sr.Close();//2020.3.16解决文件锁的bug
            return text;
        }

        private void button_JingzhunGetProbablity_Click(object sender, EventArgs e)
        {
            textBox_JingzhunProbablity.Text = Read(path);
        }

        private void button_Gacha10_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    List<GachaResult> ls = new List<GachaResult>();
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
                    listBox_Result.Items.Add("");
                    listBox_Result.SelectedIndex = listBox_Result.Items.Count - 2;
                    break;
                case 1:
                case 2:
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
                case 3:
                    List<GachaResult> ls_2 = new List<GachaResult>();
                    for (int i = 0; i < 10; i++)
                    {
                        ls_2.Add(BP_GachaMain());
                        ls_2.Add(BP_GachaSub());
                    }
                    ls_2 = ls_2.OrderByDescending(x => x.value).ToList();
                    for (int i = 0; i < ls_2.Count; i++)
                    {
                        for (int j = i + 1; j < ls_2.Count; j++)
                        {
                            if (ls_2[i].name == ls_2[j].name && ls_2[i].type != TypeS.Stigmata.ToString() && ls_2[i].type != TypeS.Weapon.ToString())
                            {
                                ls_2[i].count += ls_2[j].count;
                                ls_2.RemoveAt(j);
                                i--; j--;
                                if (i == -1) i = 0;
                            }
                        }
                    }
                    foreach (var item in ls_2)
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
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    List<GachaResult> ls = new List<GachaResult>
                    {
                        KC_Gacha(),
                        KC_GachaSub()
                    };
                    listBox_Result.Items.Add(ls[ls.Count - 2].name);
                    listBox_Result.Items.Add(ls[ls.Count - 1].name);
                    listBox_Result.SelectedIndex = listBox_Result.Items.Count - 1;
                    break;
                case 1:
                case 2:
                    List<GachaResult> ls_1 = new List<GachaResult>
                    {
                        JZ_GachaMain(),
                        JZ_GachaMaterial()
                    };
                    listBox_Result.Items.Add(ls_1[ls_1.Count - 2].name);
                    listBox_Result.Items.Add(ls_1[ls_1.Count - 1].name);
                    listBox_Result.SelectedIndex = listBox_Result.Items.Count - 1;
                    break;
                case 3:
                    List<GachaResult> ls_2 = new List<GachaResult>();
                    ls_2.Add(BP_GachaMain());
                    ls_2.Add(BP_GachaSub());
                    listBox_Result.Items.Add(ls_2[ls_2.Count - 2].name);
                    listBox_Result.Items.Add(ls_2[ls_2.Count - 1].name);
                    listBox_Result.SelectedIndex = listBox_Result.Items.Count - 1;
                    break;
            }
        }

        public GachaResult KC_Gacha()
        {
            Random rd = new Random(GetRandomSeed());
            double pro_1 = rd.Next(0, 1000000) / (double)10000;

            path = Path.Combine(CQSave.AppDirectory, "概率", "扩充概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            Count++;
        jumpin:
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
                if (ini.Object["详情"]["Baodi"].GetValueOrDefault("1") == "1")
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
                else
                {
                    Count--;
                    goto jumpin;
                }
            }
        }
        //扩充副产物
        public GachaResult KC_GachaSub()
        {
            GachaResult gr = new GachaResult();
            Random rd = new Random(GetRandomSeed());
            double pro_2 = rd.Next(0, 100000) / (double)1000;

            path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();

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
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Weapon2"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Weapon2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星武器");
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
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Stigmata2"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Stigmata2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星圣痕");
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
            byte[] bytes = new byte[rd.Next(100, 10000000)];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public GachaResult JZ_GachaMain()
        {
            Random rd = new Random(GetRandomSeed());
            double pro_1 = rd.Next(0, 10000000) / (double)100000;
            Count_JZ++;
        jumpin:
            label13.Text = $"保底剩余{10 - Count_JZ}发";
            GachaResult gr = new GachaResult();
            path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();

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
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Weapon3"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Weapon3_Item{rd.Next(0, count)}"].GetValueOrDefault("3星武器");
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
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Stigmata3"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Stigmata3_Item{rd.Next(0, count)}"].GetValueOrDefault("3星圣痕");
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
                        int count = Convert.ToInt32(ini.Object["详情"]["Count_Weapon2"].GetValueOrDefault("0"));
                        gr.name = ini.Object["详情"][$"Weapon2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星武器");
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
                        int count = Convert.ToInt32(ini.Object["详情"]["Count_Stigmata2"].GetValueOrDefault("0"));
                        gr.name = ini.Object["详情"][$"Stigmata2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星圣痕");
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
                if (ini.Object["详情"]["B_Baodi"].GetValueOrDefault("1") == "1")
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
            //Thread.Sleep(20);
            Random rd = new Random(GetRandomSeed());
            double pro_0 = Probablity_JZ装备经验 + Probablity_JZ通用进化材料 + Probablity_JZGold;
            double pro_1 = rd.Next(0, Convert.ToInt32(pro_0 * 100)) / (double)100;
            GachaResult gr = new GachaResult();
            path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();

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

        public GachaResult BP_GachaMain()
        {
            Random rd = new Random(GetRandomSeed());
            double pro_1 = rd.Next(0, 1000000) / (double)10000;
            Count++;
            label13.Text = $"保底剩余{10 - Count}发";
            path = Path.Combine(CQSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();

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
                    else if (pro_0 < Probablity_BPSdebris + Probablity_BPAdebris)
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
                else if (pro_1 < Probablity_BP角色卡 + Probablity_BP角色碎片 + Probablity_BP装备)
                {
                    double pro_2 = rd.Next(0, (int)(10000 * (Probablity_BPWeapon4 + Probablity_BPStigmata4))) / (double)10000;
                    string path = $@"{cq.CQApi.AppDirectory}概率\标配概率.txt";
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
                if (ini.Object["详情"]["Baodi"].GetValueOrDefault("1") == "1")
                {
                    Count = 0;
                    GetTargetItem = false;
                    double pro_0 = rd.Next(0, Convert.ToInt32((Probablity_BP角色卡 - Probablity_BPB) * 1000)) / (double)1000;
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
            double pro_2 = rd.Next(0, 150000) / (double)1000;
            path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();

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
                path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");

                if (pro_0 <= 50)
                {
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Weapon2"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Weapon2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星武器");
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
                    int count = Convert.ToInt32(ini.Object["详情"]["Count_Stigmata2"].GetValueOrDefault("0"));
                    gr.name = ini.Object["详情"][$"Stigmata2_Item{rd.Next(0, count)}"].GetValueOrDefault("2星圣痕");
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

        string GetBPCharacter(string pos)
        {
            int count;
            path = Path.Combine(CQSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            Random rd = new Random(GetRandomSeed());
            string result = "";
            switch (pos)
            {
                case "S":
                    count = Convert.ToInt32(ini.Object["详情_S"]["Count"].GetValueOrDefault("0"));
                    result = ini.Object["详情_S"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"S角色{count}");
                    break;
                case "A":
                    count = Convert.ToInt32(ini.Object["详情_A"]["Count"].GetValueOrDefault("0"));
                    result = ini.Object["详情_A"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"A角色{count}");
                    break;
                case "B":
                    count = Convert.ToInt32(ini.Object["详情_B"]["Count"].GetValueOrDefault("0"));
                    result = ini.Object["详情_B"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"B角色{count}");
                    break;
            }
            return result;
        }

        string GetBPWeapon()
        {
            int count;
            path = Path.Combine(CQSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();
            Random rd = new Random(GetRandomSeed());
            string result;
            count = Convert.ToInt32(ini.Object["详情_Weapon"]["Count"].GetValueOrDefault("0"));
            result = ini.Object["详情_Weapon"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"四星武器{count}");
            return result;
        }

        string GetBPStigmata()
        {
            int count;
            path = Path.Combine(CQSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();
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
            count = Convert.ToInt32(ini.Object["详情_Stigmata"]["Count"].GetValueOrDefault("0"));
            result = ini.Object["详情_Stigmata"][$"Index{rd.Next(1, count + 1)}"].GetValueOrDefault($"四星圣痕{count}{pos}");
            return result;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label12.Text = "当前池:" + tabControl1.TabPages[tabControl1.SelectedIndex].Text;            
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    path = Path.Combine(CQSave.AppDirectory, "概率", "扩充概率.txt");
                    ini = new IniConfig(path);
                    ini.Load();
                    Probablity_KC角色卡 = Convert.ToDouble(ini.Object["综合概率"]["角色卡"].GetValueOrDefault("15.00").Replace("%", ""));
                    Probablity_KC角色碎片 = Convert.ToDouble(ini.Object["综合概率"]["角色碎片"].GetValueOrDefault("26.25").Replace("%", ""));
                    Probablity_KC材料 = Convert.ToDouble(ini.Object["综合概率"]["材料"].GetValueOrDefault("58.75").Replace("%", ""));
                    Probablity_UpS = Convert.ToDouble(ini.Object["详细概率"]["UpS角色"].GetValueOrDefault("1.50").Replace("%", ""));
                    Probablity_UpA = Convert.ToDouble(ini.Object["详细概率"]["UpA角色"].GetValueOrDefault("4.50").Replace("%", ""));
                    Probablity_A = Convert.ToDouble(ini.Object["详细概率"]["A角色"].GetValueOrDefault("3.00").Replace("%", ""));
                    Probablity_Adebris = Convert.ToDouble(ini.Object["详细概率"]["UpA角色碎片"].GetValueOrDefault("15.00").Replace("%", ""));
                    Probablity_Sdebris = Convert.ToDouble(ini.Object["详细概率"]["UpS角色碎片"].GetValueOrDefault("2.25").Replace("%", ""));
                    Probablity_Material技能材料 = Convert.ToDouble(ini.Object["详细概率"]["技能材料"].GetValueOrDefault("10.00").Replace("%", ""));
                    Probablity_Material反应炉 = Convert.ToDouble(ini.Object["详细概率"]["低星装备材料"].GetValueOrDefault("26.41").Replace("%", ""));
                    Probablity_Material紫色角色经验 = Convert.ToDouble(ini.Object["详细概率"]["角色经验"].GetValueOrDefault("11.17").Replace("%", ""));
                    Probablity_Material吼咪宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼咪宝藏"].GetValueOrDefault("2.232").Replace("%", ""));
                    Probablity_Material吼美宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼美宝藏"].GetValueOrDefault("3.334").Replace("%", ""));
                    Probablity_Material吼姆宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼姆宝藏"].GetValueOrDefault("5.556").Replace("%", ""));

                    Text_UpS = ini.Object["详情"]["UpS"].GetValueOrDefault("S角色卡");
                    Text_UpA = ini.Object["详情"]["UpA"].GetValueOrDefault("UpA角色卡");
                    Text_A1 = ini.Object["详情"]["Item0"].GetValueOrDefault("A角色卡1");
                    Text_A2 = ini.Object["详情"]["Item1"].GetValueOrDefault("A角色卡2");
                    Text_A3 = ini.Object["详情"]["Item2"].GetValueOrDefault("A角色卡3");

                    listBox_Kuochong.Items.Clear();
                    textBox_KuochongUpS.Text = ini.Object["详情"]["UpS"].GetValueOrDefault("");
                    textBox_KuochongUpA.Text = ini.Object["详情"]["UpA"].GetValueOrDefault("");
                    int count = Convert.ToInt32(ini.Object["详情"]["Count"].GetValueOrDefault("0"));
                    for (int i = 0; i < count; i++)
                    {
                        listBox_Kuochong.Items.Add(ini.Object["详情"][$"Item{i}"].GetValueOrDefault(""));
                    }

                    break;
                case 2:
                    path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");
                    ini = new IniConfig(path);
                    ini.Load();
                    Probablity_Weapon4Total = Convert.ToDouble(ini.Object["综合概率"]["4星武器"].GetValueOrDefault("4.958").Replace("%", ""));
                    Probablity_Stigmata4Total = Convert.ToDouble(ini.Object["综合概率"]["4星圣痕"].GetValueOrDefault("7.437").Replace("%", ""));
                    Probablity_Weapon3Total = Convert.ToDouble(ini.Object["综合概率"]["3星武器"].GetValueOrDefault("11.231").Replace("%", ""));
                    Probablity_Stigmata3Total = Convert.ToDouble(ini.Object["综合概率"]["3星圣痕"].GetValueOrDefault("33.694").Replace("%", ""));
                    Probablity_JZ通用进化材料 = Convert.ToDouble(ini.Object["综合概率"]["通用进化材料"].GetValueOrDefault("17.072").Replace("%", ""));
                    Probablity_JZ装备经验 = Convert.ToDouble(ini.Object["综合概率"]["装备经验"].GetValueOrDefault("17.072").Replace("%", ""));
                    Probablity_JZGold = Convert.ToDouble(ini.Object["综合概率"]["金币"].GetValueOrDefault("8.536").Replace("%", ""));

                    Probablity_JZUpWeapon4 = Convert.ToDouble(ini.Object["详细概率"]["Up武器"].GetValueOrDefault("2.479").Replace("%", ""));
                    Probablity_JZUpStigmata4 = Convert.ToDouble(ini.Object["详细概率"]["Up圣痕单件"].GetValueOrDefault("1.240").Replace("%", ""));
                    Probablity_JZWeapon4 = Convert.ToDouble(ini.Object["详细概率"]["四星武器"].GetValueOrDefault("0.413").Replace("%", ""));
                    Probablity_JZStigmata4 = Convert.ToDouble(ini.Object["详细概率"]["四星圣痕单件"].GetValueOrDefault("0.310").Replace("%", ""));
                    Probablity_Weapon3 = Convert.ToDouble(ini.Object["详细概率"]["三星武器"].GetValueOrDefault("0.511").Replace("%", ""));
                    Probablity_Stigmata3 = Convert.ToDouble(ini.Object["详细概率"]["三星圣痕单件"].GetValueOrDefault("0.936").Replace("%", ""));
                    Probablity_Material镜面 = Convert.ToDouble(ini.Object["详细概率"]["镜面"].GetValueOrDefault("6.828").Replace("%", ""));
                    Probablity_Material反应炉 = Convert.ToDouble(ini.Object["详细概率"]["反应炉"].GetValueOrDefault("10.242").Replace("%", ""));
                    Probablity_Material紫色经验材料 = Convert.ToDouble(ini.Object["详细概率"]["紫色经验材料"].GetValueOrDefault("8.536").Replace("%", ""));
                    Probablity_Material蓝色经验材料 = Convert.ToDouble(ini.Object["详细概率"]["蓝色经验材料"].GetValueOrDefault("8.536").Replace("%", ""));
                    Probablity_Material吼咪宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼咪宝藏"].GetValueOrDefault("1.707").Replace("%", ""));
                    Probablity_Material吼美宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼美宝藏"].GetValueOrDefault("2.561").Replace("%", ""));
                    Probablity_Material吼姆宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼姆宝藏"].GetValueOrDefault("4.267").Replace("%", ""));

                    Text_UpWeapon = ini.Object["详情"]["B_UpWeapon"].GetValueOrDefault("Up四星武器");
                    Text_UpStigmata = ini.Object["详情"]["B_UpStigmata"].GetValueOrDefault("Up四星圣痕");
                    Text_Weapon1 = ini.Object["详情"]["B_Weapon_Item0"].GetValueOrDefault("四星武器1");
                    Text_Weapon2 = ini.Object["详情"]["B_Weapon_Item1"].GetValueOrDefault("四星武器2");
                    Text_Weapon3 = ini.Object["详情"]["B_Weapon_Item2"].GetValueOrDefault("四星武器3");
                    Text_Weapon4 = ini.Object["详情"]["B_Weapon_Item3"].GetValueOrDefault("四星武器4");
                    Text_Weapon5 = ini.Object["详情"]["B_Weapon_Item4"].GetValueOrDefault("四星武器5");
                    Text_Weapon6 = ini.Object["详情"]["B_Weapon_Item5"].GetValueOrDefault("四星武器6");
                    Text_Stigmata1 = ini.Object["详情"]["B_Stigmata_Item0"].GetValueOrDefault("四星圣痕1");
                    Text_Stigmata2 = ini.Object["详情"]["B_Stigmata_Item1"].GetValueOrDefault("四星圣痕2");
                    Text_Stigmata3 = ini.Object["详情"]["B_Stigmata_Item2"].GetValueOrDefault("四星圣痕3");
                    Text_Stigmata4 = ini.Object["详情"]["B_Stigmata_Item3"].GetValueOrDefault("四星圣痕4");

                    listBox_JingzhunWeapon.Items.Clear();
                    listBox_JingzhunStigmata.Items.Clear();
                    textBox_JZBUpWeapon.Text = ini.Object["详情"]["B_UpWeapon"].GetValueOrDefault("");
                    textBox_JZBUpStigmata.Text = ini.Object["详情"]["B_UpStigmata"].GetValueOrDefault("");
                    if (textBox_JZBUpStigmata.Text == "" || textBox_JZBUpWeapon.Text == "")
                    {
                        textBox_JZBUpWeapon.Text = ini.Object["详情"]["B_UpWeaponBackup"].GetValueOrDefault("");
                        textBox_JZBUpStigmata.Text = ini.Object["详情"]["B_UpStigmataBackup"].GetValueOrDefault("");
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        listBox_JingzhunWeapon.Items.Add(ini.Object["详情"][$"B_Weapon_Item{i}"].GetValueOrDefault(""));
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        listBox_JingzhunStigmata.Items.Add(ini.Object["详情"][$"B_Stigmata_Item{i}"].GetValueOrDefault(""));
                    }
                    break;
                case 1:
                    path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");
                    ini = new IniConfig(path);
                    ini.Load();
                    Probablity_Weapon4Total = Convert.ToDouble(ini.Object["综合概率"]["4星武器"].GetValueOrDefault("4.958").Replace("%", ""));
                    Probablity_Stigmata4Total = Convert.ToDouble(ini.Object["综合概率"]["4星圣痕"].GetValueOrDefault("7.437").Replace("%", ""));
                    Probablity_Weapon3Total = Convert.ToDouble(ini.Object["综合概率"]["3星武器"].GetValueOrDefault("11.231").Replace("%", ""));
                    Probablity_Stigmata3Total = Convert.ToDouble(ini.Object["综合概率"]["3星圣痕"].GetValueOrDefault("33.694").Replace("%", ""));
                    Probablity_JZ通用进化材料 = Convert.ToDouble(ini.Object["综合概率"]["通用进化材料"].GetValueOrDefault("17.072").Replace("%", ""));
                    Probablity_JZ装备经验 = Convert.ToDouble(ini.Object["综合概率"]["装备经验"].GetValueOrDefault("17.072").Replace("%", ""));
                    Probablity_JZGold = Convert.ToDouble(ini.Object["综合概率"]["金币"].GetValueOrDefault("8.536").Replace("%", ""));

                    Probablity_JZUpWeapon4 = Convert.ToDouble(ini.Object["详细概率"]["Up武器"].GetValueOrDefault("2.479").Replace("%", ""));
                    Probablity_JZUpStigmata4 = Convert.ToDouble(ini.Object["详细概率"]["Up圣痕单件"].GetValueOrDefault("1.240").Replace("%", ""));
                    Probablity_JZWeapon4 = Convert.ToDouble(ini.Object["详细概率"]["四星武器"].GetValueOrDefault("0.413").Replace("%", ""));
                    Probablity_JZStigmata4 = Convert.ToDouble(ini.Object["详细概率"]["四星圣痕单件"].GetValueOrDefault("0.310").Replace("%", ""));
                    Probablity_Weapon3 = Convert.ToDouble(ini.Object["详细概率"]["三星武器"].GetValueOrDefault("0.511").Replace("%", ""));
                    Probablity_Stigmata3 = Convert.ToDouble(ini.Object["详细概率"]["三星圣痕单件"].GetValueOrDefault("0.936").Replace("%", ""));
                    Probablity_Material镜面 = Convert.ToDouble(ini.Object["详细概率"]["镜面"].GetValueOrDefault("6.828").Replace("%", ""));
                    Probablity_Material反应炉 = Convert.ToDouble(ini.Object["详细概率"]["反应炉"].GetValueOrDefault("10.242").Replace("%", ""));
                    Probablity_Material紫色经验材料 = Convert.ToDouble(ini.Object["详细概率"]["紫色经验材料"].GetValueOrDefault("8.536").Replace("%", ""));
                    Probablity_Material蓝色经验材料 = Convert.ToDouble(ini.Object["详细概率"]["蓝色经验材料"].GetValueOrDefault("8.536").Replace("%", ""));
                    Probablity_Material吼咪宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼咪宝藏"].GetValueOrDefault("1.707").Replace("%", ""));
                    Probablity_Material吼美宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼美宝藏"].GetValueOrDefault("2.561").Replace("%", ""));
                    Probablity_Material吼姆宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼姆宝藏"].GetValueOrDefault("4.267").Replace("%", ""));

                    Text_UpWeapon = ini.Object["详情"]["A_UpWeapon"].GetValueOrDefault("Up四星武器");
                    Text_UpStigmata = ini.Object["详情"]["A_UpStigmata"].GetValueOrDefault("Up四星圣痕");
                    Text_Weapon1 = ini.Object["详情"]["A_Weapon_Item0"].GetValueOrDefault("四星武器1");
                    Text_Weapon2 = ini.Object["详情"]["A_Weapon_Item1"].GetValueOrDefault("四星武器2");
                    Text_Weapon3 = ini.Object["详情"]["A_Weapon_Item2"].GetValueOrDefault("四星武器3");
                    Text_Weapon4 = ini.Object["详情"]["A_Weapon_Item3"].GetValueOrDefault("四星武器4");
                    Text_Weapon5 = ini.Object["详情"]["A_Weapon_Item4"].GetValueOrDefault("四星武器5");
                    Text_Weapon6 = ini.Object["详情"]["A_Weapon_Item5"].GetValueOrDefault("四星武器6");
                    Text_Stigmata1 = ini.Object["详情"]["A_Stigmata_Item0"].GetValueOrDefault("四星圣痕1");
                    Text_Stigmata2 = ini.Object["详情"]["A_Stigmata_Item1"].GetValueOrDefault("四星圣痕2");
                    Text_Stigmata3 = ini.Object["详情"]["A_Stigmata_Item2"].GetValueOrDefault("四星圣痕3");
                    Text_Stigmata4 = ini.Object["详情"]["A_Stigmata_Item3"].GetValueOrDefault("四星圣痕4");

                    listBox_JZAWeapon.Items.Clear();
                    listBox_JZAStigmata.Items.Clear();
                    textBox_JZAUpWeapon.Text = ini.Object["详情"]["A_UpWeapon"].GetValueOrDefault("");
                    textBox_JZAStigmataUp.Text = ini.Object["详情"]["A_UpStigmata"].GetValueOrDefault("");
                    for (int i = 0; i < 6; i++)
                    {
                        string item = ini.Object["详情"][$"A_Weapon_Item{i}"].GetValueOrDefault("");
                        if (!string.IsNullOrEmpty(item)) listBox_JZAWeapon.Items.Add(item);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        string item = ini.Object["详情"][$"A_Stigmata_Item{i}"].GetValueOrDefault("");
                        if (!string.IsNullOrEmpty(item)) listBox_JZAStigmata.Items.Add(item);
                    }
                    break;
                case 3:
                    path = Path.Combine(CQSave.AppDirectory, "概率", "标配概率.txt");
                    ini = new IniConfig(path);
                    ini.Load();
                    Probablity_BP角色卡 = Convert.ToDouble(ini.Object["综合概率"]["角色卡"].GetValueOrDefault("6.00").Replace("%", ""));
                    Probablity_BP角色碎片 = Convert.ToDouble(ini.Object["综合概率"]["角色碎片"].GetValueOrDefault("17.25").Replace("%", ""));
                    Probablity_BP装备 = Convert.ToDouble(ini.Object["综合概率"]["四星装备"].GetValueOrDefault("1.19").Replace("%", ""));
                    Probablity_BP材料 = Convert.ToDouble(ini.Object["综合概率"]["材料"].GetValueOrDefault("75.56").Replace("%", ""));
                    Probablity_BPS = Convert.ToDouble(ini.Object["详细概率"]["S角色"].GetValueOrDefault("1.50").Replace("%", ""));
                    Probablity_BPA = Convert.ToDouble(ini.Object["详细概率"]["A角色"].GetValueOrDefault("4.50").Replace("%", ""));
                    Probablity_BPB = Convert.ToDouble(ini.Object["详细概率"]["B角色"].GetValueOrDefault("1.00").Replace("%", ""));
                    Probablity_BPSdebris = Convert.ToDouble(ini.Object["详细概率"]["S角色碎片"].GetValueOrDefault("2.25").Replace("%", ""));
                    Probablity_BPAdebris = Convert.ToDouble(ini.Object["详细概率"]["A角色碎片"].GetValueOrDefault("15.00").Replace("%", ""));
                    Probablity_BPWeapon4 = Convert.ToDouble(ini.Object["详细概率"]["4星武器"].GetValueOrDefault("0.46").Replace("%", ""));
                    Probablity_BPStigmata4 = Convert.ToDouble(ini.Object["详细概率"]["4星圣痕"].GetValueOrDefault("0.73").Replace("%", ""));

                    Probablity_Material技能材料 = Convert.ToDouble(ini.Object["详细概率"]["技能材料"].GetValueOrDefault("10.00").Replace("%", ""));
                    Probablity_Material反应炉 = Convert.ToDouble(ini.Object["详细概率"]["低星装备材料"].GetValueOrDefault("26.41").Replace("%", ""));
                    Probablity_Material紫色角色经验 = Convert.ToDouble(ini.Object["详细概率"]["角色经验"].GetValueOrDefault("11.17").Replace("%", ""));
                    Probablity_Material吼咪宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼咪宝藏"].GetValueOrDefault("2.232").Replace("%", ""));
                    Probablity_Material吼美宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼美宝藏"].GetValueOrDefault("3.334").Replace("%", ""));
                    Probablity_Material吼姆宝藏 = Convert.ToDouble(ini.Object["详细概率"]["吼姆宝藏"].GetValueOrDefault("5.556").Replace("%", ""));

                    listBox_BPS.Items.Clear();
                    listBox_BPA.Items.Clear();
                    listBox_BPB.Items.Clear();
                    count = Convert.ToInt32(ini.Object["详情_S"]["Count"].GetValueOrDefault("0"));
                    for (int i = 0; i < count; i++)
                    {
                        listBox_BPS.Items.Add(ini.Object["详情_S"][$"Index{i + 1}"].GetValueOrDefault(""));
                    }
                    count = Convert.ToInt32(ini.Object["详情_A"]["Count"].GetValueOrDefault("0"));
                    for (int i = 0; i < count; i++)
                    {
                        listBox_BPA.Items.Add(ini.Object["详情_A"][$"Index{i + 1}"].GetValueOrDefault(""));
                    }
                    count = Convert.ToInt32(ini.Object["详情_B"]["Count"].GetValueOrDefault("0"));
                    for (int i = 0; i < count; i++)
                    {
                        listBox_BPB.Items.Add(ini.Object["详情_B"][$"Index{i + 1}"].GetValueOrDefault(""));
                    }
                    checkBox_BPAt.Checked = (ini.Object["设置"]["ResultAt"].GetValueOrDefault("0") == "1") ? true : false;
                    checkBox_BPBaodi.Checked = (ini.Object["设置"]["Baodi"].GetValueOrDefault("1") == "1") ? true : false;
                    break;
            }
        }

        private void button_KuochongPlus_Click(object sender, EventArgs e)
        {
            if (listBox_Kuochong.Items.Count >= 3) return;
            listBox_Kuochong.Items.Add(textBox_Kuochong.Text);
            textBox_Kuochong.Text = "";
        }

        private void button_KuochongSub_Click(object sender, EventArgs e)
        {
            listBox_Kuochong.Items.RemoveAt(listBox_Kuochong.SelectedIndex);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox_ControlGroup.Text))
                {
                    textBox_ControlGroup.Text = "0";
                }
                else
                {
                    Convert.ToInt64(textBox_ControlGroup.Text);
                }
            }
            catch
            {
                MessageBox.Show("后台群输入格式有误");
                return;
            }
            UnsaveFlag = false;
            path = Path.Combine(CQSave.AppDirectory, "概率", "扩充概率.txt");
            ini = new IniConfig(path);
            ini.Load();
            ini.Object["详情"]["Count"]=new IValue(listBox_Kuochong.Items.Count.ToString());
            ini.Object["详情"]["UpS"]=new IValue(textBox_KuochongUpS.Text);
            ini.Object["详情"]["UpA"]=new IValue(textBox_KuochongUpA.Text);
            ini.Object["详情"]["Baodi"]=new IValue((checkBox_KuochongBaodi.Checked) ? "1" : "0");
            ini.Object["详情"]["ResultAt"]=new IValue((checkBox_KuochongAt.Checked) ? "1" : "0");
            for (int i = 0; i < listBox_Kuochong.Items.Count; i++)
            {
                ini.Object["详情"][$"Item{i}"] = new IValue(listBox_Kuochong.Items[i].ToString());
            }
            ini.Save();

            path = Path.Combine(CQSave.AppDirectory, "概率", "精准概率.txt");
            ini = new IniConfig(path);
            ini.Load();
            ini.Object["详情"]["A_UpWeapon"]=new IValue(textBox_JZAUpWeapon.Text);
            ini.Object["详情"]["A_UpStigmata"]=new IValue(textBox_JZAStigmataUp.Text);
            ini.Object["详情"]["A_Baodi"]=new IValue((checkBox_JZABaodi.Checked) ? "1" : "0");
            ini.Object["详情"]["A_ResultAt"]=new IValue((checkBox_JZAAt.Checked) ? "1" : "0");
            for (int i = 0; i < listBox_JZAWeapon.Items.Count; i++)
            {
                ini.Object["详情"][$"A_Weapon_Item{i}"]=new IValue(listBox_JZAWeapon.Items[i].ToString());
            }
            for (int i = 0; i < listBox_JZAStigmata.Items.Count; i++)
            {
                ini.Object["详情"][$"A_Stigmata_Item{i}"]=new IValue(listBox_JZAStigmata.Items[i].ToString());
            }
            //ini.Object["详情"]["Count_Weapon"]=new IValue(listBox_JingzhunWeapon.Items.Count.ToString());
            //ini.Object["详情"]["Count_Stigmata"]=new IValue(listBox_JingzhunStigmata.Items.Count.ToString());
            ini.Object["详情"]["B_UpWeapon"]=new IValue(textBox_JZBUpWeapon.Text);
            ini.Object["详情"]["B_UpStigmata"]=new IValue(textBox_JZBUpStigmata.Text);
            ini.Object["详情"]["B_UpWeaponBackup"]=new IValue(textBox_JZBUpWeapon.Text);
            ini.Object["详情"]["B_UpStigmataBackup"]=new IValue(textBox_JZBUpStigmata.Text);

            ini.Object["详情"]["B_Baodi"]=new IValue((checkBox_JingzhunBaodi.Checked) ? "1" : "0");
            ini.Object["详情"]["B_ResultAt"]=new IValue((checkBox_JingzhunAt.Checked) ? "1" : "0");

            for (int i = 0; i < listBox_JingzhunWeapon.Items.Count; i++)
            {
                ini.Object["详情"][$"B_Weapon_Item{i}"]=new IValue(listBox_JingzhunWeapon.Items[i].ToString());
            }
            for (int i = 0; i < listBox_JingzhunStigmata.Items.Count; i++)
            {
                ini.Object["详情"][$"B_Stigmata_Item{i}"]=new IValue(listBox_JingzhunStigmata.Items[i].ToString());
            }
            ini.Save();

            path = Path.Combine(CQSave.AppDirectory, "Config.ini");
            ini = new IniConfig(path);
            ini.Load();
            ini.Object["后台群"]["Id"]=new IValue(textBox_ControlGroup.Text);
            ini.Object["群控"]["Count"]=new IValue(listBox_Group.Items.Count.ToString());
            for (int i = 0; i < listBox_Group.Items.Count; i++)
            {
                ini.Object["群控"][$"Item{i}"]=new IValue(listBox_Group.Items[i].ToString());
            }
            ini.Object["接口"]["Private"]=new IValue((checkBox1.Checked) ? "1" : "0");
            ini.Object["接口"]["Group"]=new IValue((checkBox2.Checked) ? "1" : "0");
            if (listBox_Group.SelectedIndex >= 0)
            {
                string groupid = listBox_Group.Items[listBox_Group.SelectedIndex].ToString();
                path = $@"{cq.CQApi.AppDirectory}\Config.ini";
                ini.Object[groupid]["Count"] = new IValue(listBox_Admin.Items.Count.ToString());
                for (int i = 0; i < listBox_Admin.Items.Count; i++)
                {
                    ini.Object[groupid][$"Index{i}"] = new IValue(listBox_Admin.Items[i].ToString());
                }
            }
            ini.Save();

            path = Path.Combine(CQSave.AppDirectory, "概率", "标配概率.txt");
            ini = new IniConfig(path);
            ini.Load();

            if (listBox_BPS.Items.Count > 0)
            {
                ini.Object["设置"]["Baodi"]=new IValue((checkBox_BPBaodi.Checked) ? "1" : "0");
                ini.Object["设置"]["ResultAt"]=new IValue((checkBox_BPAt.Checked) ? "1" : "0");

                ini.Object["详情_S"]["Count"]=new IValue(listBox_BPS.Items.Count.ToString());
                ini.Object["详情_A"]["Count"]=new IValue(listBox_BPA.Items.Count.ToString());
                ini.Object["详情_B"]["Count"]=new IValue(listBox_BPB.Items.Count.ToString());

                for (int i = 0; i < listBox_BPS.Items.Count; i++)
                {
                    ini.Object["详情_S"][$"Index{i + 1}"]=new IValue(listBox_BPS.Items[i].ToString());
                }
                for (int i = 0; i < listBox_BPA.Items.Count; i++)
                {
                    ini.Object["详情_A"][$"Index{i + 1}"]=new IValue(listBox_BPA.Items[i].ToString());
                }
                for (int i = 0; i < listBox_BPB.Items.Count; i++)
                {
                    ini.Object["详情_B"][$"Index{i + 1}"]=new IValue(listBox_BPB.Items[i].ToString());
                }
            }

            ini.Save();

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
                pictureBox_KuoChong.Image = Image.FromFile(path + $"\\{textBox_KuochongUpS.Text}.png");
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
                pictureBox_KuoChong.Image = Image.FromFile(path + $"\\{textBox_KuochongUpA.Text}.png");
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
                pictureBox_KuoChong.Image = Image.FromFile(path + $"\\{listBox_Kuochong.Items[listBox_Kuochong.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\角色卡 下找到 {listBox_Kuochong.Items[listBox_Kuochong.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void button_Gacha10Pic_Click(object sender, EventArgs e)
        {
            List<GachaResult> ls = new List<GachaResult>();
            CombinePng cp = new CombinePng();
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
                            if (ls[i].name == ls[j].name && ls[i].type != TypeS.Character.ToString())
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
                    MessageBox.Show(cp.Gacha(ls, 0, tabControl1.SelectedIndex, 10));
                    break;
                case 1:
                case 2:
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
                    //CombinePng cp = new CombinePng();
                    MessageBox.Show(cp.Gacha(ls, 1, tabControl1.SelectedIndex, 10));
                    break;
                case 3:
                    for (int i = 0; i < 10; i++)
                    {
                        ls.Add(BP_GachaMain());
                        ls.Add(BP_GachaSub());
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
                    MessageBox.Show(cp.Gacha(ls, 2, 0, 10));
                    break;

            }
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
                pictureBox_JingZhun.Image = Image.FromFile(path + $"\\{listBox_JingzhunWeapon.Items[listBox_JingzhunWeapon.SelectedIndex].ToString()}.png");
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
                pictureBox_JingZhun.Image = Image.FromFile(path + $"\\{listBox_JingzhunStigmata.Items[listBox_JingzhunStigmata.SelectedIndex].ToString()}上.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\圣痕卡 下找到 {listBox_JingzhunStigmata.Items[listBox_JingzhunStigmata.SelectedIndex].ToString()}上.png 文件");
            }

        }

        private void button_JingzhunUpStigmataPic_Click(object sender, EventArgs e)
        {
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\圣痕卡\{textBox_JZBUpStigmata.Text}上.png";

            try
            {
                pictureBox_JingZhun.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\圣痕卡 下找到 {textBox_JZBUpStigmata.Text}上.png 文件");
            }
        }

        private void button_JingzhunUpWeaponPic_Click(object sender, EventArgs e)
        {
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\武器\{textBox_JZBUpWeapon.Text}.png";
            try
            {
                pictureBox_JingZhun.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\武器 下找到 {textBox_JZBUpWeapon.Text}.png 文件");
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

        private void button_AdminPlus_Click(object sender, EventArgs e)
        {
            UnsaveFlag = true;
            try
            {
                Convert.ToInt64(textBox_Admin.Text);
            }
            catch
            {
                MessageBox.Show("QQ格式有误");
                return;
            }
            listBox_Admin.Items.Add(textBox_Admin.Text);
            textBox_Admin.Text = "";
        }

        bool tempflag = true;

        private void listBox_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UnsaveFlag && tempflag)
            {
                if (MessageBox.Show("当前群的更改未保存，是否继续?", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    tempflag = false;
                    listBox_Group.SelectedIndex = LastGroupChoice;
                    return;
                }
            }
            UnsaveFlag = false;
            tempflag = true;
            if (listBox_Group.SelectedIndex < 0) return;
            LastGroupChoice = listBox_Group.SelectedIndex;
            listBox_Admin.Items.Clear();
            path = Path.Combine(CQSave.AppDirectory, "Config.ini");
            ini = new IniConfig(path);
            ini.Load();
            string groupid = listBox_Group.Items[listBox_Group.SelectedIndex].ToString();
            label16.Text = $"编辑群:{groupid} 群管中";
            int count = Convert.ToInt32(ini.Object[groupid]["Count"].GetValueOrDefault("0"));
            for (int i = 0; i < count; i++)
            {
                listBox_Admin.Items.Add(ini.Object[groupid][$"Index{i}"].GetValueOrDefault("0"));
            }
        }

        private void button_AdminSub_Click(object sender, EventArgs e)
        {
            UnsaveFlag = true;
            if (listBox_Admin.SelectedIndex < 0) return;
            listBox_Admin.Items.RemoveAt(listBox_Admin.SelectedIndex);
        }

        private void 批量导入群列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportGroupList fm = new ImportGroupList();
            fm.Show();
        }

        private void button_JZAWeaponUpPic_Click(object sender, EventArgs e)
        {
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\武器\{textBox_JZAUpWeapon.Text}.png";
            try
            {
                pictureBox_JZA.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\武器 下找到 {textBox_JZAUpWeapon.Text}.png 文件");
            }

        }

        private void button_JZAStigmataUpPic_Click(object sender, EventArgs e)
        {
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\圣痕卡\{textBox_JZAStigmataUp.Text}上.png";
            try
            {
                pictureBox_JZA.Image = Image.FromFile(path);
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\圣痕卡 下找到 {textBox_JZAStigmataUp.Text}上.png 文件");
            }

        }

        private void button_JZAWeaponPlus_Click(object sender, EventArgs e)
        {
            if (listBox_JZAWeapon.Items.Count >= 6) return;
            listBox_JZAWeapon.Items.Add(textBox_JZAWeapon.Text);
            textBox_JZAWeapon.Text = "";
        }

        private void button_JZAWeaponSub_Click(object sender, EventArgs e)
        {
            if (listBox_JZAWeapon.SelectedIndex < 0) return;
            listBox_JZAWeapon.Items.RemoveAt(listBox_JZAWeapon.SelectedIndex);
        }

        private void listBox_JZAWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_JZAWeapon.SelectedIndex < 0) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\武器";
            try
            {
                pictureBox_JZA.Image = Image.FromFile(path + $"\\{listBox_JZAWeapon.Items[listBox_JZAWeapon.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\武器 下找到 {listBox_JZAWeapon.Items[listBox_JZAWeapon.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void button_JZAStigmataPlus_Click(object sender, EventArgs e)
        {
            if (listBox_JZAStigmata.Items.Count >= 4) return;
            listBox_JZAStigmata.Items.Add(textBox_JZAStigmata.Text);
            textBox_JZAStigmata.Text = "";
        }

        private void button_JZAStigmataSub_Click(object sender, EventArgs e)
        {
            if (listBox_JZAStigmata.SelectedIndex < 0) return;
            listBox_JZAStigmata.Items.RemoveAt(listBox_JZAStigmata.SelectedIndex);
        }

        private void listBox_JZAStigmata_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_JZAStigmata.SelectedIndex < 0) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\圣痕卡";
            try
            {
                pictureBox_JZA.Image = Image.FromFile(path + $"\\{listBox_JZAStigmata.Items[listBox_JZAStigmata.SelectedIndex].ToString()}上.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\圣痕卡 下找到 {listBox_JZAStigmata.Items[listBox_JZAStigmata.SelectedIndex].ToString()}上.png 文件");
            }
        }

        private void button_JZAGetProbablity_Click(object sender, EventArgs e)
        {
            textBox_JZAProbablity.Text = Read(path);
        }

        private void button_BPSPlus_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (var item in listBox_BPS.Items)
            {
                if (item.ToString() == textBox_BPS.Text)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                listBox_BPS.Items.Add(textBox_BPS.Text);
            }
            else
            {
                MessageBox.Show($"{textBox_BPS.Text} 项目已存在");
            }
            textBox_BPS.Text = "";
        }

        private void button_BPSSub_Click(object sender, EventArgs e)
        {
            listBox_BPS.Items.RemoveAt(listBox_BPS.SelectedIndex);
        }

        private void button_BPAPlus_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (var item in listBox_BPA.Items)
            {
                if (item.ToString() == textBox_BPA.Text)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                listBox_BPA.Items.Add(textBox_BPA.Text);
            }
            else
            {
                MessageBox.Show($"{textBox_BPA.Text} 项目已存在");
            }
            textBox_BPA.Text = "";
        }

        private void button_BPASub_Click(object sender, EventArgs e)
        {
            listBox_BPA.Items.RemoveAt(listBox_BPA.SelectedIndex);
        }

        private void button_BPBPlus_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (var item in listBox_BPB.Items)
            {
                if (item.ToString() == textBox_BPB.Text)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                listBox_BPB.Items.Add(textBox_BPB.Text);
            }
            else
            {
                MessageBox.Show($"{textBox_BPB.Text} 项目已存在");
            }
            textBox_BPB.Text = "";
        }

        private void button_BPBSub_Click(object sender, EventArgs e)
        {
            listBox_BPB.Items.RemoveAt(listBox_BPB.SelectedIndex);
        }

        private void listBox_BPS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_BPS.SelectedIndex >= 0)
            {
                int temp = listBox_BPS.SelectedIndex;
                button_BPSSub.PerformClick();
                if (temp == 0) return;
                listBox_BPS.SelectedIndex = temp - 1;
            }
        }

        private void listBox_BPA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_BPA.SelectedIndex >= 0)
            {
                int temp = listBox_BPA.SelectedIndex;
                button_BPASub.PerformClick();
                if (temp == 0) return;
                listBox_BPA.SelectedIndex = temp - 1;
            }
        }

        private void listBox_BPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_BPB.SelectedIndex >= 0)
            {
                int temp = listBox_BPB.SelectedIndex;
                button_BPBSub.PerformClick();
                if (temp == 0) return;
                listBox_BPB.SelectedIndex = temp - 1;
            }
        }

        private void listBox_JingzhunWeapon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_JingzhunWeapon.SelectedIndex >= 0)
            {
                int temp = listBox_JingzhunWeapon.SelectedIndex;
                button_JingzhunWeaponSub.PerformClick();
                if (temp == 0) return;
                listBox_JingzhunWeapon.SelectedIndex = temp - 1;
            }
        }

        private void listBox_JingzhunStigmata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_JingzhunStigmata.SelectedIndex >= 0)
            {
                int temp = listBox_JingzhunStigmata.SelectedIndex;
                button_JingzhunStigmataSub.PerformClick();
                if (temp == 0) return;
                listBox_JingzhunStigmata.SelectedIndex = temp - 1;
            }
        }

        private void listBox_JZAWeapon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_JZAWeapon.SelectedIndex >= 0)
            {
                int temp = listBox_JZAWeapon.SelectedIndex;
                button_JZAWeaponSub.PerformClick();
                if (temp == 0) return;
                listBox_JZAWeapon.SelectedIndex = temp - 1;
            }
        }

        private void listBox_JZAStigmata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_JZAStigmata.SelectedIndex >= 0)
            {
                int temp = listBox_JZAStigmata.SelectedIndex;
                button_JZAStigmataSub.PerformClick();
                if (temp == 0) return;
                listBox_JZAStigmata.SelectedIndex = temp - 1;
            }
        }

        private void listBox_Kuochong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_Kuochong.SelectedIndex >= 0)
            {
                int temp = listBox_Kuochong.SelectedIndex;
                button_KuochongSub.PerformClick();
                if (temp == 0) return;
                listBox_Kuochong.SelectedIndex = temp - 1;
            }
        }

        private void listBox_BPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_BPS.SelectedIndex < 0) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_BP.Image = Image.FromFile(path + $"\\{listBox_BPS.Items[listBox_BPS.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\角色卡 下找到 {listBox_BPS.Items[listBox_BPS.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void listBox_BPA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_BPA.SelectedIndex < 0) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_BP.Image = Image.FromFile(path + $"\\{listBox_BPA.Items[listBox_BPA.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\角色卡 下找到 {listBox_BPA.Items[listBox_BPA.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void listBox_BPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_BPB.SelectedIndex < 0) return;
            string path = $@"{cq.CQApi.AppDirectory}\装备卡\角色卡";
            try
            {
                pictureBox_BP.Image = Image.FromFile(path + $"\\{listBox_BPB.Items[listBox_BPB.SelectedIndex].ToString()}.png");
            }
            catch
            {
                MessageBox.Show($@"未在{cq.CQApi.AppDirectory}\装备卡\角色卡 下找到 {listBox_BPB.Items[listBox_BPB.SelectedIndex].ToString()}.png 文件");
            }

        }

        private void textBox_BPS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_BPS.Text))
            {

                button_BPSPlus.PerformClick();
                listBox_BPS.SelectedIndex = listBox_BPS.Items.Count - 1;
            }
        }

        private void textBox_BPA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_BPA.Text))
            {

                button_BPAPlus.PerformClick();
                listBox_BPA.SelectedIndex = listBox_BPA.Items.Count - 1;
            }
        }

        private void textBox_BPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_BPB.Text))
            {

                button_BPBPlus.PerformClick();
                listBox_BPB.SelectedIndex = listBox_BPB.Items.Count - 1;
            }
        }

        private void textBox_JingzhunWeapon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_JingzhunWeapon.Text))
            {

                button_JingzhunWeaponPlus.PerformClick();
                listBox_JingzhunWeapon.SelectedIndex = listBox_JingzhunWeapon.Items.Count - 1;
            }
        }

        private void textBox_JingzhunStigmata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_JingzhunStigmata.Text))
            {
                button_JingzhunStigmataPlus.PerformClick();
                listBox_JingzhunStigmata.SelectedIndex = listBox_JingzhunStigmata.Items.Count - 1;
            }
        }

        private void textBox_JZAWeapon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_JZAWeapon.Text))
            {
                button_JZAWeaponPlus.PerformClick();
                listBox_JZAWeapon.SelectedIndex = listBox_JZAWeapon.Items.Count - 1;
            }
        }

        private void textBox_JZAStigmata_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_JZAStigmata.Text))
            {
                button_JZAStigmataPlus.PerformClick();
                listBox_JZAStigmata.SelectedIndex = listBox_JZAStigmata.Items.Count - 1;
            }
        }

        private void textBox_KuochongA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_Kuochong.Text))
            {
                button_KuochongPlus.PerformClick();
                listBox_Kuochong.SelectedIndex = listBox_Kuochong.Items.Count - 1;
            }
        }

        private void textBox_BPA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_BPB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_BPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_JingzhunWeapon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_JingzhunStigmata_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_JZAWeapon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_JZAStigmata_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_Kuochong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_Group_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_Admin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        private void textBox_Group_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_Group.Text))
            {
                button_GroupPlus.PerformClick();
            }
        }

        private void textBox_Admin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrEmpty(textBox_Admin.Text))
            {
                button_AdminPlus.PerformClick();
            }
        }

        private void listBox_Group_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_Group.SelectedIndex >= 0)
            {
                int temp = listBox_Group.SelectedIndex;
                button_GroupSub.PerformClick();
                if (temp == 0) return;
                listBox_Group.SelectedIndex = temp - 1;
            }
        }

        private void listBox_Admin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBox_Admin.SelectedIndex >= 0)
            {
                int temp = listBox_Admin.SelectedIndex;
                button_AdminSub.PerformClick();
                if (temp == 0) return;
                listBox_Admin.SelectedIndex = temp - 1;
            }
        }

        private void button_GetUpdate_Click(object sender, EventArgs e)
        {
            label_NewVersion.Visible = true;
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                GetUpdate_button();
            }).Start();
            //MessageBox.Show("开始拉取版本号，请耐心等待，不要关闭控制台");
        }

        void GetUpdate_button()
        {
            GetUpdate getUpdate = new GetUpdate();
            try
            {
                GetUpdate.Update update = getUpdate.GetVersion(cq);
                label_NewVersion.Text = $"最新版本:{update.GachaVersion}";
                if (update.GachaVersion != cq.CQApi.AppInfo.Version.ToString())
                {
                    MessageBox.Show($"有更新了！\n\n新版本:{update.GachaVersion}\n\n更新时间:{update.Date}\n\n更新内容:{update.Whatsnew}\n\n前往论坛或者GitHub下载吧", "有新版本", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("当前就是最新了哦");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"拉取版本号失败\n错误信息:{e.Message}\n请稍后再试");
            }
        }

        private void 关于界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aboutme fm = new Aboutme();
            fm.Show();
        }

        private void 扩展设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtraConfig fm = new ExtraConfig();
            fm.Show();
        }

        private void AbyssHelper_Click(object sender, EventArgs e)
        {
            AbyssHelper fm = new AbyssHelper();
            fm.Show();
        }
    }
}
