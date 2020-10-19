using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using log4net;

namespace ControlWorks.Services.Rest.Controllers
{
    public class DiagnosticController : ApiController
    {
        private readonly ILog _log = LogManager.GetLogger("ControlWorksLogger");

        [HttpGet]
        [Route("api/Diagnostic/GetHeartbeat")]
        public IHttpActionResult GetHeartbeat()
        {
            try
            {
                return Ok(DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                ex.Data.Add("DiagnosticController.Operation", "GetHeartbeat");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }

    public class HeartBeatInfo
    {
        public DateTime RequestTime { get; set; }
        public string RequestName { get; set; }
    }

}
