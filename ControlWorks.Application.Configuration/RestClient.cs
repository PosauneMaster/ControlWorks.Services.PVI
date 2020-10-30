using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlWorks.Application.Configuration
{
    public class RestClient
    {
        public event EventHandler<HeartbeatEventArgs> Heartbeat;
        public event EventHandler<CpuInfoEventArgs> CpuInfoUpdated;
        public event EventHandler<VariableInfoEventArgs> VariableInfoUpdated;

        public string _baseUri;

        private static readonly Lazy<HttpClient> client = new Lazy<HttpClient>(() => new HttpClient());

        public static HttpClient Client => client.Value;

        public RestClient()
        {
        }

        public RestClient(string baseUri)
        {
            _baseUri = baseUri;
        }

        private void OnHeartbeat(DateTime? dt)
        {
            var temp = Heartbeat;
            temp?.Invoke(this, new HeartbeatEventArgs() { HeartbeatTime = dt});
        }

        private void OnCpuInfoUpdated(CpuClientInfo[] cpuClientInfos)
        {
            var temp = CpuInfoUpdated;
            temp?.Invoke(this, new CpuInfoEventArgs() { CpuClientInfo = cpuClientInfos });
        }

        private void OnVariableInfoUpdated(List<VariableInfo> variableInfoList)
        {
            var temp = VariableInfoUpdated;
            temp?.Invoke(this, new VariableInfoEventArgs() { VariableInfoList = variableInfoList });
        }

        public async void Post<T>(T data)
        {

            var url = "http://localhost:9002/api/Diagnostic/GetHeartbeat";
            var json = JsonConvert.SerializeObject(data);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(url, postData);

            string result = response.Content.ReadAsStringAsync().Result;

            var jsonResult = JsonConvert.DeserializeObject<DateTime>(result);
        }

        public async Task<string> Get()
        {
            var url = "http://localhost:9002/api/Diagnostic/GetHeartbeat";
            string result = null;

            var response = await Client.GetAsync(url);

            result = response.Content.ReadAsStringAsync().Result;

            return result;
        }

        public async Task GetHeartbeat()
        {
            var url = "http://localhost:9002/api/Diagnostic/GetHeartbeat";
            DateTime? responseValue = null;

            try
            {
                var response = await Client.GetAsync(url).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var jsonResult = JsonConvert.DeserializeObject<DateTime>(result);
                    responseValue = jsonResult;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                OnHeartbeat(responseValue);
            }
        }

        public async Task GetCpuClientInfo()
        {
            var url = "http://localhost:9002/api/Cpu/GetDetails";

            try
            {
                var response = await Client.GetAsync(url).ConfigureAwait(false);

                var result = response.Content.ReadAsStringAsync().Result;
                var jsonResult = JsonConvert.DeserializeObject<CpuClientInfo[]>(result);

                OnCpuInfoUpdated(jsonResult);

            }
            catch (Exception ex)
            {
                OnCpuInfoUpdated(null);
            }
        }

        public async Task<bool> AddOrUpdateCpuClientInfo(CpuUpdateInfo cpuUpdateInfo)
        {
            var url = "http://localhost:9002/api/Cpu/Add";

            try
            {
                var json = JsonConvert.SerializeObject(cpuUpdateInfo);
                var postData = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await Client.PostAsync(url, postData);

                return response.IsSuccessStatusCode;

            }
            catch(Exception)
            {

            }

            return false;
        }

        public async Task<bool> DeleteCpu(string id)
        {
            var url = $"http://localhost:9002/api/Cpu/DeleteByName/{id}";

            try
            {

                var response = await Client.DeleteAsync(url);

                return response.IsSuccessStatusCode;

            }
            catch (Exception)
            {

            }

            return false;
        }

        public async Task GetVariableDetails(string id)
        {
            var url = $"http://localhost:9002/api/Variables/GetDetails/{id}";

            List<VariableInfo> jsonResult = null;

            try
            {
                var response = await Client.GetAsync(url);

                var result = response.Content.ReadAsStringAsync().Result;
                jsonResult = JsonConvert.DeserializeObject<List<VariableInfo>>(result);

            }
            catch (Exception)
            {

            }

            OnVariableInfoUpdated(jsonResult);
        }

        public async Task<bool> AddVariable(string cpu, string variableName)
        {
            var url = $"http://localhost:9002/api/Variables/Add/";

            try
            {
                var info = new VariableUpdateInfo() { CpuName = cpu, VariableName = variableName };

                var json = JsonConvert.SerializeObject(info);
                var postData = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await Client.PostAsync(url, postData);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

            }

            return false;

        }

        public async Task<bool> DeleteVariable(string cpu, string variableName)
        {
            var url = $"http://localhost:9002/api/Variables/Delete/";

            try
            {
                var info = new VariableUpdateInfo() { CpuName = cpu, VariableName = variableName };

                var json = JsonConvert.SerializeObject(info);
                var postData = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await Client.PostAsync(url, postData);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

            }

            return false;

        }
    }

    public class HeartbeatEventArgs : EventArgs
    {
        public DateTime? HeartbeatTime { get; set; }
    }

    public class CpuInfoEventArgs : EventArgs
    {
        public CpuClientInfo[] CpuClientInfo { get; set; }
    }

    public class VariableInfoEventArgs : EventArgs
    {
        public List<VariableInfo> VariableInfoList { get; set; }
    }
}
