using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureCarts.Api
{
    public class NikeApi:BaseApi
    {
        public readonly string LoginUrl = "https://www.nike.com/profile/login?Content-Locale=en_US";

        public readonly string CartUrl = "https://secure-store.nike.com/us/checkout/html/cart.jsp";

        public BaseApiResponse Login(string username, string password)
        {
            var data = new Dictionary<string, string>();
            
            data.Add("login", username);

            data.Add("password", password);

            data.Add("rememberMe", "false");
            
            return DoPost(LoginUrl, data);
        }
        

        public BaseApiResponse GetCartData(BaseApiResponse loginResponse)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            
            headers.Add("NikePlusId", loginResponse.Headers["NikePlusId"]);

            return DoGet(CartUrl, headers, loginResponse.Cookies);
        }
    }
}