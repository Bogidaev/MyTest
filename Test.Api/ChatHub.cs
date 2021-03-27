using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Api.Interfaces;
using Test.Api.ModelDto;

namespace Test.Api
{
    public class ChatHub : Hub
    {
        private readonly IClientService _service;

        public ChatHub(IClientService service)
        {
            this._service = service;
        }

        public async Task Send(string message)
        {
            var context = this.Context.GetHttpContext();
            string ip = context.Connection.RemoteIpAddress.ToString();

            await this._service.SendMessageAsync(new MessageDTO {Text = message, Ip = ip });
        }

        public async Task PrintMessages()
        {
            var printMessages = await this._service.PrintMessagesAsync();
            await this.Clients.Caller.SendAsync("printMessages", printMessages);
        }
    }
}
