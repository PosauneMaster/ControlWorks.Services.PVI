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
            pviApplicationMock.Setup(p => p.GetCpuByName(It.IsAny<string>())).Returns(Task.FromResult(cpuDetailResponse));
            var proc = new MessageProcessor(pviApplicationMock.Object);
            var response = proc.Process(message);

            pviApplicationMock.Verify(p => p.GetCpuByName(It.IsAny<string>()), Times.Once);

            //dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(response.Message, new ExpandoObjectConverter());
            //var dict = (IDictionary<string, object>)obj;

            //Assert.True(dict.Keys.Count == 1);
            //Assert.True(dict.Keys.Contains("IsError"));
            //Assert.Equal("True", dict["IsError"].ToString());
            //Assert.Equal(guid, response.Id);
            //Assert.True(response.IsSuccess);
            //Assert.Null(response.Errors);

        }






    }
}
