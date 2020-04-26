using Native.Tool.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Diagnostics;
using me.luohuaming.Gacha.UI;

namespace me.luohuaming.Gacha.Code
{
    class PaChonger
    {
        #region --json解析类--
        public class Item_list
        {
            public string itemstring { get; set; }
        }

        public class Data
        {
            public List<Item_list> item_list { get; set; }
        }

        public class OCR_Result
        {
            public int ret { get; set; }
            public string msg { get; set; }
            public Data data { get; set; }
        }
        public OCR_Result res;
        #endregion

        #region --字段--
        public static string UPAWeapon;
        public static string UPAStigmata;
        public static string UPBWeapon;
        public static string UPBStigmata;
        public static DateTime JZAStartTime;
        public static List<string> JZWeapon = new List<string>();
        public static List<string> JZStigmata = new List<string>();
        public static DateTime JZBStartTime;
        public static List<string> KC = new List<string>();
        public static DateTime KCStartTime;
        public static string ret_Text;
        #endregion

        /// <summary>
        /// 爬取崩坏三公告来获取池子
        /// </summary>
        /// <param name="opiton">寻找公告的关键字</param>
        public string GetPoolOnline(string opiton)
        {
            Initialize();
            string blackboard = "https://www.bh3.com/news/cate/175";
            try
            {
                byte[] by = HttpWebClient.Get(blackboard);
                string a = Encoding.UTF8.GetString(by);
                HtmlDocument htmlDoc = new HtmlDocument();

                for (int q = 1; q <= 10; q++)
                {
                    htmlDoc.LoadHtml(a);
                    var rootnode = htmlDoc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[2]/div[1]/div[2]/div[{q}]");
                    var url = rootnode.SelectSingleNode("a[1]").GetAttributeValue("href", "");
                    var Url = rootnode.SelectSingleNode("a[1]/div[2]/div[1]/div[1]");

                    url = $"https://www.bh3.com{url.Trim()}";
                    string title = HttpUtility.HtmlDecode(Url.InnerText.Trim());                    

                    if (title.Contains(opiton))
                    {
                        ret_Text += $"{title} 链接:{url}\n";
                        byte[] by2 = HttpWebClient.Get(url);
                        string a2 = Encoding.UTF8.GetString(by2);
                        htmlDoc.LoadHtml(a2);
                        if (title.Contains("精准"))
                        {
                            try
                            {
                                rootnode = htmlDoc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[3]/div[2]");
                                var count = rootnode.ChildNodes.Count;
                                if (count > 60)
                                {
                                    string imgurl = "";
                                    List<string> Uptime = new List<string>();
                                    foreach (var item in rootnode.ChildNodes)
                                    {
                                        if (item.SelectSingleNode("img[1]") != null)
                                        {
                                            try
                                            {
                                                HttpWebClient.Get(item.SelectSingleNode("img[1]").GetAttributeValue("src", ""));
                                                imgurl = item.SelectSingleNode("img[1]").GetAttributeValue("src", "");
                                                Uptime = GetUpDate(imgurl);
                                                for (int i = 0; i < Uptime.Count; i++)
                                                {
                                                    Uptime[i] = Date2DateTtime(Uptime[i]).ToString();
                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    foreach (var item in rootnode.ChildNodes)
                                    {
                                        if (item.InnerText.Contains("精准补给A为"))
                                        {
                                            //UPA
                                            var result = GetUpStaff(item.InnerText);
                                            if (result.Count == 2)
                                            {
                                                ret_Text += $"精准A UP:{result[0]} {result[1]}\n";
                                                UPAWeapon=result[0];
                                                UPAStigmata=result[1];
                                            }
                                            try
                                            {
                                                if (Uptime.Count == 4)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";
                                                    JZAStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else if (Uptime.Count == 2)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";
                                                    JZAStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", "精准A时间获取异常");
                                                }
                                            }
                                            catch
                                            {
                                                CQSave.CQLog.Info("崩坏三公告获取", "精准A时间获取失败");
                                            }
                                        }
                                        if (item.InnerText.Contains("精准补给B为"))
                                        {
                                            //UPB
                                            var result = GetUpStaff(item.InnerText);
                                            if (result.Count == 2)
                                            {
                                                ret_Text += $"精准B UP:{result[0]} {result[1]}\n";
                                                UPBWeapon=result[0];
                                                UPBStigmata=result[1];
                                            }
                                            try
                                            {
                                                if (Uptime.Count == 4)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[2]}\n";
                                                    JZBStartTime = Convert.ToDateTime(Uptime[2]);
                                                }
                                                else if (Uptime.Count == 2)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";
                                                    JZBStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", "精准B时间获取异常");
                                                }
                                            }
                                            catch
                                            {
                                                CQSave.CQLog.Info("崩坏三公告获取", "精准B时间获取失败");
                                            }
                                        }
                                        if (item.InnerText.Contains("【★4武器】"))
                                        {
                                            var WeaponText = item;
                                            int weaponcount = 0;
                                            ret_Text += "池子内容:\n";
                                            for (int i = 0; i < 8; i++)
                                            {
                                                WeaponText = WeaponText.NextSibling.NextSibling;
                                                try
                                                {
                                                    List<string> WeaponList = GetItem(WeaponText.InnerText);
                                                    weaponcount += WeaponList.Count;
                                                    
                                                    foreach (var item2 in WeaponList)
                                                    {
                                                        ret_Text += $"{item2}\n";
                                                        JZWeapon.Add(item2);
                                                    }
                                                    if (weaponcount == 7) break;
                                                }
                                                catch (Exception ex)
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", ex.Message + " " + ex.StackTrace + "\n");
                                                }
                                            }
                                        }
                                        if (item.InnerText.Contains("【★4圣痕】"))
                                        {
                                            var StigmataText = item;
                                            ret_Text += "池子内容:\n";
                                            for (int i = 0; i < 5; i++)
                                            {
                                                StigmataText = StigmataText.NextSibling.NextSibling;
                                                try
                                                {
                                                    List<string> StigmataList = GetItem(StigmataText.InnerText);
                                                    
                                                    foreach (var item2 in StigmataList)
                                                    {
                                                        ret_Text += $"{item2}\n";                                                        
                                                        JZStigmata.Add(item2);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", ex.Message + " " + ex.StackTrace + "\n");
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (count == 16)
                                {
                                    string imgurl = "";
                                    List<string> Uptime = new List<string>();
                                    foreach (var item in rootnode.ChildNodes)
                                    {
                                        if (item.SelectSingleNode("img[1]") != null)
                                        {
                                            try
                                            {
                                                HttpWebClient.Get(item.SelectSingleNode("img[1]").GetAttributeValue("src", ""));
                                                imgurl = item.SelectSingleNode("img[1]").GetAttributeValue("src", "");
                                                Uptime = GetUpDate(imgurl);
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    foreach (var item in rootnode.ChildNodes)
                                    {
                                        if (item.InnerText.Contains("精准补给A为"))
                                        {
                                            //UPA
                                            var result = GetUpStaff(item.InnerText);
                                            if (result.Count == 4)
                                            {
                                                ret_Text += $"精准A UP:{result[0]} {result[1]}\n";
                                                UPAWeapon=result[0];
                                                UPAStigmata=result[1];
                                            }
                                            try
                                            {
                                                if (Uptime.Count == 4)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";
                                                    JZAStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else if (Uptime.Count == 2)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";
                                                    JZAStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", "精准A时间获取异常");
                                                }
                                            }
                                            catch
                                            {
                                                CQSave.CQLog.Info("崩坏三公告获取", "精准A时间获取失败");
                                            }
                                        }
                                        if (item.InnerText.Contains("精准补给B为"))
                                        {
                                            //UPB
                                            var result = GetUpStaff(item.InnerText);
                                            if (result.Count == 4)
                                            {
                                                ret_Text += $"精准B UP:{result[2]} {result[3]}\n";
                                                UPBWeapon=result[2];
                                                UPBStigmata=result[3];
                                            }
                                            try
                                            {
                                                if (Uptime.Count == 4)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";

                                                    JZBStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else if (Uptime.Count == 2)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";

                                                    JZBStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", "精准B时间获取异常");
                                                }
                                            }
                                            catch
                                            {
                                                CQSave.CQLog.Info("崩坏三公告获取", "精准B时间获取失败");
                                            }
                                        }
                                        if (item.InnerText.Contains("【★4武器】"))
                                        {
                                            string[] WeaponText = item.InnerText.Split('\n');
                                            ret_Text += "池子内容:\n";
                                            for (int i = 0; i < WeaponText.Length - 1; i++)
                                            {
                                                try
                                                {
                                                    List<string> WeaponList = GetItem(WeaponText[1 + i]); 
                                                    
                                                    foreach (var item2 in WeaponList)
                                                    {
                                                        ret_Text += $"{item2}\n";
                                                        JZWeapon.Add(item2);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", ex.Message + " " + ex.StackTrace + "\n");
                                                }
                                            }
                                        }
                                        if (item.InnerText.Contains("【★4圣痕】"))
                                        {
                                            string[] StigmataText = item.InnerText.Split('\n');
                                            ret_Text += "池子内容:\n";
                                            for (int i = 0; i < StigmataText.Length - 1; i++)
                                            {
                                                try
                                                {
                                                    List<string> StigmataList = GetItem(StigmataText[1 + i]);
                                                    
                                                    foreach (var item2 in StigmataList)
                                                    {
                                                        ret_Text += $"{item2}\n";
                                                        JZStigmata.Add(item2);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", ex.Message + " " + ex.StackTrace + "\n");
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (count == 14)
                                {
                                    string imgurl = "";
                                    List<string> Uptime = new List<string>();

                                    foreach (var item in rootnode.ChildNodes)
                                    {
                                        if (item.SelectSingleNode("img[1]") != null)
                                        {
                                            try
                                            {
                                                HttpWebClient.Get(item.SelectSingleNode("img[1]").GetAttributeValue("src", ""));
                                                imgurl = item.SelectSingleNode("img[1]").GetAttributeValue("src", "");
                                                Uptime = GetUpDate(imgurl);
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    foreach (var item in rootnode.ChildNodes)
                                    {
                                        if (item.InnerText.Contains("精准补给A为"))
                                        {
                                            //UPA
                                            var result = GetUpStaff(item.InnerText);
                                            if (result.Count == 2)
                                            {
                                                ret_Text += $"精准A UP:{result[0]} {result[1]}\n";
                                                UPAWeapon=result[0];
                                                UPAStigmata=result[1];
                                            }
                                            try
                                            {
                                                if (Uptime.Count == 4)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";

                                                    JZAStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else if (Uptime.Count == 2)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";

                                                    JZAStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", "精准A时间获取异常");
                                                }
                                            }
                                            catch
                                            {
                                                CQSave.CQLog.Info("崩坏三公告获取", "精准A时间获取失败");
                                            }
                                        }
                                        if (item.InnerText.Contains("精准补给B为"))
                                        {
                                            //UPB
                                            var result = GetUpStaff(item.InnerText);
                                            if (result.Count == 2)
                                            {
                                                ret_Text += $"精准B UP:{result[0]} {result[1]}\n";
                                                UPBWeapon=result[0];
                                                UPBStigmata=result[1];
                                            }
                                            try
                                            {
                                                if (Uptime.Count == 4)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";

                                                    JZAStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else if (Uptime.Count == 2)
                                                {
                                                    ret_Text += $"开始时间:{Uptime[0]}\n";

                                                    JZAStartTime = Convert.ToDateTime(Uptime[0]);
                                                }
                                                else
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", "精准B时间获取异常");
                                                }
                                            }
                                            catch
                                            {
                                                CQSave.CQLog.Info("崩坏三公告获取", "精准B时间获取失败");
                                            }
                                        }
                                        if (item.InnerText.Contains("【★4武器】"))
                                        {
                                            string[] WeaponText = item.InnerText.Split('\n');
                                            ret_Text += "池子内容:\n";
                                            for (int i = 0; i < WeaponText.Length - 1; i++)
                                            {
                                                try
                                                {
                                                    List<string> WeaponList = GetItem(WeaponText[1 + i]); 
                                                    
                                                    foreach (var item2 in WeaponList)
                                                    {
                                                        ret_Text += $"{item2}\n";
                                                        JZWeapon.Add(item2);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", ex.Message + " " + ex.StackTrace + "\n");
                                                }
                                            }
                                        }
                                        if (item.InnerText.Contains("【★4圣痕】"))
                                        {
                                            string[] StigmataText = item.InnerText.Split('\n');
                                            ret_Text += "池子内容:\n";
                                            for (int i = 0; i < StigmataText.Length - 1; i++)
                                            {
                                                try
                                                {
                                                    List<string> StigmataList = GetItem(StigmataText[1 + i]);
                                                    
                                                    foreach (var item2 in StigmataList)
                                                    {
                                                        ret_Text += $"{item2}\n";
                                                        JZStigmata.Add(item2);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    CQSave.CQLog.Info("崩坏三公告获取", ex.Message + " " + ex.StackTrace + "\n");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                CQSave.CQLog.Info("崩坏三公告获取", ex.Message + " " + ex.StackTrace + "\n");
                            }
                        }
                        else if (title.Contains("扩充"))
                        {
                            try
                            {
                                rootnode = htmlDoc.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/div[2]/div[1]/div[2]/div[1]/div[1]/div[3]/div[2]");
                                var count = rootnode.ChildNodes.Count;
                                string imgurl = "";
                                List<string> Uptime = new List<string>();
                                foreach (var item in rootnode.ChildNodes)
                                {
                                    if (item.SelectSingleNode("img[1]") != null)
                                    {
                                        try
                                        {
                                            HttpWebClient.Get(item.SelectSingleNode("img[1]").GetAttributeValue("src", ""));
                                            imgurl = item.SelectSingleNode("img[1]").GetAttributeValue("src", "");
                                            Uptime = GetUpDate(imgurl);
                                            for (int i = 0; i < Uptime.Count; i++)
                                            {
                                                Uptime[i] = Date2DateTtime(Uptime[i]).ToString();
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                //扩充项目
                                ret_Text += "扩充补给 UP:\n";
                                foreach (var item in GetUpStaff(title.Substring(0, title.IndexOf("扩充补给"))))
                                {
                                    ret_Text += $"{item}\n";
                                    KC.Add(item);
                                }
                                var kuochong = rootnode.SelectSingleNode($"p[2]");
                                GetUpStaff(kuochong.InnerText);
                                ret_Text += "其他角色:\n";
                                int KCCount = 0;
                                foreach (var item in res.data.item_list)
                                {
                                    string path = Path.Combine($@"{CQSave.AppDirectory}\装备卡\角色卡\", $"{item.itemstring}.png");                                    
                                    if (File.Exists(path))
                                    {
                                        ret_Text += $"{item.itemstring}\n";
                                        KC.Add(item.itemstring);
                                        KCCount++;
                                    }
                                    else if (item.itemstring.Length >= 5)
                                    {
                                        //路径修正
                                        string pathrepair = item.itemstring.Insert(item.itemstring.IndexOf("真红骑士") == -1 ? 3 : 4, "·");
                                        path = Path.Combine($@"{CQSave.AppDirectory}\装备卡\角色卡\", $"{pathrepair}.png");
                                        if (File.Exists(path))
                                        {
                                            ret_Text += $"{pathrepair}\n";
                                            KC.Add(pathrepair);
                                            KCCount++;
                                        }
                                    }
                                    if (KCCount == 3) break;
                                }
                                if (Uptime.Count < 2)
                                {
                                    CQSave.CQLog.Info("崩坏三公告获取", "扩充时间获取异常");
                                }
                                else
                                {
                                    ret_Text += $"开始时间:{Uptime[0]}\n";

                                    KCStartTime = Convert.ToDateTime(Uptime[0]);
                                }
                            }
                            catch (Exception ex)
                            {
                                CQSave.CQLog.Info("崩坏三公告获取", ex.Message + " " + ex.StackTrace + "\n");
                            }
                        }
                        //目前只支持寻找一次
                        break;
                    }
                }
                return ret_Text;
            }
            catch (Exception exc)
            {
                CQSave.CQLog.Info("崩坏三公告获取", exc.Message);
                return null;
            }
        }
        /// <summary>
        /// 字段初始化（重置）
        /// </summary>
        public void Initialize()
        {
            JZWeapon.Clear();
            JZStigmata.Clear();
            UPAWeapon = string.Empty;
            UPAStigmata = string.Empty;
            UPBWeapon = string.Empty;
            UPBStigmata = string.Empty;
            KC.Clear();
            ret_Text = "";
        }
        /// <summary>
        /// 格式化时间文本为DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DateTime Date2DateTtime(string str)
        {
            str = str.Replace(" ", "").Replace("（", "(").Replace("）", ")").Replace("：", ":").Replace("O", "0").Replace("o", "0").Replace("中午", "");
            str = Regex.Replace(str, @"\(周.*\)", "");
            DateTime dt = DateTime.ParseExact(str, "M月d日H:m", System.Globalization.CultureInfo.CurrentCulture);
            return dt;
        }
        /// <summary>
        /// 获取池子内所有武器或圣痕
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<string> GetItem(string str)
        {
            str = HttpUtility.HtmlDecode(str);
            str = str.Replace("：", ":").Replace(" ", "");
            str = str.Substring(str.IndexOf(":") + 1).Trim();
            str = Regex.Replace(str, "（.*）", "");
            if (str.Contains("|"))
            {
                return str.Split('|').ToList();
            }
            else
            {
                List<string> a = new List<string>
                {
                    str
                };
                return a;
            }
        }
        /// <summary>
        /// 筛选出字符串中的UP物品
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<string> GetUpStaff(string str)
        {
            var match = Regex.Matches(str, "「.*?」");
            List<string> a = new List<string>();
            foreach (var item in match)
            {
                a.Add(HttpUtility.HtmlDecode(item.ToString()).Replace("「", "").Replace("」", ""));
            }
            if (match.Count == 0)
            {
                a = str.Split('&').ToList();
            }
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = a.Count - 1; j > i; j--)
                {
                    if (a[i] == a[j])
                    {
                        a.RemoveAt(j);
                    }
                }
            }
            return a;
        }
        /// <summary>
        /// 通过OCR获取Up开始时间
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <returns></returns>
        public List<string> GetUpDate(string url)
        {
            HttpWebClient http = new HttpWebClient();
            http.DownloadFile(url, $"{CQSave.ImageDirectory}\\ocrtemp.jpg");

            List<string> result = new List<string>();
            res = TXOCR();
            if (res.ret != 0)
            {
                Debug.WriteLine(res.ret);
            }
            foreach (var item in res.data.item_list)
            {
                if (Regex.IsMatch(item.itemstring, @"\(周.*\)"))
                {
                    result.Add(res.data.item_list[res.data.item_list.IndexOf(item)].itemstring.ToUpper().Replace("（", "(").Replace("）", ")").Replace("O", "0"));
                }
            }
            return result;
        }
        //进行OCR
        public OCR_Result TXOCR()
        {
            string image = HttpTool.UrlEncode(Convert.ToBase64String(File.ReadAllBytes($"{CQSave.ImageDirectory}\\ocrtemp.jpg"))).Replace("%3d", "%3D").Replace("%2f", "%2F").Replace("%2b", "%2B");
            string app_id = "2123406403";
            string app_key = "KtGhR0iLzgWbdykj";
            string timestamp = GetTimeStamp().ToString();
            Random rd = new Random();
            string noicestr = rd.Next().ToString();
            string sign = $"app_id={app_id}&image={image}&nonce_str={noicestr}&time_stamp={timestamp}&app_key={app_key}";
            sign = GetMD5(sign).ToUpper();
            string url = $"https://api.ai.qq.com/fcgi-bin/ocr/ocr_generalocr";
            string data = $"app_id={app_id}&image={image}&nonce_str={noicestr}&time_stamp={timestamp}&sign={sign}";
            byte[] by = Encoding.UTF8.GetBytes(data);
            by = HttpWebClient.Post(url, by);
            string result = Encoding.UTF8.GetString(by);
            //richTextBox1.Text = result;
            return JsonConvert.DeserializeObject<OCR_Result>(result);
        }
        //获取时间戳
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        //MD5
        public string GetMD5(string str)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(str);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}
