using System;
using System.IO;
using System.Net;
using System.Text;

namespace Assets.Scripts.Utils
{
    public static class SimpleHttpClient
    {
        public static string Get(string request, int timeout)
        {
            /* Check inputs. */
            if (request == null)
                throw new ArgumentNullException("Request string cannot be null in HTTP GET request.");
            if (timeout < 0)
                throw new ArgumentOutOfRangeException("Integer timeout cannot be less than zero in HTTP GET request.");            

            /* Do HTTP GET request. */
            WebResponse resp = null;
            StreamReader sr = null;
            try
            {
                var uri = new Uri(request, UriKind.Absolute);
                var req = WebRequest.Create(uri);
                req.Timeout = timeout;

                resp = req.GetResponse();
                var stream = resp.GetResponseStream();
                if (stream == null)
                    return string.Empty; // No response.

                sr = new StreamReader(stream, Encoding.UTF8);
                var text = sr.ReadToEnd();

                return text;
            }            
            finally
            {
                if (sr != null)
                    sr.Close();
                if (resp != null)
                    resp.Close();
            }
        }

        public static string Post(string url, string request, int timeout)
        {
            /* Check inputs. */
            if (url == null)
                throw new ArgumentNullException("URL string cannot be null in HTTP POST request.");
            if (request == null)
                throw new ArgumentNullException("Data string cannot be null in HTTP POST request.");
            if (timeout < 0)
                throw new ArgumentOutOfRangeException("Integer timeout cannot be less than zero in HTTP POST request.");          

            /* Do HTTP POST request. */
            WebResponse resp = null;
            StreamReader sr = null;
            try
            {
                var uri = new Uri(url);
                var req = WebRequest.Create(uri);
                req.Method = "POST";
                req.Timeout = timeout;
                req.ContentType = "application/x-www-form-urlencoded";
                var sentData = Encoding.UTF8.GetBytes(request);
                req.ContentLength = sentData.Length;
                var sendStream = req.GetRequestStream();
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();

                resp = req.GetResponse();
                var receiveStream = resp.GetResponseStream();
                if (receiveStream == null)
                    return string.Empty; // No response.

                sr = new StreamReader(receiveStream, Encoding.UTF8);
                var read = new char[256];
                var count = sr.Read(read, 0, 256);
                var text = string.Empty;
                while (count > 0)
                {
                    var str = new string(read, 0, count);
                    text += str;
                    count = sr.Read(read, 0, 256);
                }

                return text;
            }            
            finally
            {
                if (sr != null)
                    sr.Close();
                if (resp != null)
                    resp.Close();
            }
        }
    }
}
