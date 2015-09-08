using HtmlAgilityPack;
using SecureCarts.Api;
using SecureCarts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
            TheData="",
            NikeUserName = "fehimdervisbegovic@gmail.com",
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
                m.ResponseData = nike.GetCartData(formData.NikeUserName, formData.NikePassword);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(m.ResponseData.Content);

                foreach (HtmlNode n in doc.DocumentNode.SelectNodes("//div[contains(@class, 'ch4_cartItemActions')]"))
                    n.Remove();

                foreach (HtmlNode n in doc.DocumentNode.SelectNodes("//div[contains(@class, 'ch4_cartItem')]"))
                {
                    if (n.Attributes.Contains("data-qa-product-id"))
                    {
                        m.TheData += n.OuterHtml;
                        //ch4_cartItemActions
                    }
                    
                    
                    
                }

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