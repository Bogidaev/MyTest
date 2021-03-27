using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Model.Requests;
using Test.Model.Responses;
using Test.Server.Interfaces;

namespace Test.Server.Services
{
    public class Rabbit : IRabbit
    {
        private readonly IServerService _serverService;
        private readonly IBus _bus;

        public Rabbit(IServerService serverService, IBus bus)
        {
            this._serverService = serverService;
            this._bus = bus;
        }

        public async Task Registration()
        {
            await this._bus.PubSub.SubscribeAsync<Model.Message>(string.Empty, _serverService.HandleTextMessage);
            await this._bus.Rpc.RespondAsync<PrintMessageRequest, PrintMessageResponse>(_serverService.HandlePrintMessages);
        }
    }
}
