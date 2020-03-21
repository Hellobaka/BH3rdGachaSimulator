using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;

namespace me.cqp.luohuaming.Gacha.Code
{
    class GetDragon
    {
        #region json解析类
        public class TalkativeList
        {
            public string uin { get; set; }
            public string avatar { get; set; }
            public string name { get; set; }
            public string desc { get; set; }
            public string btnText { get; set; }
            public string text { get; set; }
        }
        public class Dragon
        {
            public List<TalkativeList> talkativeList { get; set; }
        }
        #endregion
        public class talk
        {
            public string qqid;
            public string name;
            public string days;
        }
        static CQGroupMessageEventArgs cq;
        public List<talk> GetDragonList(CQGroupMessageEventArgs e)
        {
            cq = e;
            string response = GetHtmlWithUtf($@"https://qun.qq.com/interactive/honorlist?gc={e.FromGroup.Id}&type=1");
            string talkjson = getjson(response);
            List<talk> ls = GetTalkActiveList(talkjson);
            return ls;
        }
        string getjson(string response)
        {
            int startindex = response.IndexOf("window.__INITIAL_STATE__=") + "window.__INITIAL_STATE__=".Length;
            int length = response.IndexOf("</script>", startindex) - startindex;
            string str = response.Substring(startindex, length);
            return str;
        }
        List<talk> GetTalkActiveList(string response)
        {
            List<talk> ls = new List<talk>();
            Dragon result = JsonConvert.DeserializeObject<Dragon>(response);
            List<TalkativeList> temp = new List<TalkativeList>();
            temp = result.talkativeList;
            foreach (var item in temp)
            {
                talk tk = new talk();
                tk.qqid = item.uin;
                tk.name = item.name;
                tk.days = item.desc;
                ls.Add(tk);
            }
            return ls;
        }
        /// <summary>
        /// C#使用GZIP解压缩完整读取网页内容 Get方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetHtmlWithUtf(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Host = "qun.qq.com";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, br");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.9"); 
            request.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
            string cookie = cq.CQApi.GetCookies("qun.qq.com");
            //cq.CQLog.WriteLine(Native.Sdk.Cqp.Enum.CQLogLevel.Info,"Cookie获取",cookie);
            request.Headers.Add(HttpRequestHeader.Cookie,cookie);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Mobile Safari/537.36";
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
