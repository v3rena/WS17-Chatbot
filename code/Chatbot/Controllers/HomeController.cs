using Chatbot.Interfaces;
using System.Web.Mvc;

namespace Chatbot.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public ActionResult Index()
        {
            ViewBag.Title = "ChatBot";

            return View();
        }
    }
}
