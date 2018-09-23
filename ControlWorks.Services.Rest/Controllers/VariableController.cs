using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ControlWorks.Services.Rest.Processors;
using log4net;

namespace ControlWorks.Services.Rest.Controllers
{
    public class VariablesController : ApiController
    {
        private readonly ILog _log = LogManager.GetLogger("ControlWorksLogger");

        [HttpGet]
        public async Task<IHttpActionResult> GetByCpu(string id)
        {
            try
            {
                var variableProcessor = new VariableProcessor(WebApiApplication.PviApp);

                var settings = await variableProcessor.FindByCpuName(id);

                if (settings == null)
                {
                    var message = "Variables not found";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
                return Ok(settings);
            }
            catch (Exception ex)
            {
                ex.Data.Add("VariableController.Operation", "GetVariables");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAciveByCpu(string id)
        {
            try
            {
                var variableProcessor = new VariableProcessor(WebApiApplication.PviApp);

                var settings = await variableProcessor.FindActiveByCpuName(id);

                if (settings == null)
                {
                    var message = "Variables not found";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
                return Ok(settings);
            }
            catch (Exception ex)
            {
                ex.Data.Add("VariableController.Operation", "GetVariables");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


    }
}
