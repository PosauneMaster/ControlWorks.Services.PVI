using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ControlWorks.Common;
using ControlWorks.Services.Rest.Processors;
using log4net;

namespace ControlWorks.Services.Rest.Controllers
{
    public class CpuController : ApiController
    {
        private readonly ILog _log = LogManager.GetLogger("ControlWorksLogger");

        [HttpGet]
        [Route("api/Cpu/GetDetails")]
        public async Task<IHttpActionResult> GetDetails()
        {
            try
            {
                var requestProcessor = new RequestProcessor(WebApiApplication.PviApp);

                var details = await requestProcessor.GetCpuDetails();

                if (details == null)
                {
                    var message = "Cpu services not found";
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

        [HttpGet]
        public async Task<IHttpActionResult> GetByName(string id)
        {
            try
            {
                var requestProcessor = new RequestProcessor(WebApiApplication.PviApp);

                var details = await requestProcessor.GetCpuByName(id);

                if (details == null)
                {
                    var message = $"Cpu name {id} not found";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                ex.Data.Add("PviController.Operation", "GetByName");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByIp(string id)
        {
            try
            {
                var requestProcessor = new RequestProcessor(WebApiApplication.PviApp);

                var details = await requestProcessor.GetCpuByIp(id);

                if (details == null)
                {
                    var message = $"Cpu IP {id} services not found";
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                }

                return Ok(details);
            }
            catch (Exception ex)
            {
                ex.Data.Add("PviController.Operation", "GetByIp");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Add(CpuInfoRequest info)
        {
            try
            {
                var requestProcessor = new RequestProcessor(WebApiApplication.PviApp);

                await requestProcessor.Add(info);

                return Ok();
            }
            catch (Exception ex)
            {
                ex.Data.Add("PviController.Operation", "Add");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteByIp(string id)
        {
            try
            {
                var requestProcessor = new RequestProcessor(WebApiApplication.PviApp);

                await requestProcessor.DeleteCpuByIp(id);

                return Ok();
            }
            catch (Exception ex)
            {
                ex.Data.Add("PviController.Operation", "DeleteByIp");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteByName(string id)
        {
            try
            {
                var requestProcessor = new RequestProcessor(WebApiApplication.PviApp);

                await requestProcessor.DeleteCpuByName(id);

                return Ok();
            }
            catch (Exception ex)
            {
                ex.Data.Add("PviController.Operation", "DeleteByName");
                _log.Error(ex.Message, ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
