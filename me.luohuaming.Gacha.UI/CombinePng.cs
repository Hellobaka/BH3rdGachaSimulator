using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Tool.IniConfig;

namespace me.luohuaming.Gacha.UI
{
    public class CombinePng
    {
        //private CQMenuCallEventArgs cq = CQSave.cq_menu;
        private CQGroupMessageEventArgs cq = CQSave.cq_group;

        static public int Width_AP;
        static public int Width_Gold;
        static public int Width_Diamond;
        static public int Height_1 = 45;
        static public float WordTrap = 3;
        static IniConfig ini;
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
            catch(Exception e)
            {
                CQSave.CQLog.Info("图片合成", $"发生错误，错误信息:{e.Message} map1.size={map1.Size} map2.size={map2.Size} bg.size={background.Size}");
            }
            return bitMap;
        }

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
            catch(Exception e)
            {
                CQSave.CQLog.Info("图片合成", $"发生错误，错误信息:{e.Message} map1.size={map1.Size} map2.size={map2.Size} bg.size={background.Size}");
            }
            return bitMap;
        }

        public Image Money(int money)
        {
            Bitmap bitMap = new Bitmap(CalcPicLength(money), 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, CalcPicLength(money), 29));
            float x = 0;
            for (int i = 0; i < money.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{CQSave.AppDirectory}数字\{money.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{CQSave.AppDirectory}数字\{money.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }
        public Image diamond(int diamond)
        {
            Bitmap bitMap = new Bitmap(CalcPicLength(diamond), 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, CalcPicLength(diamond), 29));
            float x = 0;
            for (int i = 0; i < diamond.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{CQSave.AppDirectory}数字\{diamond.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{CQSave.AppDirectory}数字\{diamond.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }
        public Image AP(int AP, int AP_Max)
        {
            Bitmap bitMap = new Bitmap(21 * AP.ToString().Length + 21 * AP_Max.ToString().Length + 22, 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, 21 * AP.ToString().Length, 29));
            float x = 0;
            for (int i = 0; i < AP.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{CQSave.AppDirectory}数字\{AP.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{CQSave.AppDirectory}数字\{AP.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            Image img_Last = Image.FromFile($@"{CQSave.AppDirectory}数字\{AP.ToString().Substring(AP.ToString().Length - 1, 1)}.png");
            x += img_Last.Width - WordTrap;
            Image img_2 = Image.FromFile($@"{CQSave.AppDirectory}数字\斜杠.png");
            g1.DrawImage(img_2, x + 1, 0, img_2.Width, 29);
            x += img_2.Width + 1;
            for (int i = 0; i < AP_Max.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{CQSave.AppDirectory}数字\{AP_Max.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{CQSave.AppDirectory}数字\{AP_Max.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }
        public Image Money(int money,int a)
        {
            string appdirectory = CQSave.AppDirectory;
            Bitmap bitMap = new Bitmap(CalcPicLength(money), 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, CalcPicLength(money), 29));
            float x = 0;
            for (int i = 0; i < money.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{appdirectory}数字\{money.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{appdirectory}数字\{money.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }
        public Image diamond(int diamond,int a)
        {
            string appdirectory = CQSave.AppDirectory;

            Bitmap bitMap = new Bitmap(CalcPicLength(diamond), 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, CalcPicLength(diamond), 29));
            float x = 0;
            for (int i = 0; i < diamond.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{appdirectory}数字\{diamond.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{appdirectory}数字\{diamond.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }
        public Image AP(int AP, int AP_Max,int a)
        {
            string appdirectory = CQSave.AppDirectory;

            Bitmap bitMap = new Bitmap(21 * AP.ToString().Length + 21 * AP_Max.ToString().Length + 22, 29);
            Graphics g1 = Graphics.FromImage(bitMap);
            g1.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, 21 * AP.ToString().Length, 29));
            float x = 0;
            for (int i = 0; i < AP.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{appdirectory}数字\{AP.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{appdirectory}数字\{AP.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            Image img_Last = Image.FromFile($@"{appdirectory}数字\{AP.ToString().Substring(AP.ToString().Length - 1, 1)}.png");
            x += img_Last.Width - WordTrap;
            Image img_2 = Image.FromFile($@"{appdirectory}数字\斜杠.png");
            g1.DrawImage(img_2, x + 1, 0, img_2.Width, 29);
            x += img_2.Width + 1;
            for (int i = 0; i < AP_Max.ToString().Length; i++)
            {
                Image img = Image.FromFile($@"{appdirectory}数字\{AP_Max.ToString().Substring(i, 1)}.png");
                if (i > 0)
                {
                    Image img_His = Image.FromFile($@"{appdirectory}数字\{AP_Max.ToString().Substring(i - 1, 1)}.png");
                    x += img_His.Width - WordTrap;
                }
                g1.DrawImage(img, x, 0, img.Width, 29);
            }
            return bitMap;
        }

        public string Gacha(List<抽卡.GachaResult> ls, int region, int JZpos, int count)
        {
            string appdirectory = CQSave.AppDirectory;
            int x, y;
            x = 160;
            y = 190;
            Image background = Image.FromFile($@"{appdirectory}装备卡\框\抽卡背景.png");
            Image img = null;
            Random rd = new Random();
            foreach (var item in ls)
            {
                img = GenerateCard(item.evaluation, 1, item);
                background = CombinImage(background, img, x, y, img.Width,img.Height);
                if (background == null) return null;
                if (item.type == 抽卡.TypeS.Stigmata.ToString())
                {
                    switch (item.name.Substring(item.name.Length - 1))
                    {
                        case "上":
                            img = Image.FromFile($@"{appdirectory}装备卡\框\Stigmata1.png");
                            background = CombinImage(background, img, x - 18, y - 18, 65, 65);
                            break;
                        case "中":
                            img = Image.FromFile($@"{appdirectory}装备卡\框\Stigmata2.png");
                            background = CombinImage(background, img, x - 18, y - 18, 65, 65);
                            break;
                        case "下":
                            img = Image.FromFile($@"{appdirectory}装备卡\框\Stigmata3.png");
                            background = CombinImage(background, img, x - 18, y - 18, 65, 65);
                            break;
                    }
                }
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
            int money = rd.Next(100000, 100000000);
            Image img_1 = Money(money, 1);
            Width_Gold = 1620 + (164 - img_1.Width) / 2;
            background = CombinImage(background, img_1, Width_Gold, Height_1);
            //int Diamond = rd.Next(0, 30000);
            Image img_2 = diamond(rd.Next(0, 456413), 1);
            Width_Diamond = 1975 + (111 - img_2.Width) / 2;
            background = CombinImage(background, img_2, Width_Diamond, Height_1);
            int ap_Max = rd.Next(154, 165);
            int ap = rd.Next(0, ap_Max);
            Image img_3 = AP(ap, ap_Max, 1);
            Width_AP = 1319 + (127 - img_3.Width) / 2;
            background = CombinImage(background, img_3, Width_AP, Height_1);
            string name = GetDate();
            switch (region)
            {
                case 0:
                    if (count == 1)
                    {
                        img_3 = Image.FromFile($@"{appdirectory}装备卡\框\扩充单抽.png");
                    }
                    else
                    {
                        img_3 = Image.FromFile($@"{appdirectory}装备卡\框\扩充十连.png");
                    }
                    background = CombinImage(background, img_3, 126, 960);
                    break;
                case 1:
                    switch (JZpos)
                    {
                        case 1:
                            if (count == 1)
                            {
                                img_3 = Image.FromFile($@"{appdirectory}装备卡\框\精准A单抽.png");
                            }
                            else
                            {
                                img_3 = Image.FromFile($@"{appdirectory}装备卡\框\精准A十连.png");
                            }
                            break;
                        case 2:
                            if (count == 2)
                            {
                                img_3 = Image.FromFile($@"{appdirectory}装备卡\框\精准B单抽.png");
                            }
                            else
                            {
                                img_3 = Image.FromFile($@"{appdirectory}装备卡\框\精准B十连.png");
                            }
                            break;
                    }
                    background = CombinImage(background, img_3, 126, 960);
                    break;
                case 2:
                    if (count == 1)
                    {
                        img_3 = Image.FromFile($@"{appdirectory}装备卡\框\标配单抽.png");
                    }
                    else
                    {
                        img_3 = Image.FromFile($@"{appdirectory}装备卡\框\标配十连.png");
                    }
                    background = CombinImage(background, img_3, 126, 960);
                    break;
            }
            if (!Directory.Exists($@"{CQSave.ImageDirectory}\装备结果"))
            {
                Directory.CreateDirectory($@"{CQSave.ImageDirectory}\装备结果");
            }
            background.Save($@"{CQSave.ImageDirectory}\装备结果\{name}.jpg", ImageFormat.Jpeg);
            background.Dispose();
            img.Dispose();
            img_1.Dispose();
            img_2.Dispose();
            img_3.Dispose();
            return $"{GetAppImageDirectory(appdirectory)}\\装备结果\\{name}.jpg";
        }
        /// <summary>
        /// 合成子模块
        /// </summary>
        /// <param name="ls">抽卡结果数组</param>
        /// <param name="region">来源:0 扩充，1 精准，2标配</param>
        /// <param name="JZPos">精准池来源:1 A池，2 B池</param>
        /// <param name="count">单抽与十连</param>
        /// <param name="Diamond">水晶数目</param>
        /// <returns></returns>
        public string Gacha(List<Gacha.GachaResult> ls,int region,int JZPos,int count,int Diamond)
        {
            try
            {
                int x, y;
                x = 160;
                y = 190;
                Image background = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\抽卡背景.png");
                Image img = null;
                Random rd = new Random();
                long temp1, temp2;
                if (cq != null)
                {
                     temp1= cq.FromGroup.Id;
                     temp2 = cq.FromQQ.Id;
                }
                else
                {
                     temp1 = -1;
                     temp2 = CQSave.cq_private.FromQQ.Id;
                }
                ls = ItemExistInRepositories(ls, temp1, temp2);
                foreach (var item in ls)
                {
                    img = GenerateCard(item.evaluation, 1, item);
                    background = CombinImage(background, img, x, y);
                    //x + 133,y - 17
                    if (item.isnew)
                    {
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\New!.png");
                        background = CombinImage(background, img, x + 133, y - 17, img.Width, img.Height);
                    }
                    if (item.type == 抽卡.TypeS.Stigmata.ToString())
                    {
                        switch (item.name.Substring(item.name.Length - 1))
                        {
                            case "上":
                                img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\Stigmata1.png");
                                background = CombinImage(background, img, x - 18, y - 18, 65, 65);
                                break;
                            case "中":
                                img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\Stigmata2.png");
                                background = CombinImage(background, img, x - 18, y - 18, 65, 65);
                                break;
                            case "下":
                                img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\Stigmata3.png");
                                background = CombinImage(background, img, x - 18, y - 18, 65, 65);
                                break;
                        }
                    }
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
                int money = rd.Next(100000, 100000000);
                Image img_1 = Money(money);
                Width_Gold = 1620 + (164 - img_1.Width) / 2;
                background = CombinImage(background, img_1, Width_Gold, Height_1);
                //int Diamond = rd.Next(0, 30000);
                Image img_2 = diamond(Diamond);
                Width_Diamond = 1975 + (111 - img_2.Width) / 2;
                background = CombinImage(background, img_2, Width_Diamond, Height_1);
                int ap_Max = rd.Next(154, 165);
                int ap = rd.Next(0, ap_Max);
                Image img_3 = AP(ap, ap_Max);
                Width_AP = 1319 + (127 - img_3.Width) / 2;
                background = CombinImage(background, img_3, Width_AP, Height_1);
                string name = GetDate();
                switch (region)
                {
                    case 0:
                        if (count == 1)
                        {
                            img_3 = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\扩充单抽.png");
                        }
                        else
                        {
                            img_3 = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\扩充十连.png");
                        }
                        background = CombinImage(background, img_3, 126, 960);
                        break;
                    case 1:
                        switch (JZPos)
                        {
                            case 1:
                                if (count == 1)
                                {
                                    img_3 = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\精准A单抽.png");
                                }
                                else
                                {
                                    img_3 = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\精准A十连.png");
                                }
                                break;
                            case 2:
                                if (count == 2)
                                {
                                    img_3 = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\精准B单抽.png");
                                }
                                else
                                {
                                    img_3 = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\精准B十连.png");
                                }
                                break;
                        }
                        background = CombinImage(background, img_3, 126, 960);
                        break;
                    case 2:
                        if (count == 1)
                        {
                            img_3 = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\标配单抽.png");
                        }
                        else
                        {
                            img_3 = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\标配十连.png");
                        }
                        background = CombinImage(background, img_3, 126, 960);
                        break;
                }
                if (!Directory.Exists($@"{CQSave.ImageDirectory}\装备结果"))
                {
                    Directory.CreateDirectory($@"{CQSave.ImageDirectory}\装备结果");
                }
                Point p = new Point(1471, 813);
                Font font = new Font("汉仪丫丫体简", 15F);
                Color color = Color.FromArgb(0, 0, 0);
                background = AddText2Image(background, "Powered by @水银之翼", p, font, color, 0);
                ini = new IniConfig(CQSave.AppDirectory + "Config.ini");
                ini.Load();
                if (ini.Object["ExtraConfig"]["ImageFormat"].GetValueOrDefault("jpg") == "jpg")
                {
                    if (!Directory.Exists($@"{CQSave.ImageDirectory}\装备结果"))
                    {
                        Directory.CreateDirectory($@"{CQSave.ImageDirectory}\装备结果");
                    }
                    //cq.CQLog.Info($@"{GetAppImageDirectory(CQSave.AppDirectory)}\装备结果\{name}.jpg");
                    background.Save($@"{CQSave.ImageDirectory}\装备结果\{name}.jpg", ImageFormat.Jpeg);
                    background.Dispose();
                    img.Dispose();
                    img_1.Dispose();
                    img_2.Dispose();
                    img_3.Dispose();
                    return $"装备结果\\{name}.jpg";
                }
                else
                {
                    if (!Directory.Exists($@"{CQSave.ImageDirectory}\装备结果"))
                    {
                        Directory.CreateDirectory($@"{CQSave.ImageDirectory}\装备结果");
                    }
                    //cq.CQLog.Info($@"{GetAppImageDirectory(CQSave.AppDirectory)}\装备结果\{name}.jpg");
                    background.Save($@"{CQSave.ImageDirectory}\装备结果\{name}.png");
                    background.Dispose();
                    img.Dispose();
                    img_1.Dispose();
                    img_2.Dispose();
                    img_3.Dispose();
                    return $"装备结果\\{name}.png";
                }

            }
            catch (Exception e)
            {
                cq.CQLog.Error($"图片合成错误，错误信息{e.Message}");
                return null;
            }
        }
        /// <summary>
        /// 给图片添加文字水印
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="text">文字</param>
        /// <param name="p">文本位置中心点坐标</param>
        /// <param name="font">字体</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="angle">旋转角度（顺时针）</param>
        /// <returns></returns>
        public static Image AddText2Image(Image img, string text, Point p, Font font, Color fontColor, int angle)
        {
            using (var g = Graphics.FromImage(img))
            using (var brush = new SolidBrush(fontColor))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var sizeF = g.MeasureString(text, font);
                g.ResetTransform();
                g.TranslateTransform(p.X, p.Y);
                g.RotateTransform(angle);
                g.DrawString(text, font, brush, new PointF(-sizeF.Width / 2, -sizeF.Height / 2));
            }
            return img;
        }

        public static Image AddSlimText2Image(Image img, string text, Point p, Font font, Color fontColor, int angle)
        {
            using (var g = Graphics.FromImage(img))
            using (var brush = new SolidBrush(fontColor))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                char[] ch = text.ToCharArray();
                SizeF charSize;
                float sepDist = -13f;
                var sizeF = g.MeasureString(text, font);
                PointF pf = new PointF(-sizeF.Width / 2 + 5, -sizeF.Height / 2);
                g.ResetTransform();
                g.TranslateTransform(p.X, p.Y);
                g.RotateTransform(angle);
                foreach (char c in ch)
                {
                    //获取字符尺寸
                    charSize = g.MeasureString(c.ToString(), font);
                    if (c == '0') pf.Y += 1;
                    g.DrawString(c.ToString(), font, brush, pf);
                    pf.X += (charSize.Width + sepDist);
                    if (c == '0') pf.Y -= 1;
                }
            }
            return img;
        }
        /// <summary>
        /// 合成装备卡图片
        /// </summary>
        /// <param name="fullstar">几颗黄色星星</param>
        /// <param name="emptystar">几颗灰色星星</param>
        /// <returns></returns>
        public Image GenerateCard(int fullstar, int emptystar, 抽卡.GachaResult gr)
        {
            string appdirectory = CQSave.AppDirectory;
            Image background = (gr.quality == 1) ? Image.FromFile($@"{appdirectory}装备卡\框\框蓝.png") : Image.FromFile($@"{appdirectory}装备卡\框\框.png");
            Image main = null;
            try
            {
                switch (gr.type)
                {
                    case "Character":
                        main = Image.FromFile($@"{appdirectory}装备卡\角色卡\{gr.name}.png");
                        background = CombinImage(background, main, 5, 14, 196, 172);
                        break;
                    case "debri":
                        main = Image.FromFile($@"{appdirectory}装备卡\碎片\{gr.name}.png");
                        background = CombinImage(background, main, 5, 14, 196, 172);
                        break;
                    case "Stigmata":
                        main = Image.FromFile($@"{appdirectory}装备卡\圣痕卡\{gr.name}.png");
                        background = CombinImage(background, main, 5, 14, 196, 172);

                        break;
                    case "Material":
                        main = Image.FromFile($@"{appdirectory}装备卡\材料\{gr.name}.png");
                        background = CombinImage(background, main, 26, 17, 178, 170);
                        break;
                    case "Weapon":
                        main = Image.FromFile($@"{appdirectory}装备卡\武器\{gr.name}.png");
                        background = CombinImage(background, main, 5, 14, 196, 172);
                        break;
                }
            }
            catch
            {
                main = Image.FromFile($@"{appdirectory}装备卡\框\ItemEmpty #1004496.png");
                background = CombinImage(background, main, 48, 13, 119, 172);
            }

            if (gr.type == 抽卡.TypeS.Character.ToString())
            {
                Image img = null;
                switch (gr.class_)
                {
                    case "SSS":
                        img = Image.FromFile($@"{appdirectory}装备卡\框\Star_Avatar_5M.png");
                        break;
                    case "SS":
                        img = Image.FromFile($@"{appdirectory}装备卡\框\Star_Avatar_4M.png");
                        break;
                    case "S":
                        img = Image.FromFile($@"{appdirectory}装备卡\框\Star_Avatar_3M.png");
                        break;
                    case "A":
                        img = Image.FromFile($@"{appdirectory}装备卡\框\Star_Avatar_2M.png");
                        break;
                    case "B":
                        img = Image.FromFile($@"{appdirectory}装备卡\框\Star_Avatar_1M.png");
                        break;
                }
                //Image img = Image.FromFile((gr.class_ == "S") ? $@"{appdirectory}装备卡\框\Star_Avatar_3M.png" : $@"{appdirectory}装备卡\框\Star_Avatar_2M.png");
                background = CombinImage(background, img, 53, 160, 106, 91);
                return background;
            }
            if (gr.type == 抽卡.TypeS.Material.ToString() || gr.type == 抽卡.TypeS.debri.ToString())
            {
                //Point p = new Point(106, 225);
                Point p = new Point((gr.count.ToString().Length == 1) ? 88 : 85, 225);
                Font font = new Font("Impact", 25F);
                Color color = Color.FromArgb(67, 67, 67);
                background = AddSlimText2Image(background, "×", p, font, color, 0);
                p = new Point((gr.count.ToString().Length == 1) ? 106 : 108, 228);
                font = new Font("方正兰亭粗黑_GBK", 25F);
                background = AddSlimText2Image(background, $"{gr.count}", p, font, color, 0);
            }
            else
            {
                Point p = new Point((gr.level.ToString().Length == 1) ? 94 : 92, 225);
                Font font = new Font("Impact", 25F);
                Color color = Color.FromArgb(67, 67, 67);
                background = AddText2Image(background, "Lv.", p, font, color, 0);
                p = new Point((gr.level.ToString().Length == 1) ? 113 : 117, 228);
                font = new Font("方正兰亭粗黑_GBK", 25F);
                background = AddSlimText2Image(background, $"{gr.level}", p, font, color, 0);
            }
            string str = "";
            for (int i = 0; i < fullstar; i++)
            {
                str += "1";
            }
            for (int i = 0; i < emptystar; i++)
            {
                str += "0";
            }
            if (gr.type == 抽卡.TypeS.debri.ToString()) return background;
            char[] ch = str.ToCharArray();
            int trap = 26;
            if (str.Length % 2 == 0)
            {
                int x = 103 - (str.Length / 2) * trap; int y = 171;
                Image img = null;
                for (int i = 0; i < str.Length; i++)
                {
                    if (ch[i] == '1')
                    {
                        img = Image.FromFile($@"{appdirectory}装备卡\框\StarBig #1916506.png");
                    }
                    else
                    {
                        img = Image.FromFile($@"{appdirectory}装备卡\框\StarBigGray.png");
                    }
                    background = CombinImage(background, img, x, y, 33, 33);
                    x += trap;
                }
            }
            else
            {
                int x = 90 - (str.Length / 2) * trap; int y = 171;
                Image img = null;
                for (int i = 0; i < str.Length; i++)
                {
                    if (ch[i] == '1')
                    {
                        img = Image.FromFile($@"{appdirectory}装备卡\框\StarBig #1916506.png");
                    }
                    else
                    {
                        img = Image.FromFile($@"{appdirectory}装备卡\框\StarBigGray.png");
                    }
                    background = CombinImage(background, img, x, y, 33, 33);
                    x += trap;
                }
            }
            return background;
        }

        public Image GenerateCard(int fullstar, int emptystar, Gacha.GachaResult gr)
        {

            Image background = (gr.quality == 1) ? Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\框蓝.png") : Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\框.png");
            Image main = null;
            try
            {
                switch (gr.type)
                {
                    case "Character":
                        main = Image.FromFile($@"{CQSave.AppDirectory}装备卡\角色卡\{gr.name}.png");
                        background = CombinImage(background, main, 5, 14, 196, 172);
                        break;
                    case "debri":
                        main = Image.FromFile($@"{CQSave.AppDirectory}装备卡\碎片\{gr.name}.png");
                        background = CombinImage(background, main, 5, 14, 196, 172);
                        break;
                    case "Stigmata":
                        main = Image.FromFile($@"{CQSave.AppDirectory}装备卡\圣痕卡\{gr.name}.png");
                        background = CombinImage(background, main, 5, 14, 196, 172);

                        break;
                    case "Material":
                        main = Image.FromFile($@"{CQSave.AppDirectory}装备卡\材料\{gr.name}.png");
                        background = CombinImage(background, main, 26, 17, 178, 170);
                        break;
                    case "Weapon":
                        main = Image.FromFile($@"{CQSave.AppDirectory}装备卡\武器\{gr.name}.png");
                        background = CombinImage(background, main, 5, 14, 196, 172);
                        break;
                }
            }
            catch
            {
                main = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\ItemEmpty #1004496.png");
                background = CombinImage(background, main, 48, 13, 119, 172);
                ini = new IniConfig(CQSave.AppDirectory + "Config.ini");
                ini.Load();
                long controlgroup =Convert.ToInt64( ini.Object["后台群"]["Id"].GetValueOrDefault("0"));
                if (controlgroup != 0) cq.CQApi.SendGroupMessage(controlgroup, $"发现图片缺失,请排查是否为命名错误 type={gr.type} name={gr.name}");                
            }

            if (gr.type == 抽卡.TypeS.Character.ToString())
            {
                Image img = null;
                switch (gr.class_)
                {
                    case "SSS":
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\Star_Avatar_5M.png");
                        break;
                    case "SS":
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\Star_Avatar_4M.png");
                        break;
                    case "S":
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\Star_Avatar_3M.png");
                        break;
                    case "A":
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\Star_Avatar_2M.png");
                        break;
                    case "B":
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\Star_Avatar_1M.png");
                        break;
                }
                //Image img = Image.FromFile((gr.class_ == "S") ? $@"{CQSave.AppDirectory}装备卡\框\Star_Avatar_3M.png" : $@"{CQSave.AppDirectory}装备卡\框\Star_Avatar_2M.png");
                background = CombinImage(background, img, 53, 160, 106, 91);
                return background;
            }
            if (gr.type == 抽卡.TypeS.Material.ToString() || gr.type == 抽卡.TypeS.debri.ToString())
            {
                //Point p = new Point(106, 225);
                Point p = new Point((gr.count.ToString().Length == 1) ? 88 : 85, 225);
                Font font = new Font("Impact", 25F);
                Color color = Color.FromArgb(67, 67, 67);
                background = AddSlimText2Image(background, "×", p, font, color, 0);
                p = new Point((gr.count.ToString().Length == 1) ? 106 : 108, 228);
                font = new Font("方正兰亭粗黑_GBK", 25F);
                background = AddSlimText2Image(background, $"{gr.count}", p, font, color, 0);
            }
            else
            {
                Point p = new Point((gr.level.ToString().Length == 1) ? 94 : 92, 225);
                Font font = new Font("Impact", 25F);
                Color color = Color.FromArgb(67, 67, 67);
                background = AddText2Image(background, "Lv.", p, font, color, 0);
                p = new Point((gr.level.ToString().Length == 1) ? 113 : 117, 228);
                font = new Font("方正兰亭粗黑_GBK", 25F);
                background = AddSlimText2Image(background, $"{gr.level}", p, font, color, 0);
            }
            string str = "";
            for (int i = 0; i < fullstar; i++)
            {
                str += "1";
            }
            for (int i = 0; i < emptystar; i++)
            {
                str += "0";
            }
            if (gr.type == 抽卡.TypeS.debri.ToString()) return background;
            char[] ch = str.ToCharArray();
            int trap = 26;
            if (str.Length % 2 == 0)
            {
                int x = 103 - (str.Length / 2) * trap; int y = 171;
                Image img = null;
                for (int i = 0; i < str.Length; i++)
                {
                    if (ch[i] == '1')
                    {
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\StarBig #1916506.png");
                    }
                    else
                    {
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\StarBigGray.png");
                    }
                    background = CombinImage(background, img, x, y, 33, 33);
                    x += trap;
                }
            }
            else
            {
                int x = 90 - (str.Length / 2) * trap; int y = 171;
                Image img = null;
                for (int i = 0; i < str.Length; i++)
                {
                    if (ch[i] == '1')
                    {
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\StarBig #1916506.png");
                    }
                    else
                    {
                        img = Image.FromFile($@"{CQSave.AppDirectory}装备卡\框\StarBigGray.png");
                    }
                    background = CombinImage(background, img, x, y, 33, 33);
                    x += trap;
                }
            }
            return background;
        }

        static string GetDate()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static string GetAppImageDirectory(string dir)
        {
            var ImageDirectory = Path.Combine(Environment.CurrentDirectory, "data", "image");
            //var path= dir.Substring(0, dir.IndexOf("\\app\\me.cqp.luohuaming.Gacha")) + "\\image";

            return ImageDirectory;
        }

        int CalcPicLength(long number)
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
        /// <summary>
        /// 检查同名项目是否存在于仓库中
        /// </summary>
        /// <param name="name"></param>
        /// <param name="groupid"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public List<Gacha.GachaResult> ItemExistInRepositories(List<Gacha.GachaResult> ls, long groupid, long qq)
        {
            string str;
            string path = $@"{CQSave.cq_start.CQApi.AppDirectory}data.db";
            SQLiteConnection cn = new SQLiteConnection("data source=" + path);
            cn.Open();
            foreach(var item in ls)
            {
                Thread.Sleep(100);
                str = $"select count(*) from Repositories where name='{item.name}' and fromgroup={groupid} and qq={qq}";
                SQLiteCommand cmd = new SQLiteCommand(str, cn);
                using (SQLiteDataReader sr = cmd.ExecuteReader())
                {                
                    sr.Read();
                    item.isnew=(sr.GetInt32(0) == 0) ? true : false;
                }
            }
            cn.Close();
            GC.Collect();
            return ls;
        }
    }

}
