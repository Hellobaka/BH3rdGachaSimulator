using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Native.Sdk.Cqp.EventArgs;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using Newtonsoft.Json;

namespace me.luohuaming.Gacha.UI
{
    public class GetUpdate
    {
        static CQMenuCallEventArgs cq;
        public class Update
        {
            public string GachaVersion { get; set; }
            public string Date { get; set; }
            public string Whatsnew { get; set; }
        }
        public Update GetVersion(CQMenuCallEventArgs e)
        {
            cq = e;
            string str=GetHtmlWithUtf("https://raw.githubusercontent.com/Hellobaka/BH3rdGachaSimulator/master/New.json");
            //string version = str.Substring(str.IndexOf("<div class=\"app_name\">水银崩坏三抽卡模拟<span class=\"app_version app_monospace\">")+ "<div class=\"app_name\">水银崩坏三抽卡模拟<span class=\"app_version app_monospace\">".Length, 5);
            Update version= JsonConvert.DeserializeObject<Update>(str);
            return version;
        }
        /// <summary>
        /// C#使用GZIP解压缩完整读取网页内容 Get方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetHtmlWithUtf(string Url) 
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            //request.Host = "cqp.cc"; 
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            //request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            //request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.9");
            //request.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
            //request.Method = "GET";
            //request.ContentType = "text/html;charset=UTF-8";
            //request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Mobile Safari/537.36";
            string sHTML = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            sHTML = reader.ReadToEnd();
                        }
                    }
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            sHTML = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            sHTML = reader.ReadToEnd();
                        }
                    }
                }
            }
            return sHTML;
        }

    }
}
