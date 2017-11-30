using Chatbot.BusinessLayer.Interfaces;
using Chatbot.BusinessLayer.Models;
using Chatbot.DataAccessLayer.Context;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Chatbot.Controllers
{
    [RoutePrefix("plugin-configuration")]
    public class PluginConfigurationController : Controller
    {
        private readonly IBusinessLayer businessLayer;

        public PluginConfigurationController(IBusinessLayer businessLayer)
        {
            this.businessLayer = businessLayer;
        }

        // GET: PluginConfigurations
        public ActionResult Index()
        {
            return View(businessLayer.GetPluginConfigurations());
        }

        // GET: PluginConfigurations/Details/EchoBot/ConfigSetting
        public ActionResult Details(string name, string key)
        {
            if (name == null || key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PluginConfiguration pluginConfiguration = businessLayer.GetPluginConfiguration(name, key);
            if (pluginConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(pluginConfiguration);
        }

        // GET: Hospitals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PluginConfigurations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Key,Value")] PluginConfiguration pluginConfiguration)
        {
            if (ModelState.IsValid)
            {
                businessLayer.AddPluginConfiguration(pluginConfiguration);
                return RedirectToAction("Index");
            }

            return View(pluginConfiguration);
        }

        // GET: PluginConfigurations/Edit/EchoBot/ConfigSetting
        public ActionResult Edit(string name, string key)
        {
            if (name == null || key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PluginConfiguration pluginConfiguration = businessLayer.GetPluginConfiguration(name, key);
            if (pluginConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(pluginConfiguration);
        }

        // POST: PluginConfigurations/Edit/EchoBot/ConfigSetting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Key,Value")] PluginConfiguration pluginConfiguration)
        {
            if (ModelState.IsValid)
            {
                businessLayer.SavePluginConfiguration(pluginConfiguration);
                return RedirectToAction("Index");
            }
            return View(pluginConfiguration);
        }

        // GET: PluginConfigurations/Delete/EchoBot/ConfigSetting
        public ActionResult Delete(string name, string key)
        {
            if (name == null || key == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PluginConfiguration pluginConfiguration = businessLayer.GetPluginConfiguration(name, key);
            if (pluginConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(pluginConfiguration);
        }

        // POST: PluginConfigurations/Delete/EchoBot/ConfigSetting
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string name, string key)
        {
            businessLayer.DeletePluginConfiguration(name, key);
            return RedirectToAction("Index");
        }
    }
}
