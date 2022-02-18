using aspnet48sample.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace aspnet48sample.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Lazy<string> _targetFrameworkName = new Lazy<string>(() => AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName);
        private static readonly Lazy<string> _ipAddress = new Lazy<string>(() => string.Join(", ", Dns.GetHostEntry(Dns.GetHostName()).AddressList.ToList()));

        public ActionResult Index()
        {
            var model = new DataModel
            {
                MachineName = Environment.MachineName,
                AspNetVersion = _targetFrameworkName.Value,
                IpAddress = _ipAddress.Value
            };

            var environmentVariables = new Dictionary<string, string>();

            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                environmentVariables.Add(entry.Key.ToString(), entry.Value.ToString());
            }

            model.EnvironmentVariables = environmentVariables.OrderBy(x => x.Key);

            var serverVariables = new Dictionary<string, string>();

            foreach (string entry in Request.ServerVariables)
            {
                serverVariables.Add(entry, Request.ServerVariables[entry]);
            }

            model.ServerVariables = serverVariables.OrderBy(x => x.Key);

            return View(model);
        }
    }
}