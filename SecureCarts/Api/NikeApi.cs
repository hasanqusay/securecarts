using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SecureCarts.Api
{
    public class NikeApi:BaseApi
    {
        public readonly string LoginUrl = "https://www.nike.com/profile/login?Content-Locale=en_US";

        public readonly string CartUrl = "https://secure-store.nike.com/us/checkout/html/cart.jsp?Country=US";
       
        public BaseApiResponse Login(string username, string password)
        {
            var data = new Dictionary<string, string>();
            
            data.Add("login", username);

            data.Add("password", password);

            data.Add("rememberMe", "false");
            
            return DoPost(LoginUrl, data);
        }


        public BaseApiResponse GetCartData(string username, string password)
        {
            var l = Login(username, password);

            var cc = new CookieCollection();

            cc.Add(new Cookie("CONSUMERCHOICE", "us/en_us", "/", "nike.com"));
            cc.Add(new Cookie("NIKE_COMMERCE_COUNTRY", "US", "/", "nike.com"));
            cc.Add(new Cookie("NIKE_COMMERCE_LANG_LOCALE", "en_US", "/", "nike.com"));
            cc.Add(new Cookie("nike_locale", "us/en_us", "/", "nike.com"));

            var hc = CookieHelper.GetAllCookiesFromHeader(l.Headers["Set-Cookie"], "nike.com");

            cc.Add(hc);

            return DoGet(CartUrl, null, cc);
        }
    }
}