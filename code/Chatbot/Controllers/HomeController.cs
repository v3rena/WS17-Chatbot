using Chatbot.Interfaces;
using System.Web.Mvc;

namespace Chatbot.Controllers
{
    public class HomeController : Controller
    {
        private IBusinessLayer _bl;

        public HomeController(IBusinessLayer bl)
        {
            _bl = bl;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "ChatBot";

            return View();
        }
    }
}
