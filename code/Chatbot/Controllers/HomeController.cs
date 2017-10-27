using Chatbot.Interfaces;
using System.Web;
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
            var request = HttpContext.Request;
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, HttpRuntime.AppDomainAppVirtualPath == "/" ? "" : HttpRuntime.AppDomainAppVirtualPath);
            ViewBag.BasePath = baseUrl;

            ViewBag.Title = "ChatBot";

            return View();
        }
    }
}
