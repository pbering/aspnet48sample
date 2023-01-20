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
        private static readonly Lazy<string> _ipAddress = new Lazy<string>(() => string.Join(", ", Dns.GetHostEntry(Dns.GetHostName()).AddressList.ToList()));

        public ActionResult Index()
        {
            var body = new StringBuilder();

            body.AppendLine("# INFO");
            body.AppendLine();
            body.AppendFormat("MachineName: {0}", Environment.MachineName);
            body.AppendLine();
            body.AppendFormat("AspNetVersion: {0}", _targetFrameworkName.Value);
            body.AppendLine();
            body.AppendFormat("IpAddresses: {0}", _ipAddress.Value);
            body.AppendLine();

            body.AppendLine();
            body.AppendLine("# SERVER VARIABLES");
            body.AppendLine();

            foreach (string entry in Request.ServerVariables)
            {
                body.AppendFormat("{0}: {1}", entry, Request.ServerVariables[entry]);
            }

            body.AppendLine();
            body.AppendLine();
            body.AppendLine("# ENVIRONMENT VARIABLES");

            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                body.AppendFormat("{0}: {1}", entry.Key.ToString(), entry.Value.ToString());
                body.AppendLine();
            }

            return Content(body.ToString(), "text/plain");
        }
    }
}