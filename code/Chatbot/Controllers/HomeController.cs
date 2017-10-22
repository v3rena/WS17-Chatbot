using Chatbot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chatbot.Controllers
{
    public class HomeController : Controller
    {
        private IBusinessLayer<Test> _bl;

        public HomeController(IBusinessLayer<Test> bl)
        {
            _bl = bl;
        }


        public ActionResult Index()
        {
            ViewBag.Title = _bl.GetName();

            return View();
        }
    }
}
