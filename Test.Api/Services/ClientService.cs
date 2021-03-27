using AutoMapper;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Api.Data.Entities;
using Test.Api.Interfaces;
using Test.Api.ModelDto;
using Test.Core.Interfaces;
using Test.Model.Requests;
using Test.Model.Responses;

namespace Test.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork uow, IBus bus, IMapper mapper)
        {
            this._uow = uow;
            this._bus = bus;
            this._mapper = mapper;
        }

        public async Task<PrintMessageResponse> PrintMessagesAsync()
        {
            return await _bus.Rpc.RequestAsync<PrintMessageRequest, PrintMessageResponse>(new PrintMessageRequest());
        }

        public async Task SendMessageAsync(MessageDTO messageDTO)
        {   
            var massageRepository = _uow.GetRepository<Message>();
            var entity = this._mapper.Map<Message>(messageDTO);
            await massageRepository.InsertAsync(entity);
            await _uow.Commit();

            var message = this._mapper.Map<Model.Message>(messageDTO);
            await _bus.PubSub.PublishAsync(message);
            

        }
    }
}
