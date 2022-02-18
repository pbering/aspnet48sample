using System.Collections.Generic;
using System.Linq;

namespace aspnet48sample.Models
{
    public class DataModel
    {
        public string MachineName { get; set; }
        public string AspNetVersion { get; internal set; }
        public IOrderedEnumerable<KeyValuePair<string, string>> EnvironmentVariables { get; set; }
        public IOrderedEnumerable<KeyValuePair<string, string>> ServerVariables { get; set; }
    }
}