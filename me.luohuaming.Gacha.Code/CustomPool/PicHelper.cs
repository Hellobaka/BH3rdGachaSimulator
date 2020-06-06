using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using me.luohuaming.Gacha.UI;
using Native.Sdk.Cqp;
using Native.Tool.IniConfig;

namespace me.luohuaming.Gacha.Code.CustomPool
{
    /// <summary>
    /// 生成图片的帮助类
    /// </summary>
    public class PicHelper
    {
        static public int Height_1 = 45;
        static public float WordTrap = 3;
        static IniConfig ini;
        static readonly string dir = CQSave.AppDirectory; 

        /// <summary>
        /// 拼接图片
        /// </summary>
        /// <param name="background">原图</param>
        /// <param name="newimg">新图</param>
        /// <param name="x">左上角x</param>
        /// <param name="y">左上角y</param>
        /// <returns></returns>
        public Image CombinImage(Image background, Image newimg, int x, int y)
        {
            Bitmap map1 = new Bitmap(background);
            Bitmap map2 = new Bitmap(newimg);
            var width = background.Width;
            var height = background.Height;
            // 初始化画布(最终的拼图画布)并设置宽高
            Bitmap bitMap = new Bitmap(width, height);
            // 初始化画板
            Graphics g1 = Graphics.FromImage(bitMap);
            // 将画布涂为透明(底部颜色可自行设置)
            try
            {
                g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, width, height));
                //在x=0，y=0处画上图一
                g1.DrawImage(map1, 0, 0, background.Width, background.Height);
                g1.DrawImage(map2, x, y, newimg.Width, newimg.Height);
                //碎片 圣痕表示 x-18,y-18
                //new 标识 x+133,y-17
                map1.Dispose();
                map2.Dispose();
                g1.Dispose();
            }
            catch
            {
            }
            return bitMap;
        }

        /// <summary>
        /// 拼接图片,可指定大小
        /// </summary>
        /// <param name="background">原图</param>
        /// <param name="newimg">新图</param>
        /// <param name="x">左上角x</param>
        /// <param name="y">左上角y</param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <returns></returns>
        public Image CombinImage(Image background, Image newimg, int x, int y, int width, int height)
        {
            Bitmap map1 = new Bitmap(background);
            Bitmap map2 = new Bitmap(newimg);
            // 初始化画布(最终的拼图画布)并设置宽高
            Bitmap bitMap = new Bitmap(background.Width, background.Height);
            // 初始化画板
            Graphics g1 = Graphics.FromImage(bitMap);
            // 将画布涂为透明(底部颜色可自行设置)
            try
            {
                g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, 206, 246));
                //在x=0，y=0处画上图一
                g1.DrawImage(map1, 0, 0, background.Width, background.Height);
                g1.DrawImage(map2, x, y, width, height);
                //碎片 圣痕表示 x-18,y-18
                //new 标识 x+133,y-17
                map1.Dispose();
                map2.Dispose();
                g1.Dispose();
                GC.Collect();
            }
            catch
            {
            }
            return bitMap;
        }

        /// <summary>
        /// 生成 金币 的数字图片
        /// </summary>
        /// <param name="money">目标金额</param>
        /// <returns></returns>
        public Image Money(int money)
        {
            Bitmap bitMap = new Bitmap(CalcPicLength(money), 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, CalcPicLength(money), 29));
            float x = 0;
            for (int i = 0; i < money.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{dir}\数字\{money.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{dir}\数字\{money.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }

        /// <summary>
        /// 生成 水晶 的数字图片
        /// </summary>
        /// <param name="diamond">目标金额</param>
        /// <returns></returns>
        public Image Diamond(int diamond)
        {
            Bitmap bitMap = new Bitmap(CalcPicLength(diamond), 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, CalcPicLength(diamond), 29));
            float x = 0;
            for (int i = 0; i < diamond.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{dir}\数字\{diamond.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{dir}\数字\{diamond.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }

        /// <summary>
        /// 生成 体力 的数字图片
        /// </summary>
        /// <param name="AP">体力左侧数值</param>
        /// <param name="AP_Max">体力右侧数值</param>
        /// <returns></returns>
        public Image AP(int AP, int AP_Max)
        {
            Bitmap bitMap = new Bitmap(21 * AP.ToString().Length + 21 * AP_Max.ToString().Length + 22, 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, 21 * AP.ToString().Length, 29));
            float x = 0;
            for (int i = 0; i < AP.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{dir}\数字\{AP.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{dir}\数字\{AP.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            Image img_Last = Image.FromFile($@"{dir}\数字\{AP.ToString().Substring(AP.ToString().Length - 1, 1)}.png");
            x += img_Last.Width - WordTrap;
            Image img_2 = Image.FromFile($@"{dir}\数字\斜杠.png");
            g1.DrawImage(img_2, x + 1, 0, img_2.Width, 29);
            x += img_2.Width + 1;
            for (int i = 0; i < AP_Max.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{dir}\数字\{AP_Max.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{dir}\数字\{AP_Max.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }
       
        /// <summary>
        /// 生成图片，返回图片CQ码路径
        /// </summary>
        /// <param name="cp">存放池子信息的对象</param>
        /// <param name="ls">抽卡结果数组</param>
        /// <param name="Diamond">目标水晶数目</param>
        /// <returns></returns>
        public string GeneratePic(PoolInfo cp,List<PoolContent>ls,int Diamond)
        {
            try
            {
                int x, y;
                x = 160;
                y = 190;
                //$@"{dir}\装备卡\框\抽卡背景.png"
                Image background = Image.FromFile(cp.BackgroundImg);
                Image img = null;
                Random rd = new Random();
                foreach (var item in ls)
                {
                    img = GenerateCard(item);
                    background = CombinImage(background, img, x, y);
                    if (x < 1960)
                    {
                        x += 300;
                    }
                    else
                    {
                        if (y == 190)
                        {
                            x = 160;
                            y = 530;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                background= CombinImage(background, Image.FromFile(cp.InfoPicPath), 126, 960);
                //金币
                int money = rd.Next(100000, 100000000);
                Image img_1 = Money(money);
                int Width_Gold = 1620 + (164 - img_1.Width) / 2;
                background = CombinImage(background, img_1, Width_Gold, Height_1);
                //水晶
                Image img_2 = this.Diamond(Diamond);
                int Width_Diamond = 1975 + (111 - img_2.Width) / 2;
                background = CombinImage(background, img_2, Width_Diamond, Height_1);
                //体力
                int ap_Max = rd.Next(154, 165);
                int ap = rd.Next(0, ap_Max);
                Image img_3 = AP(ap, ap_Max);
                int Width_AP = 1319 + (127 - img_3.Width) / 2;
                background = CombinImage(background, img_3, Width_AP, Height_1);
                //水印
                Point p = new Point(1471, 813);
                Font font = new Font("汉仪丫丫体简", 15F);
                Color color = Color.FromArgb(0, 0, 0);
                background = AddText2Image(background, "Powered by @水银之翼", p, font, color);

                string name = GetDate();
                if (!Directory.Exists($@"{CQSave.ImageDirectory}\\装备结果"))
                {
                    Directory.CreateDirectory($@"{CQSave.ImageDirectory}\\装备结果");
                }

                ini = new IniConfig(dir+ "\\Config.ini");
                ini.Load();
                if (ini.Object["ExtraConfig"]["ImageFormat"].GetValueOrDefault("jpg") == "jpg")
                {
                    background.Save($@"{CQSave.ImageDirectory}\\装备结果\{name}.jpg", ImageFormat.Jpeg);
                    return CQApi.CQCode_Image($"装备结果\\{name}.jpg").ToSendString();
                }
                else
                {
                    background.Save($@"{dir}\\装备结果\{name}.png");
                    return CQApi.CQCode_Image($"装备结果\\{name}.png").ToSendString();
                }

            }
            catch (Exception e)
            {
                CQSave.CQLog.Info("抽卡图片生成", $"生成错误,错误信息:{e.Message}");
                return null;
            }
        }       

        /// <summary>
        /// 合成装备卡图片
        /// </summary>
        public Image GenerateCard(PoolContent pc)
        {            
            Image background =Image.FromFile( pc.BoardPath);            
            Image main;
            try
            {
                //为了美观,需要先生成卡内容,再生成框
                //读取卡主要内容
                main = Image.FromFile(pc.PicPath);
                //备份框内容,准备覆盖
                Image background_backup = (Image)background.Clone();
                background = CombinImage(background, main, 4, 10, 198, 198);
                background = CombinImage(background, background_backup, 0, 0);
            }
            catch(FileNotFoundException)
            {
                //未找到图片
                main = Image.FromFile($@"{dir}装备卡\框\ItemEmpty #1004496.png");
                //读取后台群
                ini = new IniConfig(dir + "Config.ini");ini.Load();
                long groupid = ini.Object["后台群"]["Id"].GetValueOrDefault((long)0);
                //发送错误信息
                if (groupid != 0)
                    CQSave.cq_group.CQApi.SendGroupMessage(groupid, $"发现图片缺失,请排查是否为命名错误 输入的图片路径={pc.PicPath} 名称={pc.Name}");
                background = CombinImage(background, main, 48, 13, 119, 172);
            }
            //显示评级,不显示个数和星级
            if (pc.Displaymode1 == "显示评级")
            {
                Image img = Image.FromFile(pc.BigIcon);
                background = CombinImage(background, img, 53, 160, 106, 91);
                return background;
            }
            else//显示个数及星级
            {
                Point p = new Point(103, 225);
                Font font = new Font("Impact", 25F);
                Color color = Color.FromArgb(67, 67, 67);
                string text = (pc.Displaymode2 == "显示个数") ? $"×{pc.Count}" : $"Lv.{pc.Level}";
                background = AddText2Image(background,text, p, font, color);
            }
            //绘制星星
            string str = "";
            for (int i = 0; i < pc.StarCount; i++)
            {
                str += "1";
            }
            for (int i = 0; i < pc.EmptyStarCount; i++)
            {
                str += "0";
            }
            char[] ch = str.ToCharArray();
            int trap = 26;
            //奇数与偶数的位置不同
            if (str.Length % 2 == 0)
            {
                int x = 103 - (str.Length / 2) * trap; int y = 171;
                for (int i = 0; i < str.Length; i++)
                {
                    Image img;
                    if (ch[i] == '1')
                    {
                        img = Image.FromFile($@"{dir}装备卡\框\StarBig #1916506.png");
                    }
                    else
                    {
                        img = Image.FromFile($@"{dir}装备卡\框\StarBigGray.png");
                    }
                    background = CombinImage(background, img, x, y, 33, 33);
                    x += trap;
                }
            }
            else
            {
                int x = 90 - (str.Length / 2) * trap; int y = 171;
                for (int i = 0; i < str.Length; i++)
                {
                    Image img;
                    if (ch[i] == '1')
                    {
                        img = Image.FromFile($@"{dir}装备卡\框\StarBig #1916506.png");
                    }
                    else
                    {
                        img = Image.FromFile($@"{dir}装备卡\框\StarBigGray.png");
                    }
                    background = CombinImage(background, img, x, y, 33, 33);
                    x += trap;
                }
            }
            return background;
        }

        /// <summary>
        /// 给图片添加文字水印
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="text">文字</param>
        /// <param name="p">文本位置中心点坐标</param>
        /// <param name="font">字体</param>
        /// <param name="fontColor">字体颜色</param>
        /// <returns></returns>
        public static Image AddText2Image(Image img, string text, Point p, Font font, Color fontColor)
        {
            using (var g = Graphics.FromImage(img))
            using (var brush = new SolidBrush(fontColor))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var sizeF = g.MeasureString(text, font);
                g.ResetTransform();
                g.TranslateTransform(p.X, p.Y);
                g.DrawString(text, font, brush, new PointF(-sizeF.Width / 2, -sizeF.Height / 2));
            }
            return img;
        }
        private static string GetDate()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private static int CalcPicLength(long number)
        {
            int length = 0;
            for (int i = 0; i < number.ToString().Length; i++)
            {
                switch (number.ToString().Substring(i, 1))
                {
                    case "0":
                        length += 21;
                        break;
                    case "1":
                        length += 13;
                        break;
                    case "2":
                        length += 21;
                        break;
                    case "3":
                        length += 21;
                        break;
                    case "4":
                        length += 20;
                        break;
                    case "5":
                        length += 21;
                        break;
                    case "6":
                        length += 21;
                        break;
                    case "7":
                        length += 17;
                        break;
                    case "8":
                        length += 21;
                        break;
                    case "9":
                        length += 21;
                        break;
                }
            }
            length -= Convert.ToInt32(Math.Round(WordTrap * (number.ToString().Length - 1)));
            return length;
        }
    }

}
