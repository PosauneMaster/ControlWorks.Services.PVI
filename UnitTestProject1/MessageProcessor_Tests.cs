using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlWorks.Services.Messaging;
using ControlWorks.Services.PVI;
using ControlWorks.Services.PVI.Panel;
using ControlWorks.Services.PVI.Pvi;
using ControlWorks.Services.PVI.Variables;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit;

namespace UnitTestProject1
{
    public class MessageProcessor_Tests
    {
        private static Guid guid = new Guid("BE89FC99-CD7D-4CC6-AFB0-4975AC7B4E49");

        [Fact]
        public void Process_MessageActionStart_CallsConnect()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.Start,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.Connect()).Verifiable();
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response =  proc.Process(message);   
            
            pviApplicationMock.Verify(p => p.Connect(), Times.Once);

            Assert.Equal(guid, response.Id);
            Assert.Equal("Start", response.Message);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Errors);

        }

        [Fact]
        public void Process_MessageActionStart_ThrowsException()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.Start,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.Connect()).Throws();
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.Connect(), Times.Once);

            Assert.Equal(guid, response.Id);
            Assert.Equal("Start", response.Message);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Errors);

        }



        [Fact]
        public void Process_MessageActionStart_ThrowsNullReferenceException()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.Start,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.Connect()).Throws<NullReferenceException>();
            var proc = new MessageProcessor(pviApplicationMock.Object);


            var response = proc.Process(message);
            Assert.Equal(guid, response.Id);
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Errors);
            Assert.Equal("Object reference not set to an instance of an object.", response.Errors[0].Error);
        }

        [Fact]
        public void Process_MessageActionStop_CallsDisconnect()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.Stop,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.Disconnect()).Verifiable();
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.Disconnect(), Times.Once);

            Assert.Equal(guid, response.Id);
            Assert.Equal("Stop", response.Message);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Errors);

        }

        [Fact]
        public void Process_MessageActionStop_ThrowsNullReferenceException()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.Stop,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.Disconnect()).Throws<NullReferenceException>();
            var proc = new MessageProcessor(pviApplicationMock.Object);

            var response = proc.Process(message);
            Assert.Equal(guid, response.Id);
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Errors);
            Assert.Equal("Object reference not set to an instance of an object.", response.Errors[0].Error);
        }

        [Fact]
        public void Process_MessageActionIsConnected_CallsGetIsConnected()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.IsConnected,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.GetIsConnected()).Returns(true);
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.GetIsConnected(), Times.Once);

            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(response.Message, new ExpandoObjectConverter());
            var dict = (IDictionary<string, object>) obj;

            Assert.True(dict.Keys.Count == 1);
            Assert.True(dict.Keys.Contains("IsConnected"));
            Assert.Equal("True", dict["IsConnected"].ToString());
            Assert.Equal(guid, response.Id);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Errors);

        }

        [Fact]
        public void Process_MessageActionIsConnected_ThrowsNullReferenceException()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.IsConnected,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.GetIsConnected()).Throws<NullReferenceException>();
            var proc = new MessageProcessor(pviApplicationMock.Object);

            var response = proc.Process(message);
            Assert.Equal(guid, response.Id);
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Errors);
            Assert.Equal("Object reference not set to an instance of an object.", response.Errors[0].Error);
        }

        [Fact]
        public void Process_MessageActionIsError_CallsGetIsError()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.IsError,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.GetHasError()).Returns(true);
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.GetHasError(), Times.Once);

            dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(response.Message, new ExpandoObjectConverter());
            var dict = (IDictionary<string, object>)obj;

            Assert.True(dict.Keys.Count == 1);
            Assert.True(dict.Keys.Contains("IsError"));
            Assert.Equal("True", dict["IsError"].ToString());
            Assert.Equal(guid, response.Id);
            Assert.True(response.IsSuccess);
            Assert.Null(response.Errors);

        }

        [Fact]
        public void Process_MessageActionIsError_ThrowsNullReferenceException()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.IsError,
                Data = String.Empty
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.GetHasError()).Throws<NullReferenceException>();
            var proc = new MessageProcessor(pviApplicationMock.Object);

            var response = proc.Process(message);
            Assert.Equal(guid, response.Id);
            Assert.False(response.IsSuccess);
            Assert.NotNull(response.Errors);
            Assert.Equal("Object reference not set to an instance of an object.", response.Errors[0].Error);
        }

        [Fact]
        public void Process_MessageActionGetCpuByName_CallsGetsCpuName()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.GetCpuByName,
                Data = "cpuname1"
            };

            var cpuDetailResponse = new CpuDetailResponse
            {
                Description = "cpuname1",
                IsConnected = true,
                HasError = false,
                IpAddress = "120.0.0.1",
                Name = "cpuname1",
                Error = null
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.GetCpuByName(It.IsAny<string>())).Returns(cpuDetailResponse);
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.GetCpuByName(It.IsAny<string>()), Times.Once);

            var result = JsonConvert.DeserializeObject<CpuDetailResponse>(response.Message);

            Assert.NotNull(result);
            Assert.IsType<CpuDetailResponse>(result);
            Assert.Equal("cpuname1", result.Description);
            Assert.True(result.IsConnected);
            Assert.False(result.HasError);
            Assert.Equal("120.0.0.1", result.IpAddress);
            Assert.Equal("cpuname1", result.Name);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Process_MessageActionGetCpuByIp_CallsGetsCpuByIp()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.GetCpuByIp,
                Data = "cpuname1"
            };

            var cpuDetailResponse = new CpuDetailResponse
            {
                Description = "cpuname1",
                IsConnected = true,
                HasError = false,
                IpAddress = "120.0.0.1",
                Name = "cpuname1",
                Error = null
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.GetCpuByIp(It.IsAny<string>())).Returns(cpuDetailResponse);
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.GetCpuByIp(It.IsAny<string>()), Times.Once);

            var result = JsonConvert.DeserializeObject<CpuDetailResponse>(response.Message);

            Assert.NotNull(result);
            Assert.IsType<CpuDetailResponse>(result);
            Assert.Equal("cpuname1", result.Description);
            Assert.True(result.IsConnected);
            Assert.False(result.HasError);
            Assert.Equal("120.0.0.1", result.IpAddress);
            Assert.Equal("cpuname1", result.Name);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Process_MessageActionAddCpu_CallsAddCpu()
        {
            var cpuInfo = new CpuInfo
            {
                Name = "cpu1",
                Description = "cpu1",
                IpAddress = "100.2.3.120",
            };

            var m = new Message
            {
                Id = guid,
                Action = MessageAction.AddCpu,
                Data = JsonConvert.SerializeObject(cpuInfo)
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.AddCpu(It.IsAny<CpuInfo>()));
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.AddCpu(It.IsAny<CpuInfo>()), Times.Once);
        }

        [Fact]
        public void Process_MessageActionUpdateCpu_CallsUpdateCpu()
        {
            var cpuInfo = new CpuInfo
            {
                Name = "cpu1",
                Description = "cpu1",
                IpAddress = "100.2.3.120",
            };

            var m = new Message
            {
                Id = guid,
                Action = MessageAction.UpdateCpu,
                Data = JsonConvert.SerializeObject(cpuInfo)
            };

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.UpdateCpu(It.IsAny<CpuInfo>()));
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.UpdateCpu(It.IsAny<CpuInfo>()), Times.Once);
        }

        [Fact]
        public void Process_MessageActionDeleteCpuByName_CallsDeleteCpuByName()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.DeleteCpuByName,
                Data = "cpuname"
            };

            var message = JsonConvert.SerializeObject(m);

            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.DeleteCpuByName(It.IsAny<string>()));
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.DeleteCpuByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Process_MessageActionDeleteCpuByIp_CallsDeleteCpuByIP()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.DeleteCpuByIp,
                Data = "cpuname"
            };

            var message = JsonConvert.SerializeObject(m);

            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.DeleteCpuByIp(It.IsAny<string>()));
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.DeleteCpuByIp(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void Process_MessageActionGetAllCpu_CallsGetsAllCpu()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.GetAllCpuData,
                Data = "cpuname1"
            };

            var cpuDetailResponse1 = new CpuDetailResponse
            {
                Description = "cpuname1",
                IsConnected = true,
                HasError = false,
                IpAddress = "120.0.0.1",
                Name = "cpuname1",
                Error = null
            };
            var cpuDetailResponse2 = new CpuDetailResponse
            {
                Description = "cpuname2",
                IsConnected = true,
                HasError = false,
                IpAddress = "120.0.0.2",
                Name = "cpuname2",
                Error = null
            };
            var cpuDetailResponse3 = new CpuDetailResponse
            {
                Description = "cpuname3",
                IsConnected = true,
                HasError = false,
                IpAddress = "120.0.0.3",
                Name = "cpuname3",
                Error = null
            };

            var list = new List<CpuDetailResponse> {cpuDetailResponse1, cpuDetailResponse2, cpuDetailResponse3};

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.GetCpuData()).Returns(list.ToArray());
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.GetCpuData(), Times.Once);

            var result = JsonConvert.DeserializeObject<CpuDetailResponse[]>(response.Message);

            Assert.NotNull(result);
            Assert.IsType<CpuDetailResponse[]>(result);
            Assert.True(3 == result.Length);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(list[i].Description, result[i].Description);
                Assert.Equal(list[i].IsConnected, result[i].IsConnected);
                Assert.Equal(list[i].HasError, result[i].HasError);
                Assert.Equal(list[i].IpAddress, result[i].IpAddress);
                Assert.Equal(list[i].Name, result[i].Name);
                Assert.Null(list[i].Error);
            }

        }

        [Fact]
        public void Process_MessageActionReadAllVariables_CallsReadAllVariables()
        {
            var m = new Message
            {
                Id = guid,
                Action = MessageAction.ReadAllVariables,
                Data = "cpu1"
            };

            var variableResponse = new VariableResponse("cpu1");
            variableResponse.AddValue("variable1", "value1");
            variableResponse.AddValue("variable2", "value2");
            variableResponse.AddValue("variable3", "value3");

            var message = JsonConvert.SerializeObject(m);
            Mock<IPviAplication> pviApplicationMock = new Mock<IPviAplication>();
            pviApplicationMock.Setup(p => p.ReadAllVariables(It.IsAny<string>())).Returns(variableResponse);
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.ReadAllVariables(It.IsAny<string>()), Times.Once);

            var result = JsonConvert.DeserializeObject< VariableResponse>(response.Message);

            Assert.NotNull(result);
            Assert.IsType< VariableResponse>(result);
            Assert.True(3 == result.Values.Length);

            for (int i = 0; i < variableResponse.Values.Length; i++)
            {
                Assert.Equal(variableResponse.Values[i].Name, result.Values[i].Name);
                Assert.Equal(variableResponse.Values[i].Value, result.Values[i].Value);
            }

        }
    }
}
