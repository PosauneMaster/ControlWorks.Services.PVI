using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlWorks.Application.Configuration
{
    public class RestClient
    {
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

        public async Task<DateTime?> GetHeartbeat()
        {
            var url = "http://localhost:9002/api/Diagnostic/GetHeartbeat";

            try
            {
                var response = await Client.GetAsync(url).ConfigureAwait(false);

                var result = response.Content.ReadAsStringAsync().Result;
                var jsonResult = JsonConvert.DeserializeObject<DateTime>(result);

                return jsonResult;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<CpuClientInfo[]> GetCpuClientInfo()
        {
            var url = "http://localhost:9002/api/Cpu/GetDetails";

            try
            {
                var response = await Client.GetAsync(url).ConfigureAwait(false);

                var result = response.Content.ReadAsStringAsync().Result;
                var jsonResult = JsonConvert.DeserializeObject<CpuClientInfo[]>(result);

                return jsonResult;
            }
            catch (Exception ex)
            {
                return null;
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

        public async Task<List<VariableInfo>> GetVariableDetails(string id)
        {
            var url = $"http://localhost:9002/api/Variables/GetDetails/{id}";

            try
            {
                var response = await Client.GetAsync(url);

                var result = response.Content.ReadAsStringAsync().Result;
                var jsonResult = JsonConvert.DeserializeObject<List<VariableInfo>>(result);

                return jsonResult;

            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
