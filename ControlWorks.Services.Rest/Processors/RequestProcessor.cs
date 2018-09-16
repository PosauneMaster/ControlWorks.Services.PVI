using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Common;
using ControlWorks.Services.PVI.Pvi;
using ControlWorks.Services.PVI;
using log4net;
using ControlWorks.Services.PVI.Panel;

namespace ControlWorks.Services.Rest.Processors
{
    public interface IRequestProcessor
    {
        Task<List<CpuDetailResponse>> GetCpuDetails();
        Task<CpuDetailResponse> GetCpuByName(string name);
        Task<CpuDetailResponse> GetCpuByIp(string ip);
        Task Add(CpuInfoRequest request);
        Task Update(CpuInfoRequest request);
        Task DeleteCpuByName(string name);
        Task DeleteCpuByIp(string ip);

    }
    public class RequestProcessor : BaseProcessor, IRequestProcessor
    {
        private readonly ILog _log = LogManager.GetLogger("ControlWorksLogger");

        private IPviApplication _application;

        public RequestProcessor() { }

        public RequestProcessor(IPviApplication application)
        {
            _application = application;
        }

        public async Task<List<CpuDetailResponse>> GetCpuDetails()
        {
            var result = await Task.Run(() => _application.GetCpuData().ToList());

            return result;
        }

        public async Task<CpuDetailResponse> GetCpuByName(string name)
        {
            var result = await Task.Run(() => _application.GetCpuByName(name));

            return result;
        }

        public async Task<CpuDetailResponse> GetCpuByIp(string ip)
        {
            var result = await Task.Run(() => _application.GetCpuByIp(ip));

            return result;
        }

        public async Task Add(CpuInfoRequest request)
        {
            var info = new CpuInfo()
            {
                Name = request.Name,
                Description = request.Description,
                IpAddress = request.IpAddress
            };

            _log.Info($"RequestProcessor Operation=Add request={ToJson(request)}");

            await Task.Run(() => _application.AddCpu(info));
        }

        public async Task Update(CpuInfoRequest request)
        {
            var info = new CpuInfo()
            {
                Name = request.Name,
                Description = request.Description,
                IpAddress = request.IpAddress
            };

            _log.Info($"RequestProcessor Operation=Update request={ToJson(request)}");

            await Task.Run(() => _application.UpdateCpu(info));
        }

        public async Task DeleteCpuByName(string name)
        {
            _log.Info($"RequestProcessor Operation=DeleteCpuByName name={name}");

            await Task.Run(() => _application.DeleteCpuByName(name));
        }

        public async Task DeleteCpuByIp(string ip)
        {
            _log.Info($"RequestProcessor Operation=DeleteCpuByIp name={ip}");

            await Task.Run(() => _application.DeleteCpuByIp(ip));
        }
    }
}
