using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlWorks.Application.Configuration
{
    public class CpuClientInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public string IsConnected { get; set; }
        public string HasError { get; set; }
        public CpuClentErrorInfo Error { get; set; }

        public string ErrorCode
        {
            get
            { 
                if (Error != null)
                {
                    return Error.ErrorCode;
                }

                return String.Empty;
            }
        }

        public string ErrorText
        {
            get
            {
                if (Error != null)
                {
                    return Error.ErrorText;
                }

                return String.Empty;
            }
        }


    }

    public class CpuClentErrorInfo
    {
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }
}
