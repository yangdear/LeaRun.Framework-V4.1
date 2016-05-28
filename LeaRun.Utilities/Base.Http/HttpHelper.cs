using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LeaRun.Utilities
{
    public static class HttpHelper
    {
        static HttpHelper()
        {
            ServicePointManager.DefaultConnectionLimit = 512;
            UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17";
            Timeout = 100000;
        }

        class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address) as HttpWebRequest;
                if (request == null) return null;
                request.Timeout = Timeout;
                request.UserAgent = UserAgent;
                return request;
            }
        }

        /// <summary>
        /// 获取或设置 使用的UserAgent信息
        /// </summary>
        /// <remarks>
        /// 可以到<see cref="http://www.sum16.com/resource/user-agent-list.html"/>查看更多User-Agent
        /// </remarks>
        public static String UserAgent { get; set; }
        /// <summary>
        /// 获取或设置 请求超时时间
        /// </summary>
        public static Int32 Timeout { get; set; }

        public static Boolean GetContentString(String url, out String message, Encoding encoding = null)
        {
            try
            {
                if (encoding == null) encoding = Encoding.UTF8;
                using (var wc = new MyWebClient())
                {
                    message = encoding.GetString(wc.DownloadData(url));
                    return true;
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
        }

        /// <summary>
        ///  POST数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static String Post(String url, Byte[] data, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            String str;
            using (var wc = new MyWebClient())
            {
                wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
                var ret = wc.UploadData(url, "POST", data);
                str = encoding.GetString(ret);
            }
            return str;
        }

        public static Byte[] DownloadData(String address)
        {
            Byte[] data;
            using (var wc = new MyWebClient())
            {
                data = wc.DownloadData(address);
            }
            return data;
        }
        public static Int64 GetContentLength(String url)
        {
            Int64 length;
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = UserAgent;
            req.Method = "HEAD";
            req.Timeout = 5000;
            var res = (HttpWebResponse)req.GetResponse();
            if (res.StatusCode == HttpStatusCode.OK)
            {
                length = res.ContentLength;
            }
            else
            {
                length = -1;
            }
            res.Close();
            return length;
        }
    }
}
