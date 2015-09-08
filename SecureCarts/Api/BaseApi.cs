using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SecureCarts.Api
{
    public class BaseApiResponse {

        public BaseApiResponse()
        {
            Headers = new Dictionary<string, string>();

            Cookies = new CookieCollection();
        }

        public Dictionary<string, string> Headers { get; set; }
        
        public CookieCollection Cookies { get; set; }

        public string Content { get; set; }
    }

    public class BaseApi
    {
        public BaseApiResponse DoGet(string url, Dictionary<string, string> headers, CookieCollection cookies)
        {
            BaseApiResponse bAR = new BaseApiResponse();

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.0";

            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            //setting up custom headers
            if (headers != null)
            {
                foreach (var h in headers)
                    request.Headers[h.Key] = h.Value;
            }

            //setting up custom cookies
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                foreach (Cookie c in cookies)
                    request.CookieContainer.Add(c);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //headers
            foreach (var h in response.Headers.AllKeys)
                bAR.Headers.Add(h, response.Headers[h]);

            //cookies
            foreach (Cookie c in response.Cookies)
                bAR.Cookies.Add(c);

            if (bAR.Headers.ContainsKey("Set-Cookie") && !String.IsNullOrEmpty(bAR.Headers["Set-Cookie"]))
            {
                var cc = CookieHelper.GetAllCookiesFromHeader(bAR.Headers["Set-Cookie"], "");

                foreach (Cookie c in cc)
                    bAR.Cookies.Add(c);
            }

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            bAR.Content = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();

            return bAR;
        }

        public BaseApiResponse DoPost(string url, Dictionary<string, string> formData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.0";
            
            request.Accept = "application/json, text/javascript, */*; q=0.01";

            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            string rawFormData = "";

            foreach (var d in formData)
            {
                if (String.IsNullOrEmpty(rawFormData))
                    rawFormData = String.Format("{0}={1}", d.Key, HttpContext.Current.Server.UrlEncode(d.Value));
                else
                    rawFormData += String.Format("&{0}={1}", d.Key, HttpContext.Current.Server.UrlEncode(d.Value));
            }

            request.Method = "POST";

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(rawFormData);
            }

            var response = (HttpWebResponse)request.GetResponse();

            BaseApiResponse bAR = new BaseApiResponse();

            //headers
            foreach (var h in response.Headers.AllKeys)
                bAR.Headers.Add(h, response.Headers[h]);

            //cookies
            foreach (Cookie c in response.Cookies)            
                bAR.Cookies.Add(c);

            if (bAR.Headers.ContainsKey("Set-Cookie") && !String.IsNullOrEmpty(bAR.Headers["Set-Cookie"]))
            {
                var cc = CookieHelper.GetAllCookiesFromHeader(bAR.Headers["Set-Cookie"], "");

                foreach (Cookie c in cc)
                    bAR.Cookies.Add(c);
            }
                

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content. 
            bAR.Content = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();

            return bAR;
        }
    }


}