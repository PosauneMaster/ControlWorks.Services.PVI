using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ControlWorks.Services.PVI.Variables;
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
                    var message = $"Variables for Cpu {id} not found";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
                return Ok(settings);
            }
            catch (Exception ex)
            {
                ex.Data.Add("VariableController.Operation", "GetByCpu");
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
                ex.Data.Add("VariableController.Operation", "GetAciveByCpu");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetDetails(string id)
        {
            try
            {
                var variableProcessor = new VariableProcessor(WebApiApplication.PviApp);

                var settings = await variableProcessor.GetVariableDetails(id);

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


        [HttpPost]
        [Route("api/Variables/Update")]
        public async Task<IHttpActionResult> Update([FromBody]VariableInfo variableInfo)
        {
            try
            {
                if (variableInfo == null)
                {
                    var message = "Variable Info is null";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
                var variableProcessor = new VariableProcessor(WebApiApplication.PviApp);
                await variableProcessor.UpdateVariables(variableInfo.CpuName, variableInfo.Variables);

                return Ok();
            }
            catch (Exception ex)
            {
                ex.Data.Add("VariableController.Operation", "Update");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


        [HttpPost]
        [Route("api/Variables/Add")]
        public async Task<IHttpActionResult> Add([FromBody]VariableDetail variableDetail)
        {
            try
            {
                if (variableDetail == null)
                {
                    var message = "Variable Info is null";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
                var variableProcessor = new VariableProcessor(WebApiApplication.PviApp);
                await variableProcessor.Add(variableDetail.CpuName, variableDetail.VariableName);

                return Ok();
            }
            catch (Exception ex)
            {
                ex.Data.Add("VariableController.Operation", "Update");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("api/Variables/Delete")]
        public async Task<IHttpActionResult> Delete([FromBody]VariableDetail variableDetail)
        {
            try
            {
                if (variableDetail == null)
                {
                    var message = "Variable Name is null";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }
                var variableProcessor = new VariableProcessor(WebApiApplication.PviApp);
                await variableProcessor.RemoveVariables(variableDetail.CpuName, new List<string>(new[] { variableDetail.VariableName }));

                return Ok();
            }
            catch (Exception ex)
            {
                ex.Data.Add("VariableController.Operation", "Update");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }


    }
}
