using System.Collections.Generic;
using System.Threading.Tasks;
using ControlWorks.Services.ConfigurationProvider;

namespace ControlWorks.Services.PVI
{
    public interface IVariableApi
    {
        Task<List<VariableDetailRespose>> GetAll();
        Task<VariableDetailRespose> FindByCpuNameAsync(string name);
        VariableDetailRespose FindByCpuName(string name);
        Task AddRange(string cpuName, IEnumerable<string> variableNames);
        Task RemoveRange(string cpuName, IEnumerable<string> variableNames);
        Task<VariableDetailRespose> Copy(string source, string destination);
        Task AddCpuRange(string[] cpus);
        Task RemoveCpuRange(string[] cpus);

    }
    public class VariableApi : IVariableApi
    {
        public async Task<List<VariableDetailRespose>> GetAll()
        {
            var list = new List<VariableDetailRespose>();
            var response = await Task.Run(() =>
            {

                var collection = new VariableInfoCollection();
                collection.Open(AppSettings.VariableSettingsFile);
                return collection.GetAll();
            }).ConfigureAwait(false);

            foreach (var v in response)
            {
                list.Add(new VariableDetailRespose()
                {
                    CpuName = v.CpuName,
                    VariableNames = v.Variables
                });
            }

            return list;
        }

        public async Task<VariableDetailRespose> FindByCpuNameAsync(string name)
        {
            var response = await Task.Run(() =>
            {
                var collection = new VariableInfoCollection();
                collection.Open(AppSettings.VariableSettingsFile);

                return collection.FindByCpu(name);
            }).ConfigureAwait(false);

            if (response == null)
            {
                return new VariableDetailRespose
                {
                    CpuName = name,
                    VariableNames = null,
                    Errors = new ErrorResponse
                    {
                        Error = $"No entry exists with the Cpu name {name}"
                    }
                };
            }

            return new VariableDetailRespose
            {
                CpuName = response.CpuName,
                VariableNames = response.Variables
            };
        }

        public VariableDetailRespose FindByCpuName(string name)
        {
            var collection = new VariableInfoCollection();
            collection.Open(AppSettings.VariableSettingsFile);
            var response = collection.FindByCpu(name);

            if (response == null)
            {
                return new VariableDetailRespose
                {
                    CpuName = name,
                    VariableNames = null,
                    Errors = new ErrorResponse
                    {
                        Error = $"No entry exists with the Cpu name {name}"
                    }
                };
            }

            return new VariableDetailRespose
            {
                CpuName = response.CpuName,
                VariableNames = response.Variables
            };
        }

        public async Task AddCpuRange(string[] cpus)
        {
            await Task.Run(() =>
            {
                var collection = new VariableInfoCollection();
                collection.Open(AppSettings.VariableSettingsFile);
                collection.AddCpuRange(cpus);
                collection.Save(AppSettings.VariableSettingsFile);
            }).ConfigureAwait(false);

        }

        public async Task RemoveCpuRange(string[] cpus)
        {
            await Task.Run(() =>
            {
                var collection = new VariableInfoCollection();
                collection.Open(AppSettings.VariableSettingsFile);
                collection.RemoveCpuRange(cpus);
                collection.Save(AppSettings.VariableSettingsFile);
            }).ConfigureAwait(false);
        }



        public async Task AddRange(string cpuName, IEnumerable<string> variableNames)
        {
            await Task.Run(() =>
            {
                var collection = new VariableInfoCollection();
                collection.Open(AppSettings.VariableSettingsFile);
                collection.AddRange(cpuName, variableNames);
                collection.Save(AppSettings.VariableSettingsFile);
            }).ConfigureAwait(false);
        }

        public async Task RemoveRange(string cpuName, IEnumerable<string> variableNames)
        {
            await Task.Run(() =>
            {
                var collection = new VariableInfoCollection();
                collection.Open(AppSettings.VariableSettingsFile);
                collection.RemoveRange(cpuName, variableNames);
                collection.Save(AppSettings.VariableSettingsFile);
            }).ConfigureAwait(false);
        }

        public async Task<VariableDetailRespose> Copy(string source, string destination)
        {
            var srcCpu = await FindByCpuNameAsync(source);
            if (srcCpu != null && srcCpu.Errors == null)
            {
                await AddRange(destination, srcCpu.VariableNames).ConfigureAwait(false);
                return await FindByCpuNameAsync(destination).ConfigureAwait(false);
            }
            else
            {
                return new VariableDetailRespose()
                {
                    CpuName = destination,
                    Errors = new ErrorResponse()
                    {
                        Error = $"CpuName {source} not found"
                    }
                };
            }
        }
    }
}
