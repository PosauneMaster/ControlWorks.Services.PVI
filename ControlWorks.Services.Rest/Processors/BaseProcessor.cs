using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlWorks.Services.Rest
{
    public abstract class BaseProcessor
    {
        protected virtual string ToJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch { return String.Empty; }
        }

    }
}
