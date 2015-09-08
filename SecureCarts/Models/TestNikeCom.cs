using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SecureCarts.Api;

namespace SecureCarts.Models
{
    public class TestNikeCom
    {
        public string Url { get; set; }
        public string TheData { get; set; }

        public string NikeUserName { get; set; }

        public string NikePassword { get; set; }

        public BaseApiResponse ResponseData { get; set; }
    }
}