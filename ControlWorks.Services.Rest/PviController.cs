using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ControlWorks.Services.Rest
{
    public class PviController : ApiController
    {
        public async Task<IHttpActionResult> GetDetails()
        {
            try
            {
                var requestProcessor = WebApiApplication.Locator.GetInstance<IServiceProcessor>();

                var details = await requestProcessor.GetServiceDetails();

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
                WebApiApplication.Logger.Log(new LogEntry(LoggingEventType.Error, ex.Message, ex));
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



    }
}
