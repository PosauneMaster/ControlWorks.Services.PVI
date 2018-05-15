using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI
{
    public class DataResponse
    {
        public string Name { get; set; }
        public ExpandoObject Data { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
