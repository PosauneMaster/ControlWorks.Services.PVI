using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Common
{
    public class ConnectionInfo
    {
        public string Name { get; set; }
        public string DataSource { get; set; }
        public bool? IntegratedSecurity { get; set; }
        public string InitialCatalog { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Timeout { get; set; }

    }
}
