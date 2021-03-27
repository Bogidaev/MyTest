using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.Model.Requests;
using Test.Model.Responses;
using Test.Server.Data.Entities;
using Test.Server.Interfaces;
using System.Linq;

namespace Test.Server.Services
{
    public class ServerService : IServerService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public ServerService(IUnitOfWork uow, IBus bus, IMapper mapper)
        {
            this._uow = uow;
            this._bus = bus;
            this._mapper = mapper;
        }

        public async Task HandleTextMessage(Model.Message message)
        {
            var messageRepository = _uow.GetRepository<Message>();
            var entity = this._mapper.Map<Message>(message);
            await messageRepository.InsertAsync(entity);
            await _uow.Commit();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", entity.Text);
            Console.ResetColor();
        }

        public async Task<PrintMessageResponse> HandlePrintMessages(PrintMessageRequest Request)
        {
            try
            {
                var messageRepository = _uow.GetRepository<Message>();
                var messageList = await messageRepository.GetAll().OrderBy(x=>x.Date).ToListAsync();
                var masList = this._mapper.Map<List<Model.Message>>(messageList);

                return new PrintMessageResponse { Messages = masList };
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
