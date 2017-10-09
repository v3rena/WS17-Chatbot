using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Chatbot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //Database.SetInitializer(new TestInitializer());
            /*
            using (var ctx = new TestContext())
            {
                Test test = new Test() { TestName = "it worked" };

                ctx.Test.Add(test);
                ctx.SaveChanges();
            }
            */

            using (var context = new TestContext())
            {
                var test = new Test() { TestName = "it worked" };
                context.Test.Add(test);
                context.SaveChanges();
            }
        }
    }
}
