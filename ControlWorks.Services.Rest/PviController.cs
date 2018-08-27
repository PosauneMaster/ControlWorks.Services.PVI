using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;

namespace ControlWorks.Services.Rest
{
    public class PviController : ApiController
    {
        private readonly ILog _log = LogManager.GetLogger("FileLogger");

        public IHttpActionResult GetDetails() 
        {
            try
            {
                var processor = new ServiceProcessor(null);

                var details = processor.GetServiceDetails();

                if (details == null)
                {
                    var message = "Pvi Service not found";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                ex.Data.Add("PviController.Operation", "GetDetails");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



    }
}
