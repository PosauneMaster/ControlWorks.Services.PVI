﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Common;
using ControlWorks.Services.PVI;
using ControlWorks.Services.PVI.Pvi;
using ControlWorks.Services.PVI.Variables;
using log4net;

namespace ControlWorks.Services.Rest.Processors
{
    public interface IVariableProcessor
    {
        Task<List<VariableDetailRespose>> GetAll();
        Task<VariableResponse> FindByCpuName(string name);
        Task<VariableResponse> FindActiveByCpuName(string name);
        Task Add(string cpuName, string variableName);
        Task RemoveVariables(string cpuName, IEnumerable<string> variables);
        Task<VariableDetailRespose> Copy(string source, string destination);
        Task AddMaster(string[] variables);
        Task AddCpuRange(string[] cpus);
        Task RemoveCpuRange(string[] cpus);
        Task<List<VariableDetails>> GetVariableDetails(string cpuName);
    }


    public class VariableProcessor : IVariableProcessor
    {
        private readonly ILog _log = LogManager.GetLogger("ControlWorksLogger");
        private IPviApplication _application;

        public VariableProcessor() { }

        public VariableProcessor(IPviApplication application)
        {
            _application = application;
        }

        public async Task UpdateVariables(string cpuName, string[] variableNames)
        {
            var fw = new FileWrapper();
            var variableCollection = new VariableInfoCollection(fw);
            await Task.Run(() => 
            {
                variableCollection.UpdateCpuVariables(cpuName, variableNames);
            });

        }

        public async Task<List<VariableDetails>> GetVariableDetails(string cpuName)
        {
            var result = await Task.Run(() => _application.GetVariableDetails(cpuName));
            return result;
        }

        public async Task Add(string cpuName, string variableName)
        {
            var details = await GetVariableDetails(cpuName);
            var list = details.Select(v => v.Name).ToList();
            list.Add(variableName);
            _application.AddVariables(cpuName, list);
        }

        public Task AddCpuRange(string[] cpus)
        {
            throw new NotImplementedException();
        }

        public Task AddMaster(string[] variables)
        {
            throw new NotImplementedException();
        }

        public Task<VariableDetailRespose> Copy(string source, string destination)
        {
            throw new NotImplementedException();
        }

        public Task<VariableResponse> FindByCpuName(string name)
        {
            var result = Task.Run(() => _application.ReadAllVariables(name));
            return result;
        }

        public Task<VariableResponse> FindActiveByCpuName(string name)
        {
            return Task.Run(() => _application.ReadActiveVariables(name));
        }

        public Task<List<VariableDetailRespose>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task RemoveVariables(string cpuName, IEnumerable<string> variables)
        {
            return Task.Run(() => _application.RemoveVariables(cpuName, variables.ToList()));
        }

        public Task RemoveCpuRange(string[] cpus)
        {
            throw new NotImplementedException();
        }
    }
}
