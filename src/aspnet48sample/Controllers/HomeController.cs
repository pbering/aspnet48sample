using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace aspnet48sample.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Lazy<string> _targetFrameworkName = new(() => AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName);
        private static readonly Lazy<IEnumerable<KeyValuePair<string, string>>> _ipAddress = new(GetIpAddresses);
        private static readonly Lazy<IEnumerable<KeyValuePair<string, string>>> _environmentVariables = new(GetEnvironmentVariables);

        public ActionResult Index()
        {
            var values = new List<KeyValuePair<string, string>>(_ipAddress.Value);

            values.Insert(0, new KeyValuePair<string, string>("Hostname", Environment.MachineName));
            values.Add(new KeyValuePair<string, string>("AspNetVersion", _targetFrameworkName.Value));
            values.Add(new KeyValuePair<string, string>("ServerVariables", "/srv"));
            values.Add(new KeyValuePair<string, string>("EnvironmentVariables", "/env"));

            return RenderValues(values);
        }

        public ActionResult ServerVariables()
        {
            var values = new Dictionary<string, string>();

            foreach (string entry in Request.ServerVariables)
            {
                if (entry == "ALL_RAW" || entry == "ALL_HTTP")
                {
                    continue;
                }

                values.Add(entry, Request.ServerVariables[entry]);
            }

            return RenderValues(values.OrderBy(x => x.Key));
        }

        public ActionResult EnvironmentVariables()
        {
            return RenderValues(_environmentVariables.Value);
        }

        private ActionResult RenderValues(IEnumerable<KeyValuePair<string, string>> values)
        {
            var body = new StringBuilder();

            foreach (var entry in values)
            {
                body.AppendFormat("{0}: {1}", entry.Key, entry.Value);
                body.AppendLine();
            }

            return Content(body.ToString(), "text/plain");
        }

        private static IEnumerable<KeyValuePair<string, string>> GetIpAddresses()
        {
            var values = new List<KeyValuePair<string, string>>();

            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                values.Add(new KeyValuePair<string, string>("IP", ip.ToString()));
            }

            return values;
        }

        private static IEnumerable<KeyValuePair<string, string>> GetEnvironmentVariables()
        {
            var values = new Dictionary<string, string>();

            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                values.Add(entry.Key.ToString(), entry.Value.ToString());
            }

            return values.OrderBy(x => x.Key);
        }
    }
}