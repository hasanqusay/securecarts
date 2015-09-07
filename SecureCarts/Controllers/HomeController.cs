using SecureCarts.Api;
using SecureCarts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SecureCarts.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var m = new TestNikeCom() { 
            TheData="Some čadkf alskjfd asljd čslakjf dčlkasj fčlajsdčflkajčflajfčklaj čfklja čfkja čfkljs fephpq98f pq98wefwiufls",
            Url = "http://www.nike.com/us/en_us" //"http://store.nike.com/us/en_us/pw/jordan-shoes/brkZc8d?ipp=120"
            };
            return View(m);
        }

        public ActionResult Index(TestNikeCom formData)
        {
            var m = new TestNikeCom()
            {
                TheData = "",
                Url = formData.Url
            };

            //calling Nike API
            var nike = new NikeApi();

            try
            {
                m.ResponseData = nike.Login("fehimdervisbegovic@gmail.com","qwERasDF12#");

                //m.ResponseData = nike.GetCartData(m.ResponseData);
            }
            catch (WebException we)
            {
                m.TheData = we.Message;
            }

            return View(m);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}