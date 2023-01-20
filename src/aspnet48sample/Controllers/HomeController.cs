using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace aspnet48sample.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Lazy<string> _targetFrameworkName = new Lazy<string>(() => AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName);
        private static readonly Lazy<IPAddress[]> _ipAddress = new Lazy<IPAddress[]>(() => Dns.GetHostEntry(Dns.GetHostName()).AddressList);

        public ActionResult Index()
        {
            var body = new StringBuilder();

            body.AppendFormat("Hostname: {0}", Environment.MachineName);
            body.AppendLine();
            
            foreach (var entry in _ipAddress.Value)
            {
                body.AppendFormat("IP: {0}", entry);
                body.AppendLine();
            }
            
            body.AppendFormat("AspNetVersion: {0}", _targetFrameworkName.Value);
            body.AppendLine();
            body.AppendLine("ServerVariables: /srv");
            body.AppendLine("EnvironmentVariables: /env");
            
            return Content(body.ToString(), "text/plain");
        }

        public ActionResult ServerVariables()
        {
            var body = new StringBuilder();

            foreach (string entry in Request.ServerVariables)
            {
                body.AppendFormat("{0}: {1}", entry, Request.ServerVariables[entry]);
            }

            return Content(body.ToString(), "text/plain");
        }

        public ActionResult EnvironmentVariables()
        {
            var body = new StringBuilder();
                      
            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                body.AppendFormat("{0}: {1}", entry.Key.ToString(), entry.Value.ToString());
                body.AppendLine();
            }

            return Content(body.ToString(), "text/plain");
        }
    }
}